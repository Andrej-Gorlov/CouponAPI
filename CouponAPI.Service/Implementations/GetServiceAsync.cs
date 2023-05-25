namespace CouponAPI.Service.Implementations
{
    public class GetServiceAsync
    {
        public class Query : IRequest<IBaseResponse<List<CouponDTO>>> { }

        public class Handler : IRequestHandler<Query, IBaseResponse<List<CouponDTO>>>
        {
            private readonly ApplicationDbContext _context;

            public ILogger<GetServiceAsync> _logger;

            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, ILogger<GetServiceAsync> logger, IMapper mapper)
            {
                _logger = logger;
                _mapper = mapper;
                _context = context;
            }

            public async Task<IBaseResponse<List<CouponDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {

                var coupons = _mapper.Map<List<CouponDTO>>(await _context.Coupons.ToListAsync());
                if (coupons.Count is 0)
                {
                    _logger.LogInformation("купон не найден (class: GetServiceAsync/method: Handle).");
                    return null;
                }
                _logger.LogInformation("возврат всеx купонов.");
                return new BaseResponse<List<CouponDTO>>().Success(coupons, ResponseStatus.Ok);
            }
        }
    }
}
