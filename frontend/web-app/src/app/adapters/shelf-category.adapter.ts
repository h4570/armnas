import { Adapter } from './adapter';
import { ShelfCategory } from '../models/shelf-category.model';

export class ShelfCategoryAdapter extends Adapter<ShelfCategory> {

    adapt(raw: any): ShelfCategory {
        return raw;
    }

}
