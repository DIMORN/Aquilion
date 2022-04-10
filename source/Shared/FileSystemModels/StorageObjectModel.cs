using System.ComponentModel;
using Service.Storage;

namespace Shared
{
    public class StorageObjectModel : FileSystemModel
    {
        public StorageObjectModel(StorageObject storageObject, PropertyChangedEventHandler selected)
        {
            Name = storageObject.Name;
            FullName = storageObject.FullName;
            Extension = storageObject.StorageObjectType switch
            {
                StorageObjectType.Folder => "Folder",
                StorageObjectType.DocumentsFolder => "Documents Folder",
                StorageObjectType.MusicFolder => "Music Folder",
                StorageObjectType.PicturesVideoFolder => "Pictures & Video Folder",
                StorageObjectType.ContactsFolder => "Contacts Library",
            };
            PropertyChanged += selected;
        }
    }
}