using CouponAPI.DAL;
using CouponAPI.Domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CouponAPI.Service.Implementations
{
    public class CreateServiceAsync
    {
        public class Command : IRequest
        {
            public Coupon? Coupon { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;
            public ILogger<CreateServiceAsync> _logger { get; }
            public Handler(ApplicationDbContext context, ILogger<CreateServiceAsync> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Coupons.Add(request.Coupon);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
