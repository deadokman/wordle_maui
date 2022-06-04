using MauiApp4.ViewModels.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4.ViewModels.interfaces
{
    public interface IKeyboardEventEmitter
    {
        /// <summary>
        /// Keyboard button state changing event
        /// </summary>
        public event KeyboardButtonStateChangingDelegate KeyboardButtonStateChanging;
        
        /// <summary>
        /// Keyboard button cliecked
        /// </summary>

        public event ControlButtonClickedDelegate ControlButtonClicked;
    }
}
