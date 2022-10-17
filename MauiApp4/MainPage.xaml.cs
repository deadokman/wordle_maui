using MauiApp4.ViewModels;

namespace MauiApp4;

public partial class MainPage : ContentPage
{
    public MainPage(WordlyViewModel viewModel)
	{
		InitializeComponent();
#if WINDOWS
        this.MaximumWidthRequest = 800;
        this.MaximumHeightRequest = 1020;
#endif
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
	}
}

