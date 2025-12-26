using System.Windows.Input;

namespace MauiCalculator.ViewModels;

public class CalculatorViewModel : BaseViewModel
{
    private double _firstNumber;
    private string _operator = string.Empty;
    private bool _isNewEntry;

    private string _display = "0";
    public string Display
    {
        get => _display;
        set
        {
            if (_display != value)
            {
                _display = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand ButtonCommand { get; }

    public CalculatorViewModel()
    {
        ButtonCommand = new Command<string>(OnButtonPressed);
    }

    private void OnButtonPressed(string input)
    {
        if (input == "C")
        {
            Display = "0";
            _firstNumber = 0;
            _operator = string.Empty;
            _isNewEntry = false;
            return;
        }

        if (double.TryParse(input, out _))
        {
            if (Display == "0" || _isNewEntry)
            {
                Display = input;
                _isNewEntry = false;
            }
            else
            {
                Display += input;
            }
            return;
        }

        if (input is "+" or "-" or "*" or "/")
        {
            _firstNumber = double.Parse(Display);
            _operator = input;
            _isNewEntry = true;
            return;
        }

        if (input == "=")
        {
            var secondNumber = double.Parse(Display);

            double result = _operator switch
            {
                "+" => _firstNumber + secondNumber,
                "-" => _firstNumber - secondNumber,
                "*" => _firstNumber * secondNumber,
                "/" => secondNumber == 0 ? 0 : _firstNumber / secondNumber,
                _ => secondNumber
            };

            Display = result.ToString();
            _isNewEntry = true;
        }
    }

}
