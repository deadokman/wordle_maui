
/* Unmerged change from project 'MauiApp4 (net6.0-maccatalyst)'
Before:
using System;
After:
using MauiApp4;
using MauiApp4;
using MauiApp4.Bll;
using System;
*/
using MauiApp4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp4
{
    public class ViewModelLocator
    {
        private static ViewModelLocator _instance;
        public static ViewModelLocator Instance => _instance ?? (_instance = new ViewModelLocator());

        private MauiApp _app;

        private ViewModelLocator()
        {
        }

        public void SetApp(MauiApp app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
        }

        public WordlyViewModel WordlyViewModel => _app.Services.GetService<WordlyViewModel>();

        public KeyboardViewModel KeyboardViewModel => _app.Services.GetService<KeyboardViewModel>();
    }
}
