using System.Collections.Generic;
using System.Linq;


namespace ProiectPO;

public class AdminService
{
    private List<Vehicle> vehicles;
    private List<Rental> rentals;

    public AdminService(List<Vehicle> vehicles, List<Rental> rentals)
    {
        this.vehicles = vehicles;
        this.rentals = rentals;
    }

    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
    }

    public void UpdateVehicle(int id, string model, string brand, double pricePerHour)
    {
        foreach (var v in vehicles)
        {
            if (v.Id == id)
            {
                v.Model = model;
                v.Brand = brand;
                v.PricePerHour = pricePerHour;
            }
        }
    }

    public void RemoveVehicle(int id)
    {
        vehicles.RemoveAll(v => v.Id == id);
    }

    public void ChangeVehicleStatus(int id, VehicleStatus status)
    {
        foreach (var v in vehicles)
        {
            if (v.Id == id)
            {
                v.Status = status;
            }
        }
    }

    public List<Rental> GetRentalHistory()
    {
        return rentals;
    }
}