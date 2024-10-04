using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ThucTheDinhDanh
    {
        public Guid Id { get; set; }
        public string TenThucThe {  get; set; }
        public string MoTa {  get; set; }
        public bool KichHoat {  get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy {  get; set; }
        public DateTime? UpdatedDate {  get; set; }
        public int? UpdatedBy { get; set; }
        public string SystemId {  get; set; }
        public string TuKhoa {  get; set; }
        public string TraLoi {  get; set; }
        public string GhiChu {  get; set; }
    }
}
