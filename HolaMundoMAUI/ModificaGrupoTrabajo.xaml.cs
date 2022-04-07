using Bibliotec;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class ModificarGrupoTrabajo
{
	bool activado;
	Grupo_Trabajo gr = new();
	PresenciaContext presenciaContext = new PresenciaContext();
	public ModificarGrupoTrabajo()
	{
		InitializeComponent();
		activado = false;
		SetListView();
		//Recojo todos los Turnos de la tabla de MySql
		var Horas= new List<string>();
		var Minutos = new List<string>();
		//Creo una lista para guardar todos los turnos existentes
		var listaTurnos = new List<string>();
		//Para cada lista que haya en la seleccion Turno, añado al selector (Picker de la interfaz) El nombre del turno
		for(int i=0;i<24;i++)
		{
            if (i < 10) {
				Horas.Add("0" + i.ToString());
            }
            else
            {
				Horas.Add(i.ToString());
			}
		}
		SelectorHoraEntrada.ItemsSource=Horas;
		SelectorHoraSalida.ItemsSource = Horas;
		for (int i = 0; i < 60; i++)
		{
			if (i < 10)
			{
				Minutos.Add("0" + i.ToString());
			}
			else
			{
				Minutos.Add(i.ToString());
			}
		}
		SelectorMinutoEntrada.ItemsSource = Minutos;
		SelectorMinutoSalida.ItemsSource = Minutos;
	}
	public void VolverAlMain(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
	}
	public void SetListView()
    {
		var GrupoTrabajo = presenciaContext.Grupo_Trabajo.ToList();
		ListViewUsuarios.ItemsSource = GrupoTrabajo;
	}
	public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		Grupo_Trabajo item = e.SelectedItem as Grupo_Trabajo;
		gr = item;

	}
	public void MostrarEditar(object sender, EventArgs e)
    {
		Grupo_Trabajo gt = gr;
		if(gr.Turno == CampoUsuario.Text)
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
			CampoUsuario.Text = gr.Turno;
			SelectorHoraEntrada.SelectedItem = gr.HoraEntrada.Substring(0, 2);
			SelectorMinutoEntrada.SelectedItem = gr.HoraEntrada.Substring(3, 2);
			SelectorHoraSalida.SelectedItem = gr.HoraSalida.Substring(0, 2);
			SelectorMinutoSalida.SelectedItem = gr.HoraSalida.Substring(3, 2);
			SelectorHoraEntrada.IsEnabled = true;
			SelectorMinutoEntrada.IsEnabled=true;
			SelectorHoraSalida.IsEnabled = true;
			SelectorMinutoSalida.IsEnabled = true;
			SelectorHoraSalida.IsVisible = true;
			SelectorMinutoSalida.IsVisible = true;
			SelectorHoraEntrada.IsVisible = true;
			SelectorMinutoEntrada.IsVisible = true;
			LabelHoraEntrada.IsVisible = true;
			LabelHoraEntrada.IsEnabled = true;
			LabelHoraSalida.IsVisible = true;
			LabelHoraSalida.IsEnabled = true;
			activado = true;
        }
        else
        {
			CampoUsuario.IsVisible = false;
			CampoUsuario.IsEnabled = false;
			BotonGuardarCambios.IsEnabled = false;
			BotonGuardarCambios.IsVisible = false;
			CampoUsuario.Text = "";
			SelectorHoraEntrada.SelectedItem = "";
			SelectorMinutoEntrada.SelectedItem = "";
			SelectorHoraSalida.SelectedItem = "";
			SelectorMinutoSalida.SelectedItem = "";
			SelectorHoraEntrada.IsEnabled = false;
			SelectorMinutoEntrada.IsEnabled = false;
			SelectorHoraSalida.IsEnabled = false;
			SelectorMinutoSalida.IsEnabled = false;
			SelectorHoraSalida.IsVisible = false;
			SelectorMinutoSalida.IsVisible = false;
			LabelHoraEntrada.IsVisible=false;
			LabelHoraEntrada.IsEnabled = false;
			LabelHoraSalida.IsVisible = false;
			LabelHoraSalida.IsEnabled = false;
			activado = false;
		}	
	}

	public void GuardarCambios(object sender, EventArgs e)
    {
		var HoraEntrada = SelectorHoraEntrada.SelectedItem + ":" + SelectorMinutoEntrada.SelectedItem;
		var HoraSalida = SelectorHoraSalida.SelectedItem + ":" + SelectorMinutoSalida.SelectedItem;
		LabelAvisos.Text = CampoUsuario.Text+""+HoraEntrada + " " + HoraSalida;

		bool inserta =OperacionesDBContext.actualizarGrupoTrabajo(CampoUsuario.Text, HoraEntrada, HoraSalida);
        if (inserta == true)
        {
			LabelAvisos.Text = "Los cambios se han guardado correctamente.";
			LabelAvisos.TextColor = Colors.Green;
        }
        else
        {
			LabelAvisos.Text = "No se realizaron los cambios.";
			LabelAvisos.TextColor = Colors.Red;
		}
    }
}