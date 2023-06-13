using PhotoNotes.Models;
using PhotoNotes.ViewModels;

namespace PhotoNotes.Views;

public partial class FolderView : ContentPage
{
    private readonly FolderViewModel viewModel;

    public FolderView(FolderViewModel vm)
    {
        this.BindingContext = viewModel = vm;
        InitializeComponent();
    }

    private async void FileCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0) return;

        await viewModel.SelectFile((e.CurrentSelection[0] as FileItem).CurrPath);
        (sender as CollectionView).SelectedItems.Clear();
        (sender as CollectionView).SelectedItem = null;
    }
}