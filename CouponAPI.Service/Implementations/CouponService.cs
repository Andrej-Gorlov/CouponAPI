using CouponAPI.Domain.Entity;
using System.Xml.Linq;
using WatchDog;

namespace CouponAPI.Service.Implementations
{
    public class CouponService : ICouponService
    {
        private ICouponRepository _couponRep;
        private IMapper _mapper;
        private BaseResponse<CouponDTO> baseResponse;
        private string message = "";
        public CouponService(ICouponRepository couponRep, IMapper mapper)
        {
            _couponRep = couponRep;
            _mapper = mapper;
            baseResponse = new();
        }
        /// <summary>
        /// Создание купона.
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns>Базовый ответ.</returns>
        public async Task<IBaseResponse<CouponDTO>> CreateServiceAsync(CreateCouponDTO createModel)
        {
            WatchLogger.Log($"Создание купона. / method: CreateServiceAsync");
            if (await _couponRep.GetByAsync(x => x.CouponCode == createModel.CouponCode) != null)
            {
                WatchLogger.Log("Купон с таким кодом существует.");
                baseResponse.DisplayMessage = "Купон с таким кодом существует.";
                baseResponse.Status = Status.ExistsCode;
                return baseResponse;
            }
            var coupon = await _couponRep.CreateAsync(_mapper.Map<Coupon>(createModel));
            if (coupon != null)
            {
                WatchLogger.Log("Купон создан.");
            }
            else
            {
                WatchLogger.Log("Купон не создан.");
                baseResponse.DisplayMessage = "Купон не создан.";
            }
            baseResponse.Result = _mapper.Map<CouponDTO>(coupon);
            WatchLogger.Log($"Ответ отправлен контролеру/method: CreateServiceAsync");
            return baseResponse;
        }
        /// <summary>
        /// Удаление купона
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Базовый ответ.</returns>
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            WatchLogger.Log($"Удаление купона. / method: DeleteServiceAsync");
            var baseResponse = new BaseResponse<bool>();
            WatchLogger.Log($"Поиск купона по id: {id}. / method: DeleteServiceAsync");
            Coupon coupon = await _couponRep.GetByAsync(x => x.CouponId == id, true);
            if (coupon is null)
            {
                WatchLogger.Log($"Купон c id: {id} не найден.");
                baseResponse.DisplayMessage = $"Купон c id: {id} не найден.";
                baseResponse.Result = false;
                WatchLogger.Log($"Ответ отправлен контролеру (false)/ method: DeleteServiceAsync");
                return baseResponse;
            }
            await _couponRep.DeleteAsync(coupon);
            baseResponse.DisplayMessage = "Купон удален.";
            baseResponse.Result = true;
            WatchLogger.Log($"Ответ отправлен контролеру (true)/ method: DeleteServiceAsync");
            return baseResponse;
        }
        /// <summary>
        /// Вывод купона
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Базовый ответ.</returns>
        public async Task<IBaseResponse<CouponDTO>> GetByIdServiceAsync(int id)
        {
            WatchLogger.Log($"Поиск купона по id: {id}. / method: GetByIdServiceAsync");
            Coupon coupon = await _couponRep.GetByAsync(x => x.CouponId == id);
            if (coupon is null)
            {
                WatchLogger.Log($"Купон по id [{id}] не найден.");
                baseResponse.DisplayMessage = $"Купон по id [{id}] не найден.";
            }
            else
            {
                WatchLogger.Log($"Вывод купон по id [{id}]");
            }
            baseResponse.Result = _mapper.Map<CouponDTO>(coupon);
            WatchLogger.Log($"Ответ отправлен контролеру/ method: GetByIdServiceAsync");
            return baseResponse;
        }
        /// <summary>
        /// Список купонов (возможно приминение фильра и поиска)
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="filter"></param>
        /// <param name="search"></param>
        /// <returns>Базовый ответ.</returns>
        public async Task<IBaseResponse<PagedList<CouponDTO>>> GetServiceAsync(PagingQueryParameters paging, string? filter = null, string? search = null)
        {
            WatchLogger.Log($"Список купонов. /method: GetServiceAsync");
            var baseResponse = new BaseResponse<PagedList<CouponDTO>>();
            IEnumerable<Coupon>? coupons = null;
            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(search))
            {
                var result = await FilterAndSearchAsync(coupons, filter, search);
                coupons = result.Item1;
                baseResponse.DisplayMessage = result.Item2;
            }
            else if (!string.IsNullOrEmpty(filter) && string.IsNullOrEmpty(search))
            {
                var result = await FilterAndSearchAsync(coupons, filter);
                coupons = result.Item1;
                baseResponse.DisplayMessage = result.Item2;
            }
            else if (string.IsNullOrEmpty(filter) && string.IsNullOrEmpty(search))
            {
                coupons = await _couponRep.GetAsync(search: search);
            }
            if (coupons is null)
            {
                WatchLogger.Log("Список купонов.");
                baseResponse.DisplayMessage = "Список купонов.";
            }
            else
            {
                WatchLogger.Log("Список купонов.");
                IEnumerable<CouponDTO> listCoupons = _mapper.Map<IEnumerable<CouponDTO>>(coupons);
                WatchLogger.Log("применение пагинации. /method: GetServiceAsync");
                baseResponse.Result = PagedList<CouponDTO>.ToPagedList(
                    listCoupons, paging.PageNumber, paging.PageSize);
            }
            WatchLogger.Log($"Ответ отправлен контролеру/ method: GetServiceAsync");
            return baseResponse;
        }
        /// <summary>
        /// Обновление купона.
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns>Базовый ответ.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IBaseResponse<CouponDTO>> UpdateServiceAsync(UpdateCouponDTO updateModel)
        {
            WatchLogger.Log($"Обновление купона.");
            var carent = await _couponRep.GetByAsync(x => x.CouponId == updateModel.CouponId, false);
            if (carent is null)
            {
                WatchLogger.Log("Попытка обновить объект, которого нет в хранилище.");
                throw new NullReferenceException("Попытка обновить объект, которого нет в хранилище.");
            }
            var coupon = await _couponRep.UpdateAsync(_mapper.Map<Coupon>(updateModel), carent); ;
            baseResponse.DisplayMessage = "Купон обновился.";
            baseResponse.Result = _mapper.Map<CouponDTO>(coupon);
            WatchLogger.Log($"Ответ отправлен контролеру/ method: UpdateServiceAsync");
            return baseResponse;
        }
        /// <summary>
        /// Фильтр и поиск купонов по значению
        /// </summary>
        /// <param name="coupons"></param>
        /// <param name="filter"></param>
        /// <returns>Отфильтрованный список и сообщение</returns>
        private async Task<(IEnumerable<Coupon>?, string)> FilterAndSearchAsync(IEnumerable<Coupon>? coupons, string filter, string? search = null)
        {
            WatchLogger.Log($"Поиск купонов по фильтру: {filter}. / method: FilterAsync");
            DateTime date;
            decimal price;
            if (DateTime.TryParse(filter.ToString(), out date))
            {
                coupons = await _couponRep.GetAsync(x => x.DateTimeCreateCoupon.Date == date.Date, search);

                if (coupons is null)
                    MessageFilter(true, filter, search);
                else
                    MessageFilter(false, filter, search);
            }
            else if (Decimal.TryParse(filter.ToString(), out price))
            {
                coupons = await _couponRep.GetAsync(x => x.DiscountAmount == price, search);

                if (coupons is null)
                    MessageFilter(true, filter, search);
                else
                    MessageFilter(false, filter, search);
            }
            else
            {
                coupons = await _couponRep.GetAsync(x => x.CouponCode == filter, search);
                if (coupons is null)
                    MessageFilter(true, filter, search);
                else
                    MessageFilter(false, filter, search);
            }
            WatchLogger.Log($"Ответ отправлен GetServiceAsync/ method: FilterAsync");
            return (coupons, message);
        }
        /// <summary>
        /// Присвоение сообщения по фильтру
        /// </summary>
        /// <param name="isNull"></param>
        /// <param name="filter"></param>
        private void MessageFilter(bool isNull, string filter, string? search = null)
        {
            if (isNull)
            {
                WatchLogger.Log($"Список купонов пуст по фильтру: {filter}");
                message = $"Список купонов пуст по фильтру: {filter}";
                if (search != null)
                {
                    WatchLogger.Log($"и по поиску: {search}.");
                    message += $"и по поиску: {search}.";
                }
            }
            else
            {
                WatchLogger.Log($"Список купонов по фильтру: {filter}.");
                if (search != null)
                    WatchLogger.Log($"Список купонов по фильтру: {search}.");
            }
        }
    }
}
