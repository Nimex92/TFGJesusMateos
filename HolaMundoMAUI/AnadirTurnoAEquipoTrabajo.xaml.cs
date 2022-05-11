using Persistencia;
using Bibliotec;
namespace HolaMundoMAUI;


public partial class AnadirTurnoEquipoTrabajo : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	string NombreUsuario;
	Turno turne;
	DateTime dt = DateTime.Now;
	public AnadirTurnoEquipoTrabajo(string user,string turno,int actualiza)
	{
		InitializeComponent();
		NombreUsuario = user;
		var t = presenciaContext.Turno.Where(x => x.Nombre.Equals(turno)).FirstOrDefault();
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
		var equipos = presenciaContext.EquipoTrabajo;
		var ListaEquipo = presenciaContext.EquipoTrabajo.Where(x => x.Turnos.Contains(turne)).ToList();

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaEquipos = new List<string>();
		var ListaTareas = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno

		
		SelectorGruposTrabajo.Items.Add("-- Selecciona Grupo de trabajo.");
		SelectorTurno.Items.Add(turne.Nombre);
		foreach (EquipoTrabajo equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Nombre);
		}
		foreach (EquipoTrabajo equipo in ListaEquipo)
		{
			SelectorGruposTrabajo.Items.Remove(equipo.Nombre);
		}
		SelectorGruposTrabajo.SelectedIndex = 0;
		SelectorTurno.SelectedIndex = 0;

	}
	private void SetPickers1()
	{
		var equipos = presenciaContext.EquipoTrabajo;
		var ListaEquipo = presenciaContext.EquipoTrabajo.Where(x => x.Turnos.Contains(turne)).ToList();

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaEquipos = new List<string>();
		var ListaTareas = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno


		SelectorGruposTrabajo.Items.Add("-- Selecciona Grupo de trabajo.");
		SelectorTurno.Items.Add(turne.Nombre);
		foreach (EquipoTrabajo equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Remove(equipo.Nombre);
		}
		foreach (EquipoTrabajo equipo in ListaEquipo)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Nombre);
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
		var equipo = presenciaContext.EquipoTrabajo.Where(x => x.Nombre.Equals(equipoTrabajo)).FirstOrDefault();
		var turno = presenciaContext.Turno.Where(x => x.Nombre.Equals(NombreTurno)).FirstOrDefault();
		var ListaTurnos = equipo.Turnos;
		var ListaEquipos = turno.EquiposDeTrabajo;

		if (equipo is not null && turno is not null)
		{
			
            if (ListaEquipos.Contains(equipo))
			{
				await DisplayAlert("Alert", "El turno ya esta asignado a este equipo de trabajo", "Vale");
            }
            else
            {
				turno.EquiposDeTrabajo.Add(equipo);
				presenciaContext.SaveChanges();
				await DisplayAlert("Alert", "Se ha añadido '" + turno.Nombre + "' a grupo: " + equipo.Nombre, "OK");
				presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido " + equipo.Zonas + " a " + equipo.Nombre + " - " + dt));
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
		var equipo = presenciaContext.EquipoTrabajo.Where(x => x.Nombre.Equals(equipoTrabajo)).FirstOrDefault();
		var turno = presenciaContext.Turno.Where(x => x.Nombre.Equals(NombreTurno)).FirstOrDefault();
		equipo.Turnos.Remove(turno);
		presenciaContext.SaveChanges();
		await DisplayAlert("Alert", "Se ha Eliminado '" + turno.Nombre + "' del grupo: " + equipo.Nombre, "OK");
		presenciaContext.Logs.Add(new Log("Remover", NombreUsuario + " ha eliminado " + equipo.Zonas + " a " + equipo.Nombre + " - " + dt));
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 3));
		}

}
