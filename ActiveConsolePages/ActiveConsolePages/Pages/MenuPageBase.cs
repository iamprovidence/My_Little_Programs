namespace ActiveConsolePages.Pages
{
    public abstract class MenuPageBase : PageBase
    {
        public abstract Menu Menu { get; }

        public override void Display()
        {
            base.Display();
            Menu.Display(Output);

            Output.WriteLine();
            Output.WriteLine("Choose an option :");
        }

        public override void HandleInput()
        {
            var choice = Input.ReadInt(min: 1, max: Menu.Count);

            Menu[choice - 1].Callback();
        }
    }
}
