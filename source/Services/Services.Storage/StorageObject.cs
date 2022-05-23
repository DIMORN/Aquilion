namespace Services.Storage
{
    public class StorageObject
    {
        /// <summary>
        /// Name of object
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Fullname or fullpath to object
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Type of object
        /// </summary>
        public StorageObjectType? Type { get; set; }
        public IList<StorageObject>? Children { get; set; }
    }
}
