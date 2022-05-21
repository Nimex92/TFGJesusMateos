using ClassLibray;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AltaTrabajador : ContentPage
{
	PresenciaContext p = new PresenciaContext();
	string Username;
	DateTime dt = DateTime.Now;
	Db db = new Db();
	public AltaTrabajador(string user,int option)
	{
		InitializeComponent();
		Username = user;
		//Recojo todos los Turnos de la tabla de MySql
		var WorkShift = p.WorkGroups;
		//Creo una lista para guardar todos los turnos existentes
		var WorkShiftList = new List<string>();
		//Para cada lista que haya en la seleccion WorkShifts, añado al selector (Picker de la interfaz) El name del turno
		WorkGroupSelector.Items.Add("-- Selecciona un equipo de trabajo.");
		foreach (WorkGroup equipo in WorkShift)
        {
			WorkGroupSelector.Items.Add(equipo.Name);
		}
		List<string> Category = new List<string>();
		Category.Add("Ingeniero licenciado");
		Category.Add("Ingeniero tecnico");
		Category.Add("Jefe administrativo");
		Category.Add("Ayudante no titulado");
		Category.Add("Oficial administrativo");
		Category.Add("Subalterno");
		Category.Add("Auxiliar Administrativo");
		Category.Add("Oficial de 1ºera");
		Category.Add("Oficial de 2ºda");
		Category.Add("Oficial de 3ºera");
		Category.Add("Oficial especialista");
		Category.Add("Peon");
		Category.Add("Menor de edad o independiente");
		CategorySelector.ItemsSource = Category;
		//Selecciono el primer item de la lista a modo informatico para el Usuario
		CategorySelector.SelectedIndex = 0;
		//Segun el entero que recibe actvia la insercion o el modificado (interfaz)
		switch(option)
		{
			//Activa el boton para registrar un nuevo trabajador
			case 0:
				UpdateButton.IsVisible = false;
				RegisterButton.IsVisible = true;
				break;
			//Activa el boton para actualizar un trabajador existente
			case 1:
				UpdateButton.IsVisible = true;
				RegisterButton.IsVisible = false;
				WorkGroupSelector.IsEnabled = false;
				NameField.Text = user;
				var worker = p.Workers.Where(x => x.Name == user).Include(x=>x.User).Include(x => x.WorkGroup).FirstOrDefault();
				//Recorro los equipos del trabajador y los añado a la lista para setearlos en el Picker.
				foreach(WorkGroup e in worker.WorkGroup)
                {
					WorkShiftList.Add(e.Name);
                }
				WorkGroupSelector.SelectedIndex = 1;
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
			Random randomNumber = new Random();
			string name = NameField.Text;
			string user = name + randomNumber.Next(0, 9) + randomNumber.Next(0, 9) + randomNumber.Next(0, 9) + randomNumber.Next(0, 9);
			var selected = WorkGroupSelector.SelectedItem.ToString();
			var workGroup = p.WorkGroups.Where(x => x.Name == selected).FirstOrDefault();
			Worker workers = new Worker(name, workGroup, new User(user, "1"));
			workers.BelongstoWorkGroups = workGroup.Name;
			bool inserta = db.InsertWorker(workers,p);
            try {workers.BelongstoWorkGroups = workers.Name; } catch (NullReferenceException ex) { Debug.WriteLine(ex.StackTrace); }

			db.InsertLog(new Log("Añadir", Username + " ha aÃ±adido trabajador " + name + " - " + dt), p);
			if (inserta == true)
			{
				await DisplayAlert("Alert", "Se ha insertado correctamente el trabajador " + name, "OK");
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 2));
			}
			else
			{
				await DisplayAlert("Alert", "Error al insertar el trabajador " + workers.Name, "OK");
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
		string nombre = NameField.Text;
		var user = p.Workers.Where(x => x.Name == Username).Include(x => x.User).Include(x => x.WorkGroup).FirstOrDefault();
		var seleccionado = WorkGroupSelector.SelectedItem.ToString().Trim();
		var equipo = p.WorkGroups.Where(x => x.Name == seleccionado).FirstOrDefault();
		bool actualiza = db.UpdateWorker(user,nombre,equipo);
		
		if (actualiza == true)
		{
			db.InsertLog(new Log("AÃ±adir", Username + " ha modificado trabajador " + nombre + " - " + dt), p);
			await DisplayAlert("Alert", "Se ha modificado correctamente el trabajador " + nombre, "OK");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 2));
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
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,2));
	}


}