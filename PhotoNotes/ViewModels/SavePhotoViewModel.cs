using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoNotes.Services;

namespace PhotoNotes.ViewModels
{
    [QueryProperty(nameof(TempFilePath), nameof(TempFilePath))]
    public partial class SavePhotoViewModel : ObservableObject
    {
        private readonly IPhotoManagement photoManagement;

        [ObservableProperty]
        private int _selectedIndex = -1;

        [ObservableProperty]
        private string fileName;

        [ObservableProperty]
        private bool saveToFolder;

        public SavePhotoViewModel(IPhotoManagement photoManagement)
        {
            this.photoManagement = photoManagement;
        }

        public IList<string> FolderOptions => photoManagement.MainFolder.Folders.Select(x => x.Name).ToList();
        public IList<string> FolderOptionsShort => photoManagement.MainFolder.Folders.Select(x => x.ShortName).ToList();
        public string TempFilePath { get; set; }

        [RelayCommand]
        public async Task CreateNewFolder()
        {
            var folderName = await Shell.Current.DisplayPromptAsync("Folder Name", "What would you like to name your folder");
            if (folderName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
            {
                await Shell.Current.DisplayAlert("Error", "File Name not allowed", "Ok");
                return;
            }

            var (successfull, errMessage) = photoManagement.CreateNewFolder(folderName);
            if (!successfull)
            {
                await Shell.Current.DisplayAlert("Error", errMessage, "Ok");
                return;
            }

            OnPropertyChanged(nameof(FolderOptions));
            OnPropertyChanged(nameof(FolderOptionsShort));
            SelectedIndex = FolderOptionsShort.IndexOf(folderName);
        }

        [RelayCommand]
        public async Task Save()
        {
            if (FileName is null)
            {
                await Shell.Current.DisplayAlert("Error", "File Name cannot be empty!", "Ok");
                return;
            }
            if (SaveToFolder)
            {
                if (FileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                {
                    await Shell.Current.DisplayAlert("Error", "File Name is not valid!", "Ok");
                    return;
                }
                if (SelectedIndex == -1)
                {
                    await Shell.Current.DisplayAlert("Error", "You didn't select an folder!", "Ok");
                    return;
                }

                photoManagement.CreateNewFolder(FolderOptions[SelectedIndex]);
                var (successfull, errMessage) = photoManagement.CreateNewFile(folder: FolderOptions[SelectedIndex], name: FileName + ".png", fromPath: TempFilePath);

                if (!successfull)
                {
                    await Shell.Current.DisplayAlert("Error", errMessage, "Ok");
                    return;
                }

                await Shell.Current.GoToAsync($"../");
            }
            else
            {
                if (FileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                {
                    await Shell.Current.DisplayAlert("Error", "File Name is not valid!", "Ok");
                    return;
                }

                var (successfull, errMessage) = photoManagement.CreateNewFile(name: FileName, fromPath: TempFilePath);
                if (!successfull)
                {
                    await Shell.Current.DisplayAlert("Error", errMessage, "Ok");
                    return;
                }

                await Shell.Current.GoToAsync($"../");
            }
        }
    }
}