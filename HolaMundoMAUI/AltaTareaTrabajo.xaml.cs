using Persistencia;
using Bibliotec;

namespace HolaMundoMAUI;

public partial class AltaTareaTrabajo : ContentPage
{
    string NombreUsuario;
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

    private async void RegistraNuevaTarea(object sender, EventArgs e)
    {
        string nombre = CampoNombre.Text; 
        string descripcion=CampoDescripcion.Text;
        double numerohoras=step.Value;
        bool inserta = OperacionesDBContext.insertaTareas(nombre, descripcion, numerohoras);
        presenciaContext.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido tarea de trabajo " + nombre + " - " + dt));
        presenciaContext.SaveChanges();
        if (inserta == true)
        {
            await DisplayAlert("Alert", "Se ha insertado tarea "+nombre+".", "OK");
        }
        else
        {
            await DisplayAlert("Alert", "Error al insertar.", "OK");
        }
    }
	private void IrAlMain(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
    }
}