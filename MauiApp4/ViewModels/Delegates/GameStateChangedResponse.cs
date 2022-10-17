using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.ViewModels.Delegates
{
    public class GameStateChangedResponse
    {
        public int WordLength { get; internal set; }

        public bool NextTry { get; internal set; }

        public bool[] LetterInPlace { get; internal set; }

        public bool[] WordHasLetter { get; internal set; }
    }
}
