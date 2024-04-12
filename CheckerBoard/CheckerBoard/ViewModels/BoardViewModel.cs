using System.Windows;
using System.Windows.Input;
using Checkers.Services;
using Checkers.Models;

namespace Checkers.ViewModels
{
    public class BoardViewModel : BaseViewModel
    {
        private readonly FilesService _jsonService;

        private GameModel _gameModel;


        public ICommand ClickCellCommand { get; set; }

        public ICommand MovePieceCommand { get; set; }

        public ICommand SaveGameCommand { get; set; }

        public ICommand LoadGameCommand { get; set; }

        public ICommand NewGameCommand { get; set; }

        public ICommand DisplayInfoCommand { get; set; }

        public ICommand DisplayStatisticsCommand { get; set; }
        public GameModel GameModel
        {
            get => _gameModel;
            set
            {
                if (_gameModel != value)
                {
                    _gameModel = value;
                    OnPropertyChanged(nameof(GameModel)); 
                    OnPropertyChanged(nameof(GameModel.Cells)); 
                }
            }
        }

        public void NewGame(object parameter)
        {
            MessageBoxResult result = MessageBox.Show(
            "Will the new game support multiple jumps?",
            "New Game Settings",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
               GameModel = new GameModel();
               GameModel.HasMultipleJumps = true;
            }
            else if (result == MessageBoxResult.No)
            {
                GameModel  = new GameModel();
                GameModel.HasMultipleJumps = false;
            }
        }



        //public void SaveGame(object parameter)
        //{
        //    _jsonService.SaveObjectToFile(BoardViewModel);
        //}

        public BoardViewModel()
        {
            GameModel = new GameModel();

            NewGameCommand = new RelayCommand(NewGame);
            //SaveGameCommand=new RelayCommand(SaveGame);\

            
        }

        
    }


}