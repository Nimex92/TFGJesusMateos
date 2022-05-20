using Persistencia;
using ClassLibray;
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
		var equipos = presenciaContext.WorkGroups;
		var zonas = presenciaContext.Places;

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaEquipos = new List<string>();
		var ListaTareas = new List<string>();
<<<<<<< HEAD
		//Para cada lista que haya en la seleccion WorkShifts, añado al selector (Picker de la interfaz) El nombre del turno
=======
		//Para cada lista que haya en la seleccion Turno, a�ado al selector (Picker de la interfaz) El Nombre del turno
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
		SelectorGruposTrabajo.Items.Add("-- Selecciona Grupo de trabajo.");
		SelectorZonas.Items.Add("-- Selecciona zona.");
		foreach (WorkGroup equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Name);
		}
		foreach (Places zona in zonas)
		{
			SelectorZonas.Items.Add(zona.Name);
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
		var grupo = presenciaContext.WorkGroups.Where(x => x.Name == TurnoGrupo).FirstOrDefault();
		var zona = presenciaContext.Places.Where(x => x.Name == NombreZona).FirstOrDefault();

		if (grupo is not null && zona is not null)
		{
			grupo.Places.Add(zona);
			await DisplayAlert("Alert", "Se ha añadido '" + zona.Name + "' a grupo: " + grupo.Name, "OK");
			presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido "+grupo.Places+" a " + grupo.Name + " - " + dt));
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 4));
		}
		else
		{
			await DisplayAlert("Alert", "Error al añadir tareas.", "OK");
		}

	}
}