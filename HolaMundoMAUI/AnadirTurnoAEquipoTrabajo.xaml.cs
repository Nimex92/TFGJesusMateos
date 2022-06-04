using Persistencia;
using ClassLibray;
namespace HolaMundoMAUI;


public partial class AnadirTurnoEquipoTrabajo : ContentPage
{
	PresenciaContext p = new PresenciaContext();
	string Username;
	WorkShift WorkShift;
	DateTime now = DateTime.Now;
	Db db = new Db();
	public AnadirTurnoEquipoTrabajo(string username,string workshift,int option)
	{
		InitializeComponent();
		Username = username;
		var workShift = p.WorkShifts.Where(x => x.Name.Equals(workshift)).FirstOrDefault();
		WorkShift = workShift;
        switch (option)
        {
	        //In this case enable the create UI
			case 0:
				RegisterButton.IsVisible = true;
				UpdateButton.IsVisible = false;
				SetPickers0();
				break;
			//In this case enable the update UI
			case 1:
				RegisterButton.IsVisible = false;
				UpdateButton.IsVisible = true;
				SetPickers1();
				LabelTitulo.Text = "Eliminar Turno de grupo de trabajo";
				break;
        }
		
	}
	/// <summary>
	/// Method to fill the UI Pickers
	/// </summary>
	private void SetPickers0()
	{
		var workGroups = p.WorkGroups;
		//Recojo todos los Turnos de la tabla de MySql
		var workGroupList = p.WorkGroups.Where(x => x.WorkShifts.Contains(WorkShift)).ToList();
		//Creo una lista para guardar todos los turnos existentes
		var ListaTareas = new List<string>();
		//Para cada lista que haya en la seleccion WorkGroups, añado al selector (Picker de la interfaz) El nombre del workshift	
		WorkGroupSelector.Items.Add("-- Selecciona Grupo de trabajo.");
		WorkShiftSelector.Items.Add(WorkShift.Name);
        workGroupList.ForEach(x =>
        {
			WorkGroupSelector.Items.Add(x.Name);
		});
		WorkGroupSelector.SelectedIndex = 0;
		WorkShiftSelector.SelectedIndex = 0;
	}
	/// <summary>
	/// Method to fill the UI Pickers
	/// </summary>
	private void SetPickers1()
	{
		//Recojo todos los Turnos de la tabla de MySql
		var workGroups = p.WorkGroups;
		var workGroupsList = p.WorkGroups.Where(x => x.WorkShifts.Contains(WorkShift)).ToList();
		//Para cada lista que haya en la seleccion WorkGroups, añado al selector (Picker de la interfaz) El nombre del workshift
		WorkGroupSelector.Items.Add("-- Selecciona Grupo de trabajo.");
		WorkShiftSelector.Items.Add(WorkShift.Name);
		workGroupsList.ForEach(x =>
		{
			WorkGroupSelector.Items.Remove(x.Name);
		});
		WorkGroupSelector.SelectedIndex = 0;
		WorkShiftSelector.SelectedIndex = 0;
	}
	/// <summary>
	/// Method to back to the last screen of the app
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
	private void BotonVolver_Clicked(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,3));
	}
	/// <summary>
	/// Method to create add a WorkShift to a WorkGroup
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
	private async void BotonRegistrar_Clicked(object sender, EventArgs e)
	{
		var workGroupName = WorkGroupSelector.SelectedItem;
		var workShiftName = WorkShiftSelector.SelectedItem;
		var workGroup = p.WorkGroups.Where(x => x.Name.Equals(workGroupName)).FirstOrDefault();
		var workShift = p.WorkShifts.Where(x => x.Name.Equals(workShiftName)).FirstOrDefault();
		var workShiftList = workGroup.WorkShifts.ToList();
		var workGroupList = workShift.WorkGroups;

		if (workGroupName is not null && workShift is not null)
		{
            if (workGroupList.Contains(workGroupName))
			{
				await DisplayAlert("Error", "El turno ya esta asignado a este equipo de trabajo", "Vale");
            }
            else
            {
	            db.AddWorkShiftTOWorkGroup(workGroup, workShift, p);
				await DisplayAlert("Success", "Se ha añadido '" + workShift.Name + "' a grupo: " + workGroup.Name, "OK");
				db.InsertLog(new Log("Añadir", Username + " ha añadido " + workShift.Name + " a " + workGroup.Name + " - " + now),p);
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 3));
			}
		}
		else
		{
			await DisplayAlert("Error", "Error al añadir turno a equipo de trabajo.", "Vale");
		}

	}
	/// <summary>
	/// Method to remove a workShift from a workGroup
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
    private async void BotonActualizar_Clicked(object sender, EventArgs e)
    {
		var workGroupName = WorkGroupSelector.SelectedItem.ToString();
		var workShiftName = WorkShiftSelector.SelectedItem.ToString();
		var workGroup = p.WorkGroups.Where(x => x.Name.Equals(workGroupName)).FirstOrDefault();
		var workShift = p.WorkShifts.Where(x => x.Name.Equals(workShiftName)).FirstOrDefault();
		workGroup.WorkShifts.Remove(workShift);
		p.SaveChanges();
		await DisplayAlert("Success", "Se ha Eliminado '" + workShift.Name + "' del grupo: " + workGroup.Name, "OK");
		p.Logs.Add(new Log("Remover", Username + " ha eliminado " + workGroup.Places + " a " + workGroup.Name + " - " + now));
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 3));
		}

}
