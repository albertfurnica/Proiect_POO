using System;
using System.Collections.Generic;
using System.Linq;

namespace ProiectPO
{
    public class ReservationService
    {
        private List<Reservation> reservations;
        private List<Rental> rentals;
        private List<Vehicle> vehicles;

        public ReservationService(List<Vehicle> vehicles)
        {
            this.vehicles = vehicles;
            reservations = new List<Reservation>();
            rentals = new List<Rental>();
        }

        public Reservation CreateReservation(
            Utilizator user,
            int vehicleId,
            DateTime startDate,
            DateTime endDate)
        {
            Vehicle vehicle = vehicles.FirstOrDefault(v => v.Id == vehicleId);

            if (vehicle == null)
                throw new Exception("Vehiculul nu exista.");

            if (vehicle.Status != VehicleStatus.Available)
                throw new Exception("Vehiculul nu este disponibil.");

            vehicle.Reserve();

            Reservation reservation = new Reservation(
                reservations.Count + 1,
                user,
                vehicle,
                startDate,
                endDate
            );

            reservations.Add(reservation);
            return reservation;
        }

        public Rental ConfirmRental(int reservationId)
        {
            Reservation reservation = reservations
                .FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null)
                throw new Exception("Rezervarea nu exista.");

            reservation.Vehicle.Rent();

            double hours =
                (reservation.EndDate - reservation.StartDate).TotalHours;

            double totalCost =
                hours * reservation.Vehicle.PricePerHour;

            Rental rental = new Rental(
                rentals.Count + 1,
                reservation,
                totalCost
            );

            rentals.Add(rental);
            return rental;
        }

        public void CancelReservation(int reservationId)
        {
            Reservation reservation =
                reservations.FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null)
                throw new Exception("Rezervarea nu exista.");

            if ((reservation.StartDate - DateTime.Now).TotalHours < 24)
                throw new Exception("Rezervarea nu mai poate fi anulata.");

            reservation.Vehicle.Release();
            reservations.Remove(reservation);
        }

        public List<Reservation> GetReservationsForUser(Utilizator user)
        {
            return reservations
                .Where(r => r.User.Id == user.Id)
                .ToList();
        }

        public List<Rental> GetAllRentals()
        {
            return rentals;
        }
    }
}
