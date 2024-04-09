using System.Collections.ObjectModel;
using System.Linq;

namespace Checkers.ViewModels
{
    public class BoardViewModel : BaseViewModel
    {
        private ObservableCollection<Cell> _cells;
        private Player _currentPlayer;
        private Cell _selectedCell;
        public Cell SelectedCell
        {
            get { return _selectedCell; }
            set
            {
                _selectedCell = value;
                OnPropertyChanged(nameof(SelectedCell));
            }
        }
        public ObservableCollection<Cell> Cells
        {
            get { return _cells; }
            set
            {
                _cells = value;
                OnPropertyChanged(nameof(Cells));
            }
        }

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        public int BlackPieceCount => Cells.Count(cell => cell.Content == CheckerTypes.BlackPawn || cell.Content == CheckerTypes.BlackKing);
        public int WhitePieceCount => Cells.Count(cell => cell.Content == CheckerTypes.WhitePawn || cell.Content == CheckerTypes.WhiteKing);

        public BoardViewModel()
        {
            Cells = new ObservableCollection<Cell>();
            CurrentPlayer = Player.Black; // Black este primul jucător

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool isBlack = (i + j) % 2 == 1;
                    if (i < 3 && isBlack)
                    {
                        Cells.Add(new Cell(isBlack, CheckerTypes.BlackPawn));
                    }
                    else if (i > 4 && isBlack)
                    {
                        Cells.Add(new Cell(isBlack, CheckerTypes.WhitePawn));
                    }
                    else
                    {
                        Cells.Add(new Cell(isBlack));
                    }
                }
            }
        }

        public void MakeMove(Cell source, Cell destination)
        {
            // Verificăm dacă sursa și destinația sunt valide
            if (source == null || destination == null)
                return;

            // Verificăm dacă sursa este ocupată de o piesă și dacă este piesa jucătorului curent
            //if (!source.IsOccupied || source.Content == CheckerTypes.None || source.Content == CheckerTypes.None ||
            //    (CurrentPlayer == Player.Black && (source.Content == CheckerTypes.WhitePawn || source.Content == CheckerTypes.WhiteKing)) ||
            //    (CurrentPlayer == Player.White && (source.Content == CheckerTypes.BlackPawn || source.Content == CheckerTypes.BlackKing)))
            //    return;

            // Verificăm dacă destinația este liberă și este o poziție valabilă pentru mutare
            if (destination.IsOccupied  || !IsMoveValid(source, destination))
                return;

            // Efectuăm mutarea
            destination.Content = source.Content;
            source.Content = CheckerTypes.None;

            // Verificăm dacă piesa a ajuns la capătul tablei și o transformăm în regină
            //if ((destination.Content == CheckerTypes.BlackPawn && destination.Row == 0) ||
            //    (destination.Content == CheckerTypes.WhitePawn && destination.Row == 7))
            //{
            //    destination.Content = destination.Content == CheckerTypes.BlackPawn ? CheckerTypes.BlackKing : CheckerTypes.WhiteKing;
            //}

            // Schimbăm jucătorul curent
            CurrentPlayer = CurrentPlayer == Player.Black ? Player.White : Player.Black;

            // Actualizăm numărul de piese pentru fiecare jucător
            OnPropertyChanged(nameof(BlackPieceCount));
            OnPropertyChanged(nameof(WhitePieceCount));
            bool isBlack = source.IsBlack;

        }

        public bool IsMoveValid(Cell source, Cell destination)
        {
            // Implementăm logica de validare a mutării aici
            // Aceasta poate include verificarea direcției și a distanței de mutare, dacă o piesă poate să sară peste alta, etc.
            // Pentru scopul demo, vom returna mereu true
            return true;
        }
    }

        public enum Player
    {
        Black,
        White
    }
}