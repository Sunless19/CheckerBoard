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

            // Verificăm dacă cell este null
            if (cell == null)
                return;

            var boardViewModel = DataContext as BoardViewModel;

            // Dacă celula este ocupată și este o celulă validă pentru mutare
            if (cell.IsOccupied && boardViewModel != null)
            {
                // Dacă celula selectată anterior este diferită de celula actuală,
                // atunci considerăm că utilizatorul a selectat o altă celulă.
                if (boardViewModel.SelectedCell != cell)
                {
                    // Deselectăm celula precedentă, dacă există
                    if (boardViewModel.SelectedCell != null)
                        boardViewModel.SelectedCell.IsSelected = false;

                    // Selectăm celula curentă
                    cell.IsSelected = true;
                    boardViewModel.SelectedCell = cell;
                }
                else
                {
                    // Dacă celula selectată anterior este aceeași cu celula actuală,
                    // atunci utilizatorul a făcut clic pe aceeași celulă și dorim să anulăm selecția.
                    cell.IsSelected = false;
                    boardViewModel.SelectedCell = null;
                }
            }
            else
            {
                // Dacă celula este goală și există o celulă selectată anterior,
                // atunci încercăm să facem mutarea.
                if (boardViewModel != null && boardViewModel.SelectedCell != null)
                {
                    var sourceCell = boardViewModel.SelectedCell;
                    var destinationCell = cell;

                    // Verificăm dacă mutarea este validă
                    if (boardViewModel.IsMoveValid(sourceCell, destinationCell))
                    {
                        // Efectuăm mutarea
                        boardViewModel.MakeMove(sourceCell, destinationCell);

                        // Deselectăm celula selectată
                        sourceCell.IsSelected = false;
                        boardViewModel.SelectedCell = null;
                    }
                }
            }
        }
    }
}