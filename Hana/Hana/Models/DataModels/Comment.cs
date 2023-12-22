using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hana.Models.DataModels
{
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Agent")]
        public int? AgentId { get; set; }
        [ForeignKey("RealEstate")]
        public int? RealEstateId { get; set; }
        public string? AgentName { get; set; }
        public string? Content { get; set; }
        public  DateTime Ngaytao { get; set; }
        public virtual Agent? Agent { get; set; }
        public virtual RealEstate? RealEstate { get; set; }
    }
}