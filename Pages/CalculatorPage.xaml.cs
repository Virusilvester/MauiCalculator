using MauiCalculator.ViewModels;

namespace MauiCalculator.Pages;

public partial class CalculatorPage : ContentPage
{
    public CalculatorPage()
    {
        InitializeComponent();
        BindingContext = new CalculatorViewModel();
    }

    private void OnHistorySelected(object sender, SelectionChangedEventArgs e)
{
    var selected = e.CurrentSelection.FirstOrDefault() as string;

    if (selected == null)
        return;

    var parts = selected.Split('=');

    if (parts.Length == 2)
    {
        var result = parts[1].Trim();

        if (BindingContext is MauiCalculator.ViewModels.CalculatorViewModel vm)
        {
            vm.Display = result;
        }
    }

    ((CollectionView)sender).SelectedItem = null;
}

}
