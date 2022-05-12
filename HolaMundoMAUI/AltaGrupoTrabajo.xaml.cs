using Persistencia;
using Bibliotec;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AltaGrupoTrabajo : ContentPage
{
	string NombreUsuario;
	DateTime dt = DateTime.Now;
	string NombreEquipoTrabajo;
	string Turno;
	PresenciaContext presenciaContext = new PresenciaContext();
	EquipoTrabajo Eq;
	public AltaGrupoTrabajo(string user)
	{
		InitializeComponent();
		NombreUsuario = user;
	}
	public AltaGrupoTrabajo(string user,string equipo,int actualiza)
	{
		InitializeComponent();
		NombreUsuario = user;
		NombreEquipoTrabajo = equipo;
		CampoNombre.Text = equipo;
        switch (actualiza)
        {
			case 0:
				BotonRegistrarAdmin.IsVisible = true;
				BotonRegistrarAdmin.IsEnabled = true;
				BotonActualizarAdmin.IsVisible = false;
				BotonActualizarAdmin.IsEnabled = false;
				TituloEmpresa.Text = "Tech Talent" + Environment.NewLine + "Añadir trabajador a equipo de trabajo";
				break;
			case 1:
				BotonRegistrarAdmin.IsVisible = false;
				BotonRegistrarAdmin.IsEnabled = false;
				BotonActualizarAdmin.IsVisible = true;
				BotonActualizarAdmin.IsEnabled = true;
				var EquipoEditar = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == CampoNombre.Text).FirstOrDefault();
				Eq = EquipoEditar;
				TituloEmpresa.Text = "Tech Talent" + Environment.NewLine + "Revocar trabajador de equipo de trabajo";
				break;
        }
	}
	public async void RegistraNuevoGrupoTrabajo(object sender, EventArgs e)
	{
		PresenciaContext presenciaContext = new PresenciaContext();
		if (CampoNombre.Text.Equals("")==false)
		{
			string NombreEquipo = CampoNombre.Text;
			var EquipoExiste = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == NombreEquipo).FirstOrDefault();
			if (EquipoExiste is not null)
			{
				await DisplayAlert("Alert", "El equipo de trabajo ya existe.", "OK");
			}
			else
			{
				EquipoTrabajo eq = new EquipoTrabajo(NombreEquipo);
				OperacionesDBContext.InsertaEquipoTrabajo(eq);
				OperacionesDBContext.InsertaLog(new Log("Añadir", NombreUsuario + " ha añadido grupo de trabajo " + NombreEquipo + " - " + dt));
				await DisplayAlert("Alert", "El equipo de trabajo se ha añadido correctamente.", "OK");
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,6));
			}
		}
		else
		{
			await DisplayAlert("Alert", "Debes introducir el nombre de turno", "OK");
		}
	}
	private async void ActualizarGrupoTrabajo(object sender, EventArgs e)
    {
		bool actualizar = OperacionesDBContext.ActualizaEquipoTrabajo(Eq, CampoNombre.Text);
		if(actualizar == true)
        {
			await DisplayAlert("Alert", "Se ha modificado correctamente.","Vale");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 6));
			OperacionesDBContext.InsertaLog(new Log("Modifica", NombreUsuario + " ha modificado el grupo de trabajo " + Eq.Nombre));
        }
        else
        {
			await DisplayAlert("Alert", "No se han realizado cambios.","Vale");
		}
    }

	
	public List<string> GeneraHoras()
    {
		var ListaHoras = new List<string>();
		for (int i = 00; i < 24; i++)
		{
			if (i < 10)
			{
				ListaHoras.Add("0" + i.ToString());
			}
			else
			{
				ListaHoras.Add(i.ToString());
			}
		}
		return ListaHoras;
	}
	public List<string> GeneraMinutos()
	{
		var ListaMinutos = new List<string>();
		for (int i = 00; i < 60; i++)
		{
			if (i < 10)
			{
				ListaMinutos.Add("0" + i.ToString());
			}
			else
			{
				ListaMinutos.Add(i.ToString());
			}
		}
		return ListaMinutos;
	}
	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,6));
    }
}