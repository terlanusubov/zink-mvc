namespace Zink.MVC.Models
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public int UserRoleId { get; set; }
    }
}