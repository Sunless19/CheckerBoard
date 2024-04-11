using Checkers.ViewModels;
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
                    }
                    sourceCell.IsSelected = false;
                }
            }
        }
    }
}