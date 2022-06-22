using System;
using ActiveConsolePages.Host;
using ActiveConsolePages.Pages;

namespace ActiveConsolePages
{
    public interface IConsoleHost
    {
        void Run();
    }

    public class ConsoleHost : IConsoleHost
    {
        private readonly PageContext _context;

        internal ConsoleHost(PageContext pageContext)
        {
            _context = pageContext;
        }

        public static IConsoleHostConfigurationBuilder CreateDefaultBuilder()
        {
            return new ConsoleHostBuilder();
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    _context.CurrentPage.Display();
                    _context.CurrentPage.HandleInput();
                }
            }
            catch (Exception ex)
            {
                _context.CurrentPage.Output.DisplayError(ex.Message);
                _context.CurrentPage.Input.ReadAny();
            }
        }
    }
}
