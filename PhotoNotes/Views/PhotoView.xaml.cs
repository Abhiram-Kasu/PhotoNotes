using PhotoNotes.ViewModels;

namespace PhotoNotes.Views;

public partial class PhotoView : ContentPage
{
	public PhotoView(PhotoViewModel m)
	{
		
		InitializeComponent();
		this.BindingContext = m;
	}
}