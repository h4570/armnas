import { Injectable } from '@angular/core';
import { ODataEntitySetService, ODataServiceFactory } from 'angular-odata';
import { AppHistory } from '../models/app-history.model';
import { Message } from '../models/message.model';
import { Partition } from '../models/partition.model';

@Injectable()
export class ODataService {

    constructor(
        private factory: ODataServiceFactory
    ) { }

    public get partitions(): ODataEntitySetService<Partition> {
        return this.factory.entitySet<Partition>('Partition');
    }

    public get messages(): ODataEntitySetService<Message> {
        return this.factory.entitySet<Message>('Message');
    }

    public get appHistory(): ODataEntitySetService<AppHistory> {
        return this.factory.entitySet<AppHistory>('AppHistory');
    }


}
