using MauiApp4.Bll.iface;
using MauiApp4.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.Bll
{
    /// <summary>
    /// Game logic
    /// </summary>
    public class GameLogic : IGameLogic
    {
        private string _currentWord;

        private int _currentTryCnt = 0;

        public GameLogic()
        {
        }
        
        /// <summary>
        /// Tries count
        /// </summary>
        public int TryCount { get; private set; }

        /// <summary>
        /// Game word length
        /// </summary>
        public int WordLength { get; private set; }

        /// <summary>
        /// Current try count idx
        /// </summary>
        public int CurrentTryIdx { get => _currentTryCnt; }

        /// <summary>
        /// Validate try
        /// </summary>
        /// <param name="word">Word to validate</param>
        /// <returns>Validating word</returns>
        public ValidateWorkResponse ValidateWord(string word)
        {
            var result = new ValidateWorkResponse() { GameFinished = true };

            if (_currentTryCnt < TryCount && word.Length == WordLength)
            {
                var charInPlaceResult = new bool[WordLength];
                var charExitsResult = new bool[WordLength];

                for (var i = 0; i < WordLength; i++)
                {
                    var idx = _currentWord.IndexOf(word[i]);
                    charInPlaceResult[i] = idx == i;
                    charExitsResult[i] = idx > 0;
                }

                result.CharInPlace = charInPlaceResult;
                result.CharExists = charExitsResult;

                _currentTryCnt++;
                result.Validated = true;
                result.GameFinished = _currentTryCnt == TryCount;
                result.WordAccepted = true;
                return result;
            }

            return result;
        }

        public void SetNewWord(string word, int tryCnt = 6)
        {
            _currentWord = word;
            TryCount = tryCnt;
            _currentTryCnt = 0;
            WordLength = word.Length;
        }
    }
}
