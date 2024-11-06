using Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Fee
    {
        [Key]
        public FeeTypeEnum Type { get; set; }
        public int Value { get; set; }
    }
}
