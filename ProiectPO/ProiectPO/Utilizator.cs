namespace ProiectPO
{
    public class Utilizator : User
    {
        public Utilizator(int id, string username, string password) : base(id, username, password)
        {
        }


        public override string GetRole()
        {
            return "Utilizator";
        }
    }
}