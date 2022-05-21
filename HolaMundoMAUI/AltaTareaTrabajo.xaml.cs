using Persistencia;
using ClassLibray;

namespace HolaMundoMAUI;

public partial class AltaTareaTrabajo : ContentPage
{
    string Username;
    string TaskName;
    DateTime dt = DateTime.Now;
    PresenciaContext presenciaContext = new PresenciaContext();
    Db db = new Db();

    public AltaTareaTrabajo(string user)
	{
		InitializeComponent();
        Username = user;
        Step.ValueChanged += (sender, e) =>
        {
            HourLabel.Text = e.NewValue.ToString();
        };
    }
    public AltaTareaTrabajo(string user,string task,int option)
    {
        InitializeComponent();
        TaskName = task;
        Username = user;
        Step.ValueChanged += (sender, e) =>
        {
            HourLabel.Text = e.NewValue.ToString();
        };

        if(option  == 1)
        {
            RegisterButton.IsVisible = false;
            UpdateButton.IsVisible = true;
        }
        var WorkTask = presenciaContext.WorkTasks.Where(x => x.Name == TaskName).FirstOrDefault();
        NameField.Text = WorkTask.Name;
        DescriptionField.Text = WorkTask.Description;
        Step.Value = WorkTask.ElapsedTime;
    }

    private async void RegistraNuevaTarea(object sender, EventArgs e)
    {
        string name = NameField.Text; 
        string descripcion= DescriptionField.Text;
        double numberOfHours=Step.Value;
        bool insert = db.InsertWorkTask(name, descripcion, numberOfHours, presenciaContext);
        db.InsertLog(new Log("Añadir", Username + " ha añadido tarea de trabajo " + name + " - " + dt),presenciaContext);
        if (insert == true)
        {
            await DisplayAlert("Success", "Se ha insertado tarea "+name+".", "OK");
            App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 5));
        }
        else
        {
            await DisplayAlert("Alert", "Error al insertar.", "OK");
        }
    }
    private async void BotonActualizarAdmin_Clicked(object sender, EventArgs e)
    {
        var task = presenciaContext.WorkTasks.Where(x => x.Name == TaskName).FirstOrDefault();
        task.Name = NameField.Text;
        task.Description = DescriptionField.Text;
        task.ElapsedTime = Step.Value;
        db.UpdateWorkTask(task,NameField.Text,DescriptionField.Text,Step.Value,presenciaContext);
        await DisplayAlert("Alert","Se ha modificado la tarea correctamente.","OK");
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,5));
    }
    private void IrAlMain(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,5));
    }


}