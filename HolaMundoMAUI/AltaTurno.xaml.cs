using Persistencia;
using Bibliotec;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AltaTurno : ContentPage
{
    PresenciaContext p = new PresenciaContext();
	bool L, M, X, J, V, S, D;
    string NombreUsuario;
    DateTime dt = DateTime.Now;

    public AltaTurno()
    {
        InitializeComponent();
        RellenarPickers();
    }
    public AltaTurno(string user,int opcion)
	{
		InitializeComponent();
        NombreUsuario = user;
        RellenarPickers();
        switch (opcion)
        {
            case 0:
                BotonRegistrarAdmin.IsVisible = true;
                BotonRegistrarAdmin.IsEnabled = true;
                BotonActualizarAdmin.IsVisible = false;
                BotonActualizarAdmin.IsEnabled = false;
                break;
            case 1:
                BotonRegistrarAdmin.IsVisible = false;
                BotonRegistrarAdmin.IsEnabled = false;
                BotonActualizarAdmin.IsVisible = true;
                BotonActualizarAdmin.IsEnabled = true;
                break;
        }
    }
    public AltaTurno(string user,string NombreTurno,int opcion)
    {
        InitializeComponent();
        NombreUsuario = user;
        RellenarPickers();
        switch (opcion)
        {
            case 0:
                BotonRegistrarAdmin.IsVisible = true;
                BotonRegistrarAdmin.IsEnabled = true;
                BotonActualizarAdmin.IsVisible = false;
                BotonActualizarAdmin.IsEnabled = false;
                break;
            case 1:
                BotonRegistrarAdmin.IsVisible = false;
                BotonRegistrarAdmin.IsEnabled = false;
                BotonActualizarAdmin.IsVisible = true;
                BotonActualizarAdmin.IsEnabled = true;
                var turno = p.Turno.Where(x => x.Nombre == NombreTurno).FirstOrDefault();
                CampoNombre.Text = turno.Nombre;
                string HoraEntrada = turno.HoraEntrada.ToString().Substring(11, 2);
                string MinutoEntrada = turno.HoraEntrada.ToString().Substring(14, 2);
                string HoraSalida = turno.HoraSalida.ToString().Substring(10, 2);
                string MinutoSalida = turno.HoraSalida.ToString().Substring(13 ,2);
                Debug.WriteLine(HoraSalida + "-" + MinutoSalida);
                Debug.WriteLine(HoraEntrada + "-" + MinutoEntrada);
                SelectorHoraEntrada.SelectedItem = HoraEntrada;
                SelectorMinutoEntrada.SelectedItem = MinutoEntrada;
                SelectorHoraSalida.SelectedIndex = int.Parse(HoraSalida);
                SelectorMinutoSalida.SelectedItem = MinutoSalida;
                ValidoDesde.Date = turno.ValidoDesde;
                ValidoHasta.Date = turno.ValidoHasta;
                Lunes.IsChecked = turno.EsLunes;
                Martes.IsChecked = turno.EsMartes;
                Miercoles.IsChecked = turno.EsMiercoles;
                Jueves.IsChecked = turno.EsJueves;
                Viernes.IsChecked = turno.EsViernes;
                Sabado.IsChecked = turno.EsSabado;
                Domingo.IsChecked = turno.EsDomingo; 
                break;
        }
    }
    private async void BotonRegistrarAdmin_Clicked(object sender, EventArgs e)
    {
        string NombreTurno = CampoNombre.Text;
        if (NombreTurno is not null)
        {
            string HoraEntrada = SelectorHoraEntrada.SelectedItem.ToString();
            string MinutoEntrada = SelectorMinutoEntrada.SelectedItem.ToString();
            string TiempoEntrada = HoraEntrada + ":" + MinutoEntrada;
            string HoraSalida = SelectorHoraSalida.SelectedItem.ToString();
            string MinutoSalida = SelectorMinutoSalida.SelectedItem.ToString();
            string TiempoSalida = HoraSalida + ":" + MinutoSalida;
            DateTime entrada = DateTime.Today.AddHours(Double.Parse(HoraEntrada)).AddMinutes(Double.Parse(MinutoEntrada));
            DateTime salida = DateTime.Today.AddHours(Double.Parse(HoraSalida)).AddMinutes(Double.Parse(MinutoSalida));
            var GrupoExiste = p.Turno.Where(x => x.Nombre == NombreTurno).FirstOrDefault();
            if (GrupoExiste is not null)
            {
                await DisplayAlert("Alert", "El turno ya existe.", "OK");
            }
            else
            {
                Turno turno = new Turno(CampoNombre.Text,entrada,salida, L, M, X, J, V, S, D);
                turno.ValidoDesde = ValidoDesde.Date;
                turno.ValidoHasta = ValidoHasta.Date;
                p.Turno.Add(turno);
                p.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido turno de trabajo: " + NombreTurno + " - " + dt));
                p.SaveChanges();
                await DisplayAlert("Alert", "El turno se ha añadido correctamente.", "OK");
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 3));
            }
        }
        else
        {
            await DisplayAlert("Alert", "Debes introducir el nombre de turno", "OK");
        }
    }

    private async void BotonActualizarAdmin_Clicked(object sender, EventArgs e)
    {
        string NombreTurno = CampoNombre.Text;
        if (NombreTurno is not null)
        {
            string HoraEntrada = SelectorHoraEntrada.SelectedItem.ToString();
            string MinutoEntrada = SelectorMinutoEntrada.SelectedItem.ToString();
            string TiempoEntrada = HoraEntrada + ":" + MinutoEntrada;
            string HoraSalida = SelectorHoraSalida.SelectedItem.ToString();
            string MinutoSalida = SelectorMinutoSalida.SelectedItem.ToString();
            string TiempoSalida = HoraSalida + ":" + MinutoSalida;
            DateTime entrada = DateTime.Today.AddHours(Double.Parse(HoraEntrada)).AddMinutes(Double.Parse(MinutoEntrada));
            DateTime salida = DateTime.Today.AddHours(Double.Parse(HoraSalida)).AddMinutes(Double.Parse(MinutoSalida));
            var TurnoExiste = p.Turno.Where(x => x.Nombre == NombreTurno).FirstOrDefault();
            if (TurnoExiste is not null)
            {
                TurnoExiste.Nombre = NombreTurno;
                TurnoExiste.HoraEntrada = entrada;
                TurnoExiste.HoraSalida = salida;
                TurnoExiste.EsLunes = L;
                TurnoExiste.EsMartes = M;
                TurnoExiste.EsMiercoles = X;
                TurnoExiste.EsJueves = J;
                TurnoExiste.EsViernes = V;
                TurnoExiste.EsSabado = S;
                TurnoExiste.EsDomingo = D;
                TurnoExiste.ValidoDesde = ValidoDesde.Date;
                TurnoExiste.ValidoHasta = ValidoHasta.Date;

                p.Turno.Update(TurnoExiste);
                p.Logs.Add(new Log("Añadir", NombreUsuario + " ha añadido turno de trabajo: " + NombreTurno + " - " + dt));
                p.SaveChanges();
                await DisplayAlert("Alert", "El turno se ha modificado correctamente.", "OK");
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 3));
            }
            else
            {
                
            }
        }
        else
        {
            await DisplayAlert("Alert", "Debes introducir el nombre de turno", "OK");
        }
    }
    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
    }
    public void RellenarPickers()
    {
        SelectorHoraEntrada.ItemsSource = GeneraHoras();
        SelectorHoraSalida.ItemsSource = GeneraHoras();
        SelectorMinutoEntrada.ItemsSource = GeneraMinutos();
        SelectorMinutoSalida.ItemsSource = GeneraMinutos();
        SelectorHoraEntrada.SelectedIndex = 12;
        SelectorMinutoEntrada.SelectedIndex = 30;
        SelectorHoraSalida.SelectedIndex = 12;
        SelectorMinutoSalida.SelectedIndex = 30;
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

    private void CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		if (Lunes.IsChecked)
        {
			L = true;
            Debug.WriteLine(L);
        }
        else
        {
            L = false;
            Debug.WriteLine(L);
        }
        if (Martes.IsChecked)
        {
            M = true;
            
        }
        else
        {
            M = false;
            
        }
        if (Miercoles.IsChecked)
        {
            X = true;
            
        }
        else
        {
            X = false;
            
        }
        if (Jueves.IsChecked)
        {
            J = true;

        }
        else
        {
            J = false;
            
        }
        if (Viernes.IsChecked)
        {
            V = true;
            
        }
        else
        {
            V = false;
            
        }
        if (Sabado.IsChecked)
        {
            S = true;
            
        }
        else
        {
            S = false;
            
        }
        if (Domingo.IsChecked)
        {
            D = true;
           
        }
        else
        {
            D = false;
            
        }

    }
}