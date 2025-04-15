using System.ComponentModel.DataAnnotations;

namespace HataBildirimModel2.Models
{
    public class Location
    {
        public int Id { get; set; }//Pk

        [StringLength(100)]
        public string Name { get; set; }

        public List<FaultNotification> FaultNotifications { get; set; }
    }
}
