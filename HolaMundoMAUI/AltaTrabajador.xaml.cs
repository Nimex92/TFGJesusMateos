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
	
	/// <summary>
	/// Constructor that receives the username to persist the data
	/// </summary>
	/// <param string="user"></param>
	/// <param int="option"></param>
	public AltaTrabajador(string user,int option)
	{
		InitializeComponent();
		//Set the user variable received to an global local use
		Username = user;
		//Get all the workShifts than exists on Db and add it to a List
		var WorkShift = p.WorkGroups.ToList();
		//for each workgroup than exist on workshifs we add it to the selector
		WorkGroupSelector.Items.Add("-- Selecciona un equipo de trabajo.");
        WorkShift.ForEach(x =>
        {
			WorkGroupSelector.Items.Add(x.Name);
		});
        //Fill a List with all static category
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
		//Select the first exists item (for UI better experience)
		CategorySelector.SelectedIndex = 0;
		switch(option)
		{
			//Enable the create UI
			case 0:
				//Set the create button visible
				UpdateButton.IsVisible = false;
				RegisterButton.IsVisible = true;
				break;
			//Enable the update UI
			case 1:
				//Set the update button visible
				UpdateButton.IsVisible = true;
				RegisterButton.IsVisible = false;
				WorkGroupSelector.IsEnabled = false;
				NameField.Text = user;
				//Find a worker than march with the user than constructor receives
				var worker = p.Workers.Where(x => x.Name == user).Include(x=>x.User).Include(x => x.WorkGroup).FirstOrDefault();
				//foreach Workgroup than worker belongs it will be added to the List
				foreach(WorkGroup e in worker.WorkGroup)
                {
					WorkShiftList.Add(e.Name);
                }
				//Select the first item of the list
				WorkGroupSelector.SelectedIndex = 1;
				break;
        }
		
	}
	/// <summary>
	/// Method to add a new Worker to Db
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
	public async void AddWorkerButton_Clicked(object sender, EventArgs e)
	{
		try
		{
			Random randomNumber = new Random();
			string name = NameField.Text;
			string user = name + randomNumber.Next(0, 9) + randomNumber.Next(0, 9) + randomNumber.Next(0, 9) + randomNumber.Next(0, 9);
			var selected = WorkGroupSelector.SelectedItem.ToString();
			var workGroup = p.WorkGroups.Where(x => x.Name == selected).FirstOrDefault();
			var Nif = NifField.Text;
			var SSNumber = SSNumberField.Text;
			var category = CategorySelector.SelectedItem.ToString();
			var inserta = db.InsertWorker(new Worker(name, workGroup, new User(user, "1"),category,DateTime.Now,Nif,SSNumber),p);
			//p.Workers.Add(new Worker(name, workGroup, new User(user, "1"),CategorySelector.SelectedItem.ToString(),DateTime.Now,Nif,SSNumber));
			//p.SaveChanges();
			var workers = p.Workers.Where(x => x.Name == name).FirstOrDefault();
			try {workers.BelongstoWorkGroups = workers.WorkGroup.ToString(); } catch (NullReferenceException ex) { Debug.WriteLine(ex.StackTrace); }
			db.InsertLog(new Log("A�adir", Username + " ha a�adido trabajador " + name + " - " + dt), p);
			if (inserta == true)
			{
				await DisplayAlert("Success", "Se ha insertado correctamente el trabajador " + name, "OK");
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
	/// Method to update an exist Worker on Db
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
	private async void UpdateWorkerButton_Clicked(object sender, EventArgs e)
	{
		Random random = new Random();
		string name = NameField.Text;
		string user = name + randomNumber.Next(0, 9) + randomNumber.Next(0, 9) + randomNumber.Next(0, 9) + randomNumber.Next(0, 9);
		var selected = WorkGroupSelector.SelectedItem.ToString();
		var workGroup = p.WorkGroups.Where(x => x.Name == selected).FirstOrDefault();
		var Nif = NifField.Text;
		var SSNumber = SSNumberField.Text;
		var category = CategorySelector.SelectedItem.ToString();
		bool update = db.UpdateWorker(user,name,workGroup);
		
		if (update == true)
		{
			db.InsertLog(new Log("Modificar", Username + " ha modificado trabajador " + name + " - " + dt), p);
			await DisplayAlert("Modify", "Se ha modificado correctamente el trabajador " + name, "OK");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 2));
		}
		else
		{
			await DisplayAlert("Error", "Error al actualizar el trabajador " + name, "OK");
		}
	}
	/// <summary>
	/// Method to back to the MainScreen of the app
	/// admin
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,2));
	}


}