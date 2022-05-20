using ClassLibray;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class BorraTrabajadorDeGrupo : ContentPage
{
	PresenciaContext p = new PresenciaContext();
	string nombreUsuario;
	WorkGroup workGroup;
	public BorraTrabajadorDeGrupo(string user,WorkGroup eq)
	{
		nombreUsuario = user;
		workGroup = eq;
		InitializeComponent();
		SetPickers();
	}

	private void SetPickers()
	{
		var equipos = p.WorkGroups;
		var trabajadores = p.Workers.Where(x=>x.WorkGroup.Contains(workGroup)).ToList();
		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaGrupos = new List<string>();
		var ListaTrabajador = new List<string>();
		//Para cada lista que haya en la seleccion WorkShifts, añado al selector (Picker de la interfaz) El nombre del turno
		SelectorTareas.Items.Add("-- Selecciona Trabajador.");
		
		SelectorGruposTrabajo.Items.Add(workGroup.Name);
		
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
		Trabajador.BelongsToWorkShifts = "";

		Trabajador.WorkGroup.Remove(EquipoTrabajo);
		await DisplayAlert("Alert", BuscaTrabajador + " ya no pertenece a " + BuscaEquipo, "Vale");
		foreach (WorkGroup eq in Trabajador.WorkGroup)
		{
			Trabajador.BelongsToWorkShifts += eq.Name;
			
			var trabajadores = p.Workers.Where(x => x.WorkGroup.Contains(workGroup)).ToList();
			
			foreach (Worker t in trabajadores)
			{
				SelectorTareas.Items.Add(t.Name);
			}
		}
		p.SaveChanges();


	}
}