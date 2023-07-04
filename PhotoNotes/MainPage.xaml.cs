using CommunityToolkit.Maui.Core.Platform;
using PhotoNotes.Models;
using PhotoNotes.ViewModels;

namespace PhotoNotes;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel viewModel;
    private int initial = 0;

    protected override async void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        await SearchBar.HideKeyboardAsync(new CancellationToken());
    }

    public MainPage(MainPageViewModel m)
    {
        this.BindingContext = m;
        viewModel = m;
        InitializeComponent();
        SearchBar.TextChanged += (_, x) =>
        {
            if (string.IsNullOrWhiteSpace(x.NewTextValue))
            {
                viewModel.BackToMain();
            }
        };
    }

    private async void MenuItem_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert("Clicked", "Ya Clicked", "ok");
    }

    private async void TapGestureRecognizer_OnTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.DisplayAlert("Clicked", "Ya Clicked", "ok");
    }

    private async void SearchBar_Completed(object sender, EventArgs e)
    {
        await (sender as Entry).HideKeyboardAsync(new CancellationToken());
    }
}