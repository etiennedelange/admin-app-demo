namespace AdminApp.Data.Models
{
    public class AttorneySetting
    {
        public Attorney Attorney { get; set; }
        public int? AttorneyID { get; set; }
        public Setting Setting { get; set; }
        public int? SettingID { get; set; }
    }
}
