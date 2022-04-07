using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AltaTrabajador : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	public AltaTrabajador()
	{
		InitializeComponent();
		
		//Recojo todos los Turnos de la tabla de MySql
		var Turno = presenciaContext.Grupo_Trabajo;
		//Creo una lista para guardar todos los turnos existentes
		var listaTurnos = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno
		foreach (Grupo_Trabajo grupo in Turno)
        {
			Selector.Items.Add(grupo.Turno);
		}
		
	}

	public void RegistrarNuevoTrabajador(object sender, EventArgs e)
	{
		Random r = new Random();
		string nombre = CampoNombre.Text;
		string user = nombre+r.Next(0,9)+r.Next(0, 9)+r.Next(0, 9)+r.Next(0, 9);
		var seleccionado = Selector.SelectedItem.ToString().Trim();
		var grupo = presenciaContext.Grupo_Trabajo.Where(x => x.Turno == seleccionado).FirstOrDefault();
		OperacionesDBContext.insertaTrabajador(nombre,grupo.IdGrupo,user);
	
		
		
	}   

	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}
}