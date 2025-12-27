using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text.Json;
using Microsoft.Maui.Storage;

using MauiCalculator.Services;

namespace MauiCalculator.ViewModels;

public class CalculatorViewModel : BaseViewModel
{
    public ObservableCollection<string> History { get; } = new();
    private readonly CalculatorEngine _engine = new();
    private double _firstNumber;
    private string _operator = string.Empty;
    private bool _isNewEntry;
    private string _currentExpression = string.Empty;

    private string _display = "0";

    public ICommand ButtonCommand { get; }

    public CalculatorViewModel()
    {
        ButtonCommand = new Command<string>(OnButtonPressed);
        LoadHistory();
    }

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
    private void OnButtonPressed(string input)
    {
        if (input == "C")
        {
            Display = "0";
            History.Clear();
            Preferences.Remove("CalcHistory");
            _engine.Reset();
            _currentExpression = string.Empty;
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
            _currentExpression = $"{Display} {input}";
            _engine.Calculate(double.Parse(Display), input);
            _isNewEntry = true;
            return;
        }

        if (input == "=")
        {
            if (Display == "Error")
                return;

            var secondNumber = Display;
            var result = _engine.Calculate(double.Parse(Display), input);

            Display = double.IsNaN(result) ? "Error" : result.ToString();

            History.Insert(0, $"{_currentExpression} {secondNumber} = {Display}");
            SaveHistory();

            _isNewEntry = true;
            _currentExpression = string.Empty;
            return;
        }
    }

    private void SaveHistory()
    {
        var json = JsonSerializer.Serialize(History);
        Preferences.Set("CalcHistory", json);
    }

    private void LoadHistory()
    {
        var json = Preferences.Get("CalcHistory", "[]");
        var items = JsonSerializer.Deserialize<List<string>>(json);

        if (items == null) return;

        foreach (var item in items)
            History.Add(item);
    }


}
