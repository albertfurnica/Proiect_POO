namespace ProiectPO;

class Program
{
    static void Main(string[] args)
    {
        int ok;

        do
        {
            Console.WriteLine("Introduceti una dintre variante : ");
            Console.WriteLine("1.Inchiriere auto");
            Console.WriteLine("0.Iesire");
            ok = int.Parse(Console.ReadLine());
            switch (ok)
            {
                case 1:
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Introduceti o varianta corecta!");
                    break;
            }
        } while (ok != 0);
    }
}