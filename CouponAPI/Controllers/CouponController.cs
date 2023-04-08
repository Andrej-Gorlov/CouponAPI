using CouponAPI.Domain.Entity;
using CouponAPI.Service.Implementations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace CouponAPI.Controllers
{
    public class CouponController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Coupon>>> GetActivitys()
        {
            return await Mediator.Send(new GetServiceAsync.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetActivity(int id)
        {
            return await Mediator.Send(new GetByIdServiceAsync.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Coupon coupon)
        {
            return Ok(await Mediator.Send(new CreateServiceAsync.Command { Coupon = coupon }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Coupon coupon)
        {
            coupon.CouponId = id;
            return Ok(await Mediator.Send(new UpdateServiceAsync.Command { Coupon = coupon }));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Coupon>> DeleteActivity(int id)
        {
            return Ok(await Mediator.Send(new DeleteServiceAsync.Command { Id = id }));
        }
    }
}
