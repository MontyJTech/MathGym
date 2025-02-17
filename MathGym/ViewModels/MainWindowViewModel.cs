using Avalonia.Controls;
using System.Reactive;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using Avalonia.Input;

namespace MathGym.ViewModels;

public partial class MainWindowViewModel : ReactiveObject
{
    private Window window;
    private BasicAddition basicAddition;

    public int AugendValue => basicAddition.augend;
    public int AddendValue => basicAddition.addend;
    public string userInput;

    public string UserInput
    {
        get => userInput;
        set {
            basicAddition.givenAnswer = value;
            this.RaiseAndSetIfChanged(ref userInput, value);
        }
    }

    public ReactiveCommand<Unit, Unit> minimizeCommand { get; set;  }
    public ReactiveCommand<Unit, Unit> maximizeCommand { get; set; }
    public ReactiveCommand<Unit, Unit> closeCommand { get; set; }
    public ReactiveCommand<Unit, Unit> submitAnswerCommand { get; set; }

    public MainWindowViewModel(Window window, IObservable<KeyEventArgs> keyEvents)
    {
        this.window = window;
        basicAddition = new BasicAddition();
        basicAddition.GenerateNewProblem();

        InitializeKeybindings(keyEvents);


        StartGame();
    }

    private void StartGame()
    {
        basicAddition.GenerateNewProblem();
        this.RaisePropertyChanged(nameof(AugendValue));
    }

    private void HandleAnswerSubmitted()
    {
        if(basicAddition.CheckGivenAnswer())
        {
            HandleSuccessUI();
        }
        else
        {
            HandleFailureUI();
        }
    }

    private void HandleSuccessUI()
    {
        Debug.WriteLine("Success!");
        basicAddition.GenerateNewProblem();
        UserInput = "";
        RefreshUI();
    }

    private void HandleFailureUI()
    {
        Debug.WriteLine("Try Again!");
        UserInput = "";
        RefreshUI();
    }

    private void RefreshUI()
    {
        this.RaisePropertyChanged(nameof(AugendValue));
        this.RaisePropertyChanged(nameof(AddendValue));
    }

    private void InitializeKeybindings(IObservable<KeyEventArgs> keyEvents)
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

        submitAnswerCommand = ReactiveCommand.Create(() =>
        {
            HandleAnswerSubmitted();
        });

        keyEvents.Where(e => e.Key == Key.Enter).Subscribe(key =>
        {
            HandleAnswerSubmitted();
        });

        keyEvents.Where(e => e.Key == Key.Back).Subscribe(key =>
        {
            if (UserInput.Length != 0)
            {
                string curInput = UserInput;
                UserInput = curInput.Remove(curInput.Length - 1);
            }
        });

        keyEvents.Select(e => e.Key).Where(key => (key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9)).Subscribe(key =>
        {
            UserInput += key.ToString()[^1].ToString();
        });
    }
}
