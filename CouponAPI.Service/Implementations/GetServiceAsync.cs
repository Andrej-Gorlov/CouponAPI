using CouponAPI.DAL;
using CouponAPI.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CouponAPI.Service.Implementations
{
    public class GetServiceAsync
    {
        public class Query : IRequest<List<Coupon>> { }

        public class Handler : IRequestHandler<Query, List<Coupon>>
        {
            private readonly ApplicationDbContext _context;
            public ILogger<GetServiceAsync> _logger { get; }

            public Handler(ApplicationDbContext context, ILogger<GetServiceAsync> logger)
            {
                _logger = logger;
                _context = context;
            }

            public async Task<List<Coupon>> Handle(Query request, CancellationToken cancellationToken)
            {

                return await _context.Coupons.ToListAsync();
            }
        }
    }
}
