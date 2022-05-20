using ClassLibray;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace HolaMundoMAUI;

public partial class AnadeTrabajadorEquipoTrabajo : ContentPage
{
	PresenciaContext p = new PresenciaContext();
	string nombreUsuario;
	public AnadeTrabajadorEquipoTrabajo(string user)
	{
		nombreUsuario = user;
		InitializeComponent();
		SetPickers();
	}

	private void SetPickers()
	{
		var equipos = p.WorkGroups;
		var trabajadores = p.Workers;

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaGrupos = new List<string>();
		var ListaTrabajador = new List<string>();
		//Para cada lista que haya en la seleccion WorkShifts, añado al selector (Picker de la interfaz) El nombre del turno
		SelectorGruposTrabajo.Items.Add("-- Selecciona equipo de trabajo.");
		SelectorTareas.Items.Add("-- Selecciona Trabajador.");
		foreach (WorkGroup equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Name);
		}
		foreach (Worker t in trabajadores)
		{
			SelectorTareas.Items.Add(t.Name);
		}
		SelectorGruposTrabajo.SelectedIndex = 0;
		SelectorTareas.SelectedIndex = 0;
	}

    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 6));
    }

    private async void BotonRegistrar_Clicked(object sender, EventArgs e)
    {
		string BuscaEquipo = SelectorGruposTrabajo.SelectedItem.ToString();
		string BuscaTrabajador = SelectorTareas.SelectedItem.ToString();
		var EquipoTrabajo = p.WorkGroups.Where(x => x.Name == BuscaEquipo).Include(x=>x.Workers).FirstOrDefault();
		var Trabajador = p.Workers.Where(x => x.Name == BuscaTrabajador).Include(x=>x.WorkGroup).FirstOrDefault();
		var Equipos = EquipoTrabajo.Workers;
            
		if (Equipos.Contains(Trabajador))
        {
			await DisplayAlert("Alert", "El trabajador ya esta asignado", "Vale");
        }
        else
        {
			EquipoTrabajo.Workers.Add(Trabajador);
			Trabajador.BelongsToWorkShifts += " - "+BuscaEquipo;
			p.SaveChanges();
			await DisplayAlert("Alert", "Se ha asignado correctamente " + Trabajador.Name + " a " + EquipoTrabajo.Name, "Vale");
        }
	}
}