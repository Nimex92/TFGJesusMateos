
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using ClassLibray;

namespace HolaMundoMAUI;

public partial class PaginaFichar : ContentPage
{
	string Username;
	Worker Worker;
	bool activeCalendar,activeMain;
	Db db = new Db();
	public IDispatcherTimer MyTimer { get; set; }
	PresenciaContext p = new PresenciaContext();
	List<DateTime> CheckInList = new List<DateTime>();
	List<DateTime> CheckOutList = new List<DateTime>();
	DateTime Now = DateTime.Now;
	DateTime CheckIn;
	Day Day;
	public PaginaFichar(string username)
	{
		InitializeComponent();
		SigningsCheck(username);
		WorkTasksCheck(username);
		ClockStart();
		
		Username = username;
		var user = p.Users.Where(x=>x.Username.ToLower()==username.ToLower()).FirstOrDefault();
		LabelNameUser.Text = user.Username;
		activeCalendar = false;
		if (WorkTaskSelector.IsVisible == false)
		{
			WorkTaskStartButton.IsVisible = false;
			WorkTaskEndButton.IsVisible = false;
		}
		var worker = p.Workers
			.Where(x => x.User.Username == username)
			.Include(x => x.WorkGroup)
			.Include(x => x.User)
			.FirstOrDefault();
		Worker = worker;

		var workGroup = p.WorkGroups
			.Where(x => x.Workers.Contains(worker))
			.Include(x=>x.WorkShifts)
			.Include(x => x.Tasks)
			.FirstOrDefault();
		var tasks = workGroup.Tasks.ToList(); 
		var turnos = workGroup.WorkShifts.ToList();
		WorkShift workShiftActual = new WorkShift();
		foreach (WorkTask t in tasks)
		{
			WorkTaskSelector.Items.Add(t.Name);
		}
		foreach (WorkShift workShifth in turnos)
        {
            List<string> days = WorkDays(workShifth);
			bool workDay = false;
			var todayIs = Now.DayOfWeek.ToString();
            if (days.Contains(todayIs)) { workDay = true; }
			if (workShifth.CheckIn < Now && workShifth.CheckOut < Now && workShifth.Enabled == true && workDay == true)
			{
				CheckInList.Add(workShifth.CheckIn);
				CheckOutList.Add(workShifth.CheckOut);
			}
		}
		DaysListViewSet();
		WorkTaskSelector.SelectedIndex = 0;
		WorkTaskStartButton.IsVisible = false;
	}

	private async void LogoutButton_Clicked(object sender, EventArgs e)
	{
		LogoutButton.BackgroundColor = Color.FromRgba("#2B282D");
		bool answer = await DisplayAlert("Logout", "¿Deseas cerrar sesión?", "Si", "No");
		if (answer == true)
		{
			App.Current.MainPage = new NavigationPage(new MainPage());
		}
	}
	public void ClockStart()
    {
		MyTimer = Clock.Dispatcher.CreateTimer();
		MyTimer.Interval = TimeSpan.FromSeconds(1);
		MyTimer.IsRepeating = true;
		MyTimer.Tick += (x, y) =>
		{
			DateTime dt = DateTime.Now;

			Clock.Text =
				dt.Hour.ToString("00") + ":" +
				dt.Minute.ToString("00") + ":" +
				dt.Second.ToString("00");
		};

		MyTimer.Start();
	}
	private void SigningsCheck(string username)
	{
		var worker = p.SignedWorkers.Where(x => x.Worker.User.Username == username).FirstOrDefault();
		if (worker is not null)
		{
			CheckInButton.IsVisible = false;
			CheckOutButton.IsVisible = true;
			WorkTaskSelector.IsVisible = true;
			WorkTasksCheck(username);
		}
		else
		{
			CheckInButton.IsVisible = true;
			CheckOutButton.IsVisible = false;
			WorkTaskSelector.IsVisible = false;
		}
	}
	private void WorkTasksCheck(string username)
	{
		var worker = p.StartedTasks.Where(x => x.Worker.User.Username == username).Include(x => x.Worker).Include(x => x.Task).FirstOrDefault();
		if (worker is not null)
		{
			WorkTaskEndButton.IsVisible = true;
			WorkTaskStartButton.IsVisible = false;
		}
		else
		{
			WorkTaskEndButton.IsVisible = false;
			WorkTaskStartButton.IsEnabled = false;
		}
	}
	public void GoToMainPage(object sender, EventArgs e)
	{
		App.Current.MainPage = new NavigationPage(new MainPage());
		db.InsertLog(new Log("Logout", Username + " ha cerrado sesion - "+Now),p);
	}
	private async void BotonFichar_Clicked(object sender, EventArgs e)
	{
		var worker = p.Workers.Where(x => x.User.Username == Username).Include(x => x.WorkGroup).FirstOrDefault();
		Signing signing = new Signing(worker, Now, "Entrada");
		db.InsertSigning(signing, p);
        p.SignedWorkers.Add(new SignedWorker(worker, signing));
        p.SaveChanges();
        CheckInButton.IsVisible = false;
		CheckOutButton.IsVisible = true;
		WorkTaskSelector.IsVisible = true;
		WorkTaskStartButton.IsVisible = true;
		foreach (DateTime d in CheckInList)
		{
			if (Now < d)
			{
				var operacion = (d - Now);
				string motivo = await DisplayPromptAsync("Usted llega temprano.", "¿Cual es la razon?");
				db.InsertLog(new Log("Temprano", "El trabajador " + worker.CardNumber + " Ha llegado " + operacion + "m antes. - " + Now), p);
			}
			if (Now > d.AddMinutes(5))
			{
				var operacion = (Now - d);
				string motivo = await DisplayPromptAsync("Usted llega tarde.", "¿Cual es la razon?");
				db.InsertLog(new Log("Retraso", "El trabajador " + worker.CardNumber + " Ha llegado " + operacion + "m tarde debido a " + motivo + " - " + Now), p);
				db.InsertIssue(new Issue(worker,"Retraso de"+operacion,Now,false),p);
			}
			if (Now > d && Now <= d.AddMinutes(5))
			{
				db.InsertLog(new Log("En hora", "El trabajador " + worker.CardNumber + " Ha llegado a tiempo - " + Now), p);
			}
		}
	}
	private async void BotonPlegar_Clicked(object sender, EventArgs e)
		{
			var worker = p.Workers.Where(x => x.User.Username == Username).Include(x => x.WorkGroup).FirstOrDefault();
			db.InsertSigning(new Signing(worker, Now, "Salida"), p);
			var SignedWorker = p.SignedWorkers.Where(x => x.Worker == worker).FirstOrDefault();
			db.DeleteSignedWorker(SignedWorker, p);
			CheckInButton.IsVisible = true;
			CheckOutButton.IsVisible = false;
			WorkTaskSelector.IsVisible = false;
			if (WorkTaskSelector.IsVisible == false)
			{
				WorkTaskStartButton.IsVisible = false;
				WorkTaskEndButton.IsVisible = false;
			}
			foreach (DateTime date in CheckOutList) {
				if (Now < date)
				{
					var difference = (date - Now);
					string reason = await DisplayPromptAsync("Esta saliendo antes de hora", "¿Cual es la razon?");
					db.InsertLog(new Log("Temprano", "El trabajador " + worker.CardNumber + " Ha salido " + difference + "m antes. debido a " + reason + " - " + Now), p);
				}
				if (Now > date.AddMinutes(5))
				{
					var difference = (Now - date);
					string reason = await DisplayPromptAsync("Usted Sale tarde.", "¿Cual es la razon?");
					db.InsertLog(new Log("Retraso", "El trabajador " + worker.CardNumber + " Ha salido " + difference + "m tarde. - " + Now),p);
				}
				if (Now > date && Now <= date.AddMinutes(5))
				{
					db.InsertLog(new Log("En hora", "El trabajador " + worker.CardNumber + " Ha llegado a tiempo - " + Now),p);
				}
			}
	}
	private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
		{
			var searchWorkTask = WorkTaskSelector.SelectedItem.ToString();
			var worktask = p.WorkTasks.Where(x => x.Name == searchWorkTask).FirstOrDefault();
			var startedWorkTask = p.StartedTasks.Where(x => x.Task.Name == worktask.Name && x.TastStart.Date == Now.Date).OrderBy(x => x.TastStart).LastOrDefault();

			if (startedWorkTask is not null)
			{
				WorkTaskEndButton.IsVisible = true;
				WorkTaskStartButton.IsVisible = false;
			}
			else
			{
				WorkTaskEndButton.IsVisible = false;
				WorkTaskStartButton.IsVisible = false;
			}
		}
	private async void BotonIniciarTarea_Clicked(object sender, EventArgs e)
		{
			WorkTaskEndButton.IsVisible = true;
			WorkTaskStartButton.IsVisible = false;
			CheckIn = Now;

			if (WorkTaskSelector.SelectedItem.ToString() is not null)
			{
				var searchWorkTask = WorkTaskSelector.SelectedItem.ToString();
				var workTask = p.WorkTasks.Where(x => x.Name == searchWorkTask).FirstOrDefault();
				db.InsertStartedTask(new StartedTask(workTask, Worker, Now),p);
				db.InsertLog(new Log("Tareas", Username + " ha iniciado tarea " + workTask.Name + " - " + Now),p);
				p.SaveChanges();
		}
			else
			{
				await DisplayAlert("Alert", "Por favor, Selecciona una tarea", "OK");
			}

		}
	private void BotonAcabarTarea_Clicked(object sender, EventArgs e)
	{
		try
		{
			WorkTaskEndButton.IsVisible = false;
			WorkTaskStartButton.IsVisible = true;
			var searchWorkTask = WorkTaskSelector.SelectedItem.ToString();
			var workTask = p.WorkTasks.Where(x => x.Name == searchWorkTask).FirstOrDefault();
			var startedWorkTask = p.StartedTasks.Where(x => x.Task.Name == workTask.Name).Where(x => x.TastStart.Date == Now.Date).OrderBy(x => x.TastStart).Last();
			var totalHours = (DateTime.Now - startedWorkTask.TastStart).TotalHours;
			bool onTime = false;
			if (totalHours <= workTask.ElapsedTime)
			{
				onTime = true;
			}
			db.InsertEndedTask(new EndedTask(workTask, Worker, startedWorkTask.TastStart, Now, totalHours, onTime), p);
			db.DeleteStartedTask(startedWorkTask, p);
			db.InsertLog(new Log("Tareas", Username + " ha finalizado tarea " + workTask.Name + " - " + Now), p);
		}catch(Exception ex)
        {

        }
	}
    private void DayListView_Selected(object sender, SelectedItemChangedEventArgs e)
    {
		Day item = e.SelectedItem as Day;
		Day = item;
	}
    private void VacationRequestButton_Clicked(object sender, EventArgs e)
    {
		var trab = Worker;
		App.Current.MainPage = new NavigationPage(new AnadeDiaCalendario(Username,trab));
    }
    private void CalendarButton_Clicked(object sender, EventArgs e)
    {
			if(activeCalendar== true) { 
				ListViewCalendar.IsVisible = false;
				MainBody.IsVisible = true;
				Calendar.BackgroundColor = Color.FromRgba("#2B282D");
				VacationRequest.BackgroundColor = Color.FromRgba("#2B282D");
				activeCalendar = false;
			}
			else 
			{ 
				ListViewCalendar.IsVisible = true;
				MainBody.IsVisible = false;;
				Calendar.BackgroundColor = Color.FromRgba("#93778B");
				VacationRequest.BackgroundColor = Color.FromRgba("#2B282D");
				activeCalendar = true;
			}
	}
	private void DaysListViewSet()
    {
		var calendarSearch = p.Calendars.Where(x => x.Worker == Worker).Include(x => x.DaysOnCalendar).FirstOrDefault();
		if (calendarSearch is not null)
		{
			var dayList = calendarSearch.DaysOnCalendar.ToList();
			ListViewCalendario.ItemsSource = dayList;
			if (dayList.Count > 0)
				ListViewCalendario.SelectedItem = dayList[0];
		}
	}
    private void VolverATrabajadores_Clicked(object sender, EventArgs e)
    {
		switch (activeMain)
		{
			case true:
				ListViewCalendar.IsVisible = false;
				MainBody.IsVisible = true;
				activeMain = false;
				activeCalendar = false;
				break;
			case false:
				ListViewCalendar.IsVisible = false;
				MainBody.IsVisible = true;
				activeMain = true;
				activeCalendar = true;
				break;
		}
	}
	private string WhatDayIsIt()
    {
		string day = Now.DayOfWeek.ToString();
		return day;
    }
    private List<string> WorkDays(WorkShift t)
    {
		List<string> days = new List<string>();
		if (t.Monday == true) { days.Add("Monday"); }
		if (t.Tuesday == true) { days.Add("Tuesday"); }
		if (t.Wednesday == true) { days.Add("Wednesday"); }
		if (t.Thursday == true) { days.Add("Thursday"); }
		if (t.Friday == true) { days.Add("Friday"); }
		if (t.Saturday == true) { days.Add("Saturday"); }
		if (t.Domingo == true) { days.Add("Sunday"); }
		return days;
	}
}