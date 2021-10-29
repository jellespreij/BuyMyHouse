using Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Models
{
    public class Mortgage : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public double? CalculatedMortgage { get; set; }

        public DateTime WatchableTime { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public Mortgage() 
        {
        
        }

        public Mortgage(double calculatedMortgage) 
        {
            CalculatedMortgage = calculatedMortgage;
            WatchableTime = DateTime.Now;
        }
    }
}
