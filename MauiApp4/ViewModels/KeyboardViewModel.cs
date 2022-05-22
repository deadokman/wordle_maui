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

        private const string _alphabetFirstRow = "йцукенгшщзхъ";
        private const string _alphabetSecondRow = "фывапролджэ" + "\u0008";
        private const string _alphabetThirdRow = "ячсмитьбю" + "\u23CE";

        private Dictionary<string, KeyboardButton> _textButtonsData;

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
            Buttons[2] = GetButtonsRow(_alphabetThirdRow);
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

        private void OnButtonClickedEvent(KeyboardButton senders)
        {
            if (KeyboardButtonStateChanging?.Invoke(senders) ?? false)
            {
                senders.Color = Colors.BlueViolet;
            }
        }
    }
}
