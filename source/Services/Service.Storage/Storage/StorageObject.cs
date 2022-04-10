namespace Service.Storage
{
    public class StorageObject
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public StorageObjectType StorageObjectType { get; set; }

        public StorageObject(
            string name,
            string fullName,
            StorageObjectType storageObjectType)
        {
            Name = name;
            FullName = fullName;
            StorageObjectType = storageObjectType;
        }

    }
}