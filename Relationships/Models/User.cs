using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Relationships.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Character> Characters { get; set; }
    }
}
