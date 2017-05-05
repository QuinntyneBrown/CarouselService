import { CarouselItem } from "./carousel-item.model";

export const carouselItemActions = {
    ADD: "[CarouselItem] Add",
    EDIT: "[CarouselItem] Edit",
    DELETE: "[CarouselItem] Delete",
    CAROUSEL_ITEMS_CHANGED: "[CarouselItem] CarouselItems Changed"
};

export class CarouselItemEvent extends CustomEvent {
    constructor(eventName:string, carouselItem: CarouselItem) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { carouselItem }
        });
    }
}

export class CarouselItemAdd extends CarouselItemEvent {
    constructor(carouselItem: CarouselItem) {
        super(carouselItemActions.ADD, carouselItem);        
    }
}

export class CarouselItemEdit extends CarouselItemEvent {
    constructor(carouselItem: CarouselItem) {
        super(carouselItemActions.EDIT, carouselItem);
    }
}

export class CarouselItemDelete extends CarouselItemEvent {
    constructor(carouselItem: CarouselItem) {
        super(carouselItemActions.DELETE, carouselItem);
    }
}

export class CarouselItemsChanged extends CustomEvent {
    constructor(carouselItems: Array<CarouselItem>) {
        super(carouselItemActions.CAROUSEL_ITEMS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { carouselItems }
        });
    }
}
