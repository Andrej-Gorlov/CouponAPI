namespace CouponAPI.Service.Implementations
{
    public class CreateServiceAsync
    {
        public class Command : IRequest<IBaseResponse<Unit>>
        {
            public CreateCouponDTO? Coupon { get; set; }
        }
        public class Handler : IRequestHandler<Command, IBaseResponse<Unit>>
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

            public async Task<IBaseResponse<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("добавление купона.");
                _context.Coupons.Add(_mapper.Map<Coupon>(request.Coupon));

                _logger.LogInformation("сохранение изменений в бд.");
                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    _logger.LogInformation("Количество записей состояния, записанных в базу данных равен нулю" +
                        "Не удалось создать купон (class: CreateServiceAsync/method: Handle).");
                    return new BaseResponse<Unit>().Failure("Не удалось создать купон.");
                }
                return new BaseResponse<Unit>().Success(Unit.Value, ResponseStatus.Created);
            }
        }
    }
}
