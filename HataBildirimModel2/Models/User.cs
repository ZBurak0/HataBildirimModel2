using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace HataBildirimModel.Models
{
    public class User
    {
        public int Id { get; set; } //Pk

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        public int UnitId { get; set; } //fk
        public Unit Unit { get; set; }


        [StringLength(11)]
        public string PhoneNum { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(25)]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
