using System.Reflection;

namespace Shared;

public static class Storage
{
    public static readonly string AppLocalDirectory;
    public static readonly string IconsDirectory;
    public static readonly string ImagesDirectory;

    static Storage()
    {
        AppLocalDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Aquilion";
        IconsDirectory = $"{AppLocalDirectory}\\Icons";
        ImagesDirectory = $"{AppLocalDirectory}\\ImageData";

    }
}

