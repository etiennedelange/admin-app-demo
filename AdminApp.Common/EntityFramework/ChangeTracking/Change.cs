namespace AdminApp.Common.EntityFramework.ChangeTracking
{
    public class Change
    {
        public Change(
            string property,
            string originalValue,
            string currentValue)
        {
            Property = property;
            OriginalValue = originalValue;
            CurrentValue = currentValue;
        }

        public string Property { get; private set; }

        public string OriginalValue { get; private set; }

        public string CurrentValue { get; private set; }
    }
}
