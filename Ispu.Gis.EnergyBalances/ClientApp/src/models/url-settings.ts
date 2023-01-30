export class ServerUrl implements IServerUrl {
    url?: string | undefined;
    order!: number;
    uri?: string | undefined;

    constructor(data?: IServerUrl) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (this as any)[property] = (data as any)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.url = _data["url"];
            this.order = _data["order"];
            this.uri = _data["uri"];
        }
    }

    static fromJS(data: any): ServerUrl {
        data = typeof data === 'object' ? data : {};
        let result = new ServerUrl();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["url"] = this.url;
        data["order"] = this.order;
        data["uri"] = this.uri;
        return data;
    }
}

export interface IServerUrl {
    url?: string | undefined;
    order: number;
    uri?: string | undefined;
}