using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CheckerBoard
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private Board _board;
        public ObservableCollection<Piece> Pieces { get; set; }

        public GameViewModel()
        {
            _board = new Board();
            Pieces = new ObservableCollection<Piece>();
            InitializePieces();
        }

        private void InitializePieces()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Pieces.Add(_board.Pieces[row, col]);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}