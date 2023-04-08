using CouponAPI.Domain.Entity.CouponDTO;
using CouponAPI.Service.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace CouponAPI.Controllers
{
    public class CouponController : BaseApiController
    {
        public ILogger<CouponController> _logger;

        public CouponController(ILogger<CouponController> logger)
        {
            _logger = logger;
        }

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
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [Route("coupons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CouponDTO>>> Get()
        {
            _logger.LogInformation($"Ответ отправлен. статус: {Ok().StatusCode} /CouponController/method: Get");
            return await Mediator.Send(new GetServiceAsync.Query());
        }

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
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpGet]
        [Route("coupon/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CouponDTO>> GetById(int id)
        {
            _logger.LogInformation($"Ответ отправлен. статус: {Ok().StatusCode} /CouponController/method: GetById");
            return await Mediator.Send(new GetByIdServiceAsync.Query { Id = id });
        }

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
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpPost]
        [Route("coupon")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateCouponDTO coupon)
        {
            _logger.LogInformation($"Ответ отправлен. Cтатус: 201 /CouponController/method: Create");
            return CreatedAtAction(nameof(Get), await Mediator.Send(new CreateServiceAsync.Command { Coupon = coupon }));
        }

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
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpPut]
        [Route("coupon/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, UpdateCouponDTO coupon)
        {
            coupon.CouponId = id;
            _logger.LogInformation($"Ответ отправлен. статус: {Ok().StatusCode} /CouponController/method: Update");
            return Ok(await Mediator.Send(new UpdateServiceAsync.Command { Coupon = coupon }));
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
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        [HttpDelete]
        [Route("coupon/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation($"Ответ отправлен. статус: {Ok().StatusCode} /CouponController/method: Delete");
            return Ok(await Mediator.Send(new DeleteServiceAsync.Command { Id = id }));
        }
    }
}
