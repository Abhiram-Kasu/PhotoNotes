using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoNotes.Extensions;
using PhotoNotes.Models;
using PhotoNotes.Services;
using System.Collections.ObjectModel;

namespace PhotoNotes.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {

        private readonly IPhotoManagement photoManagement;
        [ObservableProperty]
        private bool _isBusy;

        public bool HasFiles => Files.Any();

        public bool HasFolders => Folders.Any();

        public ObservableCollection<FileItem> Files { get; set; }
        public ObservableCollection<FolderItem> Folders { get; set; }

        public MainPageViewModel(IPhotoManagement photoManagement)
        {

            this.photoManagement = photoManagement;
            (Folders, Files) = (photoManagement.MainFolder.Folders, photoManagement.MainFolder.Files);


            Files.CollectionChanged += (_, e) => OnPropertyChanged(nameof(HasFiles));
            Folders.CollectionChanged += (_, e) => OnPropertyChanged(nameof(HasFolders));




        }
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrTitle))]
        private string? currFolder = null;

        public string CurrTitle => CurrFolder ?? "Files";

        [RelayCommand]
        public void DeleteFolderItem(string name)
        {
            photoManagement.DeleteFolder(name);
            Folders.RemoveAll(x => x.Name == name);
        }
        [RelayCommand]
        public void DeleteFileItem(string name)
        {
            if (CurrFolder is null)
            {
                photoManagement.DeleteFile(null, name);

            }
            else
            {
                photoManagement.DeleteFile(CurrFolder, name);
            }
            Files.RemoveAll(x => x.Name == name);


        }

        [RelayCommand]
        public void SelectFolder(string name)
        {
            CurrFolder = name;
            var folder = Folders.Single(x => x.Name == name);


            Files = folder.Files;

            OnPropertyChanged(nameof(Files));

        }
        [RelayCommand]
        public void BackToMain()
        {
            CurrFolder = null;
            Folders = photoManagement.MainFolder.Folders;
            OnPropertyChanged(nameof(Folders));
            Files = photoManagement.MainFolder.Files;
            OnPropertyChanged(nameof(Files));

        }


    }
}
