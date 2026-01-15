namespace ProiectPO;

class Program
{
    static List<Vehicle> vehicles = new List<Vehicle>();
    static List<User> users = new List<User>();
    static AdminService adminService;
    static ReservationService reservationService;
    static User currentUser;

    static void Main(string[] args)
    {
        users.Add(new Admin(1, "admin", "admin123"));
        users.Add(new Utilizator(2, "client", "client123"));

        var car1 = new Car(101, "golf", "volkswagen", 2020, 5, 50, 4, "Benzina", true);
        var car2 = new Car(102, "logan", "dacia", 2019, 5, 30, 4, "Benzina", true);
        var car3 = new Car(103, "m4", "bmw", 2022, 4, 150, 2, "Benzina", true);

        car1.Status = VehicleStatus.Available;
        car2.Status = VehicleStatus.Available;
        car3.Status = VehicleStatus.Available;

        vehicles.Add(car1);
        vehicles.Add(car2);
        vehicles.Add(car3);
        
        reservationService = new ReservationService(vehicles);
        adminService = new AdminService(vehicles, reservationService.GetAllRentals());

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("===SISTEM INCHIRIERI AUTO===");
            Console.WriteLine("1. Autentificare");
            Console.WriteLine("0. Iesire");
            Console.Write("Selectati optiunea: ");

            string option = Console.ReadLine();
            switch (option)
            {
                //login-ul
                case "1":
                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Parola: ");
                    string password = Console.ReadLine();

                    currentUser = users.FirstOrDefault(u => u.Username == username && u.Password == password);

                    if (currentUser == null)
                    {
                        Console.WriteLine("Utilizator sau parola incorecta! Apasati orice tasta...");
                        Console.ReadKey();
                        return;
                    }

                    Console.WriteLine($"Bine ai venit, {currentUser.Username} ({currentUser.GetRole()})!");
                    System.Threading.Thread.Sleep(1000);
    
                    //pentru admin
                    if (currentUser is Admin)
                    {
                        bool inMenu = true;
                        while (inMenu)
                        {
                            Console.Clear();
                            Console.WriteLine($"=== PANOU ADMIN ({currentUser.Username}) ===");
                            Console.WriteLine("1. Adauga Vehicul");
                            Console.WriteLine("2. Vezi Toate Vehiculele");
                            Console.WriteLine("3. Sterge Vehicul");
                            Console.WriteLine("4. Schimba Status Vehicul");
                            Console.WriteLine("5. Istoric Inchirieri");
                            Console.WriteLine("0. Deconectare");
                            Console.Write("Optiune: ");

                            switch (Console.ReadLine())
                            {
                                case "1": AddVehicle(); break;
                                case "2": ShowAllVehicles(); break;
                                case "3": RemoveVehicle(); break;
                                case "4": ChangeStatus(); break;
                                case "5": ShowRentalsHistory(); break;
                                case "0": inMenu = false; currentUser = null; break;
                                default: break;
                            }
                        }
                    }
                    //pentru client
                    else if (currentUser is Utilizator)
                    {
                        bool inMenu = true;
                        while (inMenu)
                        {
                            Console.Clear();
                            Console.WriteLine($"=== MENIU CLIENT ({currentUser.Username}) ===");
                            Console.WriteLine("1. Vezi masini disponibile");
                            Console.WriteLine("2. Rezerva o masina");
                            Console.WriteLine("3. Vezi rezervarile mele");
                            Console.WriteLine("4. Confirma inchiriere (Simulare preluare)");
                            Console.WriteLine("5. Anuleaza rezervare");
                            Console.WriteLine("0. Deconectare");
                            Console.Write("Optiune: ");

                            switch (Console.ReadLine())
                            {
                                case "1": ShowAvailableVehicles(); break;
                                case "2": ReserveVehicle(); break;
                                case "3": ShowMyReservations(); break;
                                case "4": ConfirmRental(); break;
                                case "5": CancelReservation(); break;
                                case "0": inMenu = false; currentUser = null; break;
                                default: break;
                            }
                        }
                    }
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Optiune invalida. Apasati orice tasta...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    //functie pentru adaugare vehicul
    static void AddVehicle()
    {
        try
        {
            Console.WriteLine("\n--- Adaugare Masina ---");
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Brand: "); string brand = Console.ReadLine();
            Console.Write("Model: "); string model = Console.ReadLine();
            Console.Write("An: "); int year = int.Parse(Console.ReadLine());
            Console.Write("Capacitate (persoane): "); int cap = int.Parse(Console.ReadLine());
            Console.Write("Pret/Ora: "); double price = double.Parse(Console.ReadLine());
            Console.Write("Nr Usi (2 sau 4): "); int doors = int.Parse(Console.ReadLine());
            Console.Write("Combustibil: "); string fuel = Console.ReadLine();
            Console.Write("Aer Conditionat (true/false): "); bool ac = bool.Parse(Console.ReadLine());

            Car newCar = new Car(id, model, brand, year, cap, price, doors, fuel, ac);
            newCar.Status = VehicleStatus.Available;
            adminService.AddVehicle(newCar);
            Console.WriteLine("Masina adaugata cu succes!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
        Console.ReadKey();
    }

    //functie pentru stergere vehicul
    static void RemoveVehicle()
    {
        Console.Write("\nID Vehicul de sters: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            adminService.RemoveVehicle(id);
            Console.WriteLine("Operatiune efectuata.");
        }
        Console.ReadKey();
    }

    //functie pentru schimbare status
    static void ChangeStatus()
    {
        Console.Write("\nID Vehicul: ");
        int id = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Statusuri: 0-Available, 1-Reserved, 2-Rented, 3-Unavailable");
        Console.Write("Alege noul status (cifra): ");
        if (Enum.TryParse(Console.ReadLine(), out VehicleStatus status))
        {
            adminService.ChangeVehicleStatus(id, status);
            Console.WriteLine("Status actualizat.");
        }
        else
        {
            Console.WriteLine("Status invalid.");
        }
        Console.ReadKey();
    }

    //functie pentru istoricul inchirierilor
    static void ShowRentalsHistory()
    {
        Console.WriteLine("\n--- Istoric Inchirieri ---");
        var history = adminService.GetRentalHistory();
        if (history.Count == 0) Console.WriteLine("Nu exista inchirieri.");
        
        foreach (var r in history)
        {
            Console.WriteLine($"Rental #{r.Id}: {r.Reservation.User.Username} a inchiriat {r.Reservation.Vehicle.Brand} {r.Reservation.Vehicle.Model}. Cost Total: {r.TotalCost} RON");
        }
        Console.ReadKey();
    }

    //functie pentru afisarea vehiculelor
    static void ShowAllVehicles()
    {
        Console.WriteLine("\n--- Lista Vehicule ---");
        foreach (var v in vehicles)
        {
            Console.WriteLine(v.ToString());
        }
        Console.ReadKey();
    }

    //functie pentru afisarea vehiculelor valabile
    static void ShowAvailableVehicles()
    {
        Console.WriteLine("\n--- Masini Disponibile ---");
        var available = vehicles.Where(v => v.Status == VehicleStatus.Available).ToList();
        if (available.Count == 0) Console.WriteLine("Nicio masina disponibila momentan.");
        
        foreach (var v in available)
        {
            Console.WriteLine($"ID: {v.Id} | {v.Brand} {v.Model} | Pret: {v.PricePerHour}/h");
        }
        Console.ReadKey();
    }

    //functie pentru a rezerva vehiculul
    static void ReserveVehicle()
    {
        try
        {
            Console.Write("\nID Masina dorita: ");
            int id = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Format data: yyyy-MM-dd HH:mm (ex: 2024-05-20 14:00)");
            Console.Write("Data inceput: ");
            DateTime start = DateTime.Parse(Console.ReadLine());
            Console.Write("Data sfarsit: ");
            DateTime end = DateTime.Parse(Console.ReadLine());

            var res = reservationService.CreateReservation((Utilizator)currentUser, id, start, end);
            Console.WriteLine($"Rezervare creata cu succes! ID Rezervare: {res.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la rezervare: {ex.Message}");
        }
        Console.ReadKey();
    }

    //functie pentru afisarea rezervarilor
    static void ShowMyReservations()
    {
        Console.WriteLine("\n--- Rezervarile Mele ---");
        var myRes = reservationService.GetReservationsForUser((Utilizator)currentUser);
        
        if (myRes.Count == 0) Console.WriteLine("Nu aveti rezervari active.");

        foreach (var r in myRes)
        {
            Console.WriteLine($"ID: {r.Id} | Masina: {r.Vehicle.Brand} {r.Vehicle.Model} | Perioada: {r.StartDate} - {r.EndDate}");
        }
        Console.ReadKey();
    }

    //functie pentru a confirma inchiriere
    static void ConfirmRental()
    {
        Console.Write("\nIntroduceti ID-ul rezervarii pentru confirmare: ");
        if (int.TryParse(Console.ReadLine(), out int resId))
        {
            try
            {
                var rental = reservationService.ConfirmRental(resId);
                Console.WriteLine($"Inchiriere confirmata! Cost total estimat: {rental.TotalCost} RON");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        Console.ReadKey();
    }

    //functie pentru anularea rezervarii
    static void CancelReservation()
    {
        Console.Write("\nIntroduceti ID-ul rezervarii de anulat: ");
        if (int.TryParse(Console.ReadLine(), out int resId))
        {
            try
            {
                reservationService.CancelReservation(resId);
                Console.WriteLine("Rezervare anulata.");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        Console.ReadKey();
    }
}