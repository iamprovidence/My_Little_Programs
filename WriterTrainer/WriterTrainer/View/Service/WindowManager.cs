using System;
using System.Collections.Generic;

using WriterTrainer.View.Window.Dialog;

namespace WriterTrainer.View.Service
{
    public class WindowManager : Core.Interfaces.IFactory<string, Type, System.Windows.Window>
    {
        // FIELDS
        static WindowManager instance; // singleton
        IDictionary<string, Type> factory; // a factory has string as a key and WindowType as a value

        // CONSTRUCTORS
        private WindowManager()
        {
            // initialize all fields
            factory = new Dictionary<string, Type>();
        }
        static WindowManager()
        {
            // initialize singleton value
            instance = new WindowManager();
        }

        // PROPERTIES
        public static WindowManager Instance => instance;

        // METHODS
        public System.Windows.Window MakeInstance(string key)
        {
            // checking argument
            // key
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (!factory.ContainsKey(key)) throw new InvalidOperationException($"Window by key {nameof(key)} was not registered.");

            // return window instance created by current type extracted by key
            return (System.Windows.Window)Activator.CreateInstance(factory[key]);
        }
        public void Registrate(string key, Type value)
        {
            // checking argument
            // key
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (factory.ContainsKey(key)) throw new InvalidOperationException(string.Format($"Window by key {nameof(key)} has already been registered"));
            // value
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.IsInterface || value.IsAbstract) throw new ArgumentException(nameof(value));

            // registrate type
            factory.Add(key, value);
        }
        public void UnRegistrate(string key)
        {
            // checking argument
            // key
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (!factory.ContainsKey(key)) throw new InvalidOperationException($"Window by key {nameof(key)} was not registered.");

            // unregistrate
            factory.Remove(key);
        }
        // WINDOW
        public bool? ShowWindowDialog(string key)
        {
            return ShowWindowDialog(key, null);
        }
        public bool? ShowWindowDialog(string key, object viewModel)
        {
            // create window
            System.Windows.Window window = MakeInstance(key);
            // set view model
            window.DataContext = viewModel;
            // show window
            return window.ShowDialog();
        }
        // MESSAGE BOX
        public bool? ShowMessageWindow(string text)
        {
            return ShowMessageWindow(text, String.Empty, MessageBoxButtons.Ok);
        }
        /// <summary>
        /// Open a message box window and returns only when a newly opened window is closed.
        /// </summary>
        /// <param name="text">
        /// Specify the text of the window.
        /// </param>
        /// <param name="header">
        /// Specify the header text of the window.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable"/> value of type <see cref="Boolean"/> that specifies whether the activity was accepted (true) or canceled (false).
        /// <para/>
        /// The return value is the value of the <see cref="System.Windows.Window.DialogResult"/> property before a window closes.
        /// </returns>    
        /// <exception cref="InvalidOperationException">
        /// Throws when message box is not registered.
        /// </exception>
        public bool? ShowMessageWindow(string text, string header)
        {
            return ShowMessageWindow(text, header, MessageBoxButtons.Ok);
        }
        /// <summary>
        public bool? ShowMessageWindow(string text, string header, MessageBoxButtons buttonType)
        {
            // prepare window value
            MessageBox messageBoxWindow;

            // initialize window with needed buttons
            switch (buttonType)
            {
                case MessageBoxButtons.Ok:
                    {
                        messageBoxWindow = (MessageBoxOk)MakeInstance(nameof(MessageBoxOk));
                    }
                    break;
                case MessageBoxButtons.YesNo:
                    {
                        messageBoxWindow = (MessageBoxYesNo)MakeInstance(nameof(MessageBoxYesNo));
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Current value is not supported");
            }

            // set up all values
            messageBoxWindow.Text = text;
            messageBoxWindow.Header = header;

            // show window and return result
            return ((System.Windows.Window)messageBoxWindow).ShowDialog();
        }
        public void SwitchMainWindow(string key)
        {
            SwitchMainWindow(key, null);
        }
        public void SwitchMainWindow(string key, object viewModel)
        {
            // get current main window
            System.Windows.Window oldWindow = App.Current.MainWindow;

            // get new window by key
            System.Windows.Window newWindow = MakeInstance(key);
            newWindow.DataContext = viewModel;

            // set it as a new main window
            App.Current.MainWindow = newWindow;

            // switch windows
            oldWindow.Close();
            newWindow.ShowDialog();
        }
    }
}
