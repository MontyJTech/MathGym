using Avalonia.Controls;
using MathGym.ViewModels;

namespace MathGym.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(this);
        DragArea.PointerPressed += (s, e) => BeginMoveDrag(e);
    }
}