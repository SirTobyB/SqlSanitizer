export class SqlParameter {
    name: string;
    value: string;

    constructor(description: string, value: string = '') {
        this.name = description;
        this.value = value;
    }
}
