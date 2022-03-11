using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CLBuilder.Extentions
{
    /// <summary>
    /// Class SelectAllBehavior.
    /// </summary>
    public class SelectAllBehavior
    {
        /// <summary>
        /// Gets the enable property.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetEnable(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(EnableProperty);
        }

        /// <summary>
        /// Sets the enable property.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetEnable(FrameworkElement frameworkElement, bool value)
        {
            frameworkElement.SetValue(EnableProperty, value);
        }

        /// <summary>
        /// The enable property
        /// </summary>
        public static readonly DependencyProperty EnableProperty =
             DependencyProperty.RegisterAttached("Enable",
                typeof(bool), typeof(SelectAllBehavior),
                new FrameworkPropertyMetadata(false, OnEnableChanged));

        /// <summary>
        /// Handles the <see cref="E:EnableChanged" /> event.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnEnableChanged
                   (DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = d as FrameworkElement;
            if (frameworkElement == null) return;

            if (e.NewValue is bool == false) return;

            if ((bool)e.NewValue)
            {
                frameworkElement.GotFocus += SelectAll;
                frameworkElement.PreviewMouseDown += IgnoreMouseButton;
            }
            else
            {
                frameworkElement.GotFocus -= SelectAll;
                frameworkElement.PreviewMouseDown -= IgnoreMouseButton;
            }
        }

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private static void SelectAll(object sender, RoutedEventArgs e)
        {
            var frameworkElement = e.OriginalSource as FrameworkElement;
            if (frameworkElement is TextBox)
                ((TextBoxBase)frameworkElement).SelectAll();
            else if (frameworkElement is PasswordBox)
                ((PasswordBox)frameworkElement).SelectAll();
        }

        /// <summary>
        /// Ignores the mouse button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private static void IgnoreMouseButton
                (object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement == null || frameworkElement.IsKeyboardFocusWithin) return;
            e.Handled = true;
            frameworkElement.Focus();
        }
    }
}
