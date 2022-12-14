namespace Relationships.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = "Warrior";
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
