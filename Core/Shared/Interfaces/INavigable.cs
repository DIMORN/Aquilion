namespace Shared;

public interface INavigable
{
    #region Commands
    public DelegateCommand? GoBackCommand { get; }
    public DelegateCommand? GoForwardCommand { get; }

    #endregion
}
