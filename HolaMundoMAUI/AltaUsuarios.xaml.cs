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

	public void RegistrarNuevoUsuario(object sender, EventArgs e)
    {
		string Username = CampoUsuario.Text;					//Recojo el usuario de la interfaz
		string Password = CampoContrasena.Text;					//Recojo la contraseña de la interfaz
		string CompruebaPassword = CampoRepiteContrasena.Text;  //Recojo la comprobacion de la contraseña de la interfaz

		bool existe=false;
		
			var us = presenciaContext.Usuarios
							.Where(b => b.Username == Username)
							.FirstOrDefault();
		

		if (CampoUsuario.Text.Equals(us))
            {
				//El usuario existe, activamos el label de error
				existe = true;
            }
        
		if (!existe)
		{
			//Lo inserto y activo el label de aceptacion
			existe = false;
            if (CampoContrasena.Text.Equals(CampoRepiteContrasena.Text))
            {
				//Inserto usuario
				OperacionesDBContext.insertaUsuario(Username, Password);
				//Activo label de aceptacion, se ha insertado.
				LabelAvisos.Text = "Usuario insertado correctamente.";
				LabelAvisos.TextColor = Colors.Green;

            }else
            {
				//Activa label error, no coinciden las contraseñas
            }
        }
        else
        {
			//Muestro el error de que ya existe
        }
	}

	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new MainPage());

	}
}