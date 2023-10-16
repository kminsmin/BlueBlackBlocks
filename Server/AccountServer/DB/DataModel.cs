using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountServer.DB
{
    [Table("Account")]
    public class AccountDb
    {
        [Key]
        public int AccountDbId { get; set; }

        [Required(ErrorMessage = "AccountName를 입력하세요.")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "AccountPassword를 입력하세요.")]
        public string AccountPassword { get; set; }
    }
}
