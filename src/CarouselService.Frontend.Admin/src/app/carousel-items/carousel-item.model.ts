export class CarouselItem { 

    public id:any;
    
    public name:string;

    public static fromJSON(data: { name:string }): CarouselItem {

        let carouselItem = new CarouselItem();

        carouselItem.name = data.name;

        return carouselItem;
    }
}
