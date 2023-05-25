using PhotoNotes.Models;
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

    private void FileCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        viewModel.SelectFile((e.CurrentSelection[0] as FileItem).Name);
    }

    private void FolderCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;
        var currName = (e.CurrentSelection[0] as FolderItem).Name;
        (sender as CollectionView).SelectedItems.Clear();
        viewModel.SelectFolder(currName);
        
    }
}

