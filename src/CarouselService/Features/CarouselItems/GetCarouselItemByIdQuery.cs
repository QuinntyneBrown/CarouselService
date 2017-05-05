using MediatR;
using CarouselService.Data;
using CarouselService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace CarouselService.Features.CarouselItems
{
    public class GetCarouselItemByIdQuery
    {
        public class GetCarouselItemByIdRequest : IRequest<GetCarouselItemByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetCarouselItemByIdResponse
        {
            public CarouselItemApiModel CarouselItem { get; set; } 
        }

        public class GetCarouselItemByIdHandler : IAsyncRequestHandler<GetCarouselItemByIdRequest, GetCarouselItemByIdResponse>
        {
            public GetCarouselItemByIdHandler(CarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetCarouselItemByIdResponse> Handle(GetCarouselItemByIdRequest request)
            {                
                return new GetCarouselItemByIdResponse()
                {
                    CarouselItem = CarouselItemApiModel.FromCarouselItem(await _context.CarouselItems
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly CarouselServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
