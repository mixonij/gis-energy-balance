export class DbServerConfiguration implements IDbServerConfiguration {
    host!: string;
    port!: number;
    user!: string;
    password!: string;

    constructor(data?: IDbServerConfiguration) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (this as any)[property] = (data as any)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.host = _data["host"];
            this.port = _data["port"];
            this.user = _data["user"];
            this.password = _data["password"];
        }
    }

    static fromJS(data: any): IDbServerConfiguration {
        data = typeof data === 'object' ? data : {};
        let result = new DbServerConfiguration();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["host"] = this.host;
        data["port"] = this.port;
        data["user"] = this.user;
        data["password"] = this.password;
        return data;
    }
}

export interface IDbServerConfiguration {
    host: string;
    port: number;
    user: string;
    password: string;
}