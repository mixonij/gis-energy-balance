export class Project implements IProject {
    id!: string;
    name!: string;
    dbName!: string;
    creatorName!: string;
    createDate!: string;
    order!: number;
    isExternal!: boolean;
    isDirectConnection!: boolean;
    allowDirectConnection!: boolean;
    isLocal!: boolean;
    commitHistory!: boolean;
    urls?: string | undefined;

    constructor(data?: IProject) {
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
            this.name = _data["name"];
            this.dbName = _data["dbName"];
            this.creatorName = _data["creatorName"];
            this.createDate = _data["createDate"];
            this.order = _data["order"];
            this.isExternal = _data["isExternal"];
            this.isDirectConnection = _data["isDirectConnection"];
            this.allowDirectConnection = _data["allowDirectConnection"];
            this.isLocal = _data["isLocal"];
            this.commitHistory = _data["commitHistory"];
            this.urls = _data["urls"];
        }
    }

    static fromJS(data: any): Project {
        data = typeof data === 'object' ? data : {};
        let result = new Project();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["dbName"] = this.dbName;
        data["creatorName"] = this.creatorName;
        data["createDate"] = this.createDate;
        data["order"] = this.order;
        data["isExternal"] = this.isExternal;
        data["isDirectConnection"] = this.isDirectConnection;
        data["allowDirectConnection"] = this.allowDirectConnection;
        data["isLocal"] = this.isLocal;
        data["commitHistory"] = this.commitHistory;
        data["urls"] = this.urls;
        return data;
    }
}

export interface IProject {
    id: string;
    name: string;
    dbName: string;
    creatorName: string;
    createDate: string;
    order: number;
    isExternal: boolean;
    isDirectConnection: boolean;
    allowDirectConnection: boolean;
    isLocal: boolean;
    commitHistory: boolean;
    urls?: string | undefined;
}