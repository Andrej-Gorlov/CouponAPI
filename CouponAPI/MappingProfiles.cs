using AutoMapper;
using CouponAPI.Domain.Entity;
using CouponAPI.Domain.Entity.CouponDTO;

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
    }
}
