using ClassLibray;
using Persistencia;
using System.Diagnostics;
using Color = Microsoft.Maui.Graphics.Color;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

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
        GeneratePayrollsIfDayOne();
        SeedData();
	}
    public void SeedData()
    {
        var firstUser = p.Workers.Where(x => x.Name == "Nimex").FirstOrDefault();
        if (firstUser is null)
        {
            p.Workers.Add(new Worker("Jesus", new WorkGroup("Test team"), new User("Nimex", "1")));
            p.SaveChanges();
        }
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

    public void ForgotPassword_Clicked(object sender, EventArgs e)
    {
        PresenciaContext p = new PresenciaContext();
        Db db = new Db();
        var worker = p.Workers.Where(x => x.Name == NameField.Text).FirstOrDefault();
        db.InsertIssue(new Issue(worker,"Solicita cambio de contraseña",dt,false),p);
    }
	public void GeneratePayrollsIfDayOne()
    {
		if(DateTime.Now.Day == 14)
        {
            var Workers = p.Workers.ToList();
            Workers.ForEach(x =>
            {
                GeneratePayrollToPDF(x);
            });
        }
    }
    /// <summary>
    /// Method to generate a worker's payroll
    /// </summary>
    /// <param Worker="worker"></param>
    public void GeneratePayrollToPDF(Worker worker)
    {
        try
        {
            string ruta = "C:\\xampp\\htdocs\\Nominas\\" + worker.Name + "\\" + worker.Name + dt.Date + ".pdf";
            PdfWriter writer = new PdfWriter(ruta);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Nomina mensual")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
               .SetFontSize(20);
            BetterTable DatosEmpresa = NewTable(3, 2);
            DatosEmpresa.SetColorHeader(ColorConstants.LIGHT_GRAY);
            DatosEmpresa.ChangeTableFontSize(8);
            DatosEmpresa.SetNextText("Empresa");
            DatosEmpresa.SetNextText("Direccion");
            DatosEmpresa.SetNextText("CIF");
            DatosEmpresa.SetNextText("TTI Ventures");
            DatosEmpresa.SetNextText(" C. Paletillas,6,Calahorra");
            DatosEmpresa.SetNextText("B26557397");
            BetterTable DatosTrabajador = NewTable(5, 2);
            DatosTrabajador.SetColorHeader(ColorConstants.LIGHT_GRAY);
            DatosTrabajador.ChangeTableFontSize(8);
            DatosTrabajador.SetNextText("Trabajador");
            DatosTrabajador.SetNextText("Categoria");
            DatosTrabajador.SetNextText("N� Matricula");
            DatosTrabajador.SetNextText("Antiguedad");
            DatosTrabajador.SetNextText("D.N.I");
            DatosTrabajador.SetNextText(worker.Name);
            DatosTrabajador.SetNextText(worker.Category);
            DatosTrabajador.SetNextText("");
            DatosTrabajador.SetNextText((dt - worker.HiringDate).ToString());
            DatosTrabajador.SetNextText(worker.Nif);
            BetterTable OtrosDatos = NewTable(7, 2);
            OtrosDatos.SetColorHeader(ColorConstants.LIGHT_GRAY);
            OtrosDatos.ChangeTableFontSize(8);
            OtrosDatos.SetNextText("N� Afiliación S.S");
            OtrosDatos.SetNextText("Tarifa");
            OtrosDatos.SetNextText("Cod C.T.");
            OtrosDatos.SetNextText("Sección");
            OtrosDatos.SetNextText("Nro.");
            OtrosDatos.SetNextText("Periodo");
            OtrosDatos.SetNextText("Días");
            OtrosDatos.SetNextText(worker.SocialSecurityCard);
            var diasTrabajador = p.Signings.Where(x => x.Worker == worker).Where(x => x.CheckInCheckOut == "Entrada").OrderBy(x => x.SigningDate).ToList();
            int diastotal = diasTrabajador.Count;
            var lapso = diasTrabajador.First().SigningDate.Date + "-" + diasTrabajador.Last().SigningDate.Date;
            OtrosDatos[1, 6].SetText(lapso.ToString());
            OtrosDatos[1, 5].SetText(diastotal.ToString());
            BetterTable CuerpoNomina = NewTable(5, 8);
            CuerpoNomina.SetColorHeader(ColorConstants.LIGHT_GRAY);
            CuerpoNomina.ChangeTableFontSize(10);
            CuerpoNomina.RemoveBorder(1);
            CuerpoNomina.AddTableBorder(1);
            CuerpoNomina.SetNextText("Cuantía");
            CuerpoNomina.SetNextText("Precio");
            CuerpoNomina.SetNextText("Concepto");
            CuerpoNomina.SetNextText("Devengos");
            CuerpoNomina.SetNextText("Deducciones");
            BetterTable PieNomina1 = NewTable(7, 2);
            PieNomina1.SetColorHeader(ColorConstants.LIGHT_GRAY);
            PieNomina1.ChangeTableFontSize(8);
            PieNomina1.SetNextText("Rem. Total");
            PieNomina1.SetNextText("P.P. Extras");
            PieNomina1.SetNextText("Base S.S.");
            PieNomina1.SetNextText("Base A.T y DES");
            PieNomina1.SetNextText("Base I.R.P.F");
            PieNomina1.SetNextText("T. Devengo");
            PieNomina1.SetNextText("T. a deducir");
            Paragraph SubPieNomina1 = new Paragraph("* Percepciones salariales sujetas a Cot. S.S.\t\t\t\t\t\t\t\t *Percepciones no salariales excluidas Cot. S.S.")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
               .SetFontSize(6);
            BetterTable PieNomina2 = NewTable(4, 5);
            PieNomina2.ChangeTableFontSize(6);
            PieNomina2.RemoveBorder(1);
            PieNomina2.AddTableBorder(1);
            PieNomina2.SetNextText("Fecha");
            PieNomina2.SetNextText("Sello empresa");
            PieNomina2.SetNextText("Recibi");
            PieNomina2[0, 0].SetText("16 de Mayo de 2022");
            PieNomina2[4, 0].SetText("SWIFT/BIC: 789456258");
            PieNomina2[4, 0].SetText("IBAN: ES45678912324");
            PieNomina2[4, 0].SetTextAlign("START");
            PieNomina2[1, 3].SetText("Total a percibir");
            PieNomina2[1, 3].AddAllBorders();
            PieNomina2[2, 3].SetText("1245.74€");
            PieNomina2[2, 3].AddAllBorders();
            PieNomina2.AddTableBorder(1);
            BetterTable CabeceraPieNomina3 = NewTable(1, 1);
            CabeceraPieNomina3.ChangeTableFontSize(8);
            CabeceraPieNomina3.RemoveBorder(2);
            CabeceraPieNomina3.TableConjunctionDown();
            CabeceraPieNomina3.SetNextText("DETERMINACION DE LA B. DE COTIZACION A LA S.S. Y CONCEPTOS DE RECAUCACION CONJUNTA Y APORTACION A LA EMPRESA");
            BetterTable PieNomina3 = NewTable(5, 8);
            PieNomina3.TableConjunctionUp();
            PieNomina3.ChangeTableFontSize(8);
            PieNomina3.RemoveBorder(1);
            PieNomina3.AddTableBorder(1);
            PieNomina3[0, 0].SetText("Concepto");
            PieNomina3[0, 0].SetTextAlign("END");
            PieNomina3[0, 2].SetText("Base");
            PieNomina3[0, 3].SetText("Tipo");
            PieNomina3[0, 4].SetText("Aportacion Empresarial");
            PieNomina3[1, 0].SetText("1. Contingencias comunes");
            PieNomina3[1, 1].SetText("......................");
            PieNomina3[1, 2].SetText("1475.00");
            PieNomina3[1, 3].SetText("23.60");
            PieNomina3[1, 4].SetText("348.10");
            PieNomina3[2, 0].SetText("2. Contingencias profesionales");
            PieNomina3[2, 1].SetText("AT Y EP................");
            PieNomina3[2, 2].SetText("1475.00");
            PieNomina3[2, 3].SetText("1.00");
            PieNomina3[2, 4].SetText("14.75");
            PieNomina3[3, 0].SetText(" y conceptos de reaudacion");
            PieNomina3[3, 1].SetText("Desempleo--............");
            PieNomina3[3, 2].SetText("1475.00");
            PieNomina3[3, 3].SetText("5.50");
            PieNomina3[3, 4].SetText("81.13");
            PieNomina3[4, 0].SetText("Conjunta");
            PieNomina3[4, 1].SetText("Formacion profesional..");
            PieNomina3[4, 2].SetText("1475.00");
            PieNomina3[4, 3].SetText("5.50");
            PieNomina3[4, 4].SetText("71.13");
            PieNomina3[5, 1].SetText("Fondo garantia salarial");
            PieNomina3[5, 2].SetText("1475.00");
            PieNomina3[5, 3].SetText("0.62");
            PieNomina3[5, 4].SetText("5.85");
            PieNomina3[6, 0].SetText("3. Cotizaci�n horas extraordinarias");
            PieNomina3[6, 1].SetText(".................");
            PieNomina3[6, 2].SetText(".................");
            PieNomina3[6, 3].SetText(".................");
            PieNomina3[6, 4].SetText(".................");

            document.Add(header);
            document.Add(DatosEmpresa.Table);
            document.Add(DatosTrabajador.Table);
            document.Add(OtrosDatos.Table);
            document.Add(CuerpoNomina.Table);
            document.Add(PieNomina1.Table);
            document.Add(SubPieNomina1);
            document.Add(PieNomina2.Table);
            document.Add(CabeceraPieNomina3.Table);
            document.Add(PieNomina3.Table);
            document.Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param int="numeroColumnas"></param>
    /// <param int="numeroFilas"></param>
    /// <returns></returns>
    public BetterTable NewTable(int numeroColumnas, int numeroFilas)
    {
        BetterTable b = new BetterTable(numeroColumnas, numeroFilas);
        return b;
    }

}