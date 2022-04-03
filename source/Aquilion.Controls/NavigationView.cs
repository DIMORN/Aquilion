using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aquilion.Controls
{
    [ContentProperty(nameof(PageView))]
    public class NavigationView : UserControl
    {
        public static readonly DependencyProperty NavigationBarProperty = DependencyProperty.Register(
            "NavigationBar", typeof(UIElement), typeof(NavigationView), new PropertyMetadata(default(UIElement)));

        public UIElement NavigationBar
        {
            get { return (UIElement) GetValue(NavigationBarProperty); }
            set { SetValue(NavigationBarProperty, value); }
        }

        public static readonly DependencyProperty PageViewProperty = DependencyProperty.Register(
            "PageView", typeof(UIElement), typeof(NavigationView), new PropertyMetadata(default(UIElement)));

        public UIElement PageView
        {
            get { return (UIElement) GetValue(PageViewProperty); }
            set { SetValue(PageViewProperty, value); }
        }
        static NavigationView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationView), new FrameworkPropertyMetadata(typeof(NavigationView)));
        }
    }
}
