using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResponseEntities
{
    public class ChiTietThuTucHanhChinh : ThuTucHanhChinh
    {
        public string DiaDiemTiepNhan { get; set; }
        public string ThoiGianTiepNhan { get; set; }
        public string TrinhTuThucHien { get; set; }
        public string CachThucThucHien { get; set; }
        public string ThoiGianGiaiQuyet { get; set; }
        public string LePhi { get; set; }
        public string YeuCau { get; set; }
        public string TenDonVi { get; set; }
        public int IdCompact { get; set; }
        public string DiaDiemTiepNhanShort { get; set; }
        public string ThoiGianTiepNhanShort { get; set; }
        public string TrinhTuThucHienShort { get; set; }
        public string CachThucThucHienShort { get; set; }
        public string ThoiGianGiaiQuyetShort { get; set; }
        public string LePhiShort { get; set; }
        public string YeuCauShort { get; set; }
    }
}
