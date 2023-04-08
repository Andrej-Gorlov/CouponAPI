using CouponAPI.DAL;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CouponAPI.Service.Implementations
{
    public class DeleteServiceAsync
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;
            public ILogger<DeleteServiceAsync> _logger { get; }
            public Handler(ApplicationDbContext context, ILogger<DeleteServiceAsync> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var coupon = await _context.Coupons.FindAsync(request.Id);

                _context.Remove(coupon);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
