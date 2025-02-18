using Avalonia.Controls;
using System.Reactive;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using Avalonia.Input;

namespace MathGym.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private Window window;
    private ViewModelBase currentView;
    public ViewModelBase CurrentView
    {
        get => currentView;
        set
        {
            this.RaiseAndSetIfChanged(ref currentView, value);
        }
    }
    
    private GameScreenViewModel game;

    public ReactiveCommand<Unit, Unit> minimizeCommand { get; set;  }
    public ReactiveCommand<Unit, Unit> maximizeCommand { get; set; }
    public ReactiveCommand<Unit, Unit> closeCommand { get; set; }
    

    public MainWindowViewModel(Window window, IObservable<KeyEventArgs> keyEvents)
    {
        this.window = window;

        CurrentView = new StartMenuViewModel(this);
        game = new GameScreenViewModel(keyEvents);
        
        InitializeKeybindings();
    }

    public void InitializeGameView()
    {
        CurrentView = game;
        game.StartGame();
    }

    private void InitializeKeybindings()
    {
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
