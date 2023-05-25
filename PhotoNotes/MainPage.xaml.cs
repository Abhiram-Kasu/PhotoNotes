using PhotoNotes.ViewModels;

namespace PhotoNotes;

public partial class MainPage : ContentPage
{
    int initial = 0;
    private readonly MainPageViewModel viewModel;
    public MainPage(MainPageViewModel m)
    {
        this.BindingContext = m;
        viewModel = m;
        InitializeComponent();



    }


    private async void TapGestureRecognizer_OnTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.DisplayAlert("Clicked", "Ya Clicked", "ok");
    }

    private async void MenuItem_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert("Clicked", "Ya Clicked", "ok");
    }
}

