import { Carousel } from "./carousel.model";

export const carouselActions = {
    ADD: "[Carousel] Add",
    EDIT: "[Carousel] Edit",
    DELETE: "[Carousel] Delete",
    CAROUSELS_CHANGED: "[Carousel] Carousels Changed"
};

export class CarouselEvent extends CustomEvent {
    constructor(eventName:string, carousel: Carousel) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { carousel }
        });
    }
}

export class CarouselAdd extends CarouselEvent {
    constructor(carousel: Carousel) {
        super(carouselActions.ADD, carousel);        
    }
}

export class CarouselEdit extends CarouselEvent {
    constructor(carousel: Carousel) {
        super(carouselActions.EDIT, carousel);
    }
}

export class CarouselDelete extends CarouselEvent {
    constructor(carousel: Carousel) {
        super(carouselActions.DELETE, carousel);
    }
}

export class CarouselsChanged extends CustomEvent {
    constructor(carousels: Array<Carousel>) {
        super(carouselActions.CAROUSELS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { carousels }
        });
    }
}
