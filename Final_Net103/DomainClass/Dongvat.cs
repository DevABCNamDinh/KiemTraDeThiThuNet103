using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Final_Net103.DomainClass
{
    [Table("Dongvat")]
    public partial class Dongvat
    {
        public Dongvat()
        {
            Cas = new HashSet<Ca>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string? Noisong { get; set; }
        [Column("TuoithoTB")]
        public int? TuoithoTb { get; set; }

        [InverseProperty("IdcaNavigation")]
        public virtual ICollection<Ca> Cas { get; set; }
    }
}
