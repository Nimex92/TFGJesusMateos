using Persistencia;

namespace HolaMundoMAUI;

public partial class MainPage : ContentPage
{	
	PresenciaContext presenciaContext = new PresenciaContext();
	public MainPage()
	{
		InitializeComponent();
	}
	public void CambiaFichar(object sender, EventArgs e)
    {
		string NombreUsuario = CampoUsuario.Text;
		string ContrasenaUsuario = CampoContraseña.Text;
		////if (NombreUsuario == null)
		////	NombreUsuario = "";
		////if (ContrasenaUsuario == null)
		////	ContrasenaUsuario = "";

		 var usuario = presenciaContext.Usuarios
							.Where(b => b.Username == NombreUsuario)
							.FirstOrDefault();
		if (usuario==null)
			MensajeError.Text = "El usuario o la contraseña son incorrectos.";
			usuario.Username = "pepe";
			usuario.Password = "";

		if (NombreUsuario.Equals(usuario.Username) && ContrasenaUsuario.Equals(usuario.Password))
		{
			MensajeError.IsVisible = false;
			App.Current.MainPage = new NavigationPage(new PaginaFichar(NombreUsuario));
			
		}
        else
        {
			MensajeError.IsVisible=true;
        }

	}
	public void AltaUsuario(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new AltaUsuarios());
	}

}