using Bibliotec;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class MainPage : ContentPage
{	
	PresenciaContext p = new PresenciaContext();
	DateTime dt = DateTime.Now;
	DateTime Today = DateTime.Today;
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
            if (t.ValidoDesde<=Today)
            {
				t.Activo = true;

            }
			if(t.ValidoHasta<Today)
            {
				t.Activo = false;
				t.Eliminado = true;
			}
			p.SaveChanges();
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
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
				OperacionesDBContext.InsertaLog(new Log("Login", NombreUsuario + " ha iniciado sesion - "+dt));
			}
            else
            {
				App.Current.MainPage = new NavigationPage(new PaginaFichar(NombreUsuario));
				OperacionesDBContext.InsertaLog(new Log("Login", NombreUsuario + " ha iniciado sesion - "+dt));
			}
        }
        else
        {
			usuario = new("", "");
			await DisplayAlert("Alert", "El usuario o la contraseña son incorrectos.", "OK");
		}
	}

}