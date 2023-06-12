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
        SearchBar.TextChanged += (_,x) => viewModel.OnTextChanged(string.IsNullOrWhiteSpace(x.NewTextValue));
        




    }

  

    private async void TapGestureRecognizer_OnTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.DisplayAlert("Clicked", "Ya Clicked", "ok");
    }

    private async void MenuItem_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert("Clicked", "Ya Clicked", "ok");
    }

    private async void FileCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;

        await viewModel.SelectFile((e.CurrentSelection[0] as FileItem).CurrPath);
        (sender as CollectionView).SelectedItems.Clear();
        (sender as CollectionView).SelectedItem = null;
    }

    private void FolderCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;
        var currName = (e.CurrentSelection[0] as FolderItem).CurrPath;
        (sender as CollectionView).SelectedItems.Clear();
        (sender as CollectionView).SelectedItem = null;

        viewModel.SelectFolder(currName);

    }

    
}

