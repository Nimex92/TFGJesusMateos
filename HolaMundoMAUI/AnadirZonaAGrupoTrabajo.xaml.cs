using Persistencia;
using ClassLibray;
namespace HolaMundoMAUI;


public partial class AnadirZonaGrupoTrabajo : ContentPage
{
	PresenciaContext p = new PresenciaContext();
	string Username;
	DateTime now = DateTime.Now;
	Db db = new Db();
	public AnadirZonaGrupoTrabajo(string username)
	{
		InitializeComponent();
		SetPickers();
		Username = username;
	}
	/// <summary>
	/// Method to fill the UI pickers with data
	/// </summary>
	private void SetPickers()
	{
		//Recojo todos los Turnos de la tabla de MySql
		var workGroups = p.WorkGroups.ToList();
		var places = p.Places.ToList();
		//Para cada lista que haya en la seleccion WorkGroups, añado al selector (Picker de la interfaz) El nombre del turno
		WorkGroupSelector.Items.Add("-- Selecciona Grupo de trabajo.");
		PlaceSelector.Items.Add("-- Selecciona zona.");
        workGroups.ForEach(x =>
        {
			WorkGroupSelector.Items.Add(x.Name);
		});
        places.ForEach(x =>
        {
			PlaceSelector.Items.Add(x.Name);
        });
		WorkGroupSelector.SelectedIndex = 0;
		PlaceSelector.SelectedIndex = 0;
	}
	/// <summary>
	/// Method to back to the last screen of the app
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
	private void BackButton_Clicked(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,4));
	}
	/// <summary>
	/// Method to add a place to a WorkGroup
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
	private async void RegisterButton_Clicked (object sender, EventArgs e)
	{
		var workGroupName = WorkGroupSelector.SelectedItem;
		var placeName = PlaceSelector.SelectedItem;
		var workGroup = p.WorkGroups.Where(x => x.Name == workGroupName).FirstOrDefault();
		var place = p.Places.Where(x => x.Name == placeName).FirstOrDefault();

		if (workGroup is not null && place is not null)
		{
			db.AddPlaceToWorkGroup(workGroup, place, p);
			await DisplayAlert("Success", "Se ha añadido '" + place.Name + "' a grupo: " + workGroup.Name, "OK");
			db.InsertLog(new Log("Añadir", Username + " ha añadido "+workGroup.Places+" a " + workGroup.Name + " - " + now),p);
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 4));
		}
		else
		{
			await DisplayAlert("Error", "Error al añadir tareas.", "OK");
		}
	}
}