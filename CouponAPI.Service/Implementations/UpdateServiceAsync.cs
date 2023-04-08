using AutoMapper;
using CouponAPI.DAL;
using CouponAPI.Domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CouponAPI.Service.Implementations
{
    public class UpdateServiceAsync
    {
        public class Command : IRequest
        {
            public Coupon? Coupon { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;
            public ILogger<UpdateServiceAsync> _logger { get; }
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper, ILogger<UpdateServiceAsync> logger)
            {
                _mapper = mapper;
                _context = context;
                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var coupon = await _context.Coupons.FindAsync(request.Coupon.CouponId);

                _mapper.Map(request.Coupon, coupon);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
