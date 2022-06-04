using MauiApp4.Bll;
using MauiApp4.Bll.iface;
using MauiApp4.ViewModels.Delegates;
using MauiApp4.ViewModels.interfaces;
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
        // private const int TryCnt = 6;
        // private const int WordLength = 5;
        private GuessTryItem[] _guessTries;
        private static WordlyViewModel _instance;

        private string[] _currentWordBuffer;
        private int _currentWordCharIdx = 0;

        private IGameLogic _gameLogic;
        private readonly IKeyboardEventEmitter _keyboardViewModel;

        public WordlyViewModel(IGameLogic gameLogic, IKeyboardEventEmitter keyboardViewModel)
        {
            _gameLogic = gameLogic ?? throw new ArgumentNullException(nameof(gameLogic));
            _keyboardViewModel = keyboardViewModel ?? throw new ArgumentNullException(nameof(keyboardViewModel));

            _gameLogic.SetNewWord("норка");

            var tryies = new GuessTryItem[_gameLogic.TryCount];
            for (var i = 0; i < _gameLogic.TryCount; i++)
            {
                tryies[i] = new GuessTryItem(_gameLogic.WordLength)
                {
                };
            }

            GuessTries = tryies;

            // Подписаться на события клавиатуры
            _keyboardViewModel.KeyboardButtonStateChanging += OnKeyboardButtonStateChanging;
            _keyboardViewModel.ControlButtonClicked += OnControlButtonClicked;
            _currentWordBuffer = new string[_gameLogic.WordLength];
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
                return _guessTries[_gameLogic.CurrentTryIdx - 1];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool OnKeyboardButtonStateChanging(KeyboardButton sender)
        {
            if (_currentWordCharIdx < _gameLogic.WordLength)
            {
                GuessTries[_gameLogic.CurrentTryIdx].TryLetters[_currentWordCharIdx].Letter = sender.Letter;
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
            var resp = new GameStateChangedResponse();
            resp.WordLength = _gameLogic.WordLength;

            if (_currentWordCharIdx > 0 && isBackspace)
            {
                _currentWordCharIdx--;
                CurrentTryItem.TryLetters[_currentWordCharIdx].Letter = String.Empty;
                _currentWordBuffer[_currentWordCharIdx] = null;

                return resp;
            }
            else if (!isBackspace)
            {
                var validation = _gameLogic.ValidateWord(_currentWordBuffer.Aggregate((c1, c2) => c1 + c2));

                // Сбросить 
                resp.NextTry = !validation.GameFinished;
                resp.LetterInPlace = validation.CharInPlace;
                resp.WordHasLetter = validation.CharExists;

                if (!validation.GameFinished && validation.WordAccepted)
                {
                    _currentWordBuffer = new string[_gameLogic.WordLength];
                    _currentWordCharIdx = 0;
                }

                ColorizeChars(resp);
                return resp;
            } 
            else
            {
                return resp;
            }
        }

        private void ColorizeChars(GameStateChangedResponse resp)
        {
            for (int i = 0; i < resp.WordLength; i++)
            {
                if (resp.LetterInPlace[i])
                {
                    CurrentTryItem.TryLetters[i].InPlaceColor = Colors.Green;
                } 
                else if (resp.WordHasLetter[i])
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
