namespace HolaMundoMAUI;
using Persistencia;
using ClassLibray;

public partial class AltaZona : ContentPage
{
    PresenciaContext presenciaContext = new PresenciaContext();
    string NombreUsuario;
    DateTime dt = DateTime.Now;
    string Zona;
    Db db = new Db();

    public AltaZona(string user)
	{
		InitializeComponent();
        NombreUsuario= user;

    }
    public AltaZona(string user,string zona,int actualiza)
    {
        InitializeComponent();
        NombreUsuario = user;
        Zona = zona;
        switch (actualiza)
        {
            case 0:
                BotonRegistrarAdmin.IsVisible = true;
                BotonRegistrarAdmin.IsEnabled = true;
                BotonActualizarAdmin.IsVisible = false;
                BotonActualizarAdmin.IsEnabled = false;
                CampoNombre.Text = "";
                break;
            case 1:
                BotonRegistrarAdmin.IsVisible = false;
                BotonRegistrarAdmin.IsEnabled = false;
                BotonActualizarAdmin.IsVisible = true;
                BotonActualizarAdmin.IsEnabled = true;
                CampoNombre.Text = zona;
                break;
        }

    }

    private void IrAlMain(object sender,EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,4));
	}

    private async void RegistrarNuevaZona(object sender, EventArgs e)
    {
		bool inserta = db.InsertPlace(CampoNombre.Text,presenciaContext);
        presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido grupo de trabajo " + CampoNombre.Text + " - " + dt));
        presenciaContext.SaveChanges();
        if (inserta == true)
        {
			await DisplayAlert ("Alert","Zona unsertada correctamente","OK");
            App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,4));
        }
        else
        {
            await DisplayAlert("Alert", "Error al insertar", "OK");
        }
		
    }

    private void BotonActualizarAdmin_Clicked(object sender, EventArgs e)
    {
        var zona = presenciaContext.Places.Where(x => x.Name == Zona).FirstOrDefault();
        string OldZona = zona.Name;
        zona.Name = CampoNombre.Text;
        presenciaContext.Places.Update(zona);
        presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha modificado "+OldZona+" a " + zona.Name + " - " + dt));
        presenciaContext.SaveChanges();
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,4));
    }
}