using System.ComponentModel.DataAnnotations.Schema;
using AdminApp.Common.EntityFramework.ChangeTracking;

namespace AdminApp.Data.Models
{
	public class AttorneySetting : IEntityChange
	{
		public long AttorneyId { get; set; }
		public long SettingId { get; set; }

		public virtual Attorney Attorney { get; set; }
		public virtual Setting Setting { get; set; }

		#region IEntityChange members

		[NotMapped]
		public string EntityDescription => $"SettingId - {SettingId}; AttorneyId - {AttorneyId}";

		[NotMapped]
		public long EntityId => default;

		[NotMapped]
		public string EntityName => GetType()?.Name;

		#endregion
	}
}