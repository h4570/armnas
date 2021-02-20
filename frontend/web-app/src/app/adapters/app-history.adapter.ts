import { Adapter } from './adapter';
import { AppHistory } from '../models/app-history.model';

export class AppHistoryAdapter extends Adapter<AppHistory> {

    adapt(raw: any): AppHistory {
        raw.dateTime = new Date(raw.dateTime);
        return raw;
    }

}
