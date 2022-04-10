using System;
using LiteDB;

namespace Service.Storage
{
    public static class Storage
    {
        #region Public Sttatic Strings
        public static string AquilionAppLocalDirectory;

        public static string UserDirectory;

        public static string MyDocsDirectory;
        public static string MyMusicDirectory;
        public static string MyPicturesDirectory;
        public static string MyVideosDirectory;

        #endregion

        #region Public Static Properties

        public static LiteDatabase DataBase;

        public static LiteCollection<StorageObject> GenericObjectsCollection;
        #endregion

        #region Static Constructor

        static Storage()
        {
            InitializeStrings();
            InitializeDataBase();
        }
        #endregion

        #region Private Static Methods
        private static void InitializeStrings()
        {
            AquilionAppLocalDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Aquilion";
            UserDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            MyDocsDirectory = $"{UserDirectory}\\Documents";
            MyMusicDirectory = $"{UserDirectory}\\Music";
            MyPicturesDirectory = $"{UserDirectory}\\Pictures";
            MyVideosDirectory = $"{UserDirectory}\\Videos";
        }
        private static void InitializeDataBase()
        {
            DataBase = new LiteDatabase($"{AquilionAppLocalDirectory}\\Storage.ldb");
            GenericObjectsCollection = (LiteCollection<StorageObject>) DataBase.GetCollection<StorageObject>("GenericObjectsCollection");

            GenericObjectsCollection.Upsert("UserFolder",
                new StorageObject(
                    $"{Environment.UserName}'s Folder",
                    $"{Environment.UserName}:\\",
                    StorageObjectType.Folder));
            GenericObjectsCollection.Upsert("MyDocs",
                new StorageObject(
                    $"My Documents",
                    $"{Environment.UserName}:\\Documents",
                    StorageObjectType.DocumentsFolder));
            GenericObjectsCollection.Upsert("MyMusic",
                new StorageObject(
                    $"My Music",
                    $"{Environment.UserName}:\\Music",
                    StorageObjectType.MusicFolder));
            GenericObjectsCollection.Upsert("MyPicturesVideo",
                new StorageObject(
                    $"My Pictures & Video",
                    $"{Environment.UserName}:\\Pictures&Video",
                    StorageObjectType.PicturesVideoFolder));
            GenericObjectsCollection.Upsert("MyContacts",
                new StorageObject(
                    $"My Contacts",
                    $"{Environment.UserName}:\\Contacts",
                    StorageObjectType.ContactsFolder));
        }
        #endregion
    }
}
