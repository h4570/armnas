import { AppHistoryType } from './app-history-type.model';
import { AppTable } from './app-table.enum';

export interface AppHistory {
    id: number;
    tableId: AppTable;
    elementId: number;
    type: AppHistoryType;
    dateTime: Date;
    description: string;
}
