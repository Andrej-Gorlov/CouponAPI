using CouponAPI.DAL;
using CouponAPI.Domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CouponAPI.Service.Implementations
{
    public class GetByIdServiceAsync
    {
        public class Query : IRequest<Coupon>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Coupon>
        {
            private readonly ApplicationDbContext _context;
            public ILogger<GetByIdServiceAsync> _logger { get; }

            public Handler(ApplicationDbContext context, ILogger<GetByIdServiceAsync> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Coupon> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Coupons.FindAsync(request.Id);
            }
        }
    }
}
