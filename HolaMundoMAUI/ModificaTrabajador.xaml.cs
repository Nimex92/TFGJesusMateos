using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class ModificarTrabajador {
	bool activado;
	Trabajador tr = new();
	Grupo_Trabajo gr = new();
	PresenciaContext presenciaContext = new PresenciaContext();
	public ModificarTrabajador()
	{
		InitializeComponent();
		activado = false;
		SetListView();
		//Recojo todos los Turnos de la tabla de MySql
		var Turno = presenciaContext.Grupo_Trabajo;
		//Creo una lista para guardar todos los turnos existentes
		var listaTurnos = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno
		foreach (Grupo_Trabajo grupo in Turno)
		{
			Selector.Items.Add(grupo.Turno);
		}
	}
	public void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}
	public void SetListView()
    {
		var trabajador = presenciaContext.Trabajador.Include(x=>x.grupo).Include(x=>x.usuario).ToList();
		ListViewUsuarios.ItemsSource = trabajador;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Trabajador item = e.SelectedItem as Trabajador;
		tr = item;
		gr = item.grupo;
		LabelAvisos.Text = "";
	}
	public void MostrarEditar(object sender, EventArgs e)
    {
		Trabajador os = tr;
		if(os.nombre == CampoUsuario.Text)
        {
			activado = true;
        }
        else
        {
			activado=false;
        }

        if (activado == false) {
			CampoUsuario.IsVisible = true;
			CampoUsuario.IsEnabled = true;
			BotonGuardarCambios.IsVisible = true;
			BotonGuardarCambios.IsEnabled = true;
			CampoUsuario.Text = os.nombre;
			Selector.IsEnabled = true;
			Selector.IsVisible = true;
			Selector.SelectedItem = os.grupo.Turno;
			LabelIntroduceTurno.IsEnabled = true;
			LabelIntroduceTurno.IsVisible = true;
			activado = true;
        }
        else
        {
			CampoUsuario.IsVisible = false;
			CampoUsuario.IsEnabled = false;
			BotonGuardarCambios.IsEnabled = false;
			BotonGuardarCambios.IsVisible = false;
			CampoUsuario.Text = "";
			Selector.IsEnabled = false;
			Selector.IsVisible = false;
			LabelIntroduceTurno.IsEnabled = false;
			LabelIntroduceTurno.IsVisible = false;
			activado = false;
		}
	}

	public async void GuardarCambios(object sender, EventArgs e)
    {
		var NombreTrabajador = CampoUsuario.Text;
		var turno = Selector.SelectedItem.ToString();
		var GrupoTrabajo = presenciaContext.Grupo_Trabajo.Where(x => x.Turno == turno).FirstOrDefault();
		int grupo = GrupoTrabajo.IdGrupo;
		bool inserta = OperacionesDBContext.actualizaTrabajador(tr.numero_tarjeta,NombreTrabajador, grupo);
		if (inserta == true)
		{
			LabelAvisos.Text = "Se han realizado los cambios";
			LabelAvisos.TextColor = Colors.Green;
			await DisplayAlert("Alert", "Los cambios se guardaron correctamente", "OK");
		}
        else
        {
			LabelAvisos.Text = "Error al guardar los cambios";
			LabelAvisos.TextColor = Colors.Red;
			await DisplayAlert("Alert", "Los cambios no se han podido aplicar", "OK");
		}
    }
}