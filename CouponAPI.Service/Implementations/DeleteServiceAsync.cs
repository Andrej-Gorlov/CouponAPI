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

            public ILogger<DeleteServiceAsync> _logger;
            public Handler(ApplicationDbContext context, ILogger<DeleteServiceAsync> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("поиск купона по id.");
                var coupon = await _context.Coupons.FindAsync(request.Id);

                _logger.LogInformation("удаление купона по id.");
                _context.Remove(coupon);

                _logger.LogInformation("сохранение изменений в бд.");
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
