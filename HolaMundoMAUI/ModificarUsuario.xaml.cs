using Bibliotec;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class ModificarUsuario : ContentPage
{
	bool activado;
	Usuarios us = new();
	public ModificarUsuario()
	{
		InitializeComponent();
		activado = false;
		SetListView();
    }
	public void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}
	public void SetListView()
    {
		PresenciaContext presenciaContext = new PresenciaContext();
		var users = presenciaContext.Usuarios.ToList();
		ListViewUsuarios.ItemsSource = users;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Usuarios item = e.SelectedItem as Usuarios;
		us = item;
	}
	public void MostrarEditar(object sender, EventArgs e)
    {
		Usuarios os = us;
		if(us.Username == CampoUsuario.Text)
        {
			activado = true;
        }
        else
        {
			activado=false;
        }

        if (activado == false) {
			CampoUsuario.IsVisible = true;
			CampoUsuario.IsEnabled = true;
			CampoContrasena.IsVisible = true;
			CampoContrasena.IsEnabled = true;
			CampoRepiteContrasena.IsVisible = true;
			CampoRepiteContrasena.IsEnabled = true;
			BotonGuardarCambios.IsVisible = true;
			BotonGuardarCambios.IsEnabled = true;
			CampoUsuario.Text = os.Username;
			CampoContrasena.Text = os.Password;
			LabelEsAdmin.IsEnabled = true;
			LabelEsAdmin.IsVisible = true;
			BotonEsAdmin.IsVisible = true;
			BotonEsAdmin.IsEnabled = true;
			activado = true;
        }
        else
        {
			CampoUsuario.IsVisible = false;
			CampoUsuario.IsEnabled = false;
			CampoContrasena.IsVisible = false;
			CampoContrasena.IsEnabled = false;
			CampoRepiteContrasena.IsVisible = false;
			CampoRepiteContrasena.IsEnabled = false;
			BotonGuardarCambios.IsEnabled = false;
			BotonGuardarCambios.IsVisible = false;
			CampoUsuario.Text = "";
			CampoContrasena.Text = "";
			activado = false;
			LabelEsAdmin.IsEnabled = false;
			LabelEsAdmin.IsVisible = false;
			BotonEsAdmin.IsVisible = false;
			BotonEsAdmin.IsEnabled = false;
		}
	}

	public void GuardarCambios(object sender, EventArgs e)
    {
		var os = us;
		var nombre = CampoUsuario.Text;
		var contraseña = CampoContrasena.Text;
		var compruebaContraseña = CampoRepiteContrasena;
		bool admin;
		if (BotonEsAdmin.IsChecked)
        {
			admin = true;
        }
        else
        {
			admin = false;
        }
		if(CampoContrasena.Text == CampoRepiteContrasena.Text) {
			OperacionesDBContext.actualizaUsuario(os.Username,nombre, contraseña,admin);
			LabelPruebas.IsEnabled = true;
			LabelPruebas.IsVisible = true;
			LabelPruebas.Text = "Usuario actualizado correctamente.";
			LabelPruebas.TextColor = Colors.Green;
        }
        else
        {
			LabelPruebas.IsEnabled = true;
			LabelPruebas.IsVisible = true;
			LabelPruebas.Text = "Las contraseñas deben coincidir.";
			LabelPruebas.TextColor = Colors.Red;
		}
		
    }
}