export class ConnectedServer implements IConnectedServer {
    id!: string;
    urls!: string;
    order!: number;
    executeOperations!: boolean;

    constructor(data?: IConnectedServer) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property))
                    (this as any)[property] = (data as any)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.urls = _data["urls"];
            this.order = _data["order"];
            this.executeOperations = _data["executeOperations"];
        }
    }

    static fromJS(data: any): ConnectedServer {
        data = typeof data === 'object' ? data : {};
        let result = new ConnectedServer();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["urls"] = this.urls;
        data["order"] = this.order;
        data["executeOperations"] = this.executeOperations;
        return data;
    }
}

export interface IConnectedServer {
    id: string;
    urls: string;
    order: number;
    executeOperations: boolean;
}