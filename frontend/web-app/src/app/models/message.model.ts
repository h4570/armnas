export enum MessageType {
    information,
    warning,
    error
}

export interface Message {
    id: number;
    shortName: string;
    tooltip: string;
    type: MessageType;
    hasBeenRead: boolean;
    author: string;
}
