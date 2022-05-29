using MauiApp4.ViewModels.Delegates;
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
        private const int WordLength = 5;
        private GuessTryItem[] _guessTries;
        private static WordlyViewModel _instance;

        private string _currentWord;

        private string[] _currentWordBuffer = new string[WordLength];
        private int _currentWordCharIdx = 0;
        private int _currentTryCnt = 0;

        private object _lock;

        public static WordlyViewModel Instance
        {
            get
            {
                return _instance ?? (_instance = new WordlyViewModel());
            }
        }

        private WordlyViewModel()
        {
            var tryies = new GuessTryItem[TryCnt];
            for (var i = 0; i < TryCnt; i++)
            {
                tryies[i] = new GuessTryItem(WordLength)
                {
                };
            }

            GuessTries = tryies;
            _lock = new object();

            // Подписаться на события клавиатуры
            KeyboardViewModel.Instance.KeyboardButtonStateChanging += OnKeyboardButtonStateChanging;
            KeyboardViewModel.Instance.ControlButtonClicked += OnControlButtonClicked;

            _currentWord = "норка";
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

        public GuessTryItem CurrentTryItem
        {
            get
            {
                return _guessTries[_currentTryCnt];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool OnKeyboardButtonStateChanging(KeyboardButton sender)
        {
            if (_currentWordCharIdx < WordLength)
            {
                GuessTries[_currentTryCnt].TryLetters[_currentWordCharIdx].Letter = sender.Letter;
                _currentWordBuffer[_currentWordCharIdx] = sender.Letter;
                _currentWordCharIdx++;
                return true;
            }
            else
            {
                return false;
            }
        }

        private GameStateChangedResponse OnControlButtonClicked(bool isBackspace)
        {
            if (_currentWordCharIdx > 0 && isBackspace)
            {
                _currentWordCharIdx--;
                CurrentTryItem.TryLetters[_currentWordCharIdx].Letter = String.Empty;
                _currentWordBuffer[_currentWordCharIdx] = null;

                return new GameStateChangedResponse() { GameFinished = false };
            }
            else if (!isBackspace && _currentTryCnt < TryCnt && _currentWordCharIdx == WordLength)
            {
                var resp = new GameStateChangedResponse() 
                { 
                    GameFinished = false, 
                    NextTry = true, 
                    WordLength = WordLength,
                };

                InvokeWordValidate(resp);
                return resp;
            } 
            else
            {
                return new GameStateChangedResponse() { GameFinished = false };
            }
        }

        private void InvokeWordValidate(GameStateChangedResponse respToFill)
        {
            respToFill.LetterInPlace = new bool[WordLength];
            respToFill.WordHasLetter = new bool[WordLength];

            for (var i = 0; i < WordLength; i++)
            {
                var letter = _currentWordBuffer[i];
                var inPlace = _currentWord[i] == letter[0];
                var hasLetter = _currentWord.Contains(letter);
                respToFill.WordHasLetter[i] = hasLetter;
                respToFill.LetterInPlace[i] = inPlace;

                if (inPlace)
                {
                    CurrentTryItem.TryLetters[i].InPlaceColor = Colors.Green;
                } 
                else if (hasLetter)
                {
                    CurrentTryItem.TryLetters[i].InPlaceColor = Colors.DarkGoldenrod;
                } 
                else
                {
                    CurrentTryItem.TryLetters[i].InPlaceColor = Colors.Gray;
                }
            }
        }
    }
}
