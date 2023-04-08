using CouponAPI.Domain.Entity.CouponDTO;

namespace CouponAPI.Service.Implementations
{
    public class UpdateServiceAsync
    {
        public class Command : IRequest
        {
            public UpdateCouponDTO? Coupon { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;

            public ILogger<UpdateServiceAsync> _logger;

            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper, ILogger<UpdateServiceAsync> logger)
            {
                _mapper = mapper;
                _context = context;
                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("поиск купона по id.");
                var coupon = await _context.Coupons.FindAsync(request.Coupon.CouponId);

                _logger.LogInformation("применение mapper.");
                _mapper.Map(request.Coupon, coupon);

                _logger.LogInformation("сохранение изменений в бд.");
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
