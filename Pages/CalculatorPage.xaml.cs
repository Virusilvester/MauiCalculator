using MauiCalculator.ViewModels;

namespace MauiCalculator.Pages;

public partial class CalculatorPage : ContentPage
{
    public CalculatorPage()
    {
        InitializeComponent();
        BindingContext = new CalculatorViewModel();
    }
}
