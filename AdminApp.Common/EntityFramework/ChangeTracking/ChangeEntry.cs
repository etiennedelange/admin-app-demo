using System.Collections.Generic;

namespace AdminApp.Common.EntityFramework.ChangeTracking
{
    public class ChangeEntry
    {
        public ChangeEntry(
            IEntityChange entity,
            ChangeType changeType,
            IEnumerable<Change> changes)
        {
            EntityId = entity.EntityId;
            EntityName = entity.EntityName;
            EntityDescription = entity.EntityDescription;
            ChangeType = changeType;
            Changes = changes;
        }

        public long EntityId { get; private set; }

        public string EntityName { get; private set; }

        public string EntityDescription { get; private set; }

        public ChangeType ChangeType { get; private set; }

        public IEnumerable<Change> Changes { get; private set; }
    }
}
