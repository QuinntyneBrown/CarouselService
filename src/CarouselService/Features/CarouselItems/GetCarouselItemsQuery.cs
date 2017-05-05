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
    public class GetCarouselItemsQuery
    {
        public class GetCarouselItemsRequest : IRequest<GetCarouselItemsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetCarouselItemsResponse
        {
            public ICollection<CarouselItemApiModel> CarouselItems { get; set; } = new HashSet<CarouselItemApiModel>();
        }

        public class GetCarouselItemsHandler : IAsyncRequestHandler<GetCarouselItemsRequest, GetCarouselItemsResponse>
        {
            public GetCarouselItemsHandler(CarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetCarouselItemsResponse> Handle(GetCarouselItemsRequest request)
            {
                var carouselItems = await _context.CarouselItems
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetCarouselItemsResponse()
                {
                    CarouselItems = carouselItems.Select(x => CarouselItemApiModel.FromCarouselItem(x)).ToList()
                };
            }

            private readonly CarouselServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
