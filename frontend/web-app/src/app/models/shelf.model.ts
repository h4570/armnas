import { ShelfCategory } from './shelf-category.model';

export interface Shelf {
    id: number;
    barcode: string;
    categories: ShelfCategory[];
}
