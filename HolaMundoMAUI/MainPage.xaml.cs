using ClassLibray;
using Persistencia;
using System.Diagnostics;

namespace HolaMundoMAUI;

public partial class MainPage : ContentPage
{	
	PresenciaContext p = new PresenciaContext();
	DateTime dt = DateTime.Now;
	DateTime Today = DateTime.Today;
	Db db = new Db();
	public MainPage()
	{
		InitializeComponent();
		CheckWorkShifts();	
	}
	/// <summary>
	/// Checks if the workShifts are enabled and if the validUntil date overpass the actual date it
	/// sets the workShift as deleted
	/// </summary>
	public void CheckWorkShifts()
    {
		var Turnos = p.WorkShifts.ToList();
		foreach(WorkShift t in Turnos)
        {
            if (t.ValidFrom<=Today)
            {
				t.Enabled = true;
            }
			if(t.ValidUntil<Today)
            {
				t.Enabled = false;
				t.Deleted = true;
			}
			p.SaveChanges();
		}
		
    }
	/// <summary>
	/// Check if the credentials exists on the DB and if exist go to the workerPage or the adminPage
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public async void ChangeToSigningScreen(object sender, EventArgs e)
    {
		try
		{
			string NombreUsuario = NameField.Text;
			string ContrasenaUsuario = PasswordField.Text;
			var usuario = p.Users.Where(b => b.Username == NombreUsuario && b.Password == ContrasenaUsuario).FirstOrDefault();
			await GoToSigningButton.ScaleTo(1.3, 500, Easing.BounceIn);
			await GoToSigningButton.ScaleTo(1.0, 100, Easing.BounceOut);
			if (usuario is not null)
			{
				if (usuario.esAdmin == true)
				{
					CuerpoLogin.Background = Color.FromRgba("#1b5e3b");
					GoToSigningButton.Background = Color.FromRgba("#1b5e3b");
					ForgotPassword.Background = Color.FromRgba("#1b5e3b");
					await CuerpoLogin.TranslateTo(2000, 0, 1500);
					App.Current.MainPage = new NavigationPage(new PaginaAdmin(NombreUsuario));
					db.InsertLog(new Log("Login", NombreUsuario + " ha iniciado sesion - " + dt),p);
				}
				else
				{
					CuerpoLogin.Background = Color.FromRgba("#1b5e3b");
					GoToSigningButton.Background = Color.FromRgba("#1b5e3b");
					ForgotPassword.Background = Color.FromRgba("#1b5e3b");
					await CuerpoLogin.TranslateTo(-2000, 0, 1500);
					App.Current.MainPage = new NavigationPage(new PaginaFichar(NombreUsuario));
					db.InsertLog(new Log("Login", NombreUsuario + " ha iniciado sesion - " + dt),p);
				}
			}
			else
			{
				CuerpoLogin.Background = Color.FromRgba("#5e1b1b");
				GoToSigningButton.Background = Color.FromRgba("#5e1b1b");
				ForgotPassword.Background = Color.FromRgba("#5e1b1b");

				await CuerpoLogin.TranslateTo(-20, 0, 200);
				await CuerpoLogin.TranslateTo(20, 0, 200);
				await CuerpoLogin.TranslateTo(-20, 0, 200);
				await CuerpoLogin.TranslateTo(20, 0, 200);
				await CuerpoLogin.TranslateTo(-20, 0, 200);
				await  CuerpoLogin.TranslateTo(20, 0, 200);
				await CuerpoLogin.TranslateTo(0, 0, 200);
				CuerpoLogin.Background = Color.FromRgba("#2B282D");
				GoToSigningButton.Background = Color.FromRgba("#2B282D");
				ForgotPassword.Background = Color.FromRgba("#2B282D");


				DisplayAlert("Error", "El usuario o la contraseña son incorrectos.", "Vale");
			}
		}catch(NullReferenceException ex)
        {
			Debug.WriteLine(ex.ToString());
        }
	}

    private void ForgotPassword_Clicked(object sender, EventArgs e)
    {

    }
}