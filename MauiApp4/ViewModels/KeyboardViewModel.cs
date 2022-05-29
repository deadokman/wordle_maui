using MauiApp4.ViewModels.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.ViewModels
{
    public class KeyboardViewModel
    {
        public KeyboardButton[][] Buttons { get; set; }

        public KeyboardButton[] ButtonsFirstRow { get => Buttons[0]; }
        public KeyboardButton[] ButtonsSecondRow { get => Buttons[1]; }
        public KeyboardButton[] ButtonsThirdRow { get => Buttons[2]; }

        public event KeyboardButtonStateChangingDelegate KeyboardButtonStateChanging;
        public event ControlButtonClickedDelegate ControlButtonClicked;

        private const string _alphabetFirstRow = "йцукенгшщзхъ";
        private const string _alphabetSecondRow = "фывапролджэ";
        private const string _alphabetThirdRow = "ячсмитьбю";

        private Dictionary<string, KeyboardButton> _textButtonsData;
        private Stack<KeyboardButton> _currentButtonStack = new Stack<KeyboardButton>();

        private static KeyboardViewModel _instance;

        public static KeyboardViewModel Instance
        {
            get => _instance ?? (_instance = new KeyboardViewModel());
        }

        private KeyboardViewModel()
        {
            _textButtonsData = new Dictionary<string, KeyboardButton>();

            Buttons = new KeyboardButton[3][];
            Buttons[0] = GetButtonsRow(_alphabetFirstRow);
            Buttons[1] = GetButtonsRow(_alphabetSecondRow);
            var thirdRow = GetButtonsRow(_alphabetThirdRow);
            var specialCharsArray = new KeyboardButton[thirdRow.Length + 2];

            KeyboardButton backspace;
            KeyboardButton enter;

            specialCharsArray[0] = backspace = new KeyboardButton("<-");
            specialCharsArray[specialCharsArray.Length - 1] = enter = new KeyboardButton("ввод");
            Array.Copy(thirdRow, 0, specialCharsArray, 1, thirdRow.Length);

            Buttons[2] = specialCharsArray;
            backspace.ButtonClickedEvent += Backspace_ButtonClickedEvent;
            enter.ButtonClickedEvent += Enter_ButtonClickedEvent;
        }

        private void Enter_ButtonClickedEvent(KeyboardButton sender)
        {
            var resp = ControlButtonClicked?.Invoke(false);
            if (resp.NextTry)
            {
                var idxCntr = resp.WordLength - 1;
                while (_currentButtonStack.TryPop(out var btn))
                {
                    if (resp.LetterInPlace[idxCntr])
                    {
                        btn.Color = Colors.Green;
                    } 
                    else if (resp.WordHasLetter[idxCntr])
                    {
                        btn.Color = Colors.PaleGoldenrod;
                    }
                    else
                    {
                        btn.Color = Colors.Gray;
                    }

                    idxCntr--;
                }
            }
        }

        private void Backspace_ButtonClickedEvent(KeyboardButton sender)
        {
            if (_currentButtonStack.TryPop(out var btn))
            {
                ControlButtonClicked?.Invoke(true);
                btn.Reset();
            }
        }

        private void OnButtonClickedEvent(KeyboardButton senders)
        {
            if (KeyboardButtonStateChanging?.Invoke(senders) ?? false)
            {
                _currentButtonStack.Push(senders);
                senders.Color = Colors.BlueViolet;
            }
        }

        private KeyboardButton[] GetButtonsRow(string symbols)
        {
            var res = new KeyboardButton[symbols.Length];
            for (var i = 0; i < symbols.Length; i++)
            {
                var symbol = new string(symbols[i], 1);
                var btn = res[i] = _textButtonsData[symbol] = new KeyboardButton(symbol);
                btn.ButtonClickedEvent += OnButtonClickedEvent;
            }

            return res;
        }
    }
}
