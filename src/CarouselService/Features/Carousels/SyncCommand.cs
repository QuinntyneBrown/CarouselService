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
    public class SyncCommand
    {
        public class SyncRequest : IRequest<SyncResponse>
        {
            public Guid TenantUniqueId { get; set; }
        }

        public class SyncResponse
        {
            public SyncResponse()
            {

            }
        }

        public class SyncHandler : IAsyncRequestHandler<SyncRequest, SyncResponse>
        {
            public SyncHandler(CarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<SyncResponse> Handle(SyncRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly CarouselServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
