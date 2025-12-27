namespace MauiCalculator.Services;

public class CalculatorEngine
{
    private double _firstNumber;
    private string _operator = string.Empty;

    public double Calculate(double current, string input)
    {
        if (input == "/" && current == 0)
            return double.NaN;

        if (input is "+" or "-" or "*" or "/")
        {
            _firstNumber = current;
            _operator = input;
            return current;
        }

        if (input == "=")
        {
            return _operator switch
            {
                "+" => _firstNumber + current,
                "-" => _firstNumber - current,
                "*" => _firstNumber * current,
                "/" => current == 0 ? double.NaN : _firstNumber / current,
                _ => current
            };
        }

        return current;
    }

    public void Reset()
    {
        _firstNumber = 0;
        _operator = string.Empty;
    }
}
