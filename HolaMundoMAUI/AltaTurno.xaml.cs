using Persistencia;
using ClassLibray;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AltaTurno : ContentPage
{
    PresenciaContext p = new PresenciaContext();
	bool L, M, X, J, V, S, D;
    string NombreUsuario;
    DateTime dt = DateTime.Now;
    WorkShift _workShiftEditar;

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
                BotonActualizarAdmin.IsVisible = false;
                break;
            case 1:
                BotonRegistrarAdmin.IsVisible = false;
                BotonActualizarAdmin.IsVisible = true;
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
                var turno = p.WorkShifts.Where(x => x.Name == NombreTurno).FirstOrDefault();
                _workShiftEditar = turno;
                CampoNombre.Text = turno.Name;
                CampoNombre.IsEnabled = false;
                string HoraEntrada = turno.CheckIn.ToString().Substring(10, 3);
                string MinutoEntrada = turno.CheckIn.ToString().Substring(14, 2);
                string HoraSalida = turno.CheckOut.ToString().Substring(10, 3);
                string MinutoSalida = turno.CheckOut.ToString().Substring(14 ,2);
                SelectorHoraEntrada.SelectedIndex = int.Parse(HoraEntrada);
                SelectorMinutoEntrada.SelectedIndex = int.Parse(MinutoEntrada);
                SelectorHoraSalida.SelectedIndex = int.Parse(HoraSalida);
                SelectorMinutoSalida.SelectedIndex = int.Parse(MinutoSalida);
                ValidoDesde.Date = turno.ValidFrom;
                ValidoHasta.Date = turno.ValidUntil;
                Lunes.IsChecked = turno.Monday;
                Martes.IsChecked = turno.Tuesday;
                Miercoles.IsChecked = turno.Wednesday;
                Jueves.IsChecked = turno.Thursday;
                Viernes.IsChecked = turno.Friday;
                Sabado.IsChecked = turno.Saturday;
                Domingo.IsChecked = turno.Domingo; 
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
            var GrupoExiste = p.WorkShifts.Where(x => x.Name == NombreTurno).FirstOrDefault();
            if (GrupoExiste is not null)
            {
                await DisplayAlert("Alert", "El turno ya existe.", "OK");
            }
            else
            {
                WorkShift workShift = new WorkShift(CampoNombre.Text,entrada,salida, L, M, X, J, V, S, D);
                workShift.ValidFrom = ValidoDesde.Date;
                workShift.ValidUntil = ValidoHasta.Date;
                p.WorkShifts.Add(workShift);
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
        try
        {
                string NombreTurno = CampoNombre.Text;
                string HoraEntrada = SelectorHoraEntrada.SelectedItem.ToString();
                string MinutoEntrada = SelectorMinutoEntrada.SelectedItem.ToString();
                string TiempoEntrada = HoraEntrada + ":" + MinutoEntrada;
                string HoraSalida = SelectorHoraSalida.SelectedItem.ToString();
                string MinutoSalida = SelectorMinutoSalida.SelectedItem.ToString();
                string TiempoSalida = HoraSalida + ":" + MinutoSalida;
                DateTime entrada = DateTime.Today.AddHours(Double.Parse(HoraEntrada)).AddMinutes(Double.Parse(MinutoEntrada));
                DateTime salida = DateTime.Today.AddHours(Double.Parse(HoraSalida)).AddMinutes(Double.Parse(MinutoSalida));
                bool actualiza =DbUpdate.UpdateWorkShift(_workShiftEditar, NombreTurno, entrada, salida, L, M, X, J, V, S, D, ValidoDesde.Date, ValidoHasta.Date, _workShiftEditar.Enabled, _workShiftEditar.Deleted, p);
            if (actualiza == true)
            {
                DbInsert.InsertLog(new Log("Añadir", NombreUsuario + " ha añadido turno de trabajo: " + NombreTurno + " - " + dt),p);
                await DisplayAlert("Alert", "El turno se ha modificado correctamente.", "OK");
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario, 3));
            }
            
        }catch(Exception ex)
        {
            await DisplayAlert("Alert", ex.ToString(), "Vale");
        }
    }
    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario,3));
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