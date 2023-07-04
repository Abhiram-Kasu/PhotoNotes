using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoNotes.Models;
using PhotoNotes.Services;
using PhotoNotes.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoNotes.ViewModels
{
    [QueryProperty(nameof(Folder), nameof(Folder))]
    public partial class FolderViewModel : ObservableObject
    {
        private readonly IPhotoManagement photoManagement;
        private readonly MainPageViewModel mainPageViewModel;

        public FolderViewModel(IPhotoManagement pm, MainPageViewModel tempVm)
        {
            photoManagement = pm;
            mainPageViewModel = tempVm;
        }

        private FolderItem _folderItem = default!;
        public FolderItem Folder { get => _folderItem; set => SetProperty(ref _folderItem, value); }

        [RelayCommand]
        public void DeleteFileItem(string name)
        {
            photoManagement.DeleteFile(Folder.ShortName, name);
        }

        [RelayCommand]
        public async Task SelectFile(string name)
        {
            //TODO
            await Shell.Current.GoToAsync($"secret/{nameof(PhotoView)}?{nameof(PhotoViewModel.PhotoSource)}={name}");
        }

        [RelayCommand]
        public async Task GoToSettings() => await mainPageViewModel.GoToSettings();
    }
}