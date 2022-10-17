using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.ViewModels
{
    public class TryLetterItem : INotifyPropertyChanged
    {
        private Color _inPlaceColor;
        private string _letter;

        public TryLetterItem()
        {
            InPlaceColor = Colors.White;
        }

        /// <summary>
        /// Guess try letter string
        /// </summary>
        public string Letter 
        { 
            get => _letter; 
            set
            {
                _letter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Letter)));
            }
        }

        /// <summary>
        /// Цвет символа
        /// </summary>
        public Color InPlaceColor 
        { 
            get => _inPlaceColor; 
            set
            {
                _inPlaceColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InPlaceColor)));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
