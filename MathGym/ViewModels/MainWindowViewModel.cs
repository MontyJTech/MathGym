using Avalonia.Controls;
using System.Reactive;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using Avalonia.Input;
using System.Text.RegularExpressions;

namespace MathGym.ViewModels;

public partial class MainWindowViewModel : ReactiveObject
{
    private Window window;
    private Random randGenerator = new Random();

    private string augendValue = "";
    public string AugendValue
    {
        get => augendValue;
        set
        {
            this.RaiseAndSetIfChanged(ref augendValue, value);
        }
    }

    private string addendValue = "";

    public string AddendValue
    {
        get => addendValue;
        set
        {
            this.RaiseAndSetIfChanged(ref addendValue, value);
        }
    }

    private string testInput = "";
    public string TestInput
    {
        get => testInput;
        set
        {
            this.RaiseAndSetIfChanged(ref testInput, value);
        }
    }

    public ReactiveCommand<Unit, Unit> minimizeCommand { get; set;  }
    public ReactiveCommand<Unit, Unit> maximizeCommand { get; set; }
    public ReactiveCommand<Unit, Unit> closeCommand { get; set; }
    public ReactiveCommand<Unit, Unit> submitAnswerCommand { get; set; }

    public MainWindowViewModel(Window window, IObservable<KeyEventArgs> keyEvents)
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

        submitAnswerCommand = ReactiveCommand.Create(() =>
        {
            HandleAnswerSubmitted();
        });

        keyEvents.Select(e => e.Key).Where(key => key == Key.Enter).Subscribe(key =>
        {
            HandleAnswerSubmitted();
        });

        keyEvents.Select(e => e.Key).Where(key => key == Key.Back).Subscribe(key =>
        {
            if(TestInput.Length != 0)
            {
                string curInput = TestInput;
                TestInput = curInput.Remove(curInput.Length - 1);
            }
        });

        keyEvents.Select(e => e.Key).Where(key => (key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9)).Subscribe(key =>
        {
            string character = key.ToString();
            string input = character[character.Length - 1].ToString();
            string curInput = TestInput;
            curInput += input;
            TestInput = curInput;
        });

        StartGame();
    }

    private void StartGame()
    {
        GenerateNewProblem();
    }

    private void GenerateNewProblem()
    {
        AugendValue = randGenerator.Next(1, 10).ToString();
        AddendValue = randGenerator.Next(1, 10).ToString();
    }

    private void Text_Changed(object? sender, TextChangedEventArgs e)
    {
        Debug.WriteLine("Hello");
    }

    private void HandleAnswerSubmitted()
    {
        int.TryParse(TestInput, out int givenValue);

        if(givenValue == GetExpectedAnswer())
        {
            HandleSuccessUI();
        }
        else
        {
            HandleFailureUI();
        }
    }

    private int GetExpectedAnswer()
    {
        int.TryParse(AugendValue, out int aug);
        int.TryParse(AddendValue, out int add);

        return aug + add;
    }

    private void HandleSuccessUI()
    {
        Debug.WriteLine("Success!");
        GenerateNewProblem();
        TestInput = "";
    }

    private void HandleFailureUI()
    {
        Debug.WriteLine("Try Again!");
        TestInput = "";
    }
}
