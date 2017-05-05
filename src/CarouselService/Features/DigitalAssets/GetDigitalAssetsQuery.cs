using MediatR;
using CarouselService.Data;
using CarouselService.Data.Model;
using CarouselService.Features.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using static CarouselService.Features.DigitalAssets.Constants;

namespace CarouselService.Features.DigitalAssets
{
    public class GetDigitalAssetsQuery
    {
        public class GetDigitalAssetsRequest : IRequest<GetDigitalAssetsResponse> {
            public Guid TenantUniqueId { get; set; }
        }

        public class GetDigitalAssetsResponse
        {
            public ICollection<DigitalAssetApiModel> DigitalAssets { get; set; } = new HashSet<DigitalAssetApiModel>();
        }

        public class GetDigitalAssetsHandler : IAsyncRequestHandler<GetDigitalAssetsRequest, GetDigitalAssetsResponse>
        {
            public GetDigitalAssetsHandler(ICarouselServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetDigitalAssetsResponse> Handle(GetDigitalAssetsRequest request)
            {
                var digitalAssets = await _cache.FromCacheOrServiceAsync<List<DigitalAsset>>(() => _context.DigitalAssets.ToListAsync(), DigitalAssetCacheKeys.DigitalAssets);

                return new GetDigitalAssetsResponse()
                {
                    DigitalAssets = digitalAssets.Select(x => DigitalAssetApiModel.FromDigitalAsset(x)).ToList()
                };
            }

            private readonly ICarouselServiceContext _context;
            private readonly ICache _cache;
        }
    }
}