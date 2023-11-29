namespace sachu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ThamGias = new HashSet<ThamGia>();
        }
        

        [Key]
        public int MaSach { get; set; }

        [StringLength(50)]
        public string TenSach { get; set; }

        public decimal? GiaBan { get; set; }

        [StringLength(50)]
        public string MoTa { get; set; }

        [StringLength(20)]
        public string AnhBia { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? SoLuongTon { get; set; }

        public int? MaNXB { get; set; }

        public int? MaDM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
       
      
      
        public virtual DanhMuc DanhMuc { get; set; }

        public virtual NhaXuatBan NhaXuatBan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThamGia> ThamGias { get; set; }
    }
}
