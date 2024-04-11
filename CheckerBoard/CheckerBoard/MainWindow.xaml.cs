using CheckerBoard.Models;
using Checkers.ViewModels;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using System.Windows.Controls;

namespace CheckerBoard
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cell = button?.DataContext as Cell;

            if (cell == null)
                return;

            var boardViewModel = DataContext as BoardViewModel;

            if (cell.IsOccupied && boardViewModel != null)
            {
                if (boardViewModel.SelectedCell != cell)
                {
                    if (boardViewModel.SelectedCell != null)
                        boardViewModel.SelectedCell.IsSelected = false;

                    cell.IsSelected = true;
                    boardViewModel.SelectedCell = cell;
                }
                else
                {
                    cell.IsSelected = false;
                }
            }
            else
            {
                if (boardViewModel != null && boardViewModel.SelectedCell != null)
                {
                    var sourceCell = boardViewModel.SelectedCell;
                    var destinationCell = cell;

                    if (boardViewModel.IsMoveValidPawn(sourceCell, destinationCell))
                    {
                        boardViewModel.MakeMove(sourceCell, destinationCell);
                        sourceCell.IsSelected = false;
                        sourceCell.IsOccupied = false;
                        destinationCell.IsOccupied = true;
                    }
                    else if (boardViewModel.isMoveValidKing(sourceCell, destinationCell))
                    {
                        boardViewModel.MakeMove(sourceCell, destinationCell);
                        sourceCell.IsSelected = false;
                        sourceCell.IsOccupied = false;
                        destinationCell.IsOccupied = true;
                    }
                    //Trebuie sa se vada daca randul este al jucatorului pentru ca daca e rand albului si negru vrea sa ia atunci o sa ia dar nu se muta .
                    else if (existsPieceBetween(sourceCell, destinationCell, boardViewModel))
                    {
                        boardViewModel.MakeMove(sourceCell, destinationCell);
                        sourceCell.IsSelected = false;
                        sourceCell.IsOccupied = false;
                        destinationCell.IsOccupied = true;
                    }
                    sourceCell.IsSelected = false;
                }
            }
        }

        private bool existsPieceBetween(Cell sourceCell, Cell destinationCell, BoardViewModel boardViewModel)
        {
            int betweenRowIndex = (sourceCell.RowIndex + destinationCell.RowIndex) / 2;
            int betweenColumnIndex = (sourceCell.ColumnIndex + destinationCell.ColumnIndex) / 2;

            if (!destinationCell.IsOccupied)
            {
                foreach (var cell in boardViewModel.Cells)
                {
                    if (cell.RowIndex == betweenRowIndex && cell.ColumnIndex == betweenColumnIndex)
                    {
                        if (cell.IsOccupied && ((cell.Content == CheckerTypes.BlackPawn && sourceCell.Content != cell.Content) || (cell.Content == CheckerTypes.WhitePawn && sourceCell.Content != cell.Content)))
                        {
                            if ((sourceCell.Content == CheckerTypes.WhitePawn && boardViewModel.CurrentPlayer == Player.White) ||
                           (sourceCell.Content == CheckerTypes.BlackPawn && boardViewModel.CurrentPlayer == Player.Black))
                            {
                                cell.IsOccupied = false;
                                cell.Content = CheckerTypes.None;
                                return true;
                            }
                            else if((sourceCell.Content == CheckerTypes.WhiteKing && boardViewModel.CurrentPlayer == Player.White) ||
                           (sourceCell.Content == CheckerTypes.BlackKing && boardViewModel.CurrentPlayer == Player.Black))
                            {
                                cell.IsOccupied = false;
                                cell.Content = CheckerTypes.None;
                                return true;
                            }
                        }
                        else if (cell.IsOccupied && ((cell.Content== CheckerTypes.BlackKing &&sourceCell.Content!=cell.Content) || (cell.Content==CheckerTypes.WhiteKing && sourceCell.Content!=cell.Content)))
                        {
                            if ((sourceCell.Content == CheckerTypes.WhiteKing && boardViewModel.CurrentPlayer == Player.White) ||
                           (sourceCell.Content == CheckerTypes.BlackKing && boardViewModel.CurrentPlayer == Player.Black))
                            {
                                cell.IsOccupied = false;
                                cell.Content = CheckerTypes.None;
                                return true;
                            }
                            if ((sourceCell.Content == CheckerTypes.WhiteKing && boardViewModel.CurrentPlayer == Player.White) ||
                           (sourceCell.Content == CheckerTypes.BlackKing && boardViewModel.CurrentPlayer == Player.Black))
                            {
                                cell.IsOccupied = false;
                                cell.Content = CheckerTypes.None;
                                return true;
                            }
                        }
                    }
                }

            }
            return false;
        }
    }
}