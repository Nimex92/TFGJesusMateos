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
<<<<<<< HEAD
		//Para cada lista que haya en la seleccion WorkShifts, aÃ±ado al selector (Picker de la interfaz) El nombre del turno
=======
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El Nombre del turno
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
		SelectorGruposTrabajo.Items.Add("-- Selecciona equipo de trabajo.");
		SelectorTareas.Items.Add("-- Selecciona Trabajador.");
		foreach (WorkGroup equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Name);
		}
		foreach (Worker t in trabajadores)
		{
<<<<<<< HEAD
			SelectorTareas.Items.Add(t.Name);
=======
			SelectorTareas.Items.Add(t.Nombre);
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
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
<<<<<<< HEAD
		var EquipoTrabajo = p.WorkGroups.Where(x => x.Name == BuscaEquipo).Include(x=>x.Workers).FirstOrDefault();
		var Trabajador = p.Workers.Where(x => x.Name == BuscaTrabajador).Include(x=>x.WorkGroup).FirstOrDefault();
		var Equipos = EquipoTrabajo.Workers;
=======
		var EquipoTrabajo = p.EquipoTrabajo.Where(x => x.Nombre == BuscaEquipo).Include(x=>x.Trabajadores).FirstOrDefault();
		var Trabajador = p.Trabajador.Where(x => x.Nombre == BuscaTrabajador).Include(x=>x.Equipo).FirstOrDefault();
		var Equipos = EquipoTrabajo.Trabajadores;
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
            
		if (Equipos.Contains(Trabajador))
            {
				await DisplayAlert("Alert", "El trabajador ya esta asignado", "Vale");
            }
        else
            {
<<<<<<< HEAD
				EquipoTrabajo.Workers.Add(Trabajador);
				Trabajador.BelongsToWorkShifts += " - "+BuscaEquipo;
				p.SaveChanges();
				await DisplayAlert("Alert", "Se ha asignado correctamente " + Trabajador.Name + " a " + EquipoTrabajo.Name, "Vale");
=======
				EquipoTrabajo.Trabajadores.Add(Trabajador);
				Trabajador.PerteneceATurnos += " - "+BuscaEquipo;
				p.SaveChanges();
				await DisplayAlert("Alert", "Se ha asignado correctamente " + Trabajador.Nombre + " a " + EquipoTrabajo.Nombre, "Vale");
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
            }
        

	}
}