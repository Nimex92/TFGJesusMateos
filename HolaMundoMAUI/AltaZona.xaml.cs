namespace HolaMundoMAUI;
using Persistencia;

public partial class AltaZona : ContentPage
{
	public AltaZona()
	{
		InitializeComponent();
	}

	private void IrAlMain(object sender,EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}

    private async void RegistrarNuevaZona(object sender, EventArgs e)
    {
		bool inserta = OperacionesDBContext.insertaZona(CampoNombre.Text);
        if (inserta == true)
        {
			await DisplayAlert ("Alert","Zona unsertada correctamente","OK");
        }
        else
        {
            await DisplayAlert("Alert", "Error al insertar", "OK");
        }
		
    }
}