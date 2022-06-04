using ClassLibray;
using Persistencia;

namespace HolaMundoMAUI;

public partial class AnadeTareasGrupoTrabajo : ContentPage
{
	PresenciaContext p = new PresenciaContext();
	string Username;
	DateTime Now = DateTime.Now;
	Db db = new Db();
	public AnadeTareasGrupoTrabajo(string username)
	{
		InitializeComponent();
		SetPickers();
		Username = username;
	}
	private void SetPickers()
    {
		//Recojo los grupos y las tareas de la base de datos
		var workGroups = p.WorkGroups.ToList();
		var workTasks = p.WorkTasks.ToList();
		//Creo una lista para guardar todos los turnos existentes
		var workGroupList = new List<string>();
		var workTasksList = new List<string>();
		//Para cada lista que haya en la seleccion WorkGroups, a�ado al selector (Picker de la interfaz) El nombre del turno
		WorkGroupSelector.Items.Add("-- Selecciona equipo de trabajo.");
		WorkTaskSelector.Items.Add("-- Selecciona tarea.");
        workGroups.ForEach(x =>
        {
			WorkGroupSelector.Items.Add(x.Name);
		});
        workTasks.ForEach(x =>
        {
			WorkTaskSelector.Items.Add(x.Name);
		});
		WorkGroupSelector.SelectedIndex=0;
		WorkTaskSelector.SelectedIndex = 0;
	}
	/// <summary>
	/// Method to back to the mainscreen of the app
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,5));
    }
	/// <summary>
	/// Method to add a WorkTask to a WorkGroup than exist on the DB
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
    private async void BotonRegistrar_Clicked(object sender, EventArgs e)
    {
		var workGroupName = WorkGroupSelector.SelectedItem;
		var workTaskName = WorkTaskSelector.SelectedItem;
		var workGroup = p.WorkGroups.Where(x=>x.Name == workGroupName).FirstOrDefault();
		var workTask = p.WorkTasks.Where(x=>x.Name == workTaskName).FirstOrDefault();
		if(workGroup is not null && workTask is not null)
        {
			await DisplayAlert("Success", "Se ha a�adido '" + workTask.Name + "' a grupo: "+workGroup.Name, "OK");
			db.AddWorkTaskTOWorkGroup(workGroup, workTask, p);
			db.InsertLog(new Log("A�adir", Username + " ha a�adido " + workTask.Name + " a grupo " + workGroup.Name + " - " + Now),p);
			await DisplayAlert("Success", "Se ha a�adido '" + workTask.Name + "' a grupo: "+workGroup.Name, "OK");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 5));
		}
        else
        {
			await DisplayAlert("Error", "Error al a�adir tareas.", "OK");
		}

    }
}