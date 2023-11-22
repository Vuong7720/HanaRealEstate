using System;
using System.Collections.Generic;

namespace Hana.Models.DataModels
{
    public partial class RealEstateDetail
    {
        public int Id { get; set; }
        public int? RealEstateId { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public int Acreage { get; set; }
        public int RoomNumber { get; set; }
        public string? Description { get; set; }
        public bool HasPrivateWc { get; set; }
        public bool HasMezzanine { get; set; }
        public bool AllowCook { get; set; }
        public bool FreeTime { get; set; }
        public bool SecurityCamera { get; set; }
        public int? WaterPrice { get; set; }
        public int? ElectronicPrice { get; set; }
        public decimal? WifiPrice { get; set; }

        public virtual RealEstate RealEstate { get; set; }
    }
}
