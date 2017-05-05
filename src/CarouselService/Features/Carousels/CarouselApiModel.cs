using CarouselService.Data.Model;

namespace CarouselService.Features.Carousels
{
    public class CarouselApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromCarousel<TModel>(Carousel carousel) where
            TModel : CarouselApiModel, new()
        {
            var model = new TModel();
            model.Id = carousel.Id;
            model.TenantId = carousel.TenantId;
            model.Name = carousel.Name;
            return model;
        }

        public static CarouselApiModel FromCarousel(Carousel carousel)
            => FromCarousel<CarouselApiModel>(carousel);

    }
}
