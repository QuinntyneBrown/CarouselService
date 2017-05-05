import { Carousel } from "./carousel.model";
import { EditorComponent } from "../shared";
import {  CarouselDelete, CarouselEdit, CarouselAdd } from "./carousel.actions";

const template = require("./carousel-edit-embed.component.html");
const styles = require("./carousel-edit-embed.component.scss");

export class CarouselEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "carousel",
            "carousel-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.carousel ? "Edit Carousel": "Create Carousel";

        if (this.carousel) {                
            this._nameInputElement.value = this.carousel.name;  
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
        const carousel = {
            id: this.carousel != null ? this.carousel.id : null,
            name: this._nameInputElement.value
        } as Carousel;
        
        this.dispatchEvent(new CarouselAdd(carousel));            
    }

    public onCreate() {        
        this.dispatchEvent(new CarouselEdit(new Carousel()));            
    }

    public onDelete() {        
        const carousel = {
            id: this.carousel != null ? this.carousel.id : null,
            name: this._nameInputElement.value
        } as Carousel;

        this.dispatchEvent(new CarouselDelete(carousel));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "carousel-id":
                this.carouselId = newValue;
                break;
            case "carousel":
                this.carousel = JSON.parse(newValue);
                if (this.parentNode) {
                    this.carouselId = this.carousel.id;
                    this._nameInputElement.value = this.carousel.name != undefined ? this.carousel.name : "";
                    this._titleElement.textContent = this.carouselId ? "Edit Carousel" : "Create Carousel";
                }
                break;
        }           
    }

    public carouselId: any;
    
	public carousel: Carousel;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".carousel-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".carousel-name") as HTMLInputElement;}
}

customElements.define(`ce-carousel-edit-embed`,CarouselEditEmbedComponent);
