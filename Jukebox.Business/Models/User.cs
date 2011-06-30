namespace Jukebox.Business.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string FullName
        {
            get { return FirstName + " " + LastName; }             
        }

        public string Email { get; set; }
        public string UserName { get; set; }
    }
}