namespace ProiectPO
{
    public class Admin : User
    {
        public Admin(int id, string username, string password) : base(id, username, password)
        {
        }


        public override string GetRole()
        {
            return "Admin";
        }

    }
}