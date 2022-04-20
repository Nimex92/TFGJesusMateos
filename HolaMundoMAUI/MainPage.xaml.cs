using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class MainPage : ContentPage
{	
	PresenciaContext presenciaContext = new PresenciaContext();
	string user;
	public MainPage()
	{
		InitializeComponent();
		

	}
	public async void CambiaFichar(object sender, EventArgs e)
    {
		string NombreUsuario = CampoUsuario.Text;
		string ContrasenaUsuario = CampoContraseņa.Text;

        var usuario = presenciaContext.Usuarios
							.Where(b => b.Username == NombreUsuario && b.Password == ContrasenaUsuario).FirstOrDefault();
		
		if (usuario is not null) {
            if (usuario.esAdmin == true)
            {
				MensajeError.IsVisible = false;
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
				presenciaContext.Logs.Add(new Bibliotec.Log("Login", NombreUsuario + " ha iniciado sesion"));
				presenciaContext.SaveChanges();
				user = NombreUsuario;
			}
            else
            {
				MensajeError.IsVisible = false;
				App.Current.MainPage = new NavigationPage(new PaginaFichar(NombreUsuario));
				presenciaContext.Logs.Add(new Bibliotec.Log("Login", NombreUsuario + " ha iniciado sesion"));
				presenciaContext.SaveChanges();
			}
			

        }
        else
        {
			MensajeError.Text = "El usuario o la contraseņa son incorrectos.";
			usuario = new("", "");
			MensajeError.IsVisible = true;
			await DisplayAlert("Alert", "El usuario o la contraseņa son incorrectos.", "OK");
		}
	}
	public void AltaUsuario(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new AltaUsuarios(user));
	}

}