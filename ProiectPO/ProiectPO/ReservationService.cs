namespace ProiectPO;

public class ReservationService
{
    private List<Reservation> reservations = new List<Reservation>();

    public void AddReservation(Reservation reservation)
    {
        reservations.Add(reservation);
    }

    public List<Reservation> GetAllReservations()
    {
        return reservations;
    }
}