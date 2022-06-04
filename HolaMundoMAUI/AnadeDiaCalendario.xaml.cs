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
        //Set the Calendar to a global local use
        Calendar = calendar;
        //Set the Username to a global local use
        Username = username;
        //Set the UI pickers with data
        VacationSetPicker();
        switch (option)
        {
            //In this case enable the create UI
            case 0:
                RegisterButton.IsVisible = true;
                UpdateButton.IsVisible = false;
                CalendarSelector.Items.Add(calendar.Worker.Name);
                break;
            //In this case enable the update UI
            case 1:
                RegisterButton.IsVisible = false;
                UpdateButton.IsVisible = true;
                CalendarSelector.SelectedIndex = 0;
                ReasonSelector.SelectedItem=day.Reason;
                DateSelector.Date = day.Date;
                break;
        }
	}
    /// <summary>
    /// Constructor to enable the update UI
    /// </summary>
    /// <param string="username"></param>
    /// <param Worker="worker"></param>
    public AnadeDiaCalendario(string username, Worker worker)
    { 
        InitializeComponent();
        Username = username;
        Worker = worker;

        RequestButton.IsVisible = true;
        BackToMainButton.IsVisible = false;
        BackButton.IsVisible = true;
        VacationSetPicker();
    }
    /// <summary>
    /// Method to fill the UI Picker with vacation requests
    /// </summary>
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
    /// <summary>
    /// Method to add a new Day to the worker's calendar
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
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
    /// <summary>
    /// Method to update an exist Day on the worker's calendar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        App.Current.MainPage = new NavigationPage(new PaginaFichar(Worker.User.Username));
    }
    /// <summary>
    /// Method to back to the last screen of the app
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        var workerName = CalendarSelector.SelectedItem.ToString();
        var worker = p.Workers.Where(x => x.Name.Equals(workerName)).FirstOrDefault();
        var calendar = p.Calendars.Where(x => x.Worker == worker).FirstOrDefault();
        App.Current.MainPage = new NavigationPage(new PaginaFichar(Worker.User.Username));
    }
    /// <summary>
    /// Method to back to the signings screen of the app
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void BackToSignings_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaFichar(Worker.User.Username));
    }
    /// <summary>
    /// Method to add a new request vacation from the worker to the admins
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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