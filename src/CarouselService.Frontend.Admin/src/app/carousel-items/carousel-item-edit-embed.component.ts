import { CarouselItem } from "./carousel-item.model";
import { EditorComponent } from "../shared";
import {  CarouselItemDelete, CarouselItemEdit, CarouselItemAdd } from "./carousel-item.actions";

const template = require("./carousel-item-edit-embed.component.html");
const styles = require("./carousel-item-edit-embed.component.scss");

export class CarouselItemEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "carousel-item",
            "carousel-item-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.carouselItem ? "Edit Carousel Item": "Create Carousel Item";

        if (this.carouselItem) {                
            this._nameInputElement.value = this.carouselItem.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._createButtonElement.addEventListener("click", this.onCreate);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._createButtonElement.removeEventListener("click", this.onCreate);
    }

    public onSave() {
        const carouselItem = {
            id: this.carouselItem != null ? this.carouselItem.id : null,
            name: this._nameInputElement.value
        } as CarouselItem;
        
        this.dispatchEvent(new CarouselItemAdd(carouselItem));            
    }

    public onCreate() {        
        this.dispatchEvent(new CarouselItemEdit(new CarouselItem()));            
    }

    public onDelete() {        
        const carouselItem = {
            id: this.carouselItem != null ? this.carouselItem.id : null,
            name: this._nameInputElement.value
        } as CarouselItem;

        this.dispatchEvent(new CarouselItemDelete(carouselItem));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "carousel-item-id":
                this.carouselItemId = newValue;
                break;
            case "carousel-item":
                this.carouselItem = JSON.parse(newValue);
                if (this.parentNode) {
                    this.carouselItemId = this.carouselItem.id;
                    this._nameInputElement.value = this.carouselItem.name != undefined ? this.carouselItem.name : "";
                    this._titleElement.textContent = this.carouselItemId ? "Edit CarouselItem" : "Create CarouselItem";
                }
                break;
        }           
    }

    public carouselItemId: any;
    
	public carouselItem: CarouselItem;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".carousel-item-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".carousel-item-name") as HTMLInputElement;}
}

customElements.define(`ce-carousel-item-edit-embed`,CarouselItemEditEmbedComponent);
