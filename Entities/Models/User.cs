namespace Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public string Gender { get; set; }
        public List<Image> Photos { get; set; } = new();
        public List<Role> Roles { get; set; } = new();
    }
}
