using AutoMapper;
using CouponAPI.Domain.Entity;
using CouponAPI.Domain.Entity.CouponDTO;
using static System.Net.Mime.MediaTypeNames;

namespace CouponAPI
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Coupon, CouponDTO>();
            CreateMap<Coupon, CreateCouponDTO>();
            CreateMap<Coupon, UpdateCouponDTO>();
        }

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(x => {
                x.CreateMap<Coupon, CouponDTO>().ReverseMap();
                x.CreateMap<Coupon, CreateCouponDTO>().ReverseMap();
                x.CreateMap<Coupon, UpdateCouponDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
