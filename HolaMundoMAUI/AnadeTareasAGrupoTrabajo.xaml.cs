using ClassLibray;
using Persistencia;

namespace HolaMundoMAUI;

public partial class AnadeTareasGrupoTrabajo : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	string NombreUsuario;
	DateTime dt = DateTime.Now;
	public AnadeTareasGrupoTrabajo(string user)
	{
		InitializeComponent();
		SetPickers();
		NombreUsuario = user;
	}
	private void SetPickers()
    {
		var equipos = presenciaContext.WorkGroups;
		var tareas = presenciaContext.WorkTasks;

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaGrupos = new List<string>();
		var ListaTareas = new List<string>();
		//Para cada lista que haya en la seleccion WorkShifts, aÃ±ado al selector (Picker de la interfaz) El nombre del turno
		SelectorGruposTrabajo.Items.Add("-- Selecciona equipo de trabajo.");
		SelectorTareas.Items.Add("-- Selecciona tarea.");
		foreach (WorkGroup equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Name);
		}
		foreach (WorkTask tarea in tareas)
		{
			SelectorTareas.Items.Add(tarea.Name);
		}
		SelectorGruposTrabajo.SelectedIndex=0;
		SelectorTareas.SelectedIndex = 0;

	}
    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,5));
    }
    private async void BotonRegistrar_Clicked(object sender, EventArgs e)
    {
		var TurnoGrupo = SelectorGruposTrabajo.SelectedItem;
		var NombreTarea = SelectorTareas.SelectedItem;
		var grupo = presenciaContext.WorkGroups.Where(x=>x.Name == TurnoGrupo).FirstOrDefault();
		var tarea = presenciaContext.WorkTasks.Where(x=>x.Name == NombreTarea).FirstOrDefault();

		if(grupo is not null && tarea is not null)
        {
			grupo.Tasks.Add(tarea);
			presenciaContext.SaveChanges();
			await DisplayAlert("Alert", "Se ha añadido '" + tarea.Name + "' a grupo: "+grupo.Name, "OK");
			//DbInsert.AnadeTareaEquipoTrabajo(grupo,tarea,presenciaContext);
			DbInsert.InsertLog(new Log("Añadir", NombreUsuario + " ha añadido " + tarea.Name + " a grupo " + grupo.Name + " - " + dt),presenciaContext);
			await DisplayAlert("Operacion correcta", "Se ha añadido '" + tarea.Name + "' a grupo: "+grupo.Name, "OK");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 5));
		}
        else
        {
			await DisplayAlert("Alert", "Error al aÃ±adir tareas.", "OK");
		}

    }
}