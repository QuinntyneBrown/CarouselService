import { CarouselItemAdd, CarouselItemDelete, CarouselItemEdit, carouselItemActions } from "./carousel-item.actions";
import { CarouselItem } from "./carousel-item.model";
import { CarouselItemService } from "./carousel-item.service";

const template = require("./carousel-item-master-detail.component.html");
const styles = require("./carousel-item-master-detail.component.scss");

export class CarouselItemMasterDetailComponent extends HTMLElement {
    constructor(
        private _carouselItemService: CarouselItemService = CarouselItemService.Instance	
	) {
        super();
        this.onCarouselItemAdd = this.onCarouselItemAdd.bind(this);
        this.onCarouselItemEdit = this.onCarouselItemEdit.bind(this);
        this.onCarouselItemDelete = this.onCarouselItemDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "carousel-items"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.carouselItems = await this._carouselItemService.get();
        this.carouselItemListElement.setAttribute("carousel-items", JSON.stringify(this.carouselItems));
    }

    private _setEventListeners() {
        this.addEventListener(carouselItemActions.ADD, this.onCarouselItemAdd);
        this.addEventListener(carouselItemActions.EDIT, this.onCarouselItemEdit);
        this.addEventListener(carouselItemActions.DELETE, this.onCarouselItemDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(carouselItemActions.ADD, this.onCarouselItemAdd);
        this.removeEventListener(carouselItemActions.EDIT, this.onCarouselItemEdit);
        this.removeEventListener(carouselItemActions.DELETE, this.onCarouselItemDelete);
    }

    public async onCarouselItemAdd(e) {

        await this._carouselItemService.add(e.detail.carouselItem);
        this.carouselItems = await this._carouselItemService.get();
        
        this.carouselItemListElement.setAttribute("carousel-items", JSON.stringify(this.carouselItems));
        this.carouselItemEditElement.setAttribute("carousel-item", JSON.stringify(new CarouselItem()));
    }

    public onCarouselItemEdit(e) {
        this.carouselItemEditElement.setAttribute("carousel-item", JSON.stringify(e.detail.carouselItem));
    }

    public async onCarouselItemDelete(e) {

        await this._carouselItemService.remove(e.detail.carouselItem.id);
        this.carouselItems = await this._carouselItemService.get();
        
        this.carouselItemListElement.setAttribute("carousel-items", JSON.stringify(this.carouselItems));
        this.carouselItemEditElement.setAttribute("carousel-item", JSON.stringify(new CarouselItem()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "carousel-items":
                this.carouselItems = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<CarouselItem> { return this.carouselItems; }

    private carouselItems: Array<CarouselItem> = [];
    public carouselItem: CarouselItem = <CarouselItem>{};
    public get carouselItemEditElement(): HTMLElement { return this.querySelector("ce-carousel-item-edit-embed") as HTMLElement; }
    public get carouselItemListElement(): HTMLElement { return this.querySelector("ce-carousel-item-paginated-list-embed") as HTMLElement; }
}

customElements.define(`ce-carousel-item-master-detail`,CarouselItemMasterDetailComponent);
