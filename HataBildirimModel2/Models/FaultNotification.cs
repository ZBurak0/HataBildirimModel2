using HataBildirimModel.Models;
using System.ComponentModel.DataAnnotations;

namespace HataBildirimModel2.Models
{
    public class FaultNotification
    {
        public int Id { get; set; }//PK

        [StringLength(100)]
        public string Name { get; set; }
        public int UserId { get; set; } //fk
        public User User { get; set; }

        public int FaulTypeId { get; set; } //fk
        public FaulType FaulType { get; set; }

        [StringLength(200)]
        public string Explanation { get; set; }

        public int FileId { get; set; } //fk
        public Filen File { get; set; }

    }
}
