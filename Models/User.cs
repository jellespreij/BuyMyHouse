using Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class User : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public double Income { get; set; }

        [Required]
        public long LoanTerm { get; set; }

        [Required]
        public double PurchasePrice { get; set; }

        [JsonIgnore]
        public Mortgage? Mortgage {get; set;}

        public User()
        {

        }

        public User(string email, double income, long loanTerm, double purchasePrice)
        {
            Email = email;
            Income = income;
            LoanTerm = loanTerm;
            PurchasePrice = purchasePrice;
            Mortgage = null;
        }
    }
}
