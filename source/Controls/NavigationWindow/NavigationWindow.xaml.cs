using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Prism.Commands;

namespace Controls
{
    public partial class NavigationWindow : Window
    {
        public static readonly DependencyProperty ToolBarProperty = DependencyProperty.Register(
            "ToolBar", typeof(UIElement), typeof(NavigationWindow), new PropertyMetadata(default(UIElement)));

        public UIElement ToolBar
        {
            get { return (UIElement) GetValue(ToolBarProperty); }
            set { SetValue(ToolBarProperty, value); }
        }

        public static readonly DependencyProperty CloseWindowCommandProperty = DependencyProperty.Register(
            "CloseWindowCommand", typeof(DelegateCommand), typeof(NavigationWindow), new PropertyMetadata(default(DelegateCommand)));

        public DelegateCommand CloseWindowCommand
        {
            get { return (DelegateCommand) GetValue(CloseWindowCommandProperty); }
            set { SetValue(CloseWindowCommandProperty, value); }
        }

        public static readonly DependencyProperty MaximizeRestorewindowCommandProperty = DependencyProperty.Register(
            "MaximizeRestorewindowCommand", typeof(DelegateCommand), typeof(NavigationWindow), new PropertyMetadata(default(DelegateCommand)));

        public DelegateCommand MaximizeRestorewindowCommand
        {
            get { return (DelegateCommand) GetValue(MaximizeRestorewindowCommandProperty); }
            set { SetValue(MaximizeRestorewindowCommandProperty, value); }
        }

        public static readonly DependencyProperty MinimizeWindowCommandProperty = DependencyProperty.Register(
            "MinimizeWindowCommand", typeof(DelegateCommand), typeof(NavigationWindow), new PropertyMetadata(default(DelegateCommand)));

        public DelegateCommand MinimizeWindowCommand
        {
            get { return (DelegateCommand) GetValue(MinimizeWindowCommandProperty); }
            set { SetValue(MinimizeWindowCommandProperty, value); }
        }
        public NavigationWindow()
        {
            InitializeComponent();

            CloseWindowCommand = new DelegateCommand(OnCloseWindow);
            MaximizeRestorewindowCommand = new DelegateCommand(OnMaximizeRestoreWindow);
            MinimizeWindowCommand = new DelegateCommand(OnMinimizeWindow);
        }

        private void OnCloseWindow() => this.Close();
        private void OnMaximizeRestoreWindow() => this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        private void OnMinimizeWindow() => this.WindowState = WindowState.Minimized;
    }
}
