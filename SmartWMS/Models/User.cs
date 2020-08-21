using SQLite;

namespace SmartWMS.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }

        public override string ToString()
        {
            return UserId + " " + Name + " " + Surname + " " + Username + " " + Password
                + " " + UserEmail;
        }
    }
}
