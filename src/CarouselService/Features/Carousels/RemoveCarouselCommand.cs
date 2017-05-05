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
    public class RemoveCarouselCommand
    {
        public class RemoveCarouselRequest : IRequest<RemoveCarouselResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveCarouselResponse { }

        public class RemoveCarouselHandler : IAsyncRequestHandler<RemoveCarouselRequest, RemoveCarouselResponse>
        {
            public RemoveCarouselHandler(CarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveCarouselResponse> Handle(RemoveCarouselRequest request)
            {
                var carousel = await _context.Carousels.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                carousel.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveCarouselResponse();
            }

            private readonly CarouselServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
