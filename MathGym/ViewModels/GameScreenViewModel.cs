using Avalonia.Input;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;

namespace MathGym.ViewModels
{
    internal class GameScreenViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> submitAnswerCommand { get; set; }
        private BasicAddition basicAddition;

        public int AugendValue => basicAddition.augend;
        public int AddendValue => basicAddition.addend;
        public string userInput;
        public string UserInput
        {
            get => userInput;
            set
            {
                basicAddition.givenAnswer = value;
                this.RaiseAndSetIfChanged(ref userInput, value);
            }
        }

        public GameScreenViewModel(IObservable<KeyEventArgs> keyEvents) 
        { 
            basicAddition = new BasicAddition();
            InitializeKeybindings(keyEvents);
        }

        public void StartGame()
        {
            basicAddition.GenerateNewProblem();
            RefreshUI();
        }

        private void ResetGame()
        {
            //Reset score to 0
        }

        private void HandleAnswerSubmitted()
        {
            if (basicAddition.CheckGivenAnswer())
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
}
