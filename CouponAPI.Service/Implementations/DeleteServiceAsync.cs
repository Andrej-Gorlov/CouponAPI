namespace CouponAPI.Service.Implementations
{
    public class DeleteServiceAsync
    {
        public class Command : IRequest<IBaseResponse<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, IBaseResponse<Unit>?>
        {
            private readonly ApplicationDbContext _context;

            public ILogger<DeleteServiceAsync> _logger;
            public Handler(ApplicationDbContext context, ILogger<DeleteServiceAsync> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<IBaseResponse<Unit>?> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("поиск купона по id.");
                var coupon = await _context.Coupons.FindAsync(request.Id);

                if (coupon is null)
                {
                    _logger.LogInformation("купон не найден (class: DeleteServiceAsync/method: Handle).");
                    return null;
                }

                _logger.LogInformation("удаление купона по id.");
                _context.Remove(coupon);

                _logger.LogInformation("сохранение изменений в бд.");
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    _logger.LogInformation("Количество записей состояния, записанных в базу данных равен нулю" +
                        "Не удалось создать купон (class: DeleteServiceAsync/method: Handle).");
                    return new BaseResponse<Unit>().Failure("Не удалось удалить купон.");
                }
                return new BaseResponse<Unit>().Success(Unit.Value, ResponseStatus.Ok);
            }
        }
    }
}
