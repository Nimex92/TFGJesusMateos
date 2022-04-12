using Bibliotec;
using Persistencia;

namespace HolaMundoMAUI;

public partial class AnadeTareasGrupoTrabajo : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	public AnadeTareasGrupoTrabajo()
	{
		InitializeComponent();
		SetPickers();
	}
	private void SetPickers()
    {
		var grupos = presenciaContext.Grupo_Trabajo;
		var tareas = presenciaContext.Tareas;

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaGrupos = new List<string>();
		var ListaTareas = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno
		SelectorGruposTrabajo.Items.Add("-- Selecciona Grupo de trabajo.");
		SelectorTareas.Items.Add("-- Selecciona tarea.");
		foreach (Grupo_Trabajo grupo in grupos)
		{
			SelectorGruposTrabajo.Items.Add(grupo.Turno);
		}
		foreach (Tareas tarea in tareas)
		{
			SelectorTareas.Items.Add(tarea.NombreTarea);
		}
		SelectorGruposTrabajo.SelectedIndex=0;
		SelectorTareas.SelectedIndex = 0;

	}
    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
    }

    private async void BotonRegistrar_Clicked(object sender, EventArgs e)
    {
		var TurnoGrupo = SelectorGruposTrabajo.SelectedItem;
		var NombreTarea = SelectorTareas.SelectedItem;
		var grupo = presenciaContext.Grupo_Trabajo.Where(x=>x.Turno == TurnoGrupo).FirstOrDefault();
		var tarea = presenciaContext.Tareas.Where(x=>x.NombreTarea == NombreTarea).FirstOrDefault();

		if(grupo is not null && tarea is not null)
        {
			grupo.Tareas.Add(tarea);
			await DisplayAlert("Alert", "Se ha añadido '" + tarea.NombreTarea + "' a grupo: "+grupo.Turno, "OK");
			presenciaContext.SaveChanges();
        }
        else
        {
			await DisplayAlert("Alert", "Error al añadir tareas.", "OK");
		}

    }
}