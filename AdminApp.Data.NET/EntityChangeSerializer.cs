using AdminApp.Common.EntityFramework.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AdminApp.Data.NET
{
    /// <summary>
    /// EntityChangeSerializer for use with EF6
    /// </summary>
    public class EntityChangeSerializer
    {
        private readonly DbContext _dbContext;

        private static readonly string[] IgnoredProperties = { };

        public EntityChangeSerializer(DbContext context) => _dbContext = context;

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
                .ToList(); ;

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

        private ChangeEntry GetEntityChanges(DbEntityEntry<IEntityChange> entity)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    return GetAddedEntity(entity);
                case EntityState.Deleted:
                    return GetDeletedEntity(entity);
                case EntityState.Modified:
                    return GetModifiedEntity(entity);
                default:
                    throw new NotSupportedException($"Change logging not supported for EntityState {entity.State}.");
            }
        }

        private ChangeEntry GetAddedEntity(DbEntityEntry<IEntityChange> entity)
        {
            List<Change> changes = new List<Change>();

            foreach (var property in GetValidProperties(entity.CurrentValues.PropertyNames))
            {
                var current = entity.CurrentValues.GetValue<object>(property);
                if (current != null)
                {
                    changes.Add(new Change(property, originalValue: null, currentValue: current?.ToString()));
                }
            }

            return new ChangeEntry(entity.Entity, ChangeType.Added, changes);
        }

        private ChangeEntry GetModifiedEntity(DbEntityEntry<IEntityChange> entity)
        {
            List<Change> changes = new List<Change>();

            foreach (var property in GetValidProperties(entity.OriginalValues.PropertyNames))
            {
                var original = entity.OriginalValues.GetValue<object>(property);
                var current = entity.CurrentValues.GetValue<object>(property);

                if ((original != null && !original.Equals(current)) || (original == null && current != null))
                {
                    changes.Add(new Change(property, original?.ToString(), current?.ToString()));
                }
            }

            return new ChangeEntry(entity.Entity, ChangeType.Modified, changes);
        }

        private ChangeEntry GetDeletedEntity(DbEntityEntry<IEntityChange> entity)
        {
            List<Change> changes = new List<Change>();

            foreach (var property in GetValidProperties(entity.OriginalValues.PropertyNames))
            {
                var original = entity.OriginalValues.GetValue<object>(property);
                if (original != null)
                {
                    changes.Add(new Change(property, originalValue: original?.ToString(), currentValue: null));
                }
            }
            return new ChangeEntry(entity.Entity, ChangeType.Deleted, changes);
        }

        private static IEnumerable<string> GetValidProperties(IEnumerable<string> propertyNames) => propertyNames.Where(x => !IgnoredProperties.Contains(x));
    }
}
