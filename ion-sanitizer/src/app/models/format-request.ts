import {SqlParameter} from './sql-parameter';

export interface FormatRequest {
    charsToRemove: string[];
    reindent: boolean;
    indentWidth: number;
    identifierCase: string;
    keywordCase: string;
    stripComments: boolean;
    sqlQuery: string;
    parameter: SqlParameter[];
}
