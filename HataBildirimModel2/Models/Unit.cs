using System.ComponentModel.DataAnnotations;

namespace HataBildirimModel.Models
{
    public class Unit
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public List<User> Users { get; set; }
        public List<Admin> Admins { get; set; }
    }
}
