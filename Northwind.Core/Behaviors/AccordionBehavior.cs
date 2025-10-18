using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Northwind.Behaviors
{
    public static class AccordionBehavior
    {
        public static void CollapseSiblings(object sender, RoutedEventArgs e)
        {
            if (sender is not Expander expander) return;
            var parent = VisualTreeHelper.GetParent(expander);
            while (parent != null && parent is not Panel) parent = VisualTreeHelper.GetParent(parent);
            if (parent is not Panel panel) return;

            foreach (var sib in panel.Children.OfType<Expander>())
                if (!ReferenceEquals(sib, expander)) sib.IsExpanded = false;

            e.Handled = true;
        }
    }
}
