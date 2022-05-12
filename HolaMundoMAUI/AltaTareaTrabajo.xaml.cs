using Persistencia;
using Bibliotec;

namespace HolaMundoMAUI;

public partial class AltaTareaTrabajo : ContentPage
{
    string NombreUsuario;
    string NombreTarea;
    DateTime dt = DateTime.Now;
    PresenciaContext presenciaContext = new PresenciaContext();

    public AltaTareaTrabajo(string user)
	{
		InitializeComponent();
        NombreUsuario = user;
        step.ValueChanged += (sender, e) =>
        {
            LabelHoras.Text = e.NewValue.ToString();
        };
    }
    public AltaTareaTrabajo(string user,string tarea,int actualiza)
    {
        InitializeComponent();
        NombreTarea = tarea;
        NombreUsuario = user;
        step.ValueChanged += (sender, e) =>
        {
            LabelHoras.Text = e.NewValue.ToString();
        };

        if(actualiza  == 1)
        {
            BotonRegistrarAdmin.IsVisible = false;
            BotonRegistrarAdmin.IsEnabled = false;
            BotonActualizarAdmin.IsVisible = true;
            BotonActualizarAdmin.IsEnabled = true;
        }
        var Tarea = presenciaContext.Tareas.Where(x => x.NombreTarea == NombreTarea).FirstOrDefault();
        CampoNombre.Text = Tarea.NombreTarea;
        CampoDescripcion.Text = Tarea.Descripcion;
        step.Value = Tarea.TiempoEstimado;
    }

    private async void RegistraNuevaTarea(object sender, EventArgs e)
    {
        string nombre = CampoNombre.Text; 
        string descripcion=CampoDescripcion.Text;
        double numerohoras=step.Value;
        bool inserta = OperacionesDBContext.insertaTareas(nombre, descripcion, numerohoras);
        OperacionesDBContext.InsertaLog(new Log("Añadir", NombreUsuario + " ha añadido tarea de trabajo " + nombre + " - " + dt));
        if (inserta == true)
        {
            await DisplayAlert("Alert", "Se ha insertado tarea "+nombre+".", "OK");
            App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 5));
        }
        else
        {
            await DisplayAlert("Alert", "Error al insertar.", "OK");
        }
    }
    private async void BotonActualizarAdmin_Clicked(object sender, EventArgs e)
    {
        var tarea = presenciaContext.Tareas.Where(x => x.NombreTarea == NombreTarea).FirstOrDefault();
        //tarea.NombreTarea = CampoNombre.Text;
        //tarea.Descripcion = CampoDescripcion.Text;
        //tarea.TiempoEstimado = step.Value;
        OperacionesDBContext.ActualizaTarea(tarea,CampoNombre.Text,CampoDescripcion.Text,step.Value);
        await DisplayAlert("Alert","Se ha modificado la tarea correctamente.","OK");
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,5));
    }
    private void IrAlMain(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,5));
    }


}