using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ThuTucHanhChinh
    {
        public int ID { get; set; }
        public double Idx { get; set; }
        public int? IdNhomThuTuc { get; set; }
        public string TenThuTuc { get; set; }
        public string Response { get; set; }
        public string ShortResponse { get; set; }
        public string LinkResponse { get; set; }
        public int? SubId { get; set; }
    }
}
