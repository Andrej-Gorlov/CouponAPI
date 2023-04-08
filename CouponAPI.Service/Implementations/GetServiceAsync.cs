using CouponAPI.Domain.Entity.CouponDTO;

namespace CouponAPI.Service.Implementations
{
    public class GetServiceAsync
    {
        public class Query : IRequest<List<CouponDTO>> { }

        public class Handler : IRequestHandler<Query, List<CouponDTO>>
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

            public async Task<List<CouponDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("возврат всеx купонов.");
                return _mapper.Map<List<CouponDTO>>(await _context.Coupons.ToListAsync());
            }
        }
    }
}
