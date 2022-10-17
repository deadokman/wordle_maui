using MauiApp4.Bll;
using MauiApp4.Bll.iface;
using MauiApp4.ViewModels;
using MauiApp4.ViewModels.interfaces;

namespace MauiApp4;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services
				.AddSingleton<MainPage>()
                .AddSingleton<WordlyViewModel>()
				.AddSingleton<IKeyboardEventEmitter>(sp => sp.GetService<KeyboardViewModel>())
                .AddSingleton<KeyboardViewModel>()
                .AddSingleton<IGameLogic, GameLogic>();

		var serviceProvider = builder.Build();
		ViewModelLocator.Instance.SetApp(serviceProvider);
        return serviceProvider;

    }


}

