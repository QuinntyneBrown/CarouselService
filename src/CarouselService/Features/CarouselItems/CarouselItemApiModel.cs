using CarouselService.Data.Model;

namespace CarouselService.Features.CarouselItems
{
    public class CarouselItemApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromCarouselItem<TModel>(CarouselItem carouselItem) where
            TModel : CarouselItemApiModel, new()
        {
            var model = new TModel();
            model.Id = carouselItem.Id;
            model.TenantId = carouselItem.TenantId;
            model.Name = carouselItem.Name;
            return model;
        }

        public static CarouselItemApiModel FromCarouselItem(CarouselItem carouselItem)
            => FromCarouselItem<CarouselItemApiModel>(carouselItem);

    }
}
