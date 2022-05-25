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
			case 0:
				RegisterButton.IsVisible = true;
				UpdateButton.IsVisible = false;
				SetPickers0();
				break;
			case 1:
				RegisterButton.IsVisible = false;
				UpdateButton.IsVisible = true;
				SetPickers1();
				LabelTitulo.Text = "Eliminar Turno de grupo de trabajo";
				break;
        }
		
	}
	private void SetPickers0()
	{
		var workGroups = p.WorkGroups;
		//Recojo todos los Turnos de la tabla de MySql
		var workGroupList = p.WorkGroups.Where(x => x.WorkShifts.Contains(WorkShift)).ToList();
		//Creo una lista para guardar todos los turnos existentes
		var ListaTareas = new List<string>();
		//Para cada lista que haya en la seleccion WorkShifts, añado al selector (Picker de la interfaz) El nombre del workshift	
		WorkGroupSelector.Items.Add("-- Selecciona Grupo de trabajo.");
		WorkShiftSelector.Items.Add(WorkShift.Name);
        workGroupList.ForEach(x =>
        {
			WorkGroupSelector.Items.Add(x.Name);
		});
		WorkGroupSelector.SelectedIndex = 0;
		WorkShiftSelector.SelectedIndex = 0;
	}
	private void SetPickers1()
	{
		//Recojo todos los Turnos de la tabla de MySql
		var workGroups = p.WorkGroups;
		var workGroupsList = p.WorkGroups.Where(x => x.WorkShifts.Contains(WorkShift)).ToList();
		//Para cada lista que haya en la seleccion WorkShifts, añado al selector (Picker de la interfaz) El nombre del workshift
		WorkGroupSelector.Items.Add("-- Selecciona Grupo de trabajo.");
		WorkShiftSelector.Items.Add(WorkShift.Name);
		workGroupsList.ForEach(x =>
		{
			WorkGroupSelector.Items.Remove(x.Name);
		});
		WorkGroupSelector.SelectedIndex = 0;
		WorkShiftSelector.SelectedIndex = 0;
	}
	private void BotonVolver_Clicked(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,3));
	}

	private async void BotonRegistrar_Clicked(object sender, EventArgs e)
	{
		var workGroupName = WorkGroupSelector.SelectedItem;
		var workShiftName = WorkShiftSelector.SelectedItem;
		var workGroup = p.WorkGroups.Where(x => x.Name.Equals(workGroupName)).FirstOrDefault();
		var workShift = p.WorkShifts.Where(x => x.Name.Equals(workShiftName)).FirstOrDefault();
		var workShiftList = workGroup.WorkShifts.ToList();
		var workGroupList = workShift.WorkShifts;

		if (workGroupName is not null && workShift is not null)
		{
            if (workGroupList.Contains(workGroupName))
			{
				await DisplayAlert("Error", "El turno ya esta asignado a este equipo de trabajo", "Vale");
            }
            else
            {
				//workShift.WorkShifts.Add(workGroup);
				//p.SaveChanges();
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
    private async void BotonActualizar_Clicked(object sender, EventArgs e)
    {
		var equipoTrabajo = WorkGroupSelector.SelectedItem.ToString();
		var NombreTurno = WorkShiftSelector.SelectedItem.ToString();
		var equipo = p.WorkGroups.Where(x => x.Name.Equals(equipoTrabajo)).FirstOrDefault();
		var turno = p.WorkShifts.Where(x => x.Name.Equals(NombreTurno)).FirstOrDefault();
		equipo.WorkShifts.Remove(turno);
		p.SaveChanges();
		await DisplayAlert("Alert", "Se ha Eliminado '" + turno.Name + "' del grupo: " + equipo.Name, "OK");
		p.Logs.Add(new Log("Remover", Username + " ha eliminado " + equipo.Places + " a " + equipo.Name + " - " + now));
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 3));
		}

}
