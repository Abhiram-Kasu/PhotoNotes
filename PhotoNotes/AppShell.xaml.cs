using PhotoNotes.Views;

namespace PhotoNotes;

public partial class AppShell : Shell
{
    public AppShell()
    {
        Routing.RegisterRoute($"secret/{nameof(SavePhotoView)}", typeof(SavePhotoView));
        Routing.RegisterRoute($"secret/{nameof(PhotoView)}", typeof(PhotoView));
        InitializeComponent();


    }
}
