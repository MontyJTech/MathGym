using Avalonia.Controls;
using System.Reactive;
using ReactiveUI;

namespace MathGym.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private Window window;

    public ReactiveCommand<Unit, Unit> minimizeCommand { get; set;  }
    public ReactiveCommand<Unit, Unit> maximizeCommand { get; set; }
    public ReactiveCommand<Unit, Unit> closeCommand { get; set; }

    public MainWindowViewModel(Window window)
    {
        this.window = window;

        minimizeCommand = ReactiveCommand.Create(() =>
        {
            this.window.WindowState = WindowState.Minimized;
        });

        maximizeCommand = ReactiveCommand.Create(() =>
        {
            this.window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        });

        closeCommand = ReactiveCommand.Create(() =>
        {
            this.window.Close();
        });
    }
}
