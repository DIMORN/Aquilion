namespace Explorer.Core;

public class Explorer
{
    #region Singleton

    private static Explorer _instance;
    public static Explorer Instance => _instance ??= new Explorer();

    #endregion
}