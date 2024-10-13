using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KiemTraDeThiTHu1.models
{
    public partial class Hoa
    {
        public int HoaId { get; set; }
       
        public string TenHoa { get; set; } = null!;
       
        public int ThucVatId { get; set; }

        public virtual ThucVat? ThucVat { get; set; }
    }
}
