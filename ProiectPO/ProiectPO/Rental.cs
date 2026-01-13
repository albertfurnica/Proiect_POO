namespace ProiectPO;

public class Rental
{
    public int Id { get; set; }
    public Reservation Reservation { get; set; }
    public double TotalCost { get; set; }

    public Rental(int id, Reservation reservation, double totalCost)
    {
        Id = id;
        Reservation = reservation;
        TotalCost = totalCost;
    }
}