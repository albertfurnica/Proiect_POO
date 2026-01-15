namespace ProiectPO;

public abstract class Vehicle
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public double PricePerHour { get; set; }
    public int Year { get; set; }
    public int Capacity { get; set; }
    public VehicleStatus Status { get; set; }

    public Vehicle(int id, string model, string brand, double pricePerHour, int year, int capacity)
    {
        Id = id;
        Model = model;
        Brand = brand;
        PricePerHour = pricePerHour;
        Year = year;
        Capacity = capacity;
        Status = VehicleStatus.Unavailable;
    }

    public void Reserve()
    {
        if (Status != VehicleStatus.Available)
            throw new InvalidOperationException("Vehiculul nu este disponibil pentru rezervare.");
        Status = VehicleStatus.Reserved;
    }

    public void Rent()
    {
        if (Status != VehicleStatus.Reserved)
            throw new InvalidOperationException("Vehiculul trebuie sa fie rezervat inainte de a fi inchiriat.");
        Status = VehicleStatus.Rented;
    }

    public void SendToService()
    {
        if (Status == VehicleStatus.Rented)
            throw new InvalidOperationException("Un vehicul inchiriat nu poate fi trimis in service.");
        Status = VehicleStatus.InService;
    }

    public void MarkUnavailable()
    {
        Status = VehicleStatus.Unavailable;
    }

    public void Release()
    {
        if (Status == VehicleStatus.InService || Status == VehicleStatus.Unavailable)
            throw new InvalidOperationException("Vehiculul nu poate fi scos din aceasta stare.");
        Status = VehicleStatus.Available;
    }

    public void ReturnFromService()
    {
        if (Status != VehicleStatus.InService)
            throw new InvalidOperationException("Vehiculul nu este in service.");
        Status = VehicleStatus.Available;
    }
}