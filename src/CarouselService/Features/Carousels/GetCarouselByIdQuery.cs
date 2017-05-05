using MediatR;
using CarouselService.Data;
using CarouselService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace CarouselService.Features.Carousels
{
    public class GetCarouselByIdQuery
    {
        public class GetCarouselByIdRequest : IRequest<GetCarouselByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetCarouselByIdResponse
        {
            public CarouselApiModel Carousel { get; set; } 
        }

        public class GetCarouselByIdHandler : IAsyncRequestHandler<GetCarouselByIdRequest, GetCarouselByIdResponse>
        {
            public GetCarouselByIdHandler(CarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetCarouselByIdResponse> Handle(GetCarouselByIdRequest request)
            {                
                return new GetCarouselByIdResponse()
                {
                    Carousel = CarouselApiModel.FromCarousel(await _context.Carousels
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly CarouselServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
