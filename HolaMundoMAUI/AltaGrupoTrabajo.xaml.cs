using Persistencia;
using System.Diagnostics;
using ClassLibray;

namespace HolaMundoMAUI;

public partial class AltaGrupoTrabajo : ContentPage
{
	string Username;
	DateTime DateTime = DateTime.Now;
	string WorkGroupName;
	string WorkShiftName;
	PresenciaContext p;
	Db db = new Db();
	WorkGroup WorkGroup;
	/// <summary>
	/// Constructor that receives the username to persist the data
	/// </summary>
	/// <param string="user"></param>
	public AltaGrupoTrabajo(string user)
	{
		InitializeComponent();
		Username = user;
	}
	/// <summary>
	/// Constructor that receives the username, workgroup to persist the data
	/// and an option to enable the create or update UI
	/// </summary>
	/// <param string="user"></param>
	/// <param string="workGroup"></param>
	/// <param int="option"></param>
	public AltaGrupoTrabajo(string user,string workGroup,int option)
	{
	    //Initialize visual components
		InitializeComponent();
		//Set the username to a global local use
		Username = user;
		//Set the workgroup to a global local use
		WorkGroupName = workGroup;
		//Set the UI entry with the workgroup received 
		NameField.Text = workGroup;
        switch (option)
        {
        	//In this case enable the create UI
			case 0:
				RegisterButton.IsVisible = true;
				RegisterButton.IsEnabled = true;
				UpdateButton.IsVisible = false;
				UpdateButton.IsEnabled = false;
				Title.Text = "Añadir trabajador a equipo de trabajo";
				break;
			//In this case enable the update UI
            case 1:
				RegisterButton.IsVisible = false;
				RegisterButton.IsEnabled = false;
				UpdateButton.IsVisible = true;
				UpdateButton.IsEnabled = true;
				var findWorkGroup = p.WorkGroups.Where(x => x.Name == NameField.Text).FirstOrDefault();
				WorkGroup = findWorkGroup;
				Title.Text = "Revocar trabajador de equipo de trabajo";
				break;
        }
	}
	/// <summary>
	/// Method to add a new Workgroup to DB
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
	public async void AddNewWorkGroupButton_Clicked(object sender, EventArgs e)
	{
		PresenciaContext p = new PresenciaContext();
		if (NameField.Text.Equals("")==false)
		{
			string WorkGroupName = NameField.Text;
			var WorkGroup = p.WorkGroups.Where(x => x.Name == WorkGroupName).FirstOrDefault();
			if (WorkGroup is not null)
			{
				await DisplayAlert("Error", "El equipo de trabajo ya existe.", "OK");
			}
			else
			{
				WorkGroup workGroup = new WorkGroup(WorkGroupName);
				db.InsertWorkGroup(workGroup,p);
				db.InsertLog(new Log("Añadir", Username + " ha añadido grupo de trabajo " + WorkGroupName + " - " + DateTime), p);
				await DisplayAlert("Success", "El equipo de trabajo se ha añadido correctamente.", "OK");
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,6));
			}
		}
		else
		{
			await DisplayAlert("Error", "Debes introducir el nombre de turno", "OK");
		}
	}
	/// <summary>
	/// Method to update an existent Workgroup on DB
	/// </summary>
	/// <param object="sender"></param>
	/// <param EventArgs="e"></param>
	private async void ActualizarGrupoTrabajo(object sender, EventArgs e)
    {
		bool update = db.UpdateWorkGroup(WorkGroup, NameField.Text,p);
		if(update == true)
        {
			await DisplayAlert("Modify", "Se ha modificado correctamente.","Vale");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 6));
			db.InsertLog(new Log("Modificar", Username + " ha modificado el grupo de trabajo " + WorkGroup.Name), p);
        }
        else
        {
			await DisplayAlert("Error", "No se han realizado cambios.","Vale");
		}
    }
	/// <summary>
	/// Method to generare an 24h hour interval
	/// </summary>
	/// <returns>List<string> HourList</returns>
	public List<string> HourGenerate()
    {
		var HourList = new List<string>();
		for (int i = 00; i < 24; i++)
		{
			if (i < 10)
			{
				HourList.Add("0" + i.ToString());
			}
			else
			{
				HourList.Add(i.ToString());
			}
		}
		return HourList;
	}
	/// <summary>
	/// Method to generate an 60 minute interval
	/// </summary>
	/// <returns>List<string> MinuteList</returns>
	public List<string> MinuteGenerate()
	{
		var MinuteList = new List<string>();
		for (int i = 00; i < 60; i++)
		{
			if (i < 10)
			{
				MinuteList.Add("0" + i.ToString());
			}
			else
			{
				MinuteList.Add(i.ToString());
			}
		}
		return MinuteList;
	}
	/// <summary>
	/// Method to Back to the MainScreen of the app
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public void BackToMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,6));
    }
}