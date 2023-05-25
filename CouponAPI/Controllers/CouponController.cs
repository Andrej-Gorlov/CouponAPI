namespace CouponAPI.Controllers
{
    public class CouponController : BaseApiController
    {
        public CouponController(ILogger<CouponController> logger):base(logger) { }

        /// <summary>
        /// Список купонов.
        /// </summary>
        /// <returns>Вывод всех купонов.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /coupons
        ///     
        /// </remarks>
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [Route("coupons")]
        public async Task<ActionResult<List<CouponDTO>>> Get() =>
             HandleResult( await Mediator.Send(new GetServiceAsync.Query()), "CouponController", "Get");


        /// <summary>
        /// Вывод купонов по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вывод данных купона.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /coupon/{id:int}
        ///     
        ///        Id: 0   // Введите id купона.
        ///     
        /// </remarks>
        [HttpGet]
        [ResponseCache(Duration = 120)]
        [Route("coupon/{id:int}")]
        public async Task<ActionResult<CouponDTO>> GetById(int id) =>
             HandleResult( await Mediator.Send(new GetByIdServiceAsync.Query { Id = id }), 
                 "CouponController", "GetById");


        /// <summary>
        /// Создание нового купона.
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns>Создаётся купон.</returns>
        /// <remarks>
        /// 
        ///     POST /coupon   
        ///     
        /// </remarks>
        [HttpPost]
        [Route("coupon")]
        public async Task<IActionResult> Create(CreateCouponDTO coupon) =>
            HandleResult( await Mediator.Send(new CreateServiceAsync.Command { Coupon = coupon }), 
                "CouponController", "Create");


        /// <summary>
        /// Обновление купона.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coupon"></param>
        /// <returns>Обновление купона.</returns>
        /// <remarks>
        ///
        ///     PUT /coupon/{id:int}
        ///     
        ///         Id: 0   // Введите id купона.
        ///
        /// </remarks>
        [HttpPut]
        [Route("coupon/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, UpdateCouponDTO coupon)
        {
            coupon.CouponId = id;
            return HandleResult( await Mediator.Send(new UpdateServiceAsync.Command { Coupon = coupon }), 
                "CouponController", "Update");
        }

        /// <summary>
        /// Удаление купона.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Купон удаляется.</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     DELETE /coupon/{id}
        ///     
        ///        Id: 0   // Введите id купона.
        ///     
        /// </remarks>
        [HttpDelete]
        [Route("coupon/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id) =>
            HandleResult( await Mediator.Send(new DeleteServiceAsync.Command { Id = id }), 
                "CouponController", "Delete");

    }
}
