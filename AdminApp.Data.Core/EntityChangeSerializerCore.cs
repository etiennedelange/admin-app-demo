using AdminApp.Common.EntityFramework.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminApp.Data.Core
{
    /// <summary>
    /// EntityChangeSerializer for use with EFCore
    /// </summary>
    public class EntityChangeSerializerCore
    {
        private readonly DbContext _dbContext;

        private static readonly string[] IgnoredProperties = { "ConcurrencyStamp" };

        public EntityChangeSerializerCore(DbContext context) => _dbContext = context;

        /// <summary>
        /// Finds all change tracker entries of type <see cref="IEntityChange"/> and serializes it into a JSON string.
        /// </summary>
        /// <param name="converters">A collection of converters used while serializing.</param>
        /// <returns></returns>
        public string GetChangesAsJson(params JsonConverter[] converters)
        {
            var changeEntries = new List<ChangeEntry>();

            _dbContext.ChangeTracker.DetectChanges();

            var changedEntities = _dbContext.ChangeTracker
                .Entries<IEntityChange>()
                .Where(x => (x.State != EntityState.Unchanged && x.State != EntityState.Detached))
                .ToList();

            foreach (var entity in changedEntities)
            {
                ChangeEntry changeEntry = GetEntityChanges(entity);
                if (changeEntry.Changes.Any())
                {
                    changeEntries.Add(changeEntry);
                }
            }

            return changeEntries.Any() ?
                JsonConvert.SerializeObject(changeEntries, Formatting.Indented, converters) :
                string.Empty;
        }

        private static ChangeEntry GetEntityChanges(EntityEntry<IEntityChange> entity) =>
            entity.State switch
            {
                EntityState.Added => GetAddedEntity(entity),
                EntityState.Deleted => GetDeletedEntity(entity),
                EntityState.Modified => GetModifiedEntity(entity),
                _ => throw new NotSupportedException($"Change logging not supported for EntityState {entity.State}.")
            };

        private static ChangeEntry GetAddedEntity(EntityEntry<IEntityChange> entity)
        {
            List<Change> changes = new();

            foreach (var property in GetValidProperties(entity))
            {
                var current = property.CurrentValue;
                if (current != null)
                {
                    changes.Add(new Change(property.Metadata.Name, originalValue: null, currentValue: current?.ToString()));
                }
            }

            return new ChangeEntry(entity.Entity, ChangeType.Added, changes);
        }

        private static ChangeEntry GetModifiedEntity(EntityEntry<IEntityChange> entity)
        {
            List<Change> changes = new();

            foreach (var property in GetValidProperties(entity))
            {
                var original = property.OriginalValue;
                var current = property.CurrentValue;

                if ((original != null && !original.Equals(current)) || (original == null && current != null))
                {
                    changes.Add(new Change(property.Metadata.Name, original?.ToString(), current?.ToString()));
                }
            }

            return new ChangeEntry(entity.Entity, ChangeType.Modified, changes);
        }

        private static ChangeEntry GetDeletedEntity(EntityEntry<IEntityChange> entity)
        {
            List<Change> changes = new();

            foreach (var property in GetValidProperties(entity))
            {
                var original = property.OriginalValue;
                if (original != null)
                {
                    changes.Add(new Change(property.Metadata.Name, originalValue: original?.ToString(), currentValue: null));
                }
            }
            return new ChangeEntry(entity.Entity, ChangeType.Deleted, changes);
        }

        private static IEnumerable<PropertyEntry> GetValidProperties(EntityEntry<IEntityChange> entity) => entity.Properties.Where(x => !IgnoredProperties.Contains(x.Metadata.Name));
    }
}
