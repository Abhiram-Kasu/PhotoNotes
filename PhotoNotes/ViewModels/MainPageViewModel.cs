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
        private static readonly ObservableCollection<FolderItem> EmptyFolderCollection = new (Enumerable.Empty<FolderItem>());
        private static readonly ObservableCollection<FileItem> EmptyFileCollection = new(Enumerable.Empty<FileItem>());
        public MainPageViewModel(IPhotoManagement photoManagement)
        {

            this.photoManagement = photoManagement;
            (Folders, Files) = (photoManagement.MainFolder.Folders, photoManagement.MainFolder.Files);


            Files.CollectionChanged += (_, e) => OnPropertyChanged(nameof(HasFiles));
            Folders.CollectionChanged += (_, e) => OnPropertyChanged(nameof(HasFolders));




        }
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrTitle))]
        [NotifyPropertyChangedFor(nameof(ShowHomeButton))]
        private string? currFolder = null;

        public bool ShowHomeButton => CurrFolder is not null;

        public string CurrTitle => CurrFolder ?? "Files";

        [RelayCommand]
        public void BackToHome()
        {
            Folders = photoManagement.MainFolder.Folders;
            Files = photoManagement.MainFolder.Files;
            CurrFolder = null;
            OnPropertyChanged(nameof(Files));
            OnPropertyChanged(nameof(HasFolders));
            OnPropertyChanged(nameof(HasFiles));
        }

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
            
            var folder = Folders.Single(x => x.Name == name);
            CurrFolder = folder.ShortName;

            Files = folder.Files;
            Folders = EmptyFolderCollection;

            OnPropertyChanged(nameof(HasFolders));
            OnPropertyChanged(nameof(Files));

        }
        [RelayCommand]
        public void SelectFile(string name)
        {
            //TODO 
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
