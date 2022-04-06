using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AltaGrupoTrabajo : ContentPage
{

	public AltaGrupoTrabajo()
	{
		InitializeComponent();
		
		SelectorHoraEntrada.ItemsSource = GeneraHoras();
		SelectorHoraSalida.ItemsSource = GeneraHoras();
		SelectorMinutoEntrada.ItemsSource = GeneraMinutos();
		SelectorMinutoSalida.ItemsSource = GeneraMinutos();
	}
	public void RegistraNuevoGrupoTrabajo(object sender , EventArgs e)
    {
		PresenciaContext presenciaContext = new PresenciaContext();
		string NombreGrupo = CampoNombre.Text;
		string HoraEntrada = SelectorHoraEntrada.SelectedItem.ToString();
		string MinutoEntrada = SelectorMinutoEntrada.SelectedItem.ToString();
		string TiempoEntrada = HoraEntrada + ":" + MinutoEntrada;
		string HoraSalida = SelectorHoraSalida.SelectedItem.ToString();
		string MinutoSalida = SelectorMinutoSalida.SelectedItem.ToString();
		string TiempoSalida = HoraSalida + ":" + MinutoSalida;
		Debug.WriteLine(TiempoEntrada + " " + TiempoSalida);

		var GrupoExiste = presenciaContext.Grupo_Trabajo.Where(x => x.Turno == NombreGrupo).FirstOrDefault();
		if(GrupoExiste is not null)
        {
			LabelAvisos.Text = "Error, el grupo de trabajo ya existe.";
			LabelAvisos.TextColor = Colors.Red;
        }
        else
        {
			OperacionesDBContext.insertarGrupoTrabajo(NombreGrupo, TiempoEntrada, TiempoSalida);
			LabelAvisos.Text = "Grupo de trabajo creado satisfactoriamente.";
			LabelAvisos.TextColor = Colors.Green;
		}

	}
	public List<string> GeneraHoras()
    {
		var ListaHoras = new List<string>();
		for (int i = 00; i < 24; i++)
		{
			if (i < 10)
			{
				ListaHoras.Add("0" + i.ToString());
			}
			else
			{
				ListaHoras.Add(i.ToString());
			}
		}
		return ListaHoras;
	}
	public List<string> GeneraMinutos()
	{
		var ListaMinutos = new List<string>();
		for (int i = 00; i < 60; i++)
		{
			if (i < 10)
			{
				ListaMinutos.Add("0" + i.ToString());
			}
			else
			{
				ListaMinutos.Add(i.ToString());
			}
		}
		return ListaMinutos;
	}

	public void VolverAlMain(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new PaginaAdmin());
    }
}