namespace sacha.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
  

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
        [DisplayName("Mã sách")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public int MaSach { get; set; }

        [DisplayName("Tên sách")]
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(50,ErrorMessage = "{0} không đươc vượt quá 50 kí tự ")]
        public string TenSach { get; set; }
        [DisplayName("Giá bán")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public decimal? GiaBan { get; set; }

        [DisplayName("Mô tả")]
       
        [StringLength(50, ErrorMessage = "{0} không đươc vượt quá 50 kí tự ")]
        public string MoTa { get; set; }

        [DisplayName("Ảnh bìa")]
      
        [StringLength(50, ErrorMessage = "{0} không đươc vượt quá 50 kí tự ")]
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
