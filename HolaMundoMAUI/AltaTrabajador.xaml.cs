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
		//Para cada lista que haya en la seleccion WorkShifts, a√±ado al selector (Picker de la interfaz) El nombre del turno
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
		Categorias.Add("Oficial de 1∫era");
		Categorias.Add("Oficial de 2∫da");
		Categorias.Add("Oficial de 3∫era");
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
				var trab = presenciaContext.Workers.Where(x => x.Name == user).Include(x=>x.User).Include(x => x.WorkGroup).FirstOrDefault();
				//Recorro los equipos del trabajador y los a√±ado a la lista para setearlos en el Picker.
				foreach(WorkGroup e in trab.WorkGroup)
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
			var equipo = presenciaContext.WorkGroups.Where(x => x.Name == seleccionado).FirstOrDefault();
			Worker t = new Worker(nombre, equipo, new User(user, "1"));
			t.BelongsToWorkShifts = equipo.Name;
			bool inserta = DbInsert.InsertWorker(t,presenciaContext);
            try {t.BelongsToWorkShifts = equipo.Name; } catch (NullReferenceException ex) { Debug.WriteLine(ex.StackTrace); }

			DbInsert.InsertLog(new Log("AÒadir", NombreUsuario + " ha a√±adido trabajador " + nombre + " - " + dt), presenciaContext);
			if (inserta == true)
			{
				await DisplayAlert("Alert", "Se ha insertado correctamente el trabajador " + nombre, "OK");
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 2));
			}
			else
			{
				await DisplayAlert("Alert", "Error al insertar el trabajador " + t.Name, "OK");
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
		var user = presenciaContext.Workers.Where(x => x.Name == NombreUsuario).Include(x => x.User).Include(x => x.WorkGroup).FirstOrDefault();
		var seleccionado = Selector.SelectedItem.ToString().Trim();
		var equipo = presenciaContext.WorkGroups.Where(x => x.Name == seleccionado).FirstOrDefault();
		bool actualiza = DbUpdate.UpdateWorker(user,nombre,equipo);
		
		if (actualiza == true)
		{
			DbInsert.InsertLog(new Log("A√±adir", NombreUsuario + " ha modificado trabajador " + nombre + " - " + dt), presenciaContext);
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