namespace Services.HelpSupport;

public static class Help_SupportService
{
    #region Public Static Properties
    public static ObservableCollection<Activity>? Activities { get; } =
        new();
    #endregion

    #region static Constructor
    static Help_SupportService()
    {
        Initialize();
    }
    #endregion

    #region Private Static Methods
    private static void Initialize()
    {
        InitializeActivities();
    }
    private static void InitializeActivities()
    {
        //Computer tasks
        {
            Activities.Add(new Activity("View system information", ActivityDescription.ComputerFolder));
            Activities.Add(new Activity("Add or remove programs", ActivityDescription.ComputerFolder));
            Activities.Add(new Activity("Change a setting", ActivityDescription.ComputerFolder));
        }

        //Selected folder tasks
        {
            Activities.Add(new Activity("Rename this folder", ActivityDescription.Folder));
            Activities.Add(new Activity("Move this folder", ActivityDescription.Folder));
            Activities.Add(new Activity("Copy this folder", ActivityDescription.Folder));
            Activities.Add(new Activity("Publish this folder to the Web", ActivityDescription.Folder));
            Activities.Add(new Activity("Email this folder's files", ActivityDescription.Folder));
            Activities.Add(new Activity("Delete this folder", ActivityDescription.Folder));
        }

        //Selected file tasks
        {
            Activities.Add(new Activity("Rename this file", ActivityDescription.File));
            Activities.Add(new Activity("Move this file", ActivityDescription.File));
            Activities.Add(new Activity("Copy this file", ActivityDescription.File));
            Activities.Add(new Activity("Publish this file to the Web", ActivityDescription.File));
            Activities.Add(new Activity("Email this file", ActivityDescription.File));
            Activities.Add(new Activity("Delete this file", ActivityDescription.File));
        }

        //Many selected file and folder tasks
        {
            Activities.Add(new Activity("Move this selected items", ActivityDescription.Folders));
            Activities.Add(new Activity("Copy this selected items", ActivityDescription.Folders));
            Activities.Add(new Activity("Publish this selected items to the Web", ActivityDescription.Folders));
            Activities.Add(new Activity("Email this selected items", ActivityDescription.Folders));
            Activities.Add(new Activity("Delete this selected items", ActivityDescription.Folders));


        }

        //Music library tasks
        {
            Activities.Add(new Activity("Play all", ActivityDescription.MusicLib));
            Activities.Add(new Activity("Shop for music online", ActivityDescription.MusicLib));
            Activities.Add(new Activity("Create playlist", ActivityDescription.MusicLib));
        }

        //Music folder tasks
        {
            Activities.Add(new Activity("Play all", ActivityDescription.Music));
            Activities.Add(new Activity("Shop for music online", ActivityDescription.Music));
        }

        //Many selected music tasks
        {
            Activities.Add(new Activity("Play selction", ActivityDescription.ManyMusic));
            Activities.Add(new Activity("Shop for music online", ActivityDescription.ManyMusic));
        }

        //Control pane tasks
        {
            Activities.Add(new Activity("Switch to Category View", ActivityDescription.Music));
        }

        //Picture tasks
        {
            Activities.Add(new Activity("View as a slide show", ActivityDescription.Pictures));
            Activities.Add(new Activity("Order prints online", ActivityDescription.Pictures));
            Activities.Add(new Activity("Print the selected pictures", ActivityDescription.Pictures));
            Activities.Add(new Activity("Shop for pictures online", ActivityDescription.Pictures));
        }

        //Generic other places tasks
        {
            Activities.Add(new Activity("My Network Places", ActivityDescription.Generic));
            Activities.Add(new Activity("My Documents", ActivityDescription.Generic));
            Activities.Add(new Activity("Shared Documents", ActivityDescription.Generic));
            Activities.Add(new Activity("Control Panel", ActivityDescription.Generic));
        }
    }
    #endregion
}
public enum ActivityDescription
{
    Music,
    ManyMusic,
    Generic,
    PicturesVideoLib,
    MusicLib,
    ComputerFolder,
    ControlPaneFolder,
    ContactsFolder,
    File,
    Folder,
    Files,
    Folders,
    Pictures,
    Videos,
    Contact,
    Device,
    Drive,
    Storage,
    Filter
}