using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AnadeTrabajadorEquipoTrabajo : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	string nombreUsuario;
	public AnadeTrabajadorEquipoTrabajo(string user)
	{
		nombreUsuario = user;
		InitializeComponent();
		SetPickers();
	}

	private void SetPickers()
	{
		var equipos = presenciaContext.EquipoTrabajo;
		var trabajadores = presenciaContext.Trabajador;

		//Recojo todos los Turnos de la tabla de MySql
		//Creo una lista para guardar todos los turnos existentes
		var ListaGrupos = new List<string>();
		var ListaTrabajador = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno
		SelectorGruposTrabajo.Items.Add("-- Selecciona equipo de trabajo.");
		SelectorTareas.Items.Add("-- Selecciona Trabajador.");
		foreach (EquipoTrabajo equipo in equipos)
		{
			SelectorGruposTrabajo.Items.Add(equipo.Nombre);
		}
		foreach (Trabajador t in trabajadores)
		{
			SelectorTareas.Items.Add(t.nombre);
		}
		SelectorGruposTrabajo.SelectedIndex = 0;
		SelectorTareas.SelectedIndex = 0;
	}

    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 3));
    }

    private async void BotonRegistrar_Clicked(object sender, EventArgs e)
    {
		string BuscaEquipo = SelectorGruposTrabajo.SelectedItem.ToString();
		string BuscaTrabajador = SelectorTareas.SelectedItem.ToString();
		var EquipoTrabajo = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == BuscaEquipo).FirstOrDefault();
		var Trabajador = presenciaContext.Trabajador.Where(x => x.nombre == BuscaTrabajador).FirstOrDefault();
		var Trabajadores = EquipoTrabajo.Trabajadores;
		foreach(Trabajador t in Trabajadores)
        {
			Debug.WriteLine(t.nombre);
        }
		
	}
}