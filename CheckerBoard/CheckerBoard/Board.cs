using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CheckerBoard
{
    public class Board
    {
        public Piece[,] Pieces { get; set; }

        public Board()
        {
            Pieces = new Piece[8, 8];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 != 0)
                    {
                        if (row < 3)
                            Pieces[row, col] = new Piece { Type = PieceType.Red };
                        else if (row > 4)
                            Pieces[row, col] = new Piece { Type = PieceType.White };
                        else
                            Pieces[row, col] = new Piece { Type = PieceType.Empty };
                    }
                    else
                    {
                        Pieces[row, col] = new Piece { Type = PieceType.Empty };
                    }
                }
            }
        }
    }
}

namespace CheckerBoard
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}