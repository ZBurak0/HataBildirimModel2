using System.ComponentModel.DataAnnotations;

namespace HataBildirimModel2.Models
{
    public class Filen
    {
        public int Id { get; set; }//PK

        [StringLength(300)]
        public string FilePath { get; set; }

        public List<FaultNotification> FaultNotifications { get; set; }
    }
}
