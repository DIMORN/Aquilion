namespace Services.Storage
{
    public enum StorageObjectType
    {
        /// <summary>
        /// Folder object type
        /// </summary>
        Folder,

        /// <summary>
        /// File object type
        /// </summary>
        File,

        /// <summary>
        /// Special folder object type
        /// </summary>
        SpecialFolder,

        /// <summary>
        /// System folder object type
        /// </summary>
        SystemFolder,

        /// <summary>
        /// Music library or folder object type
        /// </summary>
        MusicLibrary,

        /// <summary>
        /// Pictures/Video library or folder object type
        /// </summary>
        PicturesAndVideoLibrary,

        /// <summary>
        /// Documents library object type
        /// </summary>
        DocumentsLibrary,

        /// <summary>
        /// Control pane folder object type
        /// </summary>
        ControlPaneFolder,

        /// <summary>
        /// Control pane element object type
        /// </summary>
        ControlPaneObject,

        /// <summary>
        /// Downloads folder object type
        /// </summary>
        DownloadsFolder,

        /// <summary>
        /// Computer object type
        /// </summary>
        Computer,

        /// <summary>
        /// External library object type
        /// </summary>
        ExternalLib,

        /// <summary>
        /// System object type
        /// </summary>
        SystemObject,

        /// <summary>
        /// Task object
        /// </summary>
        Task
    }
}