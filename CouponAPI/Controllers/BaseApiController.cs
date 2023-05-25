namespace CouponAPI.Controllers
{
    [Route("api/")]
    [Produces("application/json")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator? Mediator => _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>();

        protected readonly ILogger<BaseApiController> _logger;

        public BaseApiController(ILogger<BaseApiController> logger) => _logger = logger;

        /// <summary>
        /// Обработка кода состояния HTTP.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result">Базовый ответ.</param>
        /// <param name="nameController">Название Controller.</param>
        /// <param name="nameMethod">Название метода.</param>
        /// <returns>Код состояния HTTP</returns>
        /// <response code="200"> Запрос прошёл. (Успех) </response>
        /// <response code="201"> Объект создан. </response>
        /// <response code="400"> Введены недопустимые данные. </response>
        /// <response code="404"> Объект не найден. </response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        protected ActionResult HandleResult<T>(IBaseResponse<T> result, string nameController, string nameMethod)
        {
            if (result is null)
            {
                _logger.LogInformation($"Ответ отправлен. статус: {NotFound().StatusCode} /{nameController}/{nameMethod}: Get");
                return NotFound(); ;
            }
            if (result.IsSuccess && result.Result != null && result.ResponseStatus is ResponseStatus.Ok)
            {
                _logger.LogInformation($"Ответ отправлен. статус: {Ok().StatusCode} /{nameController}/{nameMethod}: Get");
                return Ok(result.Result);
            }
            if (result.IsSuccess && result.Result != null && result.ResponseStatus is ResponseStatus.Created)
            {
                _logger.LogInformation($"Ответ отправлен. статус: Cтатус: 201 /{nameController}/{nameMethod}: Get");
                return CreatedAtAction($"{nameMethod}", result.Result);
            }
            if (result.IsSuccess && result.Result is null)
            {
                _logger.LogInformation($"Ответ отправлен. статус: {NotFound().StatusCode} /{nameController}/{nameMethod}: Get");
                return NotFound();
            }
            _logger.LogInformation($"Ответ отправлен. статус: {BadRequest().StatusCode} /{nameController}/{nameMethod}: Get");
            return BadRequest(result.Error);
        }
    }
}
