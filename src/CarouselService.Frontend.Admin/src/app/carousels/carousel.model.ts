import { CarouselItem } from "../carousel-items";

export class Carousel { 

    public id:any;
    
    public name: string;

    public carouselItems: Array<CarouselItem> = [];

    public static fromJSON(data: any): Carousel {

        let carousel = new Carousel();

        carousel.name = data.name;

        carousel.carouselItems = data.carouselItems;

        return carousel;
    }
}
