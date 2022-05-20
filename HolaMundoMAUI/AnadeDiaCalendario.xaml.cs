using ClassLibray;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace HolaMundoMAUI;

public partial class AnadeDiaCalendario : ContentPage
{
    PresenciaContext p = new PresenciaContext();
    Calendar CalendarioLaboral;
    Worker traba;
    string nombreUsuario;
    public AnadeDiaCalendario(string user, Calendar cal, int actualiza, Day day)
	{
		InitializeComponent();
        CalendarioLaboral = cal;
        nombreUsuario = user;
        SetPickers();
        switch (actualiza)
        {
            case 0:
                BotonRegistrar.IsEnabled = true;
                BotonRegistrar.IsVisible = true;
                BotonActualizar.IsEnabled = false;
                BotonActualizar.IsVisible = false;
<<<<<<< HEAD
                SelectorCalendario.Items.Add(cal.Worker.Name);
=======
                SelectorCalendario.Items.Add(cal.Trabajador.Nombre);
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
                break;
            case 1:
                BotonRegistrar.IsEnabled = false;
                BotonRegistrar.IsVisible = false;
                BotonActualizar.IsEnabled = true;
                BotonActualizar.IsVisible = true;
                SelectorCalendario.SelectedIndex = 0;
                SelectorMotivo.SelectedItem=day.Reason;
                SelectorFecha.Date = day.Date;
                
                break;
        }
	}
    public AnadeDiaCalendario(string user, Worker trab)
    { 
        InitializeComponent();
        nombreUsuario = user;
        traba = trab;
        BotonActualizar.IsVisible = false;
        BotonActualizar.IsEnabled = false;
        BotonRegistrar.IsEnabled = false;
        BotonRegistrar.IsVisible = false;
        BotonSolicitar.IsVisible = true;
        BotonSolicitar.IsVisible = true;
        BotonVolver.IsEnabled = false;
        BotonVolver.IsVisible = false;
        BotonVolverFichar.IsVisible = true;
        BotonVolverFichar.IsEnabled = true;
        SetPickerVacaciones();
    }
 
    private void SetPickerVacaciones()
    {
<<<<<<< HEAD
        SelectorCalendario.Items.Add(traba.Name);
=======
        SelectorCalendario.Items.Add(traba.Nombre);
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
        SelectorCalendario.SelectedIndex = 0;
        List<string> motivos = new List<string>();
        motivos.Add("Festivo");
        motivos.Add("Festivo nocturno");
        motivos.Add("Festivo nacional");
        motivos.Add("Festivo nacional nocturno");
        motivos.Add("Dia de asuntos propios");
        motivos.Add("Baja laboral");
        motivos.Add("Baja médica");
        motivos.Add("Baja por maternidad");
        motivos.Add("Vacaciones");
        SelectorMotivo.Items.Add("-- Selecciona motivo");
        SelectorMotivo.ItemsSource = motivos;
    }
    private void SetPickers()
    {  
<<<<<<< HEAD
        SelectorCalendario.Items.Add(CalendarioLaboral.Worker.Name);
=======
        SelectorCalendario.Items.Add(CalendarioLaboral.Trabajador.Nombre);
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
        SelectorCalendario.SelectedIndex = 0;
        List<string> motivos = new List<string>();
        motivos.Add("Festivo");
        motivos.Add("Festivo nocturno");
        motivos.Add("Festivo nacional");
        motivos.Add("Festivo nacional nocturno");
        motivos.Add("Dia de asuntos propios");
        motivos.Add("Baja laboral");
        motivos.Add("Baja médica");
        motivos.Add("Baja por maternidad");
        motivos.Add("Vacaciones");
        SelectorMotivo.Items.Add("-- Selecciona motivo");
        SelectorMotivo.ItemsSource = motivos;
    }
    private async void BotonRegistrar_Clicked(object sender, EventArgs e)
    {
        var NombreTrabajador = SelectorCalendario.SelectedItem.ToString();
<<<<<<< HEAD
        var Trabajador = p.Workers.Where(x => x.Name.Equals(NombreTrabajador)).FirstOrDefault();
        var Calendario = p.Calendars.Where(x => x.Worker == Trabajador).FirstOrDefault();
=======
        var Trabajador = p.Trabajador.Where(x => x.Nombre.Equals(NombreTrabajador)).FirstOrDefault();
        var Calendario = p.Calendario.Where(x => x.Trabajador == Trabajador).FirstOrDefault();
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
        var motivo = SelectorMotivo.SelectedItem.ToString();
        var fecha = SelectorFecha.Date;
        var Dia = new Day(motivo,fecha);
        Calendario.DaysOnCalendar.Add(Dia);
        p.SaveChanges();
        await DisplayAlert("Alert", "Se ha introducido correctamente el dia.", "Vale");
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario,2));
    }
    private async void BotonActualizar_Clicked(object sender, EventArgs e)
    {
        var NombreTrabajador = SelectorCalendario.SelectedItem.ToString();
<<<<<<< HEAD
        var Trabajador = p.Workers.Where(x => x.Name.Equals(NombreTrabajador)).FirstOrDefault();
        var Calendario = p.Calendars.Where(x => x.Worker == Trabajador).FirstOrDefault();
=======
        var Trabajador = p.Trabajador.Where(x => x.Nombre.Equals(NombreTrabajador)).FirstOrDefault();
        var Calendario = p.Calendario.Where(x => x.Trabajador == Trabajador).FirstOrDefault();
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
        var motivo = SelectorMotivo.SelectedItem.ToString();
        var fecha = SelectorFecha.Date;
        var dia = p.DayOff.Where(x => x.BelongCalendar == Calendario).Where(x => x.Date == fecha).FirstOrDefault();
        var ListaDias = Calendario.DaysOnCalendar;
        foreach(Day d in ListaDias)
        {
            if(d == dia)
            {
                d.Date = SelectorFecha.Date;
                d.Reason = SelectorMotivo.SelectedItem.ToString();
                d.Enjoyed = false;
            }
        }
        p.SaveChanges();
        await DisplayAlert("Alert", "Se ha actualizado correctamente el dia.", "Vale");
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 2));
    }
    private void BotonVolver_Clicked(object sender, EventArgs e)
    {
        var NombreTrabajador = SelectorCalendario.SelectedItem.ToString();
<<<<<<< HEAD
        var Trabajador = p.Workers.Where(x => x.Name.Equals(NombreTrabajador)).FirstOrDefault();
        var Calendario = p.Calendars.Where(x => x.Worker == Trabajador).FirstOrDefault();
=======
        var Trabajador = p.Trabajador.Where(x => x.Nombre.Equals(NombreTrabajador)).FirstOrDefault();
        var Calendario = p.Calendario.Where(x => x.Trabajador == Trabajador).FirstOrDefault();
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(nombreUsuario, 2));
    }
    private void BotonVolverFichar_Clicked(object sender, EventArgs e)
    {
<<<<<<< HEAD
        App.Current.MainPage = new NavigationPage(new PaginaFichar(traba.User.Username));
=======
        App.Current.MainPage = new NavigationPage(new PaginaFichar(traba.Usuario.Username));
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
    }
    private async void BotonPideVacaciones_Clicked(object sender, EventArgs e) 
    {
        try
        {
            var seacepta = false;
            DateTime fecha = SelectorFecha.Date;
<<<<<<< HEAD
            p.VacationRequests.Add(new VacationRequest(traba.Name, fecha, seacepta));
            p.SaveChanges();
            await DisplayAlert("Alert", "Se ha realizado correctamente la solicitud", "Vale");
            App.Current.MainPage = new NavigationPage(new PaginaFichar(traba.User.Username));
=======
            p.SolicitudesVacaciones.Add(new SolicitudVacaciones(traba.Nombre, fecha, seacepta));
            p.SaveChanges();
            await DisplayAlert("Alert", "Se ha realizado correctamente la solicitud", "Vale");
            App.Current.MainPage = new NavigationPage(new PaginaFichar(traba.Usuario.Username));
>>>>>>> fb0fc5fb889192d67c03416bb018ef984a3d00be
        }catch(Exception ex) 
        {
            await DisplayAlert("Alert", ex.StackTrace , "Vale");
        }
        
    }
}