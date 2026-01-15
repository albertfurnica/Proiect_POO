using System;

namespace ProiectPO;

public class Car : Vehicle
{
    public int NumberOfDoors { get; set; }
    public string FuelType { get; set; }
    public bool HasAirConditioning { get; set; }

    public Car(
        int id,
        string model,
        string brand,
        int year,
        int capacity,
        double pricePerHour,
        int numberOfDoors,
        string fuelType,
        bool hasAirConditioning)
        : base(id, model, brand, pricePerHour,year,capacity)
    {
        NumberOfDoors = numberOfDoors;
        FuelType = fuelType;
        HasAirConditioning = hasAirConditioning;
        
        if (numberOfDoors != 2 && numberOfDoors != 4)
            throw new ArgumentException("Mașina trebuie să aibă 2 sau 4 uși.");

        if (string.IsNullOrWhiteSpace(fuelType))
            throw new ArgumentException("Tipul de combustibil este obligatoriu.");
    }

    public override string ToString()
    {
        string doorsText = NumberOfDoors == 2 ? "2 uși" : "4 uși";

        return $"{Model} ({Year}) - {FuelType}, {doorsText}, " +
               $"Aer condiționat: {(HasAirConditioning ? "Da" : "Nu")}, " +
               $"Status: {Status}";
    }
}
