using Persistencia;
using Bibliotec;
namespace HolaMundoMAUI;


public partial class AnadirZonaGrupoTrabajo : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	public AnadirZonaGrupoTrabajo()
	{
		InitializeComponent();
		SetPickers();
	}
	private void SetPickers()
	{
		var grupos = presenciaContext.Grupo_Trabajo;
		var zonas = presenciaContext.Zonas;

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaGrupos = new List<string>();
		var ListaTareas = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno
		SelectorGruposTrabajo.Items.Add("-- Selecciona Grupo de trabajo.");
		SelectorZonas.Items.Add("-- Selecciona zona.");
		foreach (Grupo_Trabajo grupo in grupos)
		{
			SelectorGruposTrabajo.Items.Add(grupo.Turno);
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
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}

	private async void BotonRegistrar_Clicked(object sender, EventArgs e)
	{
		var TurnoGrupo = SelectorGruposTrabajo.SelectedItem;
		var NombreZona = SelectorZonas.SelectedItem;
		var grupo = presenciaContext.Grupo_Trabajo.Where(x => x.Turno == TurnoGrupo).FirstOrDefault();
		var zona = presenciaContext.Zonas.Where(x => x.Nombre == NombreZona).FirstOrDefault();

		if (grupo is not null && zona is not null)
		{
			grupo.Zonas.Add(zona);
			await DisplayAlert("Alert", "Se ha añadido '" + zona.Nombre + "' a grupo: " + grupo.Turno, "OK");
			presenciaContext.SaveChanges();
		}
		else
		{
			await DisplayAlert("Alert", "Error al añadir tareas.", "OK");
		}

	}
}