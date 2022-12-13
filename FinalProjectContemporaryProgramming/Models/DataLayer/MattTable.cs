using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinalProjectContemporaryProgramming.Models.DataLayer
{
    public partial class MattTable
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        //[Required]
        [Column("FavoriteGame")]
        [StringLength(50)]
        public string FavoriteGame { get; set; }
        //[Required]
        [StringLength(50)]
        public string FavoriteSport { get; set; }
        //[Required]
        [StringLength(50)]
        public string FavoriteSeason { get; set; }
        
        public bool IAmALiar { get; set; }
    }
}
