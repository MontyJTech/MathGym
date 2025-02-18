using ReactiveUI;
using System.Reactive;

namespace MathGym.ViewModels
{
    internal class StartMenuViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindowViewModel;
        public ReactiveCommand<Unit, Unit> InitGameCommand { get; }

        public StartMenuViewModel(MainWindowViewModel mainWindowViewModel) {
            this.mainWindowViewModel = mainWindowViewModel;
            InitGameCommand = ReactiveCommand.Create(this.mainWindowViewModel.InitializeGameView);
        }
    }
}
