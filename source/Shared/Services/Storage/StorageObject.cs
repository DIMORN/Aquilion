namespace Shared
{
    public class StorageObject
    {
        public string Name;
        public string FullName;
        public StorageObjectType Type;

        public StorageObject(
            string name,
            string fullName,
            StorageObjectType type)
        {
            Name = name;
            FullName = fullName;
            Type = type;
        }

    }
}