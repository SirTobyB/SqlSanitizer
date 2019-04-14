export class SqlParameter {
    description: string;
    value: string;

    constructor(description: string) {
        this.description = description;
        this.value = '';
    }
}
