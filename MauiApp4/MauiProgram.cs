using MauiApp4.ViewModels;

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
                .AddSingleton<WordlyViewModel>()
				.AddSingleton<KeyboardViewModel>();

		return builder.Build();

    }


}

