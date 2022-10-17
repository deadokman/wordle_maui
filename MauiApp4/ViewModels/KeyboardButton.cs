using MauiApp4.ViewModels.Delegates;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiApp4.ViewModels
{
    public class KeyboardButton : INotifyPropertyChanged
    {
        private Color _color;
        private Color _initialColor;
        public ICommand ButtonClickCmd { get; private set; }

        public KeyboardButton(string letter)
        {
            Color = _initialColor = Colors.DarkBlue;
            ButtonClickCmd = new Command(() => { ButtonClickedEvent?.Invoke(this); });
            Letter = letter ?? throw new ArgumentNullException(nameof(letter));
        }

        public string Letter { get; private set; }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
            }
        }

        public void Reset() 
        {
            Color = _initialColor;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event KeyboardButtonClickedDelegate ButtonClickedEvent;
    }
}
