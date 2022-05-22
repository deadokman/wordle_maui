using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.ViewModels
{
    public sealed class WordlyViewModel : INotifyPropertyChanged
    {
        private const int TryCnt = 6;
        private GuessTryItem[] _guessTries;
        private static WordlyViewModel _instance;
        public static WordlyViewModel Instance
        {
            get
            {
                return _instance ?? (_instance = new WordlyViewModel());
            }
        }

        private WordlyViewModel()
        {
            var tries = new GuessTryItem[TryCnt];
            for (var i = 0; i < TryCnt; i++)
            {
                tries[i] = new GuessTryItem()
                {
                };
            }

            GuessTries = tries;

            // Подписаться на события клавиатуры
            KeyboardViewModel.Instance.KeyboardButtonStateChanging += OnKeyboardButtonStateChanging;
        }

        public GuessTryItem[] GuessTries 
        { 
            get => _guessTries;
            private set
            {
                _guessTries = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GuessTries)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool OnKeyboardButtonStateChanging(KeyboardButton sender)
        {
            GuessTries[0].TryLetters[0].Letter = sender.Letter;
            GuessTries[0].TryLetters[0].InPlaceColor = Colors.Green;
            return true;
        }
    }
}
