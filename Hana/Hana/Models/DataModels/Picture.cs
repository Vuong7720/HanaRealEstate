using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hana.Models.DataModels
{
    public partial class Picture
    {
        public int Id { get; set; }
        public string PictureName { get; set; }
        public int NativeWidth { get; set; }
        public int NativeHeight { get; set; }
        public int? RealEstateId { get; set; }
        [StringLength(5000)]
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public virtual RealEstate? RealEstate { get; set; }
    }
}
