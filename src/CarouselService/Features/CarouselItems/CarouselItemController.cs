using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CarouselService.Features.Core;
using static CarouselService.Features.CarouselItems.AddOrUpdateCarouselItemCommand;
using static CarouselService.Features.CarouselItems.GetCarouselItemsQuery;
using static CarouselService.Features.CarouselItems.GetCarouselItemByIdQuery;
using static CarouselService.Features.CarouselItems.RemoveCarouselItemCommand;

namespace CarouselService.Features.CarouselItems
{
    [Authorize]
    [RoutePrefix("api/carouselItem")]
    public class CarouselItemController : ApiController
    {
        public CarouselItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateCarouselItemResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateCarouselItemRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateCarouselItemResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateCarouselItemRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetCarouselItemsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetCarouselItemsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetCarouselItemByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetCarouselItemByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveCarouselItemResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveCarouselItemRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
