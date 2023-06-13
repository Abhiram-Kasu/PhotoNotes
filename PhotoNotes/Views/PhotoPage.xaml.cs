using PhotoNotes.Services;
using PhotoNotes.ViewModels;

namespace PhotoNotes.Views;

public partial class PhotoPage : ContentPage
{
    private bool loaded = false;

    public PhotoPage()
    {
        InitializeComponent();
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.NumCamerasDetected > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            cameraView.AutoStartPreview = true;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                //May need for Android idk yet
                //var _ = await cameraView.StopCameraAsync();
                var res = await cameraView.StartCameraAsync();
                loaded = true;
                await Shell.Current.GoToAsync($"//{nameof(PhotoPage)}", animate: false);
                Shell.Current.ToolbarItems.Clear();
            });
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        var res = MainThread.InvokeOnMainThreadAsync(() => cameraView.StopCameraAsync());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (loaded)
        {
            var res = MainThread.InvokeOnMainThreadAsync(() => cameraView.StartCameraAsync());
        }
    }

    private void PictureButton_Clicked(object sender, EventArgs e)
    {
        _ = MainThread.InvokeOnMainThreadAsync(async () =>
        {
            string fileName = DateTime.Now.Ticks.ToString();
            fileName += ".png";

            string snapFilePath = Path.Combine(IPhotoManagement.TempPath, fileName);
            await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.PNG, snapFilePath);
            await Shell.Current.GoToAsync($"secret/{nameof(SavePhotoView)}?{nameof(SavePhotoViewModel.TempFilePath)}={snapFilePath}");
        });
    }
}