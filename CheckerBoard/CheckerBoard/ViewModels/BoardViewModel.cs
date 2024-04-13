using System.Windows;
using System.Windows.Input;
using CheckerBoard;
using CheckerBoard.Services;
using CheckerBoard.Models;

namespace CheckerBoard.ViewModels
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

        public ICommand MultipleJumpCommand { get; set; }

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
               GameModel = new GameModel(true);
               GameModel.HasMultipleJumps = true;
            }
            else if (result == MessageBoxResult.No)
            {
                GameModel  = new GameModel(true);
                GameModel.HasMultipleJumps = false;
            }
        }



        public void SaveGame(object parameter)
        {
            _jsonService.SaveObjectToFile(GameModel);
        }
        public void LoadGame(object parameter)
        {
            var loadedGameModel = _jsonService.LoadObjectFromFile<GameModel>();
            if (loadedGameModel != null)
            {
                GameModel.Cells.Clear(); // Elimină celulele existente pentru a evita dublarea acestora
                foreach (var cell in loadedGameModel.Cells)
                {
                    GameModel.Cells.Add(cell); // Adaugă celulele încărcate în modelul actual
                    
                }
                
                GameModel.CurrentPlayer = loadedGameModel.CurrentPlayer;
                GameModel.IsGameNotInProgress = loadedGameModel.IsGameNotInProgress;
                GameModel.SelectedCell = loadedGameModel.SelectedCell;
                GameModel.Winner = loadedGameModel.Winner;
                GameModel.HasMultipleJumps = loadedGameModel.HasMultipleJumps;
                
            }
        }

        public BoardViewModel()
        {
            GameModel = new GameModel(true);
            _jsonService = new FilesService();

            NewGameCommand = new RelayCommand(NewGame);
            SaveGameCommand = new RelayCommand(SaveGame);
            LoadGameCommand = new RelayCommand(LoadGame);
            MultipleJumpCommand = new RelayCommand(MultipleJump);
            DisplayInfoCommand = new RelayCommand(DisplayInfo);
            DisplayStatisticsCommand = new RelayCommand(DisplayStatistics);
        }

        private void DisplayStatistics(object obj)
        {
            obj = this.GameModel;

        }

        private void DisplayInfo(object obj)
        {
            About aboutWindow = new About();
            aboutWindow.Show();
        }

        private void MultipleJump(object obj)
        {
            if (GameModel.HasMultipleJumps)
            {
                GameModel.HasMultipleJumps = false;
                return;
            }
            
            if (!GameModel.HasMultipleJumps)
            {
                GameModel.HasMultipleJumps = true;
                return;
            }
        }
    }


}