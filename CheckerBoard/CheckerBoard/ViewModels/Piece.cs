namespace Checkers.ViewModels
{
    public class Cell : BaseViewModel
    {
        private bool _isBlack;
        private bool _isOccupied;
        private CheckerTypes _content;
        private bool _isSelected;

        public bool IsBlack
        {
            get { return _isBlack; }
            set
            {
                _isBlack = value;
                OnPropertyChanged(nameof(IsBlack));
            }
        }

        public bool IsOccupied
        {
            get { return _isOccupied; }
            set
            {
                _isOccupied = value;
                OnPropertyChanged(nameof(IsOccupied));
            }
        }

        public CheckerTypes Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public Cell(bool isBlack, CheckerTypes content = default)
        {
            IsBlack = isBlack;
            if (content != default)
            {
                IsOccupied = true;
            }
            else
            {
                IsOccupied = false;
            }
            Content = content;
        }
    }
}