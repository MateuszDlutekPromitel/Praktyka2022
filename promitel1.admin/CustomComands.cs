using System.Windows.Input;

namespace promitel1.admin
{
    public class CustomComands
    {
        public static readonly RoutedUICommand Open = new RoutedUICommand
            (
                "Open",
                "Open",
                typeof(CustomComands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.O, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Safe = new RoutedUICommand
            (
                "Safe",
                "Safe",
                typeof(CustomComands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.S, ModifierKeys.Control)
                }
            );

    }


}