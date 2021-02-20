// --- DATE

// Interface

interface Date {
    addDays: (days: number) => Date;
    addMinutes: (minutes: number) => Date;
    getPrettyString: () => string;
    equals: (date2: Date) => boolean;
    difference: (date2: Date, divider: number) => number;
}

// Implementations

Date.prototype.addDays = function (days: number): Date {
    const date = new Date(this);
    date.setDate(this.getDate() + days);
    return date;
};

Date.prototype.addMinutes = function (minutes: number): Date {
    const date = new Date(this);
    date.setMinutes(this.getMinutes() + minutes);
    return date;
};

Date.prototype.getPrettyString = function (): string {
    const prettyDay = ('0' + this.getDate()).slice(-2);
    const prettyMonth = ('0' + (this.getMonth() + 1)).slice(-2);
    const prettyHour = ('0' + this.getHours()).slice(-2);
    const prettyMinutes = ('0' + this.getMinutes()).slice(-2);
    return `${prettyDay}.${prettyMonth}.${this.getFullYear()} ${prettyHour}:${prettyMinutes}`;
};

Date.prototype.equals = function (date2: Date): boolean {
    return this.getFullYear() === date2.getFullYear() &&
        this.getMonth() === date2.getMonth() &&
        this.getDate() === date2.getDate();
};

Date.prototype.difference = function (date2: Date, divider: number = 1000 * 60 * 60 * 24): number {
    const utc1 = Date.UTC(this.getFullYear(), this.getMonth(), this.getDate(),
        this.getHours(), this.getMinutes(), this.getSeconds(), this.getMilliseconds());
    const utc2 = Date.UTC(date2.getFullYear(), date2.getMonth(), date2.getDate(),
        date2.getHours(), date2.getMinutes(), date2.getSeconds(), date2.getMilliseconds());
    return Math.floor((utc2 - utc1) / divider);
};


// --- String

// Interface

// eslint-disable-next-line id-blacklist
interface String {
    stringEquals: (string2: string) => boolean;
    countCharacter: (findChar: string) => number;
}

// Implementations

String.prototype.stringEquals = function (string2: string): boolean {
    return this && string2 ? string2.toLowerCase().trim() === this.toLowerCase().trim() : false;
};

String.prototype.countCharacter = function (findChar: string): number {
    let result = 0;
    for (const char of this) {
        if (char === findChar) {
            result += 1;
        }
    }
    return result;
};
