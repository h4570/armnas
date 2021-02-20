import { Adapter } from './adapter';
import { Shelf } from '../models/shelf.model';
import { ShelfCategory } from '../models/shelf-category.model';

export class ShelfAdapter extends Adapter<Shelf> {

    private readonly categoryAdapter: Adapter<ShelfCategory>;

    constructor(categoryAdapter: Adapter<ShelfCategory>) {
        super();
        this.categoryAdapter = categoryAdapter;
    }

    adapt(raw: any): Shelf {
        if (raw.categories)
            raw.categories = this.categoryAdapter.adaptMany(raw.categories);
        return raw;
    }

}
