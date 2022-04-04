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

        var usuario = presenciaContext.Usuarios
							.Where(b => b.Username == NombreUsuario && b.Password == ContrasenaUsuario).FirstOrDefault();
		
		if (usuario is not null) { 
			MensajeError.IsVisible = false;
			App.Current.MainPage = new NavigationPage(new PaginaFichar(NombreUsuario));

        }
        else
        {
			MensajeError.Text = "El usuario o la contraseña son incorrectos.";
			usuario = new("", "");
			MensajeError.IsVisible = true;
		}
	}
	public void AltaUsuario(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new AltaUsuarios());
	}

}