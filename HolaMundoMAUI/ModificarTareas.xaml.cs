using Bibliotec;
using Persistencia;

namespace HolaMundoMAUI;

public partial class ModificarTareas : ContentPage
{
	PresenciaContext presenciaContext = new PresenciaContext();
	string NombreTarea;
	public ModificarTareas()
	{
		InitializeComponent();
		SetListView();
		step.ValueChanged += (sender, e) =>
		{

			LabelHoras.Text = e.NewValue.ToString();
		};
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		LayoutCampos.IsVisible = false;
		LayoutCampos.IsEnabled = false;
		Tareas item = e.SelectedItem as Tareas;
		NombreTarea = item.NombreTarea;
		CampoNombre.Text = "";
		CampoDescripcion.Text = "";
		LabelHoras.Text = "";
	}
	public void SetListView()
	{
		var Tareas = presenciaContext.Tareas.ToList();
		ListViewUsuarios.ItemsSource = Tareas;
	}
	public void MostrarEditar(object sender, EventArgs e)
	{
		LayoutCampos.IsVisible = true;
		LayoutCampos.IsEnabled = true;
		var tarea = presenciaContext.Tareas.Where(x=>x.NombreTarea == NombreTarea).FirstOrDefault();
		CampoNombre.Text = tarea.NombreTarea;
		CampoDescripcion.Text = tarea.Descripcion;
		LabelHoras.Text = tarea.TiempoEstimado.ToString();

	}
	public void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}
	public void GuardarCambios(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}

}