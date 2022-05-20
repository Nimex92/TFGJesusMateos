using ClassLibray;
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
		var Turno = presenciaContext.WorkGroups;
		//Creo una lista para guardar todos los turnos existentes
		var listaTurnos = new List<string>();
<<<<<<< HEAD
		//Para cada lista que haya en la seleccion WorkShifts, aÃ±ado al selector (Picker de la interfaz) El nombre del turno
=======
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El Nombre del turno
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
		Selector.Items.Add("-- Selecciona un equipo de trabajo.");
		foreach (WorkGroup equipo in Turno)
        {
			Selector.Items.Add(equipo.Name);
		}
		List<string> Categorias = new List<string>();
		Categorias.Add("Ingeniero licenciado");
		Categorias.Add("Ingeniero tecnico");
		Categorias.Add("Jefe administrativo");
		Categorias.Add("Ayudante no titulado");
		Categorias.Add("Oficial administrativo");
		Categorias.Add("Subalterno");
		Categorias.Add("Auxiliar Administrativo");
		Categorias.Add("Oficial de 1ºera");
		Categorias.Add("Oficial de 2ºda");
		Categorias.Add("Oficial de 3ºera");
		Categorias.Add("Oficial especialista");
		Categorias.Add("Peon");
		Categorias.Add("Menor de edad o independiente");
		SelectorCategoria.ItemsSource = Categorias;
		//Selecciono el primer item de la lista a modo informatico para el Usuario
		Selector.SelectedIndex = 0;
		//Segun el entero que recibe actvia la insercion o el modificado (interfaz)
		switch(actualiza)
		{
			//Activa el boton para registrar un nuevo trabajador
			case 0:
				BotonActualizarAdmin.IsVisible = false;
				BotonActualizarAdmin.IsEnabled = false;
				BotonRegistrarAdmin.IsVisible = true;
				BotonRegistrarAdmin.IsEnabled = true;
				break;
			//Activa el boton para actualizar un trabajador existente
			case 1:
				BotonActualizarAdmin.IsVisible = true;
				BotonActualizarAdmin.IsEnabled = true;
				BotonRegistrarAdmin.IsVisible = false;
				BotonRegistrarAdmin.IsEnabled = false;
				Selector.IsEnabled = false;
				CampoNombre.Text = user;
<<<<<<< HEAD
				var trab = presenciaContext.Workers.Where(x => x.Name == user).Include(x=>x.User).Include(x => x.WorkGroup).FirstOrDefault();
				//Recorro los equipos del trabajador y los aÃ±ado a la lista para setearlos en el Picker.
				foreach(WorkGroup e in trab.WorkGroup)
=======
				var trab = presenciaContext.Trabajador.Where(x => x.Nombre == user).Include(x=>x.Usuario).Include(x => x.Equipo).FirstOrDefault();
				//Recorro los equipos del trabajador y los añado a la lista para setearlos en el Picker.
				foreach(EquipoTrabajo e in trab.Equipo)
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
                {
					listaTurnos.Add(e.Name);
                }
				Selector.SelectedIndex = 1;
				break;
        }
		
	}
	/// <summary>
	/// Accion reactiva al pulsar el boton Registrar (Cuando esta activo)
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public async void RegistrarNuevoTrabajador(object sender, EventArgs e)
	{
		try
		{
			Random r = new Random();
			string nombre = CampoNombre.Text;
			string user = nombre + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9);
			var seleccionado = Selector.SelectedItem.ToString();
<<<<<<< HEAD
			var equipo = presenciaContext.WorkGroups.Where(x => x.Name == seleccionado).FirstOrDefault();
			Worker t = new Worker(nombre, equipo, new User(user, "1"));
			t.BelongsToWorkShifts = equipo.Name;
			bool inserta = DbInsert.InsertWorker(t,presenciaContext);
            try {t.BelongsToWorkShifts = equipo.Name; } catch (NullReferenceException ex) { Debug.WriteLine(ex.StackTrace); }

			DbInsert.InsertLog(new Log("AÃ±adir", NombreUsuario + " ha aÃ±adido trabajador " + nombre + " - " + dt), presenciaContext);
=======
			var equipo = presenciaContext.EquipoTrabajo.Where(x => x.Nombre == seleccionado).FirstOrDefault();
			var dni = CampoDni.Text;
			var numeroSeguridadSocial = CampoNumeroSS.Text;
			var categoria = SelectorCategoria.SelectedItem.ToString();
			Trabajador t = new Trabajador(nombre, equipo, new Usuarios(user, "1"),dni,numeroSeguridadSocial,categoria);
			t.PerteneceATurnos = equipo.Nombre;
			bool inserta = OperacionesDBContext.InsertaTrabajador(t);
            try {t.PerteneceATurnos = equipo.Nombre; } catch (NullReferenceException ex) { Debug.WriteLine(ex.StackTrace); }
			
			OperacionesDBContext.InsertaLog(new Log("Añadir", NombreUsuario + " ha añadido trabajador " + nombre + " - " + dt));
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
			if (inserta == true)
			{
				await DisplayAlert("Alert", "Se ha insertado correctamente el trabajador " + nombre, "OK");
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 2));
			}
			else
			{
<<<<<<< HEAD
				await DisplayAlert("Alert", "Error al insertar el trabajador " + t.Name, "OK");
=======
				await DisplayAlert("Alert", "Error al insertar el trabajador " + t.Nombre, "OK");
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be

			}
        }
        catch (Exception ex)
        {
			Debug.WriteLine(ex.StackTrace);
        }
	}
	/// <summary>
	/// Accion reactiva al pulsar el boton Actualziar (Cuando esta activo)
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private async void BotonActualizarAdmin_Clicked(object sender, EventArgs e)
	{
		Random r = new Random();
		string nombre = CampoNombre.Text;
<<<<<<< HEAD
		var user = presenciaContext.Workers.Where(x => x.Name == NombreUsuario).Include(x => x.User).Include(x => x.WorkGroup).FirstOrDefault();
=======
		var user = presenciaContext.Trabajador.Where(x => x.Nombre == NombreUsuario).Include(x => x.Usuario).Include(x => x.Equipo).FirstOrDefault();
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
		var seleccionado = Selector.SelectedItem.ToString().Trim();
		var equipo = presenciaContext.WorkGroups.Where(x => x.Name == seleccionado).FirstOrDefault();
		bool actualiza = DbUpdate.UpdateWorker(user,nombre,equipo);
		
		if (actualiza == true)
		{
			DbInsert.InsertLog(new Log("AÃ±adir", NombreUsuario + " ha modificado trabajador " + nombre + " - " + dt), presenciaContext);
			await DisplayAlert("Alert", "Se ha modificado correctamente el trabajador " + nombre, "OK");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 2));
		}
		else
		{
			await DisplayAlert("Alert", "Error al actualizar el trabajador " + nombre, "OK");
		}
	}
	/// <summary>
	/// Accion reactiva al boton volver que nos traslada de vuelta a la zona de 
	/// admin
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,2));
	}


}