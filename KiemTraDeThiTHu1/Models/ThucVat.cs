using System;
using System.Collections.Generic;

namespace KiemTraDeThiTHu1.models
{
    public partial class ThucVat
    {
        public ThucVat()
        {
            Hoas = new HashSet<Hoa>();
        }

        public int ThucVatId { get; set; }
        public string TenThucVat { get; set; } = null!;

        public virtual ICollection<Hoa> Hoas { get; set; }
    }
}
