import { CarouselAdd, CarouselDelete, CarouselEdit, carouselActions } from "./carousel.actions";
import { Carousel } from "./carousel.model";
import { CarouselService } from "./carousel.service";

const template = require("./carousel-master-detail.component.html");
const styles = require("./carousel-master-detail.component.scss");

export class CarouselMasterDetailComponent extends HTMLElement {
    constructor(
        private _carouselService: CarouselService = CarouselService.Instance	
	) {
        super();
        this.onCarouselAdd = this.onCarouselAdd.bind(this);
        this.onCarouselEdit = this.onCarouselEdit.bind(this);
        this.onCarouselDelete = this.onCarouselDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "carousels"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.carousels = await this._carouselService.get();
        this.carouselListElement.setAttribute("carousels", JSON.stringify(this.carousels));
    }

    private _setEventListeners() {
        this.addEventListener(carouselActions.ADD, this.onCarouselAdd);
        this.addEventListener(carouselActions.EDIT, this.onCarouselEdit);
        this.addEventListener(carouselActions.DELETE, this.onCarouselDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(carouselActions.ADD, this.onCarouselAdd);
        this.removeEventListener(carouselActions.EDIT, this.onCarouselEdit);
        this.removeEventListener(carouselActions.DELETE, this.onCarouselDelete);
    }

    public async onCarouselAdd(e) {

        await this._carouselService.add(e.detail.carousel);
        this.carousels = await this._carouselService.get();
        
        this.carouselListElement.setAttribute("carousels", JSON.stringify(this.carousels));
        this.carouselEditElement.setAttribute("carousel", JSON.stringify(new Carousel()));
    }

    public onCarouselEdit(e) {
        this.carouselEditElement.setAttribute("carousel", JSON.stringify(e.detail.carousel));
    }

    public async onCarouselDelete(e) {

        await this._carouselService.remove(e.detail.carousel.id);
        this.carousels = await this._carouselService.get();
        
        this.carouselListElement.setAttribute("carousels", JSON.stringify(this.carousels));
        this.carouselEditElement.setAttribute("carousel", JSON.stringify(new Carousel()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "carousels":
                this.carousels = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Carousel> { return this.carousels; }

    private carousels: Array<Carousel> = [];
    public carousel: Carousel = <Carousel>{};
    public get carouselEditElement(): HTMLElement { return this.querySelector("ce-carousel-edit-embed") as HTMLElement; }
    public get carouselListElement(): HTMLElement { return this.querySelector("ce-carousel-paginated-list-embed") as HTMLElement; }
}

customElements.define(`ce-carousel-master-detail`,CarouselMasterDetailComponent);
