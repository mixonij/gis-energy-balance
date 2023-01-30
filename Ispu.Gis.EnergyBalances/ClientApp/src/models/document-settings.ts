export class DocumentConfiguration implements IDocumentConfiguration {
    documentCheckPeriod!: number;
    documentLifetime!: number;

    constructor(data?: IDocumentConfiguration) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (this as any)[property] = (data  as any)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.documentCheckPeriod = _data["documentCheckPeriod"];
            this.documentLifetime = _data["documentLifetime"];
        }
    }

    static fromJS(data: any): DocumentConfiguration {
        data = typeof data === 'object' ? data : {};
        let result = new DocumentConfiguration();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["documentCheckPeriod"] = this.documentCheckPeriod;
        data["documentLifetime"] = this.documentLifetime;
        return data;
    }
}

export interface IDocumentConfiguration {
    documentCheckPeriod: number;
    documentLifetime: number;
}