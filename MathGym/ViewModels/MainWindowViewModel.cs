using Avalonia.Controls;
using System.Reactive;
using ReactiveUI;
using System;

namespace MathGym.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private Window window;

    private int numberOne;
    private int numberTwo;

    private int answer;

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

    private void GenerateNewProblem()
    {
        numberOne = 0; numberTwo = 0;
        answer = 0;

        Random rand = new Random();
        numberOne = rand.Next(1, 10);
        numberOne = rand.Next(1, 10);

        answer = numberOne + numberTwo;
    }

    private void CheckAnswerGiven()
    {

    }

    private void HandleSuccessUI()
    {

    }

    private void HandleFailureUI()
    {

    }
}
