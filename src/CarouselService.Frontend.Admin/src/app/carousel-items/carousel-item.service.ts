import { fetch } from "../utilities";
import { CarouselItem } from "./carousel-item.model";
import { environment } from "../environment";

export class CarouselItemService {
    constructor(private _fetch = fetch) { }

    private static _instance: CarouselItemService;

    public static get Instance() {
        this._instance = this._instance || new CarouselItemService();
        return this._instance;
    }

    public get(): Promise<Array<CarouselItem>> {
        return this._fetch({ url: `${environment.baseUrl}api/carouselitem/get`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { carouselItems: Array<CarouselItem> }).carouselItems;
        });
    }

    public getById(id): Promise<CarouselItem> {
        return this._fetch({ url: `${environment.baseUrl}api/carouselitem/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { carouselItem: CarouselItem }).carouselItem;
        });
    }

    public add(carouselItem) {
        return this._fetch({ url: `${environment.baseUrl}api/carouselitem/add`, method: `POST`, data: { carouselItem }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `${environment.baseUrl}api/carouselitem/remove?id=${options.id}`, method: `DELETE`, authRequired: true  });
    }
    
}
