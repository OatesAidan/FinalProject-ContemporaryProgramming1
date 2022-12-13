using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Aidan Oates
namespace FinalProjectContemporaryProgramming.Models.DataLayer
{
    public partial class AidanTable
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Birthdate { get; set; }
        [StringLength(50)]
        public string CollegeProgram { get; set; }
        [StringLength(50)]
        public string YearInProgram { get; set; }
    }
}
