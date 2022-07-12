using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace promitel1
{	
    public class OwnCommands
    {
        public static readonly RoutedUICommand Test = new RoutedUICommand("Test", "Test", typeof(OwnCommands), new InputGestureCollection() { new KeyGesture(Key.NumPad0) } );
    }


}