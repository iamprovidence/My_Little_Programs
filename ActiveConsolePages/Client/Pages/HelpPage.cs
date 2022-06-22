using ActiveConsolePages.Pages;

namespace Client.Pages
{
    internal class HelpPage : PageBase
    {
        public override string Title => "Help";

        public override void Display()
        {
            base.Display();

            Output.WriteLine("This is help page");
            Output.WriteLine();
            Output.WriteLine("Enter any value to exit");
        }

        public override void HandleInput()
        {
            Input.ReadAny();
            Navigation.GoBack();
        }
    }
}
