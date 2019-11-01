using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace mle_app.Views.CustomBehaviors
{
    public class ListBoxBehavior : Behavior<ListBox>
    {


        public bool ScrollIntoViewSelectedItem
        {
            get { return (bool)GetValue(ScrollIntoViewSelectedItemProperty); }
            set { SetValue(ScrollIntoViewSelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScrollIntoViewSelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollIntoViewSelectedItemProperty =
            DependencyProperty.Register("ScrollIntoViewSelectedItem", typeof(bool), typeof(ListBoxBehavior), new PropertyMetadata(default(bool)));


        protected override void OnAttached()
        {
            this.AssociatedObject.SelectionChanged += ListBox_SelectionChanged;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectionChanged -= ListBox_SelectionChanged;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ScrollIntoViewSelectedItem)
            {
                var lastItem = e.AddedItems.Count > 0 ? e.AddedItems[e.AddedItems.Count - 1] : null;

                if (lastItem != null)
                    this.AssociatedObject.ScrollIntoView(lastItem);
            }
        }


    }
}
