using System.ComponentModel.DataAnnotations;

namespace HataBildirimModel.Models
{
    public class Admin
    {
        public int Id { get; set; }//pk

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        public int UnitId { get; set; }//Fk
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
