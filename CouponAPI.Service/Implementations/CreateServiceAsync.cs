using CouponAPI.Domain.Entity.CouponDTO;

namespace CouponAPI.Service.Implementations
{
    public class CreateServiceAsync
    {
        public class Command : IRequest
        {
            public CreateCouponDTO? Coupon { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;

            public ILogger<CreateServiceAsync> _logger;

            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, ILogger<CreateServiceAsync> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("добавление купона.");
                _context.Coupons.Add(_mapper.Map<Coupon>(request.Coupon));

                _logger.LogInformation("сохранение изменений в бд.");
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
