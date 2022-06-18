using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Token
    {
        public Guid RefreshToken { get; set; }
        public string? AccessToken { get; set; }
        public bool? IsRevoked { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        [ForeignKey("Account")]
        public Guid UserId { get; set; }

        public Account? Account { get; set; }
    }
}