namespace Services.Storage
{
    public static class Storage
    {
        #region Private Static Fields

        private static object Dll;

        #endregion

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
        public static ILiteCollection<StorageObject> SystemCollection;
        public static ILiteCollection<StorageObject> ActivitiesCollection;
        #endregion

        #region Static Constructor
        static Storage()
        {
            InitializeStrings();
            InitializeDataBase();
            InitializeActivitiesCollection();
            InitializeCollections();
            InitializeSystemCollection();
            InitializeExternalCollection();
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
            SystemCollection = DataBase.GetCollection<StorageObject>("System");
            InitializeSystemCollection();

            ExternalCollection = DataBase.GetCollection<StorageObject>("External");
            InitializeExternalCollection();

            InitializeActivitiesCollection();

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
            foreach (var dir in new DirectoryInfo(UserDirectory).EnumerateDirectories())
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
            ExternalCollection.DeleteAll();
            foreach (var directory in new DirectoryInfo(ExternalServicesDirectory + "\\Themes\\").EnumerateDirectories())
            {
                var dll = GetDllFromDirectory(directory, directory.Name);
                ExternalCollection.Upsert($"Theme.{directory.Name}", new StorageObject
                {
                    Name = dll.Name,
                    FullName = dll.FullName,
                    Type = StorageObjectType.ExternalLib
                });
            }
            foreach (var directory in new DirectoryInfo(ExternalServicesDirectory + "\\Extensions\\").EnumerateDirectories())
            {
                var dll = GetDllFromDirectory(directory, directory.Name);
                ExternalCollection.Upsert($"Extension.{directory.Name}", new StorageObject
                {
                    Name = dll.Name,
                    FullName = dll.FullName,
                    Type = StorageObjectType.ExternalLib
                });
            }
        }
        private static void InitializeSystemCollection()
        {
            //SystemCollection.Upsert("ExplorerMenu.Items.TopLevel.File", new StorageObject
            //{
            //    Name = "File",
            //    FullName = "ExplorerMenu.Items.TopLevel.File",
            //    Type = StorageObjectType.SystemObject,
            //    Children = new List<StorageObject>
            //    {
            //        new StorageObject
            //        {
            //            Name = "Open",
            //            FullName = "ExplorerMenu.Items.TopLevel.File.Children.Items",
            //            Type = StorageObjectType.SystemObject,
            //        },
            //        new StorageObject
            //        {
            //            Name = "Rename",
            //            FullName = "ExplorerMenu.Items.TopLevel.File.Children.Items",
            //            Type = StorageObjectType.SystemObject,
            //        },
            //        new StorageObject
            //        {
            //            Name = "Exit",
            //            FullName = "ExplorerMenu.Items.TopLevel.File.Children.Items",
            //            Type = StorageObjectType.SystemObject,
            //        }
            //    }
            //});
            //SystemCollection.Upsert("ExplorerMenu.Items.TopLevel.Edit", new StorageObject
            //{
            //    Name = "Edit",
            //    FullName = "ExplorerMenu.Items.TopLevel.Edit",
            //    Type = StorageObjectType.SystemObject,
            //    Children = new List<StorageObject>
            //    {
            //        new StorageObject
            //        {
            //            Name = "Copy",
            //            FullName = "ExplorerMenu.Items.TopLevel.Edit.Children.Items",
            //            Type = StorageObjectType.SystemObject,
            //        },
            //        new StorageObject
            //        {
            //            Name = "Cut",
            //            FullName = "ExplorerMenu.Items.TopLevel.Edit.Children.Items",
            //            Type = StorageObjectType.SystemObject,
            //        },
            //        new StorageObject
            //        {
            //            Name = "Paste",
            //            FullName = "ExplorerMenu.Items.TopLevel.Edit.Children.Items",
            //            Type = StorageObjectType.SystemObject,
            //        }
            //    }
            //});
            //SystemCollection.Upsert("ExplorerMenu.Items.TopLevel.View", new StorageObject
            //{
            //    Name = "View",
            //    FullName = "ExplorerMenu.Items.TopLevel.View",
            //    Type = StorageObjectType.SystemObject,
            //    Children = new List<StorageObject>
            //    {
            //        new StorageObject
            //        {
            //            Name = "Preview",
            //            FullName = "ExplorerMenu.Items.TopLevel.View.Children.Items.Checkable",
            //            Type = StorageObjectType.SystemObject,
            //        },
            //        new StorageObject
            //        {
            //            Name = "Icons",
            //            FullName = "ExplorerMenu.Items.TopLevel.View.Children.Items.Checkable.SingleCheck.Group.View",
            //            Type = StorageObjectType.SystemObject,
            //        },
            //        new StorageObject
            //        {
            //            Name = "Details",
            //            FullName = "ExplorerMenu.Items.TopLevel.View.Children.Items.Checkable.SingleCheck.Group.View",
            //            Type = StorageObjectType.SystemObject,
            //        }
            //    }
            //});
            //SystemCollection.Upsert("ExplorerMenu.Items.TopLevel.Favorites", new StorageObject
            //{
            //    Name = "Favorites",
            //    FullName = "ExplorerMenu.Items.TopLevel.Favorites",
            //    Type = StorageObjectType.SystemObject
            //});
            //SystemCollection.Upsert("ExplorerMenu.Items.TopLevel.Tools", new StorageObject
            //{
            //    Name = "Tools",
            //    FullName = "ExplorerMenu.Items.TopLevel.Tools",
            //    Type = StorageObjectType.SystemObject,
            //    Children = new List<StorageObject>
            //    {
            //        new StorageObject
            //        {
            //            Name = "Folder Options",
            //            FullName = "ExplorerMenu.Items.TopLevel.Tools.Children.Items",
            //            Type = StorageObjectType.SystemObject,
            //        }
            //    }
            //});
            //SystemCollection.Upsert("ExplorerMenu.Items.TopLevel.Help", new StorageObject
            //{
            //    Name = "Help",
            //    FullName = "ExplorerMenu.Items.TopLevel.Help",
            //    Type = StorageObjectType.SystemObject,
            //    Children = new List<StorageObject>
            //    {
            //        new StorageObject
            //        {
            //            Name = "About Explorer",
            //            FullName = "ExplorerMenu.Items.TopLevel.Help.Children.Items",
            //            Type = StorageObjectType.SystemObject,
            //        }
            //    }
            //});
        }
        private static void InitializeActivitiesCollection()
        {
            ActivitiesCollection = DataBase.GetCollection<StorageObject>("Activities");

            foreach (var activity in HelpSupport.Help_SupportService.Activities)
            {
                ActivitiesCollection.Upsert($"[{activity.Type}][{activity.Header}]", new StorageObject
                {
                    Name = activity.Header,
                    Type = StorageObjectType.Task,
                    FullName = activity.Type.ToString()
                });
            }
        }

        private static FileInfo GetDllFromDirectory(DirectoryInfo di, string comparableName)
        {
            FileInfo dll = null;
            if (di.Name.ToLower() == "obj") return dll;
            foreach (var directoryInfo in di.EnumerateDirectories())
            {
                if (directoryInfo.EnumerateDirectories().Count() == 0)
                {
                    foreach (var f in directoryInfo.EnumerateFiles())
                    {
                        if (f.Name.ToLower().Contains(comparableName.ToLower()) &&
                            f.Extension.ToLower().EndsWith(".dll"))
                        {
                            dll = f;
                            Dll = dll;
                        }
                    }
                }
                if (dll != null) break;
                else
                {
                    GetDllFromDirectory(directoryInfo, comparableName);
                }
            }
            return Dll as FileInfo;
        }
        #endregion
    }
}
