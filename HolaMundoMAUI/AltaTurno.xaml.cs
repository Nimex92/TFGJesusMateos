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
    /// <summary>
    /// Constructor that receives the username to persist the data
    /// and an option to enable create or update UI
    /// </summary>
    /// <param string="user"></param>
    /// <param int="opcion"></param>
    public AltaTurno(string username,int option)
	{
		InitializeComponent();
        //Set the Username to a global local use
        Username = username;
        //Set the UI Pickers with the data
        SetPickers();
        switch (option)
        {
            //In this case enable the create UI
            case 0:
                RegisterButton.IsVisible = true;
                UpdateButton.IsVisible = false;
                break;
            //In this case enable the update UI
            case 1:
                RegisterButton.IsVisible = false;
                UpdateButton.IsVisible = true;
                break;
        }
    }
    /// <summary>
    /// Constructor that receives the username, workShiftName to persist the data
    /// and a option to enable the create or update UI
    /// </summary>
    /// <param string="user"></param>
    /// <param string="workShiftName"></param>
    /// <param int="opcion"></param>
    public AltaTurno(string username,string workShiftName,int option)
    {
        InitializeComponent();
        //Set the Username to a global local use
        Username = username;
        //Fill the UI Pickers with data
        SetPickers();
        switch (option)
        {
            //In this case enable the create UI
            case 0:
                RegisterButton.IsVisible = true;
                UpdateButton.IsVisible = false;
                break;
            //In this case enable the update UI and fill Fields with update object data
            case 1:
                RegisterButton.IsVisible = false;
                UpdateButton.IsVisible = true;
                //Search a workShift with the name passed on the global parameter
                var workShift = p.WorkShifts.Where(x => x.Name == workShiftName).FirstOrDefault();
                //Match with the global object
                WorkShift = workShift;
                //Update the object attribute "Name"
                NameField.Text = workShift.Name;
                //Disenable the field to avoid unwanted PK changes
                NameField.IsEnabled = false;
                //Conveert to a string the field of the database
                string CheckInHour = workShift.CheckIn.ToString().Substring(10, 3);
                string CheckInMinute = workShift.CheckIn.ToString().Substring(14, 2);
                string CheckOutHour = workShift.CheckOut.ToString().Substring(10, 3);
                string CheckOutMinute = workShift.CheckOut.ToString().Substring(14 ,2);
                //Convert values to Int and Set the pickers
                HourCheckInSelector.SelectedIndex = int.Parse(CheckInHour);
                MinuteCheckInSelector.SelectedIndex = int.Parse(CheckInMinute);
                HourCheckOutSelector.SelectedIndex = int.Parse(CheckOutHour);
                MinuteCheckOutSelector.SelectedIndex = int.Parse(CheckOutMinute);
                //Set the datepickers with the object values
                ValidFromPicker.Date = workShift.ValidFrom;
                ValidUntilPicker.Date = workShift.ValidUntil;
                //Set the weekdays with the object value
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
    /// <summary>
    /// Mehod to add a new WorkShift to the DB
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        //Get the name for the object
        string workShiftName = NameField.Text;
        if (workShiftName is not null)
        {
            //Gets the hours for the new object
            string HourCheckIn = HourCheckInSelector.SelectedItem.ToString();
            string MinuteCheckIn = MinuteCheckInSelector.SelectedItem.ToString();
            string FinalCheckIn = HourCheckIn + ":" + MinuteCheckIn;
            string HourCheckOut = HourCheckOutSelector.SelectedItem.ToString();
            string MinuteCheckOut = MinuteCheckOutSelector.SelectedItem.ToString();
            string FinalCheckOut = HourCheckOut + ":" + MinuteCheckOut;
            DateTime checkIn = DateTime.Today.AddHours(Double.Parse(HourCheckIn)).AddMinutes(Double.Parse(MinuteCheckIn));
            DateTime checkOut = DateTime.Today.AddHours(Double.Parse(HourCheckOut)).AddMinutes(Double.Parse(MinuteCheckOut));
            //Search if the object exists on the DB
            var workGroupSearch = p.WorkShifts.Where(x => x.Name == workShiftName).FirstOrDefault();
            if (workGroupSearch is not null)
            {
                await DisplayAlert("Error", "El turno ya existe.", "OK");
            }
            else
            {
                //If not exists then add the new object
                WorkShift workShift = new WorkShift(NameField.Text,checkIn,checkOut, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday);
                workShift.ValidFrom = ValidFromPicker.Date;
                workShift.ValidUntil = ValidUntilPicker.Date;
                //InsertWorkShift method here
                p.WorkShifts.Add(workShift);
                db.InsertLog(new Log("Añadir", Username + " ha añadido turno de trabajo: " + workShiftName + " - " + Now),p);
                p.SaveChanges();
                await DisplayAlert("Sucess", "El turno se ha añadido correctamente.", "OK");
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 3));
            }
        }
        else
        {
            await DisplayAlert("Error", "Debes introducir el nombre de turno", "OK");
        }
    }
    /// <summary>
    /// Method to update an exist WorkShift on DB
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        try
        {
                //Get the workshift name from the UI
                string workShiftName = NameField.Text;
                //Get the time parameters from the UI
                string CheckInHour = HourCheckInSelector.SelectedItem.ToString();
                string MinuteCheckIn = MinuteCheckInSelector.SelectedItem.ToString();
                string CheckInTime = CheckInHour + ":" + MinuteCheckIn;
                string CheckOutHour = HourCheckOutSelector.SelectedItem.ToString();
                string CheckOutMinute = MinuteCheckOutSelector.SelectedItem.ToString();
                string CheckOutTime = CheckOutHour + ":" + CheckOutMinute;
                //Convert it to DateTime 
                DateTime checkIn = DateTime.Today.AddHours(Double.Parse(CheckInHour)).AddMinutes(Double.Parse(MinuteCheckIn));
                DateTime checkOut = DateTime.Today.AddHours(Double.Parse(CheckOutHour)).AddMinutes(Double.Parse(CheckOutMinute));
                bool update =db.UpdateWorkShift(WorkShift, workShiftName, checkIn, checkOut, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, ValidFromPicker.Date, ValidUntilPicker.Date, WorkShift.Enabled, WorkShift.Deleted, p);
            if (update == true)
            {
                db.InsertLog(new Log("Añadir", Username + " ha añadido turno de trabajo: " + workShiftName + " - " + Now),p);
                await DisplayAlert("Success", "El turno se ha modificado correctamente.", "OK");
                App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username, 3));
            }
            
        }catch(Exception ex)
        {
            await DisplayAlert("Error", ex.ToString(), "Vale");
        }
    }
    /// <summary>
    /// Method to back to the last screen of the app
    /// </summary>
    /// <param object="sender"></param>
    /// <param EventArgs="e"></param>
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new PaginaAdmin(Username,3));
    }
    /// <summary>
    /// Method to fill the UI time pickers with data
    /// and select a default value
    /// </summary>
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
    /// <summary>
    /// Method to generate an 24 hours interval and add it to a List
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Method to generate an 60 Minute interval and add it to a List
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Method to check the changes of the checkbox of the UI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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