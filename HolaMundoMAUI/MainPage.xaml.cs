using Bibliotec;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class MainPage : ContentPage
{	
	PresenciaContext p = new PresenciaContext();
	DateTime dt = DateTime.Now;
	DateTime Today = DateTime.Today;

	string user;
	public MainPage()
	{
		InitializeComponent();
		CompruebaTurnos();
	}
	public void CompruebaTurnos()
    {
		var Turnos = p.Turno.ToList();
		foreach(Turno t in Turnos)
        {
            if (t.ValidoDesde<Today)
            {
				t.Activo = true;
            }
			if(t.ValidoHasta<Today)
            {
				t.Activo = false;
				t.Eliminado = true;
				p.SaveChanges();
			}
        }
		
    }
	public async void CambiaFichar(object sender, EventArgs e)
    {
		string NombreUsuario = CampoUsuario.Text;
		string ContrasenaUsuario = CampoContraseña.Text;

        var usuario = p.Usuarios
							.Where(b => b.Username == NombreUsuario && b.Password == ContrasenaUsuario).FirstOrDefault();
		
		if (usuario is not null) {
            if (usuario.esAdmin == true)
            {
				MensajeError.IsVisible = false;
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
				p.Logs.Add(new Bibliotec.Log("Login", NombreUsuario + " ha iniciado sesion - "+dt));
				p.SaveChanges();
				user = NombreUsuario;
			}
            else
            {
				MensajeError.IsVisible = false;
				App.Current.MainPage = new NavigationPage(new PaginaFichar(NombreUsuario));
				p.Logs.Add(new Bibliotec.Log("Login", NombreUsuario + " ha iniciado sesion - "+dt));
				p.SaveChanges();
			}
			

        }
        else
        {
			MensajeError.Text = "El usuario o la contraseña son incorrectos.";
			usuario = new("", "");
			MensajeError.IsVisible = true;
			await DisplayAlert("Alert", "El usuario o la contraseña son incorrectos.", "OK");
		}
	}
	public void AltaUsuario(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new AltaUsuarios(user));
	}

}