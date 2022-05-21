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
	public AltaGrupoTrabajo(string user)
	{
		InitializeComponent();
		Username = user;
	}
	public AltaGrupoTrabajo(string user,string workGroup,int option)
	{
		InitializeComponent();
		Username = user;
		WorkGroupName = workGroup;
		NameField.Text = workGroup;
        switch (option)
        {
			case 0:
				RegisterButton.IsVisible = true;
				RegisterButton.IsEnabled = true;
				UpdateButton.IsVisible = false;
				UpdateButton.IsEnabled = false;
				Title.Text = "Tech Talent" + Environment.NewLine + "Añadir trabajador a equipo de trabajo";
				break;
			case 1:
				RegisterButton.IsVisible = false;
				RegisterButton.IsEnabled = false;
				UpdateButton.IsVisible = true;
				UpdateButton.IsEnabled = true;
				var FindWorkGroup = p.WorkGroups.Where(x => x.Name == NameField.Text).FirstOrDefault();
				WorkGroup = FindWorkGroup;
				Title.Text = "Revocar trabajador de equipo de trabajo";
				break;
        }
	}
	public async void RegistraNuevoGrupoTrabajo(object sender, EventArgs e)
	{
		PresenciaContext p = new PresenciaContext();
		if (NameField.Text.Equals("")==false)
		{
			string WorkGroupName = NameField.Text;
			var WorkGroup = p.WorkGroups.Where(x => x.Name == WorkGroupName).FirstOrDefault();
			if (WorkGroup is not null)
			{
				await DisplayAlert("Alert", "El equipo de trabajo ya existe.", "OK");
			}
			else
			{
				WorkGroup workGroup = new WorkGroup(WorkGroupName);
				db.InsertWorkGroup(workGroup,p);
				db.InsertLog(new Log("Añadir", Username + " ha añadido grupo de trabajo " + WorkGroupName + " - " + DateTime), p);
				await DisplayAlert("Error", "El equipo de trabajo se ha añadido correctamente.", "OK");
				App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,6));
			}
		}
		else
		{
			await DisplayAlert("Alert", "Debes introducir el nombre de turno", "OK");
		}
	}
	private async void ActualizarGrupoTrabajo(object sender, EventArgs e)
    {
		bool actualizar = db.UpdateWorkGroup(WorkGroup, NameField.Text,p);
		if(actualizar == true)
        {
			await DisplayAlert("Modificar", "Se ha modificado correctamente.","Vale");
			App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 6));
			db.InsertLog(new Log("Modificar", Username + " ha modificado el grupo de trabajo " + WorkGroup.Name), p);
        }
        else
        {
			await DisplayAlert("Alert", "No se han realizado cambios.","Vale");
		}
    }

	
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
	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,6));
    }
}