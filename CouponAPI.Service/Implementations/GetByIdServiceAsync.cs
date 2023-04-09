using CouponAPI.Domain;
using CouponAPI.Domain.Entity.CouponDTO;

namespace CouponAPI.Service.Implementations
{
    public class GetByIdServiceAsync
    {
        public class Query : IRequest<IBaseResponse<CouponDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, IBaseResponse<CouponDTO>>
        {
            private readonly ApplicationDbContext _context;

            public ILogger<GetByIdServiceAsync> _logger;

            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, ILogger<GetByIdServiceAsync> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<IBaseResponse<CouponDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = new BaseResponse<CouponDTO>();
                response.Result = _mapper.Map<CouponDTO>(await _context.Coupons.FindAsync(request.Id));
                _logger.LogInformation("возврат купона по id.");
                return response;
            }
        }
    }
}
