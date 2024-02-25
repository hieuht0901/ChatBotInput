using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SubNhomThuTuc
    {
        public int ID { get; set; }
        public string TenNhom { get; set; }
        public string MoTa { get; set; }
        public int ParentId { get; set; }
    }
}
