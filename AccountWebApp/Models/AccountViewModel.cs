using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountWebApp.Models
{
    public class AccountViewModel
    {

        [Key]
        public int RefNo { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Should be less than 8")]
        public string AccountNumber { get; set; }
        [Required]

        public string AccountHolder { get; set; }
        [Required]

        public Decimal CurrentBalance { get; set; }
        [Required]

        public string BankName { get; set; }
        [Required]

        public DateTime OpeningDate { get; set; }
        [Required]

        public Boolean IsActive { get; set; }
    
}
}
