export class SqlParameter {
    name: string;
    value: string;

    constructor(description: string) {
        this.name = description;
        this.value = '';
    }
}
