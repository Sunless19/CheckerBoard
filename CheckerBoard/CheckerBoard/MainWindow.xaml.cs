using CheckerBoard.Models;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using System.Windows.Controls;
using CheckerBoard.ViewModels;

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
            var gameModel = boardViewModel.GameModel;
                
            
            gameModel.IsGameNotInProgress = false;

            if (cell.IsOccupied && gameModel != null &&gameModel.IsMultipleCaptureInProgress==false)
            {
                if (gameModel.SelectedCell != cell)
                {
                    if (gameModel.SelectedCell != null)
                        gameModel.SelectedCell.IsSelected = false;

                    cell.IsSelected = true;
                    gameModel.SelectedCell = cell;
                }
                else
                {
                    cell.IsSelected = false;
                }
            }
            else
            {
                if (gameModel != null && gameModel.SelectedCell != null)
                {
                    var sourceCell = gameModel.SelectedCell;
                    var destinationCell = cell;

                    if (gameModel.IsMoveValidPawn(sourceCell, destinationCell) && gameModel.IsMultipleCaptureInProgress==false)
                    {
                        gameModel.MakeMove(sourceCell, destinationCell);
                        sourceCell.IsSelected = false;
                        sourceCell.IsOccupied = false;
                        gameModel.notMovable = false;
                        if (destinationCell.Content == CheckerTypes.None)
                        {
                            destinationCell.IsOccupied = false;
                        }
                        else destinationCell.IsOccupied = true;

                    }
                    else if (gameModel.isMoveValidKing(sourceCell, destinationCell) && gameModel.IsMultipleCaptureInProgress==false)
                    {
                        gameModel.MakeMove(sourceCell, destinationCell);
                        sourceCell.IsSelected = false;
                        sourceCell.IsOccupied = false;
                        gameModel.notMovable = false;
                        if (destinationCell.Content == CheckerTypes.None)
                        {
                            destinationCell.IsOccupied = false;
                        }
                        else destinationCell.IsOccupied = true;
                    }
                    else if (existsPieceBetween(sourceCell, destinationCell, gameModel))
                    {
                        gameModel.MakeMove(sourceCell, destinationCell);
                        if (gameModel.HasMultipleJumps == true)
                        {
                            gameModel.IsMultipleCaptureInProgress = true;
                            gameModel.CurrentPlayer = gameModel.CurrentPlayer == Player.Black ? Player.White : Player.Black;
                            destinationCell.IsSelected = true;
                            gameModel.SelectedCell = destinationCell;
                            gameModel.notMovable = true;

                        }
                        sourceCell.IsSelected = false;
                        sourceCell.IsOccupied = false;

                        if (destinationCell.Content == CheckerTypes.None)
                        {
                            destinationCell.IsOccupied = false;
                        }
                        else destinationCell.IsOccupied = true;
                    }
                    else if(gameModel.HasMultipleJumps==true && gameModel.IsMultipleCaptureInProgress==true)
                    {
                        gameModel.CurrentPlayer = gameModel.CurrentPlayer == Player.Black ? Player.White : Player.Black;
                        gameModel.IsMultipleCaptureInProgress = false;
                    }
                    sourceCell.IsSelected = false;
                }
            }
        }

        private bool existsPieceBetween(Cell sourceCell, Cell destinationCell, GameModel gameModel)
        {
            if (destinationCell.RowIndex % 2 == destinationCell.ColumnIndex % 2) { return false;}
            float betweenRowIndex = (sourceCell.RowIndex + destinationCell.RowIndex) / 2;
            float betweenColumnIndex = (sourceCell.ColumnIndex + destinationCell.ColumnIndex) / 2;

            if (sourceCell.Content == CheckerTypes.WhiteKing || sourceCell.Content == CheckerTypes.BlackKing || (sourceCell.Content == CheckerTypes.BlackPawn && destinationCell.RowIndex > sourceCell.RowIndex) || (sourceCell.Content == CheckerTypes.WhitePawn && destinationCell.RowIndex < sourceCell.RowIndex))
                if (!destinationCell.IsOccupied)
                {
                    foreach (var cell in gameModel.Cells)
                    {
                        if (cell.RowIndex == betweenRowIndex && cell.ColumnIndex == betweenColumnIndex)
                        {
                            if (cell.IsOccupied && ((cell.Content == CheckerTypes.BlackPawn && sourceCell.Content != cell.Content) || (cell.Content == CheckerTypes.WhitePawn && sourceCell.Content != cell.Content)))
                            {
                                if ((sourceCell.Content == CheckerTypes.WhitePawn && gameModel.CurrentPlayer == Player.White) ||
                               (sourceCell.Content == CheckerTypes.BlackPawn && gameModel.CurrentPlayer == Player.Black))
                                {
                                    cell.IsOccupied = false;
                                    cell.Content = CheckerTypes.None;
                                    return true;
                                }
                                else if ((sourceCell.Content == CheckerTypes.WhiteKing && gameModel.CurrentPlayer == Player.White) ||
                               (sourceCell.Content == CheckerTypes.BlackKing && gameModel.CurrentPlayer == Player.Black))
                                {
                                    cell.IsOccupied = false;
                                    cell.Content = CheckerTypes.None;
                                    return true;
                                }
                            }
                            else if (cell.IsOccupied && ((cell.Content == CheckerTypes.BlackKing && sourceCell.Content != cell.Content) || (cell.Content == CheckerTypes.WhiteKing && sourceCell.Content != cell.Content)))
                            {
                                if ((sourceCell.Content == CheckerTypes.WhiteKing && gameModel.CurrentPlayer == Player.White) ||
                               (sourceCell.Content == CheckerTypes.BlackKing && gameModel.CurrentPlayer == Player.Black))
                                {
                                    cell.IsOccupied = false;
                                    cell.Content = CheckerTypes.None;
                                    return true;
                                }
                                if ((sourceCell.Content == CheckerTypes.WhitePawn && gameModel.CurrentPlayer == Player.White) ||
                               (sourceCell.Content == CheckerTypes.BlackPawn && gameModel.CurrentPlayer == Player.Black))
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