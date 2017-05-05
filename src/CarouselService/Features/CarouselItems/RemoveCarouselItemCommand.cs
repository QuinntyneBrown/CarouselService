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
    public class RemoveCarouselItemCommand
    {
        public class RemoveCarouselItemRequest : IRequest<RemoveCarouselItemResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveCarouselItemResponse { }

        public class RemoveCarouselItemHandler : IAsyncRequestHandler<RemoveCarouselItemRequest, RemoveCarouselItemResponse>
        {
            public RemoveCarouselItemHandler(CarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveCarouselItemResponse> Handle(RemoveCarouselItemRequest request)
            {
                var carouselItem = await _context.CarouselItems.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                carouselItem.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveCarouselItemResponse();
            }

            private readonly CarouselServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
