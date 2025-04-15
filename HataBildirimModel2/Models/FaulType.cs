using HataBildirimModel.Models;
using System.ComponentModel.DataAnnotations;

namespace HataBildirimModel2.Models
{
    public class FaulType
    {
        public int Id { get; set; }//PK

        [StringLength(50)]
        public string Name { get; set; }

        public List<FaultNotification> FaultNotifications { get; set; }
    }
}
