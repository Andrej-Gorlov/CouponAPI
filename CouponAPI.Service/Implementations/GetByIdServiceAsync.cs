using CouponAPI.Domain.Entity.CouponDTO;

namespace CouponAPI.Service.Implementations
{
    public class GetByIdServiceAsync
    {
        public class Query : IRequest<CouponDTO>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CouponDTO>
        {
            private readonly ApplicationDbContext _context;

            public ILogger<GetByIdServiceAsync> _logger;

            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, ILogger<GetByIdServiceAsync> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<CouponDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("возврат купона по id.");
                return _mapper.Map<CouponDTO>(await _context.Coupons.FindAsync(request.Id));
            }
        }
    }
}
