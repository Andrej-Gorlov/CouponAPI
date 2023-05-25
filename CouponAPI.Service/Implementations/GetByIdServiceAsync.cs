namespace CouponAPI.Service.Implementations
{
    public class GetByIdServiceAsync
    {
        public class Query : IRequest<IBaseResponse<CouponDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, IBaseResponse<CouponDTO>?>
        {
            private readonly ApplicationDbContext _context;

            public ILogger<GetByIdServiceAsync> _logger;

            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, ILogger<GetByIdServiceAsync> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<IBaseResponse<CouponDTO>?> Handle(Query request, CancellationToken cancellationToken)
            {
                var coupon = _mapper.Map<CouponDTO>(await _context.Coupons.FindAsync(request.Id));
                if (coupon.CouponId <= 0)
                {
                    _logger.LogInformation("купон не найден (class: GetByIdServiceAsync/method: Handle).");
                    return null;
                }
                _logger.LogInformation("возврат купона по id.");
                return new BaseResponse<CouponDTO>().Success(coupon, ResponseStatus.Ok);
            }
        }
    }
}
