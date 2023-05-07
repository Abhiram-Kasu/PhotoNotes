using PhotoNotes.ViewModels;

namespace PhotoNotes.Views;

public partial class SavePhotoView : ContentPage
{
	public SavePhotoView(SavePhotoViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}