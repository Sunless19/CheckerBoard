using System.Collections.ObjectModel;
using System.Linq;

namespace Checkers.ViewModels
{
    public class BoardViewModel : BaseViewModel
    {
        private ObservableCollection<Cell> _cells;
        private Player _currentPlayer;

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
            CurrentPlayer = Player.White; // Black este primul jucător

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
            // Implementează logica mutării aici

            // După ce mutarea este efectuată cu succes, schimbă jucătorul curent
            CurrentPlayer = CurrentPlayer == Player.Black ? Player.White : Player.Black;

            // Actualizează numărul de piese pentru fiecare jucător
            OnPropertyChanged(nameof(BlackPieceCount));
            OnPropertyChanged(nameof(WhitePieceCount));
        }
    }

    public enum Player
    {
        Black,
        White
    }
}