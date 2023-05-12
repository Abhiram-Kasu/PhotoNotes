using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoNotes.Extensions;
using PhotoNotes.Models;
using PhotoNotes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoNotes.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {

        private readonly IPhotoManagement photoManagement;
        [ObservableProperty]
        private bool _isBusy;

        public bool HasFiles => Files.Any();

        public bool HasFolders => Folders.Any();

        public ObservableCollection<FileItem> Files { get; set;  }
        public ObservableCollection<FolderItem> Folders { get; set; }

        public MainPageViewModel(IPhotoManagement photoManagement)
        {
            
            this.photoManagement = photoManagement;
            (Folders, Files) = (photoManagement.MainFolder.Folders, photoManagement.MainFolder.Files);

            
            Files.CollectionChanged += (_, e) => OnPropertyChanged(nameof(HasFiles));
            Folders.CollectionChanged += (_, e) => OnPropertyChanged(nameof(HasFolders));
            



        }


        public void Update()
        {
            
        }

        

    }
}
