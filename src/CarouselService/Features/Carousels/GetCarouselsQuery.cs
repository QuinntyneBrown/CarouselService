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
    public class GetCarouselsQuery
    {
        public class GetCarouselsRequest : IRequest<GetCarouselsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetCarouselsResponse
        {
            public ICollection<CarouselApiModel> Carousels { get; set; } = new HashSet<CarouselApiModel>();
        }

        public class GetCarouselsHandler : IAsyncRequestHandler<GetCarouselsRequest, GetCarouselsResponse>
        {
            public GetCarouselsHandler(CarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetCarouselsResponse> Handle(GetCarouselsRequest request)
            {
                var carousels = await _context.Carousels
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetCarouselsResponse()
                {
                    Carousels = carousels.Select(x => CarouselApiModel.FromCarousel(x)).ToList()
                };
            }

            private readonly CarouselServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
