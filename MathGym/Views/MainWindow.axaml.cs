using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;

namespace MathGym.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    //private void Button_OnClick(object? sender, RoutedEventArgs e)
    //{
    //    if(double.TryParse(Celcius.Text, out double C))
    //    {
    //        double F = C * (9d / 5d) + 32;
    //        Fahrenheit.Text = F.ToString("0.0");
    //    }
    //    else
    //    {
    //        Fahrenheit.Text = "0";
    //        Celcius.Text = "0";
    //    }
    //    Debug.WriteLine($"Clicked! Celcius: {Celcius.Text} - " + Celcius.Text);
    //}

    //private void Text_Changed(object? sender, TextChangedEventArgs e)
    //{
    //    if (double.TryParse(Celcius.Text, out double C))
    //    {
    //        double F = C * (9d / 5d) + 32;
    //        Fahrenheit.Text = F.ToString("0.0");
    //    }
    //    else if (string.IsNullOrEmpty(Celcius.Text))
    //    {
    //        Fahrenheit.Text = "";
    //    }
    //    else
    //    {
    //        Fahrenheit.Text = "0";
    //        Celcius.Text = "0";
    //    }
    //}
}