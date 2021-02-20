export class Utility {

    public static objectsEqual(o1: any, o2: any): boolean {
        for (const p in o1) {
            if (o1.hasOwnProperty(p) && o1[p] !== o2[p]) {
                return false;
            }
        }
        for (const p in o2) {
            if (o2.hasOwnProperty(p) && o1[p] !== o2[p]) {
                return false;
            }
        }
        return true;
    }

    public static getRandomId(): number { return Math.floor(Math.random() * (99999999 - 1 + 1)) + 1; }

    public static getFixedFirstName(firstName: string): string {
        if (firstName) {
            firstName = firstName.trim().toLowerCase();
            return firstName.charAt(0).toUpperCase() + firstName.slice(1);
        }
        return '';
    }

    public static getFixedLastName(lastName: string): string {
        if (lastName) {
            lastName = lastName.trim().toLowerCase();
            if (lastName.includes('-')) {
                const elements = lastName.split('-');
                let result = '';
                elements.forEach(element => result += this.getCorrectedName(element));
                return result.slice(0, -1);
            } else { return lastName.charAt(0).toUpperCase() + lastName.slice(1).toLowerCase(); }
        }
        return '';
    }

    public static getFixedFullName(firstName: string, lastName: string): string {
        if (firstName && lastName) {
            firstName = firstName.trim().toLowerCase();
            firstName = firstName.charAt(0).toUpperCase() + firstName.slice(1);
            lastName = lastName.trim().toLowerCase();
            if (lastName.includes('-')) {
                const elements = lastName.split('-');
                let result = '';
                elements.forEach(element => result += this.getCorrectedName(element));
                lastName = result.slice(0, -1);
            } else {
                lastName = lastName.charAt(0).toUpperCase() + lastName.slice(1).toLowerCase();
            }
            return `${firstName} ${lastName}`;
        }
        return '';
    }

    private static getCorrectedName(element: string): string {
        const firstLetter = element.charAt(0).toUpperCase();
        const rest = element.slice(1);
        return `${firstLetter}+${rest}-`;
    }

}
