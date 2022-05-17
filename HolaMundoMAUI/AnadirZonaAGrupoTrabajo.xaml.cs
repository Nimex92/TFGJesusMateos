using Persistencia;
using Bibliotec;
namespace HolaMundoMAUI;


public partial class AnadirZonaGrupoTrabajo : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	string NombreUsuario;
	DateTime dt = DateTime.Now;
	public AnadirZonaGrupoTrabajo(string user)
	{
		InitializeComponent();
		SetPickers();
		NombreUsuario = user;
	}
	private void SetPickers()
	{
		var equipos = presenciaContext.EquipoTrabajo;
		var zonas = presenciaContext.Zonas;

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaEquipos = new List<string>();
		var ListaTareas = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El Nombre del turno
		SelectorGruposTrabajo.Items.Add("-- Selecciona Grupo de trabajo.");
		SelectorZonas.Items.Add("-- Selecciona zona.");
		foreach (EquipoTrabajo equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Nombre);
		}
		foreach (Zonas zona in zonas)
		{
			SelectorZonas.Items.Add(zona.Nombre);
		}
		SelectorGruposTrabajo.SelectedIndex = 0;
		SelectorZonas.SelectedIndex = 0;

	}
	private void BotonVolver_Clicked(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,4));
	}

	private async void BotonRegistrar_Clicked(object sender, EventArgs e)
	{
		var TurnoGrupo = SelectorGruposTrabajo.SelectedItem;
		var NombreZona = SelectorZonas.SelectedItem;
		var grupo = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == TurnoGrupo).FirstOrDefault();
		var zona = presenciaContext.Zonas.Where(x => x.Nombre == NombreZona).FirstOrDefault();

		if (grupo is not null && zona is not null)
		{
			grupo.Zonas.Add(zona);
			await DisplayAlert("Alert", "Se ha añadido '" + zona.Nombre + "' a grupo: " + grupo.Nombre, "OK");
			presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido "+grupo.Zonas+" a " + grupo.Nombre + " - " + dt));
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 4));
		}
		else
		{
			await DisplayAlert("Alert", "Error al añadir tareas.", "OK");
		}

	}
}