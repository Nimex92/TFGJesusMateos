using Persistencia;
using ClassLibray;
namespace HolaMundoMAUI;


public partial class AnadirTurnoEquipoTrabajo : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	string NombreUsuario;
	WorkShift turne;
	DateTime dt = DateTime.Now;
	public AnadirTurnoEquipoTrabajo(string user,string turno,int actualiza)
	{
		InitializeComponent();
		NombreUsuario = user;
		var t = presenciaContext.WorkShifts.Where(x => x.Name.Equals(turno)).FirstOrDefault();
		turne = t;
        switch (actualiza)
        {
			case 0:
				BotonRegistrar.IsVisible = true;
				BotonRegistrar.IsEnabled = true;
				BotonActualizar.IsVisible = false;
				BotonActualizar.IsEnabled = false;
				SetPickers0();
				break;
			case 1:
				BotonRegistrar.IsVisible = false;
				BotonRegistrar.IsEnabled = false;
				BotonActualizar.IsVisible = true;
				BotonActualizar.IsEnabled = true;
				SetPickers1();
				LabelTitulo.Text = "Tech Talent" + Environment.NewLine + "Eliminar Turno de grupo de trabajo";
				break;
        }
		
	}
	private void SetPickers0()
	{
		var equipos = presenciaContext.WorkGroups;
		var ListaEquipo = presenciaContext.WorkGroups.Where(x => x.WorkShifts.Contains(turne)).ToList();

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
		SelectorTurno.Items.Add(turne.Name);
		foreach (WorkGroup equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Name);
		}
		foreach (WorkGroup equipo in ListaEquipo)
		{
			SelectorGruposTrabajo.Items.Remove(equipo.Name);
		}
		SelectorGruposTrabajo.SelectedIndex = 0;
		SelectorTurno.SelectedIndex = 0;

	}
	private void SetPickers1()
	{
		var equipos = presenciaContext.WorkGroups;
		var ListaEquipo = presenciaContext.WorkGroups.Where(x => x.WorkShifts.Contains(turne)).ToList();

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
		SelectorTurno.Items.Add(turne.Name);
		foreach (WorkGroup equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Remove(equipo.Name);
		}
		foreach (WorkGroup equipo in ListaEquipo)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Name);
		}
		SelectorGruposTrabajo.SelectedIndex = 0;
		SelectorTurno.SelectedIndex = 0;

	}
	private void BotonVolver_Clicked(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,3));
	}

	private async void BotonRegistrar_Clicked(object sender, EventArgs e)
	{
		var equipoTrabajo = SelectorGruposTrabajo.SelectedItem;
		var NombreTurno = SelectorTurno.SelectedItem;
		var equipo = presenciaContext.WorkGroups.Where(x => x.Name.Equals(equipoTrabajo)).FirstOrDefault();
		var turno = presenciaContext.WorkShifts.Where(x => x.Name.Equals(NombreTurno)).FirstOrDefault();
		var ListaTurnos = equipo.WorkShifts;
		var ListaEquipos = turno.WorkShifts;

		if (equipo is not null && turno is not null)
		{
			
            if (ListaEquipos.Contains(equipo))
			{
				await DisplayAlert("Alert", "El turno ya esta asignado a este equipo de trabajo", "Vale");
            }
            else
            {
				turno.WorkShifts.Add(equipo);
				presenciaContext.SaveChanges();
				await DisplayAlert("Alert", "Se ha añadido '" + turno.Name + "' a grupo: " + equipo.Name, "OK");
				presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido " + equipo.Places + " a " + equipo.Name + " - " + dt));
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 3));
			}
		}
		else
		{
			await DisplayAlert("Alert", "Error al añadir turno a equipo de trabajo.", "Vale");
		}

	}
    private async void BotonActualizar_Clicked(object sender, EventArgs e)
    {
		var equipoTrabajo = SelectorGruposTrabajo.SelectedItem.ToString();
		var NombreTurno = SelectorTurno.SelectedItem.ToString();
		var equipo = presenciaContext.WorkGroups.Where(x => x.Name.Equals(equipoTrabajo)).FirstOrDefault();
		var turno = presenciaContext.WorkShifts.Where(x => x.Name.Equals(NombreTurno)).FirstOrDefault();
		equipo.WorkShifts.Remove(turno);
		presenciaContext.SaveChanges();
		await DisplayAlert("Alert", "Se ha Eliminado '" + turno.Name + "' del grupo: " + equipo.Name, "OK");
		presenciaContext.Logs.Add(new Log("Remover", NombreUsuario + " ha eliminado " + equipo.Places + " a " + equipo.Name + " - " + dt));
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 3));
		}

}
