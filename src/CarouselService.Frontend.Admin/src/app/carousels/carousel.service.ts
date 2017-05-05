import { fetch } from "../utilities";
import { Carousel } from "./carousel.model";
import { environment } from "../environment";

export class CarouselService {
    constructor(private _fetch = fetch) { }

    private static _instance: CarouselService;

    public static get Instance() {
        this._instance = this._instance || new CarouselService();
        return this._instance;
    }

    public get(): Promise<Array<Carousel>> {
        return this._fetch({ url: `${environment.baseUrl}api/carousel/get`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { carousels: Array<Carousel> }).carousels;
        });
    }

    public getById(id): Promise<Carousel> {
        return this._fetch({ url: `${environment.baseUrl}api/carousel/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { carousel: Carousel }).carousel;
        });
    }

    public add(carousel) {
        return this._fetch({ url: `${environment.baseUrl}api/carousel/add`, method: `POST`, data: { carousel }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `${environment.baseUrl}api/carousel/remove?id=${options.id}`, method: `DELETE`, authRequired: true  });
    }
    
}
