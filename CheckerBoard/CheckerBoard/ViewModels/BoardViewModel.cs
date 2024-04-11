using System.Collections.ObjectModel;
using System.Linq;
using CheckerBoard.Models;

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
            CurrentPlayer = Player.White;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool isBlack = (i + j) % 2 == 1;
                    if (i < 3 && isBlack)
                    {
                        Cells.Add(new Cell(isBlack, CheckerTypes.BlackPawn, i, j));
                    }
                    else if (i > 4 && isBlack)
                    {
                        Cells.Add(new Cell(isBlack, CheckerTypes.WhitePawn, i, j));
                    }
                    else
                    {
                        Cells.Add(new Cell(isBlack, rowIndex: i, columnIndex: j));
                    }
                }
            }
        }

        public void MakeMove(Cell source, Cell destination)
        {
            // Verificăm dacă sursa și destinația sunt valide
            if (source == null || destination == null)
                return;

            //Verificăm dacă sursa este ocupată de o piesă și dacă este piesa jucătorului curent
            if (!source.IsOccupied || source.Content == CheckerTypes.None ||
                (CurrentPlayer == Player.Black && (source.Content == CheckerTypes.WhitePawn || source.Content == CheckerTypes.WhiteKing)) ||
                (CurrentPlayer == Player.White && (source.Content == CheckerTypes.BlackPawn || source.Content == CheckerTypes.BlackKing)))
                return;

            if (destination.IsOccupied)
                return;

            // Efectuăm mutarea
            destination.Content = source.Content;
            source.Content = CheckerTypes.None;

            // Verificăm dacă piesa a ajuns la capătul tablei și o transformăm în regină
            if ((destination.Content == CheckerTypes.WhitePawn && destination.RowIndex == 0) ||
                (destination.Content == CheckerTypes.BlackPawn && destination.RowIndex == 7))
            {
                destination.Content = destination.Content == CheckerTypes.BlackPawn ? CheckerTypes.BlackKing : CheckerTypes.WhiteKing;
            }

            // Schimbăm jucătorul curent
            CurrentPlayer = CurrentPlayer == Player.Black ? Player.White : Player.Black;

            // Actualizăm numărul de piese pentru fiecare jucător
            OnPropertyChanged(nameof(BlackPieceCount));
            OnPropertyChanged(nameof(WhitePieceCount));

        }

        public bool IsMoveValidPawn(Cell source, Cell destination)
        {
            if (source.Content == CheckerTypes.BlackKing || source.Content == CheckerTypes.WhiteKing)
                return false;
            // Verificăm dacă destinatia este o casuta goala
            if (destination.IsOccupied)
                return false;

            // Verificăm dacă mutarea este pe diagonală
            if (source.Content == CheckerTypes.WhitePawn)
            {
                if (source.ColumnIndex - 1 == destination.ColumnIndex && source.RowIndex - 1 == destination.RowIndex)
                    return true;
                else if (source.ColumnIndex + 1 == destination.ColumnIndex && source.RowIndex - 1 == destination.RowIndex)
                    return true;
                else return false;
            }

            if (source.Content == CheckerTypes.BlackPawn)
            {
                if (source.ColumnIndex - 1 == destination.ColumnIndex && source.RowIndex + 1 == destination.RowIndex)
                    return true;
                else if (source.ColumnIndex + 1 == destination.ColumnIndex && source.RowIndex + 1 == destination.RowIndex)
                    return true;
                else return false;
            }

            return true;
        }

        public bool isMoveValidKing(Cell source, Cell destination)
        {
            if (source.Content == CheckerTypes.BlackPawn || source.Content == CheckerTypes.WhitePawn)
                return false;

            if (destination.IsOccupied)
                return false;

            // mutare pe diagonala-> fata-spate .
            if (source.Content == CheckerTypes.WhiteKing)
            {
                if (source.ColumnIndex - 1 == destination.ColumnIndex && source.RowIndex - 1 == destination.RowIndex)
                    return true;
                else if (source.ColumnIndex + 1 == destination.ColumnIndex && source.RowIndex - 1 == destination.RowIndex)
                    return true;
                else if (source.ColumnIndex - 1 == destination.ColumnIndex && source.RowIndex + 1 == destination.RowIndex)
                    return true;
                else if (source.ColumnIndex + 1 == destination.ColumnIndex && source.RowIndex + 1 == destination.RowIndex)
                    return true;
                else return false;
            }

            if (source.Content == CheckerTypes.BlackKing)
            {
                if (source.ColumnIndex - 1 == destination.ColumnIndex && source.RowIndex - 1 == destination.RowIndex)
                    return true;
                else if (source.ColumnIndex + 1 == destination.ColumnIndex && source.RowIndex - 1 == destination.RowIndex)
                    return true;
                else if (source.ColumnIndex - 1 == destination.ColumnIndex && source.RowIndex + 1 == destination.RowIndex)
                    return true;
                else if (source.ColumnIndex + 1 == destination.ColumnIndex && source.RowIndex + 1 == destination.RowIndex)
                    return true;
                else return false;
            }

            return true;
        }
    }


}