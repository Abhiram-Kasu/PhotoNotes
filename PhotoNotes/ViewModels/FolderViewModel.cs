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

        public FolderViewModel(IPhotoManagement pm)
        {
            photoManagement = pm;
            
        }


        public FolderItem Folder { get; set; }

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

    }
}
