using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KH.Pepper.Core.Domain
{
    [Table("UserRefreshToken", Schema = "dbo")]
    public class UserRefreshToken
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }
        [NotMapped]
        public bool IsActive
        {
            get
            {
                return ExpirationDate > DateTime.UtcNow;
            }
        }
        public string IpAddress { get; set; }
        public bool IsInvalidated { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User{ get; set; }

    }
}
