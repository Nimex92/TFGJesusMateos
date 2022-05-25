using ClassLibray;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace HolaMundoMAUI;

public partial class AnadeDiaCalendario : ContentPage
{
    PresenciaContext p = new PresenciaContext();
    Calendar Calendar;
    Worker Worker;
    string Username;
    public AnadeDiaCalendario(string username, Calendar calendar, int option, Day day)
	{
		InitializeComponent();
        Calendar = calendar;
        Username = username;
        ReasonSetPicker();
        switch (option)
        {
            case 0:
                RegisterButton.IsVisible = true;
                UpdateButton.IsVisible = false;
                CalendarSelector.Items.Add(calendar.Worker.Name);
                break;
            case 1:
                RegisterButton.IsVisible = false;
                UpdateButton.IsVisible = true;
                CalendarSelector.SelectedIndex = 0;
                ReasonSelector.SelectedItem=day.Reason;
                DateSelector.Date = day.Date;
                break;
        }
	}
    public AnadeDiaCalendario(string username, Worker worker)
    { 
        InitializeComponent();
        Username = username;
        Worker = worker;
        UpdateButton.IsVisible = false;
        RequestButton.IsVisible = true;
        BackToMainButton.IsVisible = false;
        BackButton.IsVisible = true;
        VacationSetPicker();
    }
    private void VacationSetPicker()
    {
        CalendarSelector.Items.Add(Worker.Name);
        CalendarSelector.SelectedIndex = 0;
        List<string> reason = new List<string>();
        reason.Add("Festivo");
        reason.Add("Festivo nocturno");
        reason.Add("Festivo nacional");
        reason.Add("Festivo nacional nocturno");
        reason.Add("Dia de asuntos propios");
        reason.Add("Baja laboral");
        reason.Add("Baja médica");
        reason.Add("Baja por maternidad");
        reason.Add("Vacaciones");
        ReasonSelector.Items.Add("-- Selecciona motivo");
        ReasonSelector.ItemsSource = reason;
    }
    private void ReasonSetPicker()
    {  
        CalendarSelector.Items.Add(Calendar.Worker.Name);
        CalendarSelector.SelectedIndex = 0;
        List<string> reason = new List<string>();
        reason.Add("Festivo");
        reason.Add("Festivo nocturno");
        reason.Add("Festivo nacional");
        reason.Add("Festivo nacional nocturno");
        reason.Add("Dia de asuntos propios");
        reason.Add("Baja laboral");
        reason.Add("Baja médica");
        reason.Add("Baja por maternidad");
        reason.Add("Vacaciones");
        ReasonSelector.Items.Add("-- Selecciona motivo");
        ReasonSelector.ItemsSource = reason;
    }
    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        var workerName = CalendarSelector.SelectedItem.ToString();
        var worker = p.Workers.Where(x => x.Name.Equals(workerName)).FirstOrDefault();
        var calendar = p.Calendars.Where(x => x.Worker == worker).FirstOrDefault();
        var reason = ReasonSelector.SelectedItem.ToString();
        var date = DateSelector.Date;
        var day = new Day(reason,date);
        calendar.DaysOnCalendar.Add(day);
        p.SaveChanges();
        await DisplayAlert("Success", "Se ha introducido correctamente el dia.", "Vale");
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,2));
    }
    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        var workerName = CalendarSelector.SelectedItem.ToString();
        var worker = p.Workers.Where(x => x.Name.Equals(workerName)).FirstOrDefault();
        var calendar = p.Calendars.Where(x => x.Worker == worker).FirstOrDefault();
        var reason = ReasonSelector.SelectedItem.ToString();
        var date = DateSelector.Date;
        var day = p.DayOff.Where(x => x.BelongCalendar == calendar).Where(x => x.Date == date).FirstOrDefault();
        var dayList = calendar.DaysOnCalendar;
        foreach(Day d in dayList)
        {
            if(d == day)
            {
                d.Date = DateSelector.Date;
                d.Reason = ReasonSelector.SelectedItem.ToString();
                d.Enjoyed = false;
            }
        }
        p.SaveChanges();
        await DisplayAlert("Success", "Se ha actualizado correctamente el dia.", "Vale");
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 2));
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        var workerName = CalendarSelector.SelectedItem.ToString();
        var worker = p.Workers.Where(x => x.Name.Equals(workerName)).FirstOrDefault();
        var calendar = p.Calendars.Where(x => x.Worker == worker).FirstOrDefault();
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 2));
    }
    private void BackToSignings_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaFichar(Worker.User.Username));
    }
    private async void RequestVacationButton_Clicked(object sender, EventArgs e) 
    {
        try
        {
            var accepted = false;
            DateTime date = DateSelector.Date;
            p.VacationRequests.Add(new VacationRequest(Worker.Name, date, accepted));
            p.SaveChanges();
            await DisplayAlert("Success", "Se ha realizado correctamente la solicitud", "Vale");
            App.Current.MainPage = new NavigationPage(new PaginaFichar(Worker.User.Username));
        }catch(Exception ex) 
        {
            await DisplayAlert("Error", ex.ToString() , "Vale");
        }
        
    }
}