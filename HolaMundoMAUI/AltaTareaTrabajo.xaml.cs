using Persistencia;

namespace HolaMundoMAUI;

public partial class AltaTareaTrabajo : ContentPage
{
	public AltaTareaTrabajo()
	{
		InitializeComponent();
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
        if(inserta = true)
        {
            await DisplayAlert("Alert", "Se ha insertado una tarea.", "OK");
        }
        else
        {
            await DisplayAlert("Alert", "Error al insertar.", "OK");
        }
    }
	private void IrAlMain(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaAdmin());
    }
}