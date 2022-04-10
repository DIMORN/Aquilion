using System;
using System.Collections;
using System.Collections.Generic;

namespace Service.Navigation
{
    public class NavigationHistory : INavigation
    {
        #region Private Fields

        private NavigationModelBase first;

        #endregion

        #region Public Properties

        public bool CanGoBack => Current.Previous != null;
        public bool CanGoForward => Current.Next != null;

        public NavigationModelBase Current { get; set; }

        #endregion

        #region Events
        public event EventHandler NavigationHistoryChanged;
        #endregion

        #region Constructor
        public NavigationHistory(NavigationModelBase navigationModel)
        {
            first = navigationModel;
            Current = first;
        }
        #endregion

        #region Public Methods
        public void GoBack()
        {
            var prev = Current.Previous;

            Current = prev;

            RaiseHistoryChanged();
        }
        public void GoForward()
        {
            var next = Current.Next;

            Current = next;

            RaiseHistoryChanged();
        }
        public void AddNavigationModelToHistory(NavigationModelBase navigationModelBase)
        {
            var node = navigationModelBase;

            Current.Next = node;
            node.Previous = Current;

            Current = node;

            RaiseHistoryChanged();
        }
        #endregion

        #region Private Methods

        private void RaiseHistoryChanged()
        {
            NavigationHistoryChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Enumerator
        public IEnumerator<NavigationModelBase> GetEnumerator()
        {
            yield return Current;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}