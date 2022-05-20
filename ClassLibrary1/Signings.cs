
namespace ClassLibray
{
    public class Signing
    {
        public Signing()
        {

        }

        public int Id { get; set; }
        public Worker Worker { get; set; }
        public DateTime SigningDate { get; set; }
        public string CheckInCheckOut { get; set; }

        public Signing(Worker worker, DateTime signingDate, string checkInCheckOut)
        {
            this.Worker = worker;
            this.SigningDate = signingDate;
            this.CheckInCheckOut = checkInCheckOut;
        }
        public static List<DateTime> GetFechas(List<Signing> dateList)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach(Signing signings in dateList)
            {
                dates.Add(signings.SigningDate.Date);
            }
            return dates;
        }
    }
}