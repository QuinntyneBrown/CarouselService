using MediatR;
using CarouselService.Data;
using CarouselService.Data.Model;
using CarouselService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace CarouselService.Features.CarouselItems
{
    public class AddOrUpdateCarouselItemCommand
    {
        public class AddOrUpdateCarouselItemRequest : IRequest<AddOrUpdateCarouselItemResponse>
        {
            public CarouselItemApiModel CarouselItem { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateCarouselItemResponse { }

        public class AddOrUpdateCarouselItemHandler : IAsyncRequestHandler<AddOrUpdateCarouselItemRequest, AddOrUpdateCarouselItemResponse>
        {
            public AddOrUpdateCarouselItemHandler(CarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateCarouselItemResponse> Handle(AddOrUpdateCarouselItemRequest request)
            {
                var entity = await _context.CarouselItems
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.CarouselItem.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.CarouselItems.Add(entity = new CarouselItem() { TenantId = tenant.Id });
                }

                entity.Name = request.CarouselItem.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateCarouselItemResponse();
            }

            private readonly CarouselServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
