using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Final_Net103.DomainClass
{
    [Table("Ca")]
    public partial class Ca
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string? Ten { get; set; }
        [StringLength(100)]
        [Required]
        public string? ThucAn { get; set; }
        [StringLength(100)]
        [Required]
        public string? TapTinh { get; set; }
        [Column("IDCa")]
        [Required]
        public int? Idca { get; set; }

        [ForeignKey("Idca")]
        [InverseProperty("Cas")]
        public virtual Dongvat? IdcaNavigation { get; set; }
    }
}
