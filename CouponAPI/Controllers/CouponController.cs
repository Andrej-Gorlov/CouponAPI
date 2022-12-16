using CouponAPI.Domain.Paging;
using CouponAPI.Domain.Response;
using CouponAPI.Service.Interfaces;
using Newtonsoft.Json;

namespace CouponAPI.Controllers
{
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersionNeutral]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponSer;
        public CouponController(ICouponService couponSer) => _couponSer = couponSer;

        #region Get
        /// <summary>
        /// Список всех купонов.
        /// </summary>
        /// <param name="paging">Пагинация</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="search">Поиск</param>
        /// <returns>Вывод всех купонов</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /coupons
        ///     
        ///        PageNumber: Номер страницы   // Введите номер страницы, которую нужно показать с списоком купонов (Необязательно).
        ///        PageSize: Размер страницы    // Введите размер страницы, какое количество купонов нужно вывести (Необязательно).
        ///        filter: Фильтр               // Введите значение фильтра (Необязательно).
        ///        search: Поиск                // Введите значение поиска (Необязательно).
        ///
        /// </remarks> 
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="404"> Список купонов не найден. </response>
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [Route("coupons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging, [FromQuery] string? filter, [FromQuery] string? search)
        {
            WatchLogger.Log($"выполнен вход. /CouponController/method: Get");
            IBaseResponse<PagedList<CouponDTO>>? coupons = null;

            if (!(filter is null) && !(search is null))
            {
                WatchLogger.Log($"Получение списка продуктов по фильтру: {filter} и поиску: {search}.");
                coupons = await _couponSer.GetServiceAsync(paging, filter, search);
            }
            else if (!(filter is null) && search is null)
            {
                WatchLogger.Log($"Получение списка продуктов по фильтру: {filter}.");
                coupons = await _couponSer.GetServiceAsync(paging, filter);
            }
            else if (filter is null && !(search is null))
            {
                WatchLogger.Log($"Получение списка продуктов по поиску: {search}.");
                coupons = await _couponSer.GetServiceAsync(paging, search: search);
            }
            else if (filter is null && search is null)
            {
                WatchLogger.Log($"Получение списка продуктов.");
                coupons = await _couponSer.GetServiceAsync(paging);
            }
            if (coupons.Result is null)
            {
                WatchLogger.Log($"Ответ отправлен. статус: {NotFound().StatusCode} /CouponController/method: Get");
                return NotFound(coupons);
            }
            WatchLogger.Log($"Получение метаданных пагинации.");
            var metadata = new
            {
                coupons.Result.TotalCount,
                coupons.Result.PageSize,
                coupons.Result.CurrentPage,
                coupons.Result.TotalPages,
                coupons.Result.HasNext,
                coupons.Result.HasPrevious
            };
            WatchLogger.Log($"Добавление метаданных в заголовок запроса.");
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            WatchLogger.Log($"Ответ отправлен. статус: {Ok().StatusCode} /CouponController/method: Get");
            return Ok(coupons);
        }
        #endregion

        #region GetById
        /// <summary>
        /// Вывод купона по id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Вывод данных купона</returns>
        /// <remarks>
        /// Образец запроса:
        /// 
        ///     GET /coupon/{id:int}
        ///     
        ///        Id: 0   // Введите id купона, которого нужно показать.
        ///     
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Купон не найден.</response>
        [HttpGet]
        [ResponseCache(Duration = 120)]
        [Route("coupon/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            WatchLogger.Log($"выполнен вход. /CouponController/method: GetById");
            if (id <= 0)
            {
                WatchLogger.Log($"Ответ отправлен. id: [{id}] не может быть меньше или равно нулю. статус: {NotFound().StatusCode} /ImageController/method: GetById");
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            WatchLogger.Log($"Получение купона по id: {id}.");
            var coupon = await _couponSer.GetByIdServiceAsync(id);
            if (coupon.Result is null)
            {
                WatchLogger.Log($"Ответ отправлен. статус: {NotFound().StatusCode} /CouponController/method: GetById");
                return NotFound(coupon);
            }
            WatchLogger.Log($"Ответ отправлен. статус: {Ok().StatusCode} /CouponController/method: GetById");
            return Ok(coupon);
        }
        #endregion

        #region Create
        /// <summary>
        /// Создание нового купона.
        /// </summary>
        /// <param name="couponDTO"></param>
        /// <returns>Создаётся купон</returns>
        /// <remarks>
        /// 
        ///     POST /coupon   
        ///     
        /// </remarks>
        /// <response code="201"> Купон создан. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        [HttpPost]
        [Route("coupon")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCouponDTO couponDTO)
        {
            WatchLogger.Log($"выполнен вход. /CouponController/method: Create");
            WatchLogger.Log($"Создание нового купона");
            var coupon = (BaseResponse<CouponDTO>)await _couponSer.CreateServiceAsync(couponDTO);
            if (coupon.Status is Status.ExistsCode)
            {
                ModelState.AddModelError("", "Купон с таким кодом уже существует");
                WatchLogger.Log($"Ответ отправлен. Купон с таким кодом уже существует. Cтатус: {BadRequest().StatusCode} /CouponController/method: Create");
                return BadRequest(ModelState);
            }
            if (coupon.Result is null)
            {
                WatchLogger.Log($"Ответ отправлен. Cтатус: {BadRequest().StatusCode} /CouponController/method: Create");
                return BadRequest(coupon);
            }
            WatchLogger.Log($"Ответ отправлен. Cтатус: 201 /CouponController/method: Create");
            return CreatedAtAction(nameof(Get), coupon);
        }
        #endregion

        #region Update
        /// <summary>
        /// Обновление купона.
        /// </summary>
        /// <param name="couponDTO"></param>
        /// <returns>Обновление купона.</returns>
        /// <remarks>
        ///
        ///     PUT /coupon
        ///
        /// </remarks>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        /// <response code="404"> Купон не найден. </response>
        [HttpPut]
        [Route("coupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateCouponDTO couponDTO)
        {
            WatchLogger.Log($"Выполнен вход. /CouponController/method: Update");
            WatchLogger.Log($"Обновление купона");
            var coupon = await _couponSer.UpdateServiceAsync(couponDTO);
            if (coupon.Result is null)
            {
                WatchLogger.Log($"Ответ отправлен. Cтатус: {NotFound().StatusCode} /CouponController/method: Update");
                return NotFound(coupon);
            }
            WatchLogger.Log($"Ответ отправлен. Cтатус: {Ok().StatusCode} /CouponController/method: Update");
            return Ok(coupon);
        }
        #endregion

        #region Delete
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
        ///        Id: 0   // Введите id купона, которого нужно удалить.
        ///     
        /// </remarks>
        /// <response code="204"> Купон удалён. (нет содержимого) </response>
        /// <response code="400"> Недопустимое значение ввода </response>
        /// <response code="404"> Купон c указанным id не найден. </response>
        [HttpDelete]
        [Route("coupon/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            WatchLogger.Log($"выполнен вход. /CouponController/method: Delete");
            if (id <= 0)
            {
                WatchLogger.Log($"Ответ отправлен. id: [{id}] не может быть меньше или равно нулю. Cтатус: {BadRequest().StatusCode} /CouponController/method: Delete");
                return BadRequest($"id: [{id}] не может быть меньше или равно нулю");
            }
            WatchLogger.Log("Удаление купона.");
            var coupon = await _couponSer.DeleteServiceAsync(id);
            if (coupon.Result is false)
            {
                WatchLogger.Log($"Ответ отправлен. Cтатус: {BadRequest().StatusCode} /CouponController/method: Delete");
                return NotFound(coupon);
            }
            WatchLogger.Log($"Ответ отправлен. Cтатус: {NoContent().StatusCode} /CouponController/method: Delete");
            return NoContent();
        }
        #endregion
    }
}
