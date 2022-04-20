namespace HolaMundoMAUI;
using Persistencia;
using Bibliotec;

public partial class AltaZona : ContentPage
{
    PresenciaContext presenciaContext = new PresenciaContext();
    string NombreUsuario;
    DateTime dt = DateTime.Now;

    public AltaZona(string user)
	{
		InitializeComponent();
        NombreUsuario= user;

    }

	private void IrAlMain(object sender,EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
	}

    private async void RegistrarNuevaZona(object sender, EventArgs e)
    {
		bool inserta = OperacionesDBContext.insertaZona(CampoNombre.Text);
        presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido grupo de trabajo " + CampoNombre.Text + " - " + dt));
        presenciaContext.SaveChanges();
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