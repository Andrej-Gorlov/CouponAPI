namespace CouponAPI
{
    public class MappingConfig
    {
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
