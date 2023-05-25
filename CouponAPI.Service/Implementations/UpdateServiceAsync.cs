namespace CouponAPI.Service.Implementations
{
    public class UpdateServiceAsync
    {
        public class Command : IRequest<IBaseResponse<Unit>>
        {
            public UpdateCouponDTO? Coupon { get; set; }
        }

        public class Handler : IRequestHandler<Command, IBaseResponse<Unit>?>
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

            public async Task<IBaseResponse<Unit>?> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("поиск купона по id.");
                var coupon = await _context.Coupons.FindAsync(request.Coupon.CouponId);
                if (coupon is null)
                {
                    _logger.LogInformation("купон не найден (class: UpdateServiceAsync/method: Handle).");
                    return null;
                }
                _logger.LogInformation("применение mapper.");
                _mapper.Map(request.Coupon, coupon);

                _logger.LogInformation("сохранение изменений в бд.");
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    _logger.LogInformation("Количество записей состояния, записанных в базу данных равен нулю" +
                        "Не удалось создать купон (class: UpdateServiceAsync/method: Handle).");
                    return new BaseResponse<Unit>().Failure("Не удалось удалить купон.");
                }

                return new BaseResponse<Unit>().Success(Unit.Value, ResponseStatus.Ok);
            }
        }
    }
}
