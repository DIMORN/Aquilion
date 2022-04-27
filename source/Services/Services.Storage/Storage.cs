namespace Services.Storage;

public static class Storage
{
    #region Default Places Directories
    /// <summary>
    /// Physical directory of Aquilion data '%SystemDrive%/Users/{ActiveUser}/AppData/Local/Aquilion/'
    /// </summary>
    public static string ApplicationLocalDirectory;

    /// <summary>
    /// Physical directory of User '%SystemDrive%/Users/{ActiveUser}'
    /// </summary>
    public static string UserDirectory;

    /// <summary>
    /// Physical directory of Aquilion data '%SystemDrive%/Users/{ActiveUser}/AppData/Local/Aquilion/External'
    /// </summary>
    public static string ExternalServicesDirectory;
    #endregion

    #region User Libraries Physical Directories
    /// <summary>
    /// Physical directory of user documents '%SystemDrive%/Users/{ActiveUser}/Documents'
    /// </summary>
    public static string MyDocumentsDirectory;

    /// <summary>
    /// Physical directory of user music '%SystemDrive%/Users/{ActiveUser}/Music'
    /// </summary>
    public static string MyMusicDirectory;

    /// <summary>
    /// Physical directory of user pictures '%SystemDrive%/Users/{ActiveUser}/Pictures'
    /// </summary>
    public static string MyPicturesDirectory;

    /// <summary>
    /// Physical directory of user videos '%SystemDrive%/Users/{ActiveUser}/Videos'
    /// </summary>
    public static string MyVideoDirectory;
    #endregion

    #region User Physical Directories
    /// <summary>
    /// Physical directory of downloads '%SystemDrive%/Users/{ActiveUser}/Downloads'
    /// </summary>
    public static string DownloadsDirectory;
    #endregion

    #region DataBase Elements
    public static LiteDatabase DataBase;

    public static ILiteCollection<StorageObject> GenericCollection;
    public static ILiteCollection<StorageObject> DirectoriesCollection;
    public static ILiteCollection<StorageObject> FilesCollection;

    public static ILiteCollection<StorageObject> ExternalCollection;
    #endregion

    #region Static Constructor
    static Storage()
    {
        InitializeStrings();
        InitializeDataBase();
        InitializeCollections();
    }
    #endregion

    #region Init Methods
    private static void InitializeStrings()
    {
        ApplicationLocalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Aquilion";
        UserDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        ExternalServicesDirectory = ApplicationLocalDirectory + "\\External";

        MyDocumentsDirectory = $"{UserDirectory}\\Documents";
        MyMusicDirectory = $"{UserDirectory}\\Music";
        MyPicturesDirectory = $"{UserDirectory}\\Pictures";
        MyVideoDirectory = $"{UserDirectory}\\Videos";

        DownloadsDirectory = $"{UserDirectory}\\Downloads";
    }
    private static void InitializeDataBase()
    {
        DataBase = new LiteDatabase($"{ApplicationLocalDirectory}\\Storage.store");
    }
    private static async void InitializeCollections()
    {
        ExternalCollection = DataBase.GetCollection<StorageObject>("External");
        InitializeExternalCollection();

        GenericCollection = DataBase.GetCollection<StorageObject>("Generic");
        InitializeGenericCollection();

        DirectoriesCollection = DataBase.GetCollection<StorageObject>("Directories");
        await Task.Run(() =>
        {
            InitializeDirectoriesCollection();
        });
        
        FilesCollection = DataBase.GetCollection<StorageObject>("Files");
        await Task.Run(() =>
        {
            InitializeFilesCollection();
        });

        
    }
    private static void InitializeGenericCollection()
    {
        GenericCollection.Upsert(Locale.Storage_Object_Names_Computer, new StorageObject
        {
            Name = Locale.Storage_Object_Names_Computer,
            FullName = "computer:\\",
            Type = StorageObjectType.Computer
        });
        GenericCollection.Upsert(Locale.Storage_PhysicalDirectories_Names_MyDocuments, new StorageObject
        {
            Name = Locale.Storage_PhysicalDirectories_Names_MyDocuments,
            FullName = $"{Environment.UserName}:\\Documents",
            Type = StorageObjectType.DocumentsLibrary
        });
        GenericCollection.Upsert(Locale.Storage_PhysicalDirectories_Names_MyMusic, new StorageObject
        {
            Name = Locale.Storage_PhysicalDirectories_Names_MyMusic,
            FullName = $"{Environment.UserName}:\\Music",
            Type = StorageObjectType.MusicLibrary
        });
        GenericCollection.Upsert(Locale.Storage_Object_Names_PicturesVideoLibrary, new StorageObject
        {
            Name = Locale.Storage_Object_Names_PicturesVideoLibrary,
            FullName = $"{Environment.UserName}:\\Pictures|Video",
            Type = StorageObjectType.PicturesAndVideoLibrary
        });
        GenericCollection.Upsert(Locale.Storage_Object_Names_Downloads, new StorageObject
        {
            Name = Locale.Storage_Object_Names_Downloads,
            FullName = $"computer:\\Downloads",
            Type = StorageObjectType.DownloadsFolder
        });
    }
    private static void InitializeDirectoriesCollection()
    {
        foreach(var dir in new DirectoryInfo(UserDirectory).EnumerateDirectories())
        {
            if(!dir.Attributes.HasFlag(FileAttributes.System)
                || !dir.Attributes.HasFlag(FileAttributes.Hidden)
                || !dir.Attributes.HasFlag(FileAttributes.Encrypted))
            {
                DirectoriesCollection.Upsert(dir.FullName, new StorageObject
                {
                    Name = dir.Name,
                    FullName = dir.FullName,
                    Type = StorageObjectType.Folder
                });
                try
                {
                    if (dir.EnumerateDirectories().Count() != 0)
                    {
                        InitializeSecondDirectories(dir.FullName);
                    }
                    else
                    {
                        continue;
                    }
                }
                catch { continue; }
            }
        }
    }
    private static void InitializeSecondDirectories(string path)
    {
        foreach (var dir in new DirectoryInfo(path).EnumerateDirectories())
        {
            if (!dir.Attributes.HasFlag(FileAttributes.System)
                || !dir.Attributes.HasFlag(FileAttributes.Hidden)
                || !dir.Attributes.HasFlag(FileAttributes.Encrypted))
            {
                DirectoriesCollection.Upsert(dir.FullName, new StorageObject
                {
                    Name = dir.Name,
                    FullName = dir.FullName,
                    Type = StorageObjectType.Folder
                });
                try
                {
                    if (dir.EnumerateDirectories().Count() != 0)
                    {
                        InitializeSecondDirectories(dir.FullName);
                    }
                    else
                    {
                        continue;
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }
    private static void InitializeFilesCollection()
    {
        foreach (var file in new DirectoryInfo(UserDirectory).EnumerateFiles())
        {
            if (!file.Attributes.HasFlag(FileAttributes.System)
                || !file.Attributes.HasFlag(FileAttributes.Hidden)
                || !file.Attributes.HasFlag(FileAttributes.Encrypted))
            {
                DirectoriesCollection.Upsert(file.FullName, new StorageObject
                {
                    Name = file.Name,
                    FullName = file.FullName,
                    Type = StorageObjectType.Folder
                });
            }
        }
    }
    private static void InitializeSecondFiles(string path)
    {
        foreach (var file in new DirectoryInfo(path).EnumerateFiles())
        {
            if (!file.Attributes.HasFlag(FileAttributes.System)
                || !file.Attributes.HasFlag(FileAttributes.Hidden)
                || !file.Attributes.HasFlag(FileAttributes.Encrypted))
            {
                DirectoriesCollection.Upsert(file.FullName, new StorageObject
                {
                    Name = file.Name,
                    FullName = file.FullName,
                    Type = StorageObjectType.Folder
                });
            }
        }
    }
    private static void InitializeExternalCollection()
    {
        foreach(var dll in new DirectoryInfo(ExternalServicesDirectory).EnumerateFiles(".dll", SearchOption.AllDirectories))
        {
            if(dll.Directory.Name.ToLower() == "themes")
            {
                ExternalCollection.Upsert($"Theme.{dll.Name}", new StorageObject
                {
                    Name = dll.Name,
                    FullName = dll.FullName
                });
            }
        }
    }
    #endregion
}
