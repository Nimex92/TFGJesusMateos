using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

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
		var equipos = p.EquipoTrabajo;
		var trabajadores = p.Trabajador;

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaGrupos = new List<string>();
		var ListaTrabajador = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El Nombre del turno
		SelectorGruposTrabajo.Items.Add("-- Selecciona equipo de trabajo.");
		SelectorTareas.Items.Add("-- Selecciona Trabajador.");
		foreach (EquipoTrabajo equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Nombre);
		}
		foreach (Trabajador t in trabajadores)
		{
			SelectorTareas.Items.Add(t.Nombre);
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
		var EquipoTrabajo = p.EquipoTrabajo.Where(x => x.Nombre == BuscaEquipo).Include(x=>x.Trabajadores).FirstOrDefault();
		var Trabajador = p.Trabajador.Where(x => x.Nombre == BuscaTrabajador).Include(x=>x.Equipo).FirstOrDefault();
		var Equipos = EquipoTrabajo.Trabajadores;
            
		if (Equipos.Contains(Trabajador))
            {
				await DisplayAlert("Alert", "El trabajador ya esta asignado", "Vale");
            }
        else
            {
				EquipoTrabajo.Trabajadores.Add(Trabajador);
				Trabajador.PerteneceATurnos += " - "+BuscaEquipo;
				p.SaveChanges();
				await DisplayAlert("Alert", "Se ha asignado correctamente " + Trabajador.Nombre + " a " + EquipoTrabajo.Nombre, "Vale");
            }
        

	}
}