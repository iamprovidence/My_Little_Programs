using ActiveConsolePages.Pages;
using Client.Domain;

namespace Client.Pages
{
    internal class CalcPage : PageBase
    {
        private int? _a = null;
        private int? _b = null;
        private int? _result = null;

        private readonly Calculator _calculator;

        public override string Title => "Calculator";

        public CalcPage(Calculator calculator)
        {
            _calculator = calculator;
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Enter 2 numbers :");

            if (_a.HasValue)
            {
                Output.WriteLine($"a = {_a}");
            }
            if (_b.HasValue)
            {
                Output.WriteLine($"b = {_b}");
            }

            if (_a is null)
            {
                Output.Write("a = ");
            }
            else if (_b is null)
            {
                Output.Write("b = ");
            }
            else
            {
                Output.WriteLine($"{_a} + {_b} = {_result}");
            }
        }

        public override void HandleInput()
        {
            if (_a is null)
            {
                _a = Input.ReadInt();
            }
            else if (_b is null)
            {
                _b = Input.ReadInt();
            }

            if (_result is null)
            {
                if (_a.HasValue && _b.HasValue)
                {
                    _result = _calculator.Add(_a.Value, _b.Value);
                }
            }
            else
            {
                Input.ReadAny();
                Navigation.GoHome();
            }
        }
    }
}
