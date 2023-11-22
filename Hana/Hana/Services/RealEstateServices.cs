using System;
using System.Linq;
using Hana.Data;
using Hana.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Hana.Models.DataModels;
using Hana.Models.ViewModels;
using Hana.Interfaces;

namespace Hana.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly HanaContext _context;
        public RealEstateServices(HanaContext context)
        {
            _context = context;
        }

        public List<VM_RealEstate> GetList()
        {
            var list = _context.RealEstateDetail
                .Include(detail => detail.RealEstate.Agent)
                .Include(detail => detail.RealEstate.ReaEstateType)
                .Select(item => new VM_RealEstate
                {
                    Id = item.RealEstate.Id,
                    Title = item.Title,
                    ExprireTime = item.RealEstate.ExprireTime,
                    Price = item.Price,
                    IsActive = item.RealEstate.IsActive,
                    RealEstateTypeId = item.RealEstate.ReaEstateType != null ? item.RealEstate.ReaEstateType.Id : 0,
                    AgentName = item.RealEstate.Agent != null ? item.RealEstate.Agent.AgentName : null
                })
                .ToList();
            return list;
        }


        public IQueryable<RealEstateViewModel> GetRealEstates(string searchKey)
        {
            var source = _context.RealEstate
                            .Include(r => r.RealEstateDetail)
                            .Include(r => r.Agent)
                            .Include(r => r.Map)
                            .Where(r => r.Map.Address.Contains(searchKey)
                                     || r.Agent.AgentName.Contains(searchKey)
                                     || r.ReaEstateType.RealEstateTypeName.Contains(searchKey)
                                     || r.RealEstateDetail.Price.ToString().Contains(searchKey)
                                  )
                            .OrderByDescending(r => r.IsActive)
                            .ThenByDescending(r => r.IsAvaiable)
                            .ThenByDescending(r => r.ExprireTime)
                            .AsQueryable();

            //if (!string.IsNullOrEmpty(searchKey))
            //{
            //    source = source.Where(s => s.Map.Address.Contains(searchKey)
            //                             || s.Agent.AgentName.Contains(searchKey)
            //                             || s.ReaEstateType.RealEstateTypeName.Contains(searchKey)
            //                             || EF.Functions.Contains(s.PostTime.ToString("dd/MM/yyyy"), searchKey)
            //                             || EF.Functions.Contains(s.ExprireTime.Value.ToString("dd/MM/yyyy"), searchKey)
            //                             || EF.Functions.Contains(s.RealEstateDetail.Price.ToString(), searchKey)
            //                          );
            //}

            IQueryable<RealEstateViewModel> results = (from item in source
                                                       select new RealEstateViewModel
                                                       {
                                                           Id = item.Id,
                                                           Street = item.Map.Address,
                                                           Price = Helper.VNCurrencyFormat(item.RealEstateDetail.Price.ToString()),
                                                           PostDate = item.PostTime.ToString("dd/MM/yyyy"),
                                                           ExpireTime = item.ExprireTime == null ? string.Empty : item.ExprireTime.Value.ToString("dd/MM/yyyy"),
                                                           Agent = item.Agent.AgentName,
                                                           Type = item.ReaEstateType.RealEstateTypeName,
                                                           Status = Helper.GetStatus(item)
                                                       });

            return results;
        }
        public List<RealEstateViewModel> GetUserAllPosts(int? userId)
        {
            var results = new List<RealEstateViewModel>();
            var source = _context.RealEstate
                           .Include(r => r.RealEstateDetail)
                           .Include(r => r.ReaEstateType)
                           .Include(r => r.Agent)
                           .Include(r => r.Map)
                           .Where(r => r.AgentId == userId)
                           .OrderByDescending(r => r.PostTime)
                           .ThenByDescending(r => r.IsActive)
                           .ToList();

            foreach (var item in source)
            {
                var viewModelItem = new RealEstateViewModel
                {
                    Id = item.Id,
                    AgentId = item.Agent.Id,
                    Street = item.Map != null ? item.Map.Address : "Unknown",
                    Price = Helper.VNCurrencyFormat(item.RealEstateDetail.Price.ToString()),
                    PostDate = item.PostTime.ToString("dd/MM/yyyy"),
                    BeginTime = item.BeginTime.ToString("dd/MM/yyyy"),
                    ExpireTime = item.ExprireTime == null ? string.Empty : item.ExprireTime.Value.ToString("dd/MM/yyyy"),
                    Type = item.ReaEstateType.RealEstateTypeName,
                    Status = Helper.GetStatus(item)
                };
                results.Add(viewModelItem);
            }

            return results;
        }

        public List<RealEstateViewModel> GetCustomerConFirmList()
        {
            var results = new List<RealEstateViewModel>();
            var source = _context.RealEstate
                           .Include(r => r.RealEstateDetail)
                           .Include(r => r.ReaEstateType)
                           .Include(r => r.Agent)
                           .Include(r => r.Map)
                           .Where(r => !r.IsConfirm)
                           .OrderByDescending(r => r.PostTime)
                           .ToList();

            foreach (var item in source)
            {
                var viewModelItem = new RealEstateViewModel
                {
                    Id = item.Id,
                    Street = item.Map != null ? item.Map.Address : "Unknown",
                    Price = Helper.VNCurrencyFormat(item.RealEstateDetail.Price.ToString()),
                    Agent = item.Agent.AgentName,
                    PostDate = item.PostTime.ToString("dd/MM/yyyy"),
                    BeginTime = item.BeginTime.ToString("dd/MM/yyyy"),
                    ExpireTime = item.ExprireTime == null ? string.Empty : item.ExprireTime.Value.ToString("dd/MM/yyyy"),
                    Type = item.ReaEstateType.RealEstateTypeName,
                    Status = Helper.GetStatus(item)
                };
                results.Add(viewModelItem);
            }

            return results;
        }
        public List<RealEstateViewModel> CustomerExpireList()
        {
            var results = new List<RealEstateViewModel>();
            var source = _context.RealEstate
                           .Include(r => r.RealEstateDetail)
                           .Include(r => r.ReaEstateType)
                           .Include(r => r.Agent)
                           .Include(r => r.Map)
                           .Where(r => r.IsActive && r.ExprireTime < DateTime.Now)
                           .OrderByDescending(r => r.PostTime)
                           .ToList();

            foreach (var item in source)
            {
                var viewModelItem = new RealEstateViewModel
                {
                    Id = item.Id,
                    Street = item.Map.Address,
                    Price = Helper.VNCurrencyFormat(item.RealEstateDetail.Price.ToString()),
                    Agent = item.Agent.AgentName,
                    PostDate = item.PostTime.ToString("dd/MM/yyyy"),
                    BeginTime = item.BeginTime.ToString("dd/MM/yyyy"),
                    ExpireTime = item.ExprireTime == null ? string.Empty : item.ExprireTime.Value.ToString("dd/MM/yyyy"),
                    Type = item.ReaEstateType.RealEstateTypeName,
                    Status = Helper.GetStatus(item)
                };
                results.Add(viewModelItem);
            }

            return results;
        }

        public async Task<VM_RealEstateDetails> GetRealEstateDetails(int? id)
        {
            var info = await _context.RealEstate.Where(r => r.Id == id)
                           .Include(r => r.RealEstateDetail)
                           .Include(r => r.ReaEstateType)
                           .Include(r => r.Map)
                           .Include(r => r.Agent)
                           .SingleOrDefaultAsync();
            if (info != null)
            {
                if (info.Map == null)
                {
                    info.Map = new Map();
                }
                VM_RealEstateDetails result = Helper.MappingFromRealEstate(info);
                return result;
            }

            return null;
        }

        #region comment out
        //public VM_RealEstateDetails GetById(int? id)
        //{
        //    var details = _context.RealEstateDetail
        //        .Where(x => x.RealEstateId == id)
        //        .Include(detail => detail.RealEstate)
        //            .ThenInclude(detail => detail.Agent)
        //        .Include(detail => detail.RealEstate)
        //            .ThenInclude(detail => detail.ReaEstateType)
        //        .SingleOrDefault();

        //    var map = _context.Map.Where(m => m.RealEstateId == id).SingleOrDefault();

        //    var images = _context.Picture
        //        .Where(pic => pic.RealEstateId == id && pic.IsActive)
        //        .ToList();

        //    var imgUrls = new List<string>();
        //    foreach (var img in images)
        //    {
        //        var tempUrl = GetLinkImage(img);
        //        imgUrls.Add(tempUrl);
        //    }

        //    if (details != null)
        //    {
        //        if (map == null)
        //        {
        //            map = new Map();
        //        }

        //        var vm_rt_details = new VM_RealEstateDetails()
        //        {
        //            Id = details.RealEstate.Id,
        //            Title = details.Title,
        //            Address = map.Address ?? string.Empty,
        //            Price = details.Price,
        //            Acreage = details.Acreage,
        //            PostTime = details.RealEstate.PostTime.ToString("dd/MM/yyyy"),
        //            LastUpdate = details.RealEstate.LastUpdate?.ToString("dd/MM/yyyy"),
        //            ExprireTime = details.RealEstate.ExprireTime?.ToString("dd/MM/yyyy"),
        //            RoomNumber = details.RoomNumber,
        //            Description = details.Description,
        //            AgentName = details.RealEstate.Agent.AgentName,
        //            HasPrivateWc = details.HasPrivateWc,
        //            HasMezzanine = details.HasMezzanine,
        //            AllowCook = details.AllowCook,
        //            FreeTime = details.FreeTime,
        //            SecurityCamera = details.SecurityCamera,
        //            WaterPrice = details.WaterPrice == null ? 0 : details.WaterPrice,
        //            ElectronicPrice = details.ElectronicPrice == null ? 0 : details.ElectronicPrice,
        //            WifiPrice = details.WifiPrice,
        //            Latitude = map.Latitude,
        //            Longtitude = map.Longtitude,
        //            RealEstateTypeId = details.RealEstate.ReaEstateType.Id,
        //            IsActive = details.RealEstate.IsActive,
        //            ImageUrls = imgUrls
        //        };
        //        return vm_rt_details;
        //    }

        //    return new VM_RealEstateDetails();
        //}
        #endregion

        public int AddNewRealEstate(RealEstate realEstate)
        {
            try
            {
                _context.RealEstate.Add(realEstate);
                _context.SaveChanges();
                return realEstate.Id;
            }
            catch
            {
                return -1;
            }
        }

        public bool AddRealEstateDetails(RealEstateDetail details)
        {
            try
            {
                _context.RealEstateDetail.Add(details);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddMapForRealEstate(Map map)
        {
            try
            {
                var location = GetLocation(map.Address);
                map.WardId = location.Item1;
                map.DistrictId = location.Item2;
                map.CityId = location.Item3;
                _context.Map.Add(map);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public int AddCompleteRealEstate(VM_RealEstateDetails details, int agentId)
        {
            var realEstate = new RealEstate()
            {
                PostTime = DateTime.Now,
                LastUpdate = DateTime.Now,
                BeginTime = Convert.ToDateTime(details.BeginTime),
                ExprireTime = Convert.ToDateTime(details.ExprireTime),
                RealEstateTypeId = details.RealEstateTypeId,
                AgentId = agentId,
                ContactNumber = details.ContactNumber,
                IsActive = false,
                IsConfirm = agentId == 1,
                IsAvaiable = true,
                ConfirmStatus = agentId == 1 ? 1 : 0
            };
            var realEstateId = AddNewRealEstate(realEstate);

            //tao real estate thanh cong
            if (realEstateId != -1)
            {
                //add details
                var rtDetails = new RealEstateDetail()
                {
                    RealEstateId = realEstateId,
                    Title = details.Title,
                    Price = details.Price,
                    Acreage = details.Acreage,
                    RoomNumber = details.RoomNumber,
                    Description = details.Description,
                    HasPrivateWc = details.HasPrivateWc,
                    HasMezzanine = details.HasMezzanine,
                    AllowCook = details.AllowCook,
                    FreeTime = details.FreeTime,
                    SecurityCamera = details.SecurityCamera,
                    WaterPrice = details.IsFreeWater ? 0 : Convert.ToInt32(details.WaterPrice),
                    ElectronicPrice = details.IsFreeElectronic ? 0 : Convert.ToInt32(details.ElectronicPrice),
                    WifiPrice = details.IsFreeWifi ? 0 : details.WifiPrice
                };

                var isSuccessAddDetails = AddRealEstateDetails(rtDetails);

                //add map
                var map = new Map()
                {
                    Address = details.Address,
                    Latitude = details.Latitude,
                    Longtitude = details.Longtitude,
                    RealEstateId = realEstateId
                };
                var isSuccessAddMap = AddMapForRealEstate(map);

                //neu add thong tin chi tiet va map thanh cong thi active
                if (isSuccessAddDetails && isSuccessAddMap)
                {
                    ActiveRealEstate(realEstateId);
                    return realEstateId;
                }
                else
                {
                    return -1;
                }
            }

            return realEstateId;
        }
        public bool UpdateRealEstate(VM_RealEstateDetails details)
        {

            var rt = _context.RealEstate.Find(details.Id);
            if (rt != null)
            {
                var rt_detail = _context.RealEstateDetail.FirstOrDefault(d => d.RealEstateId == rt.Id);

                if (rt_detail != null)
                {
                    //it's update time
                    rt.LastUpdate = DateTime.Now;
                    rt.BeginTime = DateTime.Parse(details.BeginTime);
                    rt.ExprireTime = DateTime.Parse(details.ExprireTime);
                    rt.ContactNumber = details.ContactNumber;
                    rt_detail.Title = details.Title;
                    rt_detail.Price = details.Price;
                    rt_detail.Acreage = details.Acreage;
                    rt_detail.RoomNumber = details.RoomNumber;
                    rt_detail.Description = details.Description;
                    rt_detail.HasPrivateWc = details.HasPrivateWc;
                    rt_detail.HasMezzanine = details.HasMezzanine;
                    rt_detail.AllowCook = details.AllowCook;
                    rt_detail.FreeTime = details.FreeTime;
                    rt_detail.SecurityCamera = details.SecurityCamera;
                    rt_detail.WaterPrice = Convert.ToInt32(details.WaterPrice);
                    rt_detail.ElectronicPrice = Convert.ToInt32(details.ElectronicPrice);
                    rt_detail.WifiPrice = details.WifiPrice;

                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else return false;
        }

        public int DeleteRealEstate(int id, int userId)
        {
            try
            {
                var realEstate = _context.RealEstate.Find(id);
                var user = _context.Agent.Find(userId);

                if (realEstate != null && user != null)
                {
                    if (Convert.ToInt32(realEstate.AgentId) == user.Id || user.LevelId == 1)
                    {
                        _context.RealEstate.Remove(realEstate);
                        _context.SaveChanges();
                        return 1;   //thanh cong
                    }
                    else return 2;  //user khong hop le
                }
                return 0;   //not found
            }
            catch (Exception)
            {
                return -1;  //loi he thong
            }

        }
        public int DeleteMultipleRealEstates(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var realEstate = _context.RealEstate.Find(id);

                    if (realEstate != null)
                    {
                        _context.RealEstate.Remove(realEstate);
                    }
                    else
                    {
                        // Trả về 0 nếu không tìm thấy bản ghi
                        return 0;
                    }
                }

                _context.SaveChanges();
                return 1; // Trả về 1 nếu xóa thành công
            }
            catch (Exception)
            {
                return -1; // Trả về -1 nếu có lỗi hệ thống
            }
        }

        public bool DisableRealEstate(int id)
        {
            var realEstate = _context.RealEstate.Find(id);
            if (realEstate != null && realEstate.IsActive)
            {
                realEstate.IsActive = false;
                realEstate.LastUpdate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EnableRealEstate(int id)
        {
            var realEstate = _context.RealEstate.Find(id);
            if (realEstate != null && !realEstate.IsActive)
            {
                realEstate.IsActive = true;
                realEstate.LastUpdate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool BookedRealEstate(int id)
        {
            var realEstate = _context.RealEstate.Find(id);
            if (realEstate != null && realEstate.IsAvaiable)
            {
                realEstate.IsAvaiable = false;
                realEstate.LastUpdate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void ActiveRealEstate(int id)
        {
            var realEstate = _context.RealEstate.Find(id);
            if (realEstate != null && !realEstate.IsActive)
            {
                realEstate.IsActive = true;
                _context.SaveChanges();

            }
        }

        public bool IsExistRealEstate(int id)
        {
            return _context.RealEstate.Any(r => r.Id == id);
        }

        public IEnumerable<RealEstateType> GetRealEstateTypeList()
        {
            return _context.RealEstateType.ToList();
        }

        public IEnumerable<City> GetCityList()
        {
            return _context.City.ToList();
        }

        public IEnumerable<District> GetDistrictList()
        {
            return _context.District.ToList();
        }

        public Tuple<int?, int?, int> GetLocation(string address)
        {
            int cityId = 4;
            int? districtId = null;
            int? wardId = null;


            var cityList = _context.City.ToList();

            foreach (var city in cityList)
            {
                if (address.Contains(city.CityName))
                {
                    cityId = city.Id;
                    break;
                }
            }

            var districtList = _context.District.Where(d => d.CityId == cityId).ToList();
            if (districtList.Count > 0)
            {
                foreach (var district in districtList)
                {
                    if (address.Contains(district.DistrictName))
                    {
                        districtId = district.Id;
                        break;
                    }
                }
                var wardList = _context.Ward.Where(w => w.DistrictId == districtId).ToList();
                if (wardList.Count > 0)
                {
                    foreach (var ward in wardList)
                    {
                        if (address.Contains(ward.WardName))
                        {
                            wardId = ward.Id;
                            break;
                        }
                    }
                }
            }
            var result = Tuple.Create(wardId, districtId, cityId);
            return result;
        }

        /*
         ------------Client services-------------------------------
         */

        public IQueryable<Result> Filter(Condition condition)
        {
            try
            {
                if (condition == null)
                {
                    return null;
                }

                var source = _context.RealEstate
                    .Where(r => r.IsActive && r.ConfirmStatus == 1);

                if (condition.City > 0)
                {
                    source = source.Where(c => c.Map.CityId == condition.City);
                }
                if (condition.District > 0)
                {
                    source = source.Where(c => c.Map.DistrictId == condition.District);
                }
                if (condition.Type > 0)
                {
                    source = source.Where(c => c.ReaEstateType.Id == condition.Type);
                }
                if (condition.PriceRange > 0)
                {
                    var priceRangeValues = Helper.GetPriceRange(condition.PriceRange);
                    source = source.Where(c => c.RealEstateDetail.Price >= priceRangeValues[0] && c.RealEstateDetail.Price <= priceRangeValues[1]);
                }
                if (condition.AcreageRange > 0)
                {
                    var acreageRangeValues = Helper.GetAcreageRange(condition.AcreageRange);
                    source = source.Where(c => c.RealEstateDetail.Acreage >= acreageRangeValues[0] && c.RealEstateDetail.Acreage <= acreageRangeValues[1]);
                }

                // Tiếp tục với việc select và trả về kết quả
                var results = source.Select(item => new Result
                {
                    Id = item.Id,
                    Street = Helper.GetStreet(item.Map.Address),
                    Price = item.RealEstateDetail.Price,
                    Acreage = item.RealEstateDetail.Acreage,
                    Type = item.RealEstateTypeId,
                    PostTime = item.PostTime.ToString("dd/MM/yyyy"),
                    ImageUrl = Helper.GetRealEstateAvatar(item.Picture.FirstOrDefault()),
                    AgentName = item.Agent.AgentName,
                    ContactNumber = item.ContactNumber
                });

                return results;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                // Ghi log hoặc thực hiện các hành động xử lý lỗi khác
                Console.WriteLine($"Error in Filter method: {ex.Message}");
                return null;
            }
        }

        private IQueryable<RealEstate> ApplyConditionFilters(IQueryable<RealEstate> source, Condition condition)
        {
            if (condition.Type > 0)
            {
                source = source.Where(s => s.RealEstateTypeId == condition.Type);
            }
            if (condition.City > 0)
            {
                source = source.Where(x => x.Map.CityId == condition.City);
            }
            if (condition.District > 0)
            {
                source = source.Where(x => x.Map.DistrictId == condition.District);
            }

            var priceRange = Helper.GetPriceRange(condition.PriceRange);
            var minPrice = priceRange[0];
            var maxPrice = priceRange[1];
            source = source.Where(x => x.RealEstateDetail.Price >= minPrice && x.RealEstateDetail.Price <= maxPrice);

            var acreageRange = Helper.GetAcreageRange(condition.AcreageRange);
            var minAcreage = acreageRange[0];
            var maxAcreage = acreageRange[1];
            source = source.Where(x => x.RealEstateDetail.Acreage >= minAcreage && x.RealEstateDetail.Acreage <= maxAcreage);

            if (!string.IsNullOrEmpty(condition.SearchString.Trim()))
            {
                if (DateTime.TryParse(condition.SearchString, out DateTime searchDate))
                {
                    source = source.Where(x => x.PostTime < searchDate && x.ExprireTime > searchDate);
                }
                else
                {
                    source = source.Where(x => x.Map.Address.Contains(condition.SearchString) || x.RealEstateDetail.Price.ToString().Contains(condition.SearchString));
                }
            }

            return source;
        }

        public bool ConfirmRealEsate(int id, int confirmType)
        {
            try
            {
                if (id <= 0 || confirmType <= 0) return false;

                var post = _context.RealEstate.Find(id);
                if (post == null) return false;

                post.IsConfirm = true;
                post.ConfirmStatus = confirmType;
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Get recommend real estate list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Result> GetRecommendList(int? id)
        {
            var realEstate = _context.RealEstate.Where(r => r.Id == id)
                                .Include(r => r.RealEstateDetail)
                                .Include(r => r.Map)
                                .Include(r => r.Agent)
                                .Include(r => r.Map).SingleOrDefault();

            if (realEstate == null)
            {
                return null;
            }
            else
            {
                var source = _context.RealEstate.Include(r => r.RealEstateDetail)
                                                .Include(r => r.Map)
                                                .Include(r => r.Picture)
                                                .Include(r => r.Agent)
                                                .Include(r => r.ReaEstateType)
                                                .Where(r => r.Id != realEstate.Id
                                                && r.RealEstateTypeId == realEstate.RealEstateTypeId
                                                && r.IsActive && r.ConfirmStatus == 1 && r.ExprireTime > DateTime.Now && r.IsAvaiable
                                                && (r.Map.WardId == realEstate.Map.WardId
                                                || r.Map.DistrictId == realEstate.Map.DistrictId))
                                                .OrderByDescending(r => r.PostTime)
                                                .Take(4);

                var result = (from item in source
                              select new Result
                              {
                                  Id = item.Id,
                                  Street = Helper.GetStreet(item.Map.Address),
                                  Price = item.RealEstateDetail.Price,
                                  Acreage = item.RealEstateDetail.Acreage,
                                  Type = item.RealEstateTypeId,
                                  PostTime = item.PostTime.ToString("dd/MM/yyyy"),
                                  ImageUrl = Helper.GetRealEstateAvatar(item.Picture.FirstOrDefault()),
                                  AgentName = item.Agent.AgentName,
                                  ContactNumber = item.ContactNumber
                              }).ToList();
                return result;
            }
        }

        public List<VM_Location> GetAllActiveLocation()
        {
            var source = _context.RealEstate.Where(r => r.IsActive && r.IsAvaiable && r.ExprireTime > DateTime.Now)
                .Include(r => r.RealEstateDetail).Include(r => r.Map).ToList();

            var result = (from item in source
                          select new VM_Location
                          {
                              Id = item.Id,
                              Address = Helper.GetStreet(item.Map.Address),
                              Price = item.RealEstateDetail.Price,
                              Latitude = item.Map.Latitude,
                              Longtitude = item.Map.Longtitude
                          }).ToList();

            return result;
        }

        List<District> IRealEstateServices.GetDistrictList()
        {
            return _context.District.ToList();
        }

       
    }

}
