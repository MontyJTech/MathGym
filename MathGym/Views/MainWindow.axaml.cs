using Avalonia.Controls;
using Avalonia.Input;
using MathGym.ViewModels;
using System;
using System.Reactive.Subjects;

namespace MathGym.Views;

public partial class MainWindow : Window
{
    private Subject<KeyEventArgs> keyEvents = new Subject<KeyEventArgs>();

    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel(this, keyEvents);
        DragArea.PointerPressed += (s, e) => BeginMoveDrag(e);
        this.KeyDown += (sender, e) =>
        {
            Console.WriteLine("Tesmp1 ");
            keyEvents.OnNext(e);
        };
    }
}