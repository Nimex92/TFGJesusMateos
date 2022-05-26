using ClassLibray;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace HolaMundoMAUI;

public partial class AnadeTrabajadorEquipoTrabajo : ContentPage
{
	PresenciaContext p = new PresenciaContext();
	string Username;
	public AnadeTrabajadorEquipoTrabajo(string username)
	{
		Username = username;
		InitializeComponent();
		SetPickers();
	}
	/// <summary>
	/// Fill the UI Pickers with data
	/// </summary>
	private void SetPickers()
	{
		var workGroupList = p.WorkGroups.ToList();
		var workerList = p.Workers.ToList();
		//Para cada lista que haya en la seleccion WorkShifts, añado al selector (Picker de la interfaz) El nombre del turno
		WorkGroupSelector.Items.Add("-- Selecciona equipo de trabajo.");
		WorkerSelector.Items.Add("-- Selecciona Trabajador.");
		foreach (WorkGroup workGroup in workGroupList)
		{
			WorkGroupSelector.Items.Add(workGroup.Name);
		}
		foreach (Worker worker in workerList)
		{
			WorkerSelector.Items.Add(worker.Name);
		}
		if (workGroupList.Count > 0) { WorkGroupSelector.SelectedIndex = 0; }
		if (workerList.Count > 0) { WorkerSelector.SelectedIndex = 0; }
	}
	/// <summary>
	/// Method to back to the last screen of the app
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
    private void BackButton_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 6));
    }
	/// <summary>
	/// Method to add a worker to a workGroup
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private async void SubminButton_Clicked(object sender, EventArgs e)
    {
		string workGroupSearch = WorkGroupSelector.SelectedItem.ToString();
		string workerSearch = WorkerSelector.SelectedItem.ToString();
		var workGroup = p.WorkGroups.Where(x => x.Name == workGroupSearch).Include(x=>x.Workers).FirstOrDefault();
		var worker = p.Workers.Where(x => x.Name == workerSearch).Include(x=>x.WorkGroup).FirstOrDefault();
		var workerList = workGroup.Workers;
            
		if (workerList.Contains(worker))
        {
			await DisplayAlert("Error", "El trabajador ya esta asignado", "Vale");
        }
        else
        {
			workGroup.Workers.Add(worker);
			worker.BelongstoWorkGroups += " - "+workGroupSearch;
			p.SaveChanges();
			await DisplayAlert("Success", "Se ha asignado correctamente " + worker.Name + " a " + workGroup.Name, "Vale");
        }
	}
}