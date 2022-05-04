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
		var Turno = presenciaContext.EquipoTrabajo;
		//Creo una lista para guardar todos los turnos existentes
		var listaTurnos = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno
		Selector.Items.Add("-- Selecciona un equipo de trabajo.");
		foreach (EquipoTrabajo equipo in Turno)
        {
			Selector.Items.Add(equipo.Nombre);
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
				var trab = presenciaContext.Trabajador.Where(x => x.nombre == user).Include(x => x.equipo).FirstOrDefault();
				
				foreach(EquipoTrabajo e in trab.equipo)
                {
					listaTurnos.Add(e.Nombre);
                }
				//Selector.SelectedItem = trab.equipo.; 
				break;
        }
		
	}

	public async void RegistrarNuevoTrabajador(object sender, EventArgs e)
	{
		Random r = new Random();
		Trabajador t = new Trabajador();
		string nombre = CampoNombre.Text;
		string user = nombre+r.Next(0,9)+r.Next(0, 9)+r.Next(0, 9)+r.Next(0, 9);
		var seleccionado = Selector.SelectedItem.ToString();
		var equipo = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == seleccionado).FirstOrDefault();
		t.nombre = nombre;
		t.usuario = new Usuarios(user, "1"); 
		t.equipo.Add(equipo);
		t.perteneceaturnos = equipo.Nombre;
		bool inserta = OperacionesDBContext.InsertaTrabajador(t);
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
		var user = presenciaContext.Trabajador.Where(x => x.usuario.Username == NombreUsuario).Include(x => x.usuario).Include(x => x.equipo).FirstOrDefault();
		var seleccionado = Selector.SelectedItem.ToString().Trim();
		var equipo = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == seleccionado).FirstOrDefault();
		bool actualiza = OperacionesDBContext.actualizaTrabajador(user.numero_tarjeta, nombre, equipo.Id);
		presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha modificado trabajador " + nombre + " - " + dt));
		presenciaContext.SaveChanges();
		if (actualiza == true)
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