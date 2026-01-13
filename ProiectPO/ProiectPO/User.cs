namespace ProiectPO
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        protected User(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public abstract string GetRole();
    }
}