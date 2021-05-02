using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceparkAPI.Models
{
    public class Parking
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [Required]
        public string Traveller { get; set; }
        [Required]
        public string StarShip { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? TotalSum { get; set; }
        public int SpacePortId { get; set; }
    }
}
