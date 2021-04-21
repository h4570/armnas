export interface Message {
    id: number;
    shortName: string;
    tooltip: string;
    type: 'Information' | 'Warning' | 'Error';
    hasBeenRead: boolean;
    author: string;
    date: Date;
}
