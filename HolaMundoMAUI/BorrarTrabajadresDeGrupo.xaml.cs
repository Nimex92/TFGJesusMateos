using ClassLibray;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class BorraTrabajadorDeGrupo : ContentPage
{
	PresenciaContext p = new PresenciaContext();
	string Username;
	WorkGroup WorkGroup;
	public BorraTrabajadorDeGrupo(string username,WorkGroup workGroup)
	{
		Username = username;
		WorkGroup = workGroup;
		InitializeComponent();
		SetPickers();
	}
	/// <summary>
	/// Fill the UI Pickers with data
	/// </summary>
	private void SetPickers()
	{
		var workerList = p.Workers.Where(x=>x.WorkGroup.Contains(WorkGroup)).ToList();
		WorkerSelector.Items.Add("-- Selecciona Trabajador.");
		WorkGroupSelector.Items.Add(WorkGroup.Name);
		foreach (Worker worker in workerList)
		{
			WorkerSelector.Items.Add(worker.Name);
		}
		WorkGroupSelector.SelectedIndex = 0;
		WorkerSelector.SelectedIndex = 0;
	}
	/// <summary>
	/// Method to back to the last screen of the app
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 6));
    }
	/// <summary>
	/// Method to remove a worker from a workGroup
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
    private async void BotonRegistrar_Clicked(object sender, EventArgs e)
    {
		string WorkGroupSearch = WorkGroupSelector.SelectedItem.ToString();
		string workerSearch = WorkerSelector.SelectedItem.ToString();
		var workGroup = p.WorkGroups.Where(x => x.Name == WorkGroupSearch).Include(x=>x.Workers).FirstOrDefault();
		var worker = p.Workers.Where(x => x.Name == workerSearch).Include(x=>x.WorkGroup).FirstOrDefault();
		var workGroupList = workGroup.Workers;
		worker.BelongstoWorkGroups = "";
		worker.WorkGroup.Remove(workGroup);
		await DisplayAlert("Alert", workerSearch + " ya no pertenece a " + WorkGroupSearch, "Vale");
		foreach (WorkGroup workgroup in worker.WorkGroup)
		{
			worker.BelongstoWorkGroups += workgroup.Name;
			
			var workerList = p.Workers.Where(x => x.WorkGroup.Contains(workgroup)).ToList();
			
			foreach (Worker w in workerList)
			{
				WorkerSelector.Items.Add(w.Name);
			}
		}
		p.SaveChanges();
	}
}