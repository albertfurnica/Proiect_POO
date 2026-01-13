namespace ProiectPO;

public class Reservation
{
    public int Id { get; set; }
    public Utilizator User { get; set; }
    public Vehicle Vehicle { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Reservation(int id, Utilizator user, Vehicle vehicle, DateTime startDate, DateTime endDate)
    {
        Id = id;
        User = user;
        Vehicle = vehicle;
        StartDate = startDate;
        EndDate = endDate;
    }
}