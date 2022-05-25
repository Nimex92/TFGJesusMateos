using Persistencia;
using ClassLibray;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class AltaTurno : ContentPage
{
    PresenciaContext p = new PresenciaContext();
	bool Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday;
    string Username;
    DateTime Now = DateTime.Now;
    WorkShift WorkShift;
    Db db = new Db();

    public AltaTurno()
    {
        InitializeComponent();
        SetPickers();
    }
    public AltaTurno(string user,int opcion)
	{
		InitializeComponent();
        Username = user;
        SetPickers();
        switch (opcion)
        {
            case 0:
                RegisterButton.IsVisible = true;
                UpdateButton.IsVisible = false;
                break;
            case 1:
                RegisterButton.IsVisible = false;
                UpdateButton.IsVisible = true;
                break;
        }
    }
    public AltaTurno(string user,string NombreTurno,int opcion)
    {
        InitializeComponent();
        Username = user;
        SetPickers();
        switch (opcion)
        {
            case 0:
                RegisterButton.IsVisible = true;
                UpdateButton.IsVisible = false;
                break;
            case 1:
                RegisterButton.IsVisible = false;
                UpdateButton.IsVisible = true;
                var workShift = p.WorkShifts.Where(x => x.Name == NombreTurno).FirstOrDefault();
                WorkShift = workShift;
                NameField.Text = workShift.Name;
                NameField.IsEnabled = false;
                string HoraEntrada = workShift.CheckIn.ToString().Substring(10, 3);
                string MinutoEntrada = workShift.CheckIn.ToString().Substring(14, 2);
                string HoraSalida = workShift.CheckOut.ToString().Substring(10, 3);
                string MinutoSalida = workShift.CheckOut.ToString().Substring(14 ,2);
                HourCheckInSelector.SelectedIndex = int.Parse(HoraEntrada);
                MinuteCheckInSelector.SelectedIndex = int.Parse(MinutoEntrada);
                HourCheckOutSelector.SelectedIndex = int.Parse(HoraSalida);
                MinuteCheckOutSelector.SelectedIndex = int.Parse(MinutoSalida);
                ValidFromPicker.Date = workShift.ValidFrom;
                ValidUntilPicker.Date = workShift.ValidUntil;
                monday.IsChecked = workShift.Monday;
                tuesday.IsChecked = workShift.Tuesday;
                wednesday.IsChecked = workShift.Wednesday;
                thursday.IsChecked = workShift.Thursday;
                friday.IsChecked = workShift.Friday;
                saturday.IsChecked = workShift.Saturday;
                sunday.IsChecked = workShift.Domingo; 
                break;
        }
    }
    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        string workShiftName = NameField.Text;
        if (workShiftName is not null)
        {
            string HourCheckIn = HourCheckInSelector.SelectedItem.ToString();
            string MinuteCheckIn = MinuteCheckInSelector.SelectedItem.ToString();
            string FinalCheckIn = HourCheckIn + ":" + MinuteCheckIn;
            string HourCheckOut = HourCheckOutSelector.SelectedItem.ToString();
            string MinuteCheckOut = MinuteCheckOutSelector.SelectedItem.ToString();
            string FinalCheckOut = HourCheckOut + ":" + MinuteCheckOut;
            DateTime checkIn = DateTime.Today.AddHours(Double.Parse(HourCheckIn)).AddMinutes(Double.Parse(MinuteCheckIn));
            DateTime checkOut = DateTime.Today.AddHours(Double.Parse(HourCheckOut)).AddMinutes(Double.Parse(MinuteCheckOut));
            var workGroupSearch = p.WorkShifts.Where(x => x.Name == workShiftName).FirstOrDefault();
            if (workGroupSearch is not null)
            {
                await DisplayAlert("Error", "El turno ya existe.", "OK");
            }
            else
            {
                WorkShift workShift = new WorkShift(NameField.Text,checkIn,checkOut, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday);
                workShift.ValidFrom = ValidFromPicker.Date;
                workShift.ValidUntil = ValidUntilPicker.Date;
                //InsertWorkShift method here
                p.WorkShifts.Add(workShift);
                db.InsertLog(new Log("Añadir", Username + " ha añadido turno de trabajo: " + workShiftName + " - " + Now),p);
                p.SaveChanges();
                await DisplayAlert("Alert", "El turno se ha añadido correctamente.", "OK");
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 3));
            }
        }
        else
        {
            await DisplayAlert("Alert", "Debes introducir el nombre de turno", "OK");
        }
    }
    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        try
        {
                string workShiftName = NameField.Text;
                string CheckInHour = HourCheckInSelector.SelectedItem.ToString();
                string MinuteCheckIn = MinuteCheckInSelector.SelectedItem.ToString();
                string CheckInTime = CheckInHour + ":" + MinuteCheckIn;
                string CheckOutHour = HourCheckOutSelector.SelectedItem.ToString();
                string CheckOutMinute = MinuteCheckOutSelector.SelectedItem.ToString();
                string CheckOutTime = CheckOutHour + ":" + CheckOutMinute;
                DateTime checkIn = DateTime.Today.AddHours(Double.Parse(CheckInHour)).AddMinutes(Double.Parse(MinuteCheckIn));
                DateTime checkOut = DateTime.Today.AddHours(Double.Parse(CheckOutHour)).AddMinutes(Double.Parse(CheckOutMinute));
                bool update =db.UpdateWorkShift(WorkShift, workShiftName, checkIn, checkOut, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, ValidFromPicker.Date, ValidUntilPicker.Date, WorkShift.Enabled, WorkShift.Deleted, p);
            if (update == true)
            {
                db.InsertLog(new Log("Añadir", Username + " ha añadido turno de trabajo: " + workShiftName + " - " + Now),p);
                await DisplayAlert("Alert", "El turno se ha modificado correctamente.", "OK");
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 3));
            }
            
        }catch(Exception ex)
        {
            await DisplayAlert("Error", ex.ToString(), "Vale");
        }
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,3));
    }
    public void SetPickers()
    {
        HourCheckInSelector.ItemsSource = HourGenerate();
        HourCheckOutSelector .ItemsSource = HourGenerate();
        MinuteCheckInSelector.ItemsSource = MinuteGenerate();
        MinuteCheckOutSelector.ItemsSource = MinuteGenerate();
        HourCheckInSelector.SelectedIndex = 12;
        MinuteCheckInSelector.SelectedIndex = 00;
        HourCheckOutSelector.SelectedIndex = 12;
        MinuteCheckOutSelector.SelectedIndex = 00;
    }
    public List<string> HourGenerate()
    {
        var hourList = new List<string>();
        for (int i = 00; i < 24; i++)
        {
            if (i < 10)
            {
                hourList.Add("0" + i.ToString());
            }
            else
            {
                hourList.Add(i.ToString());
            }
        }
        return hourList;
    }
    public List<string> MinuteGenerate()
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
		if (monday.IsChecked){ Monday = true; } else { Monday = false; }
        if (tuesday.IsChecked) { Tuesday = true; } else { Tuesday = false; }
        if (wednesday.IsChecked) { Wednesday = true; } else { Wednesday = false; }
        if (thursday.IsChecked) { Thursday = true; } else { Thursday = false; }
        if (friday.IsChecked) { Friday = true; } else { Friday = false; }
        if (saturday.IsChecked) { Saturday = true; } else { Saturday = false; }
        if (sunday.IsChecked) { Sunday = true; } else { Sunday = false; }
    }
}