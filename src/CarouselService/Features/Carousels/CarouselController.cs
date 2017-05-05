using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CarouselService.Features.Core;
using static CarouselService.Features.Carousels.AddOrUpdateCarouselCommand;
using static CarouselService.Features.Carousels.GetCarouselsQuery;
using static CarouselService.Features.Carousels.GetCarouselByIdQuery;
using static CarouselService.Features.Carousels.RemoveCarouselCommand;

namespace CarouselService.Features.Carousels
{
    [Authorize]
    [RoutePrefix("api/carousel")]
    public class CarouselController : ApiController
    {
        public CarouselController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateCarouselResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateCarouselRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateCarouselResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateCarouselRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetCarouselsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetCarouselsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetCarouselByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetCarouselByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveCarouselResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveCarouselRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
