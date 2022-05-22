using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.ViewModels
{
    /// <summary>
    /// Guess try
    /// </summary>
    public class GuessTryItem : INotifyPropertyChanged
    {

        private string[] _guessChars = new string[5];
        private Color _background;

        public GuessTryItem(int lettersConst = 5)
        {
            TryLetters = new TryLetterItem[lettersConst];
            for (var i = 0; i < lettersConst; i++)
            {
                TryLetters[i] = new TryLetterItem();
            }
        }

        public TryLetterItem[] TryLetters { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
