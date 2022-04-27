using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AltaTrabajador : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	string NombreUsuario;
	DateTime dt = DateTime.Now;
	public AltaTrabajador(string user,int actualiza)
	{
		InitializeComponent();
		NombreUsuario = user;
		//Recojo todos los Turnos de la tabla de MySql
		var Turno = presenciaContext.Grupo_Trabajo;
		//Creo una lista para guardar todos los turnos existentes
		var listaTurnos = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno
		Selector.Items.Add("-- Selecciona un turno de trabajo.");
		foreach (Turnos grupo in Turno)
        {
			Selector.Items.Add(grupo.Turno);
		}
		Selector.SelectedIndex = 0;
		switch(actualiza)
		{
			case 0:
				BotonActualizarAdmin.IsVisible = false;
				BotonActualizarAdmin.IsEnabled = false;
				BotonRegistrarAdmin.IsVisible = true;
				BotonRegistrarAdmin.IsEnabled = true;
				break;
			case 1:
				BotonActualizarAdmin.IsVisible = true;
				BotonActualizarAdmin.IsEnabled = true;
				BotonRegistrarAdmin.IsVisible = false;
				BotonRegistrarAdmin.IsEnabled = false;
				CampoNombre.Text = user;
				var trab = presenciaContext.Trabajador.Where(x => x.nombre == user).Include(x => x.grupo).FirstOrDefault();
				Selector.SelectedItem = trab.grupo.Turno;
				break;
        }
		
	}

	public async void RegistrarNuevoTrabajador(object sender, EventArgs e)
	{
		Random r = new Random();
		string nombre = CampoNombre.Text;
		string user = nombre+r.Next(0,9)+r.Next(0, 9)+r.Next(0, 9)+r.Next(0, 9);
		var seleccionado = Selector.SelectedItem.ToString().Trim();
		var grupo = presenciaContext.Grupo_Trabajo.Where(x => x.Turno == seleccionado).FirstOrDefault();
		bool inserta = OperacionesDBContext.insertaTrabajador(nombre,grupo.IdGrupo,user);
		presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido trabajador " + nombre + " - " + dt));
		presenciaContext.SaveChanges();
		if (inserta == true)
        {
			await DisplayAlert("Alert", "Se ha insertado correctamente el trabajador " + nombre, "OK");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 2));
		}
        else
        {
			await DisplayAlert("Alert", "Error al insertar el trabajador " + nombre, "OK");
			
		}	
	}
	private async void BotonActualizarAdmin_Clicked(object sender, EventArgs e)
	{
		Random r = new Random();
		string nombre = CampoNombre.Text;
		var user = presenciaContext.Trabajador.Where(x => x.usuario.Username == NombreUsuario).Include(x => x.usuario).Include(x => x.grupo).FirstOrDefault();
		var seleccionado = Selector.SelectedItem.ToString().Trim();
		var grupo = presenciaContext.Grupo_Trabajo.Where(x => x.Turno == seleccionado).FirstOrDefault();
		bool inserta = OperacionesDBContext.actualizaTrabajador(user.numero_tarjeta, nombre, grupo.IdGrupo);
		presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha modificado trabajador " + nombre + " - " + dt));
		presenciaContext.SaveChanges();
		if (inserta == true)
		{
			await DisplayAlert("Alert", "Se ha modificado correctamente el trabajador " + nombre, "OK");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 2));
		}
		else
		{
			await DisplayAlert("Alert", "Error al insertar el trabajador " + nombre, "OK");

		}
	}

	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,2));
	}


}