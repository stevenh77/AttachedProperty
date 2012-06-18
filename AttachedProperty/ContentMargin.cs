using System.Windows;
using System.Windows.Controls;

namespace AttachedProperty
{
    public static class ContentMargin
    {
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("MarginValue", 
                typeof (Thickness), 
                typeof (ContentMargin), 
                new PropertyMetadata(default(Thickness), OnMarginChanged));

        public static Thickness GetMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarginProperty);
        }

        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginProperty, value);
        }

        private static void OnMarginChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var element = obj as FrameworkElement;
            if (element == null) return;

            element.Loaded += element_Loaded;
        }

        static void element_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            foreach (var child in panel.Children)
            {
                var element = child as FrameworkElement;
                if (element == null) continue;
                Thickness parentMargin = GetMargin(panel);
                element.Margin = new Thickness(parentMargin.Left,
                                               parentMargin.Top,
                                               parentMargin.Right,
                                               parentMargin.Bottom);
            }
        }
    }
}