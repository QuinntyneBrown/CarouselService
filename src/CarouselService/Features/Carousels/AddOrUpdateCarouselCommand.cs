using MediatR;
using CarouselService.Data;
using CarouselService.Data.Model;
using CarouselService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace CarouselService.Features.Carousels
{
    public class AddOrUpdateCarouselCommand
    {
        public class AddOrUpdateCarouselRequest : IRequest<AddOrUpdateCarouselResponse>
        {
            public CarouselApiModel Carousel { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateCarouselResponse { }

        public class AddOrUpdateCarouselHandler : IAsyncRequestHandler<AddOrUpdateCarouselRequest, AddOrUpdateCarouselResponse>
        {
            public AddOrUpdateCarouselHandler(CarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateCarouselResponse> Handle(AddOrUpdateCarouselRequest request)
            {
                var entity = await _context.Carousels
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Carousel.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Carousels.Add(entity = new Carousel() { TenantId = tenant.Id });
                }

                entity.Name = request.Carousel.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateCarouselResponse();
            }

            private readonly CarouselServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
