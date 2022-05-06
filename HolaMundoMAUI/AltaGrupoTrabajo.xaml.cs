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
				break;
			case 1:
				BotonRegistrarAdmin.IsVisible = false;
				BotonRegistrarAdmin.IsEnabled = false;
				BotonActualizarAdmin.IsVisible = true;
				BotonActualizarAdmin.IsEnabled = true;
				TituloEmpresa.Text = "Tech Talent" + Environment.NewLine + "Revocar trabajador de equipo de trabajo";
				break;
        }
	}
	public async void RegistraNuevoGrupoTrabajo(object sender, EventArgs e)
	{
		PresenciaContext presenciaContext = new PresenciaContext();
		string NombreEquipo = CampoNombre.Text;
		if (NombreEquipo is not null)
		{
			var EquipoExiste = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == NombreEquipo).FirstOrDefault();
			if (EquipoExiste is not null)
			{
				
				await DisplayAlert("Alert", "El equipo de trabajo ya existe.", "OK");
			}
			else
			{
				EquipoTrabajo eq = new EquipoTrabajo(NombreEquipo);
				OperacionesDBContext.InsertaEquipoTrabajo(eq);
				presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido grupo de trabajo " + NombreEquipo + " - " + dt));
				presenciaContext.SaveChanges();
				await DisplayAlert("Alert", "El equipo de trabajo se ha añadido correctamente.", "OK");
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,6));
			}
		}
		else
		{
			await DisplayAlert("Alert", "Debes introducir el nombre de turno", "OK");
		}
	}
	private void ActualizarGrupoTrabajo(object sender, EventArgs e)
    {
		var Eq = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == CampoNombre.Text).FirstOrDefault();
		Eq.Nombre = CampoNombre.Text;
		presenciaContext.EquipoTrabajo.Update(Eq);
		presenciaContext.SaveChanges();
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