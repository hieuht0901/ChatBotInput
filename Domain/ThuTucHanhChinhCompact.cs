using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ThuTucHanhChinhCompact
    {
        public int Id { get; set; }
        public int IdThuTuc { get; set; }
        public string DiaDiemTiepNhanShort { get; set; }
        public string ThoiGianTiepNhanShort { get; set; }
        public string TrinhTuThucHienShort { get; set; }
        public string CachThucThucHienShort { get; set; }
        public string ThoiGianGiaiQuyetShort { get; set; }
        public string LePhiShort { get; set; }
        public string YeuCauShort { get; set; }
    }
}
