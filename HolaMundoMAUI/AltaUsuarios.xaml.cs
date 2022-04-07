using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace HolaMundoMAUI;

public partial class AltaUsuarios : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	public AltaUsuarios()
	{
		InitializeComponent();
	}

	public async void RegistrarNuevoUsuario(object sender, EventArgs e)
	{
		string Username = CampoUsuario.Text;                    //Recojo el usuario de la interfaz
		string Password = CampoContrasena.Text;                 //Recojo la contraseña de la interfaz
		string CompruebaPassword = CampoRepiteContrasena.Text;  //Recojo la comprobacion de la contraseña de la interfaz

		var us = presenciaContext.Usuarios
						.Where(b => b.Username == Username)
						.FirstOrDefault();

		if (us is not null)
		{
			if (CampoUsuario.Text == us.Username)
			{
				//El usuario existe, activamos el label de error
				await DisplayAlert("Alert","El usuario "+Username+" ya existe, por favor pruebe otro","OK");
				LabelAvisos.Text = "El nombre de usuario ya existe...";
				LabelAvisos.TextColor = Colors.Red;
				LabelAvisos.IsVisible = true;
			}
        }
        else
        {
			if (CampoContrasena.Text == CampoRepiteContrasena.Text)
			{
				bool esAdmin=false;
				if(BotonEsAdmin.IsChecked)
					esAdmin = true;

				//Inserto usuario
				bool inserta = OperacionesDBContext.insertaUsuario(Username, Password,esAdmin);
                if (inserta == true) 
				{
					//Activo label de aceptacion, se ha insertado.
					await DisplayAlert("Alert", "Usuario "+Username+" insertado correctamente.", "OK");
				}
                else
                {
					await DisplayAlert("Alert", "Error al insertar el usuario "+Username+".", "OK");
                }

			}
			else
			{
				//Activa label error, no coinciden las contraseñas
				LabelAvisos.Text = "Las contraseñas deben coincidir.";
				LabelAvisos.TextColor = Colors.Red;
				LabelAvisos.IsVisible = true;
			}
		}
	}   

	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());

	}
	public void gaga(object sender, EventArgs e)
    {
		CampoContrasena.IsPassword = true;
		CampoRepiteContrasena.IsPassword = true;

    }
}