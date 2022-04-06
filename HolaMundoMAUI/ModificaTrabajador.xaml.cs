using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class ModificarTrabajador {
	bool activado;
	Trabajador tr = new();
	public ModificarTrabajador()
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
		var trabajador = presenciaContext.Trabajador.Include(x=>x.grupo).Include(x=>x.usuario).ToList();
		ListViewUsuarios.ItemsSource = trabajador;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Trabajador item = e.SelectedItem as Trabajador;
		tr = item;
	}
	public void MostrarEditar(object sender, EventArgs e)
    {
		Trabajador os = tr;
		if(os.nombre == CampoUsuario.Text)
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
			CampoUsuario.Text = os.nombre;
			CampoContrasena.Text = os.grupo.Turno;
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
		
    }
}