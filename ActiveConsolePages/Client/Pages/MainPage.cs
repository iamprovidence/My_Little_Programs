using System;
using ActiveConsolePages.Pages;

namespace Client.Pages
{
    internal class MainPage : MenuPageBase
    {
        public override string Title => "Main";

        public override Menu Menu => new Menu
        {
            ["Calculator"] = () => Navigation.GoTo<CalcPage>(),
            ["Help"] = () => Navigation.GoTo<HelpPage>(),
            ["Exit"] = () => Environment.Exit(exitCode: 0),
        };
    }
}
