using Persistencia;
using ClassLibray;

namespace HolaMundoMAUI;


public partial class AltaTareaTrabajo : ContentPage
{
    string Username;
    string workTaskName;
    DateTime dt = DateTime.Now;
    PresenciaContext presenciaContext = new PresenciaContext();
    Db db = new Db();
    /// <summary>
    /// Constructor that receives the username to persist the data
    /// </summary>
    /// <param string="user"></param>
    public AltaTareaTrabajo(string user)
	{
		InitializeComponent();
        Username = user;
        Step.ValueChanged += (sender, e) =>
        {
            HourLabel.Text = e.NewValue.ToString();
        };
    }
    /// <summary>
    /// Constructor that receives the username and workTask to persist the data
    /// and receives an option to enable create or update UI interface
    /// </summary>
    /// <param string="user"></param>
    /// <param string="task"></param>
    /// <param int="option"></param>
    public AltaTareaTrabajo(string user,string workTask,int option)
    {
        InitializeComponent();
        //Set the workTask variable received to global local use
        workTaskName = workTask;
        //Set the user variable received to global local use
        Username = user;
        //Check the stepper value every time than select another value
        Step.ValueChanged += (sender, e) =>
        {
            HourLabel.Text = e.NewValue.ToString();
        };
        //If option equals 1 enable the UI
        if(option  == 1)
        {
            RegisterButton.IsVisible = false;
            UpdateButton.IsVisible = true;
        }
        //Search the workTask on Db and set the UI fields with they
        var WorkTask = presenciaContext.WorkTasks.Where(x => x.Name == workTaskName).FirstOrDefault();
        NameField.Text = WorkTask.Name;
        DescriptionField.Text = WorkTask.Description;
        Step.Value = WorkTask.ElapsedTime;
    }
    /// <summary>
    /// Method to add a new WorkTask to DB
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
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
    /// <summary>
    /// Method to update an exist WorkTask on Db
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void BotonActualizarAdmin_Clicked(object sender, EventArgs e)
    {
        var task = presenciaContext.WorkTasks.Where(x => x.Name == workTaskName).FirstOrDefault();
        task.Name = NameField.Text;
        task.Description = DescriptionField.Text;
        task.ElapsedTime = Step.Value;
        db.UpdateWorkTask(task,NameField.Text,DescriptionField.Text,Step.Value,presenciaContext);
        await DisplayAlert("Alert","Se ha modificado la tarea correctamente.","OK");
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,5));
    }
    /// <summary>
    /// Method to back to the MainScreen of the app
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void IrAlMain(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,5));
    }


}