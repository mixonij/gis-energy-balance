export class CreateSolutionRequest implements ICreateSolutionRequest {
    name?: string | undefined;
    creatorLogin?: string | undefined;

    constructor(data?: ICreateSolutionRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (this as any)[property] = (data as any)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"];
            this.creatorLogin = _data["creatorLogin"];
        }
    }

    static fromJS(data: any): CreateSolutionRequest {
        data = typeof data === 'object' ? data : {};
        let result = new CreateSolutionRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name;
        data["creatorLogin"] = this.creatorLogin;
        return data;
    }
}

export interface ICreateSolutionRequest {
    name?: string | undefined;
    creatorLogin?: string | undefined;
}

export class BaseRenameRequest implements IBaseRenameRequest {
    solutionId?: string | undefined;
    name?: string | undefined;

    constructor(data?: IBaseRenameRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.solutionId = _data["solutionId"];
            this.name = _data["name"];
        }
    }

    static fromJS(data: any): BaseRenameRequest {
        data = typeof data === 'object' ? data : {};
        let result = new BaseRenameRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["solutionId"] = this.solutionId;
        data["name"] = this.name;
        return data;
    }
}

export interface IBaseRenameRequest {
    solutionId?: string | undefined;
    name?: string | undefined;
}


export class RenameSolutionOrBranchRequest extends BaseRenameRequest implements IRenameSolutionOrBranchRequest {

    constructor(data?: IRenameSolutionOrBranchRequest) {
        super(data);
    }

    override init(_data?: any) {
        super.init(_data);
    }

    static override fromJS(data: any): RenameSolutionOrBranchRequest {
        data = typeof data === 'object' ? data : {};
        let result = new RenameSolutionOrBranchRequest();
        result.init(data);
        return result;
    }

    override toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        super.toJSON(data);
        return data;
    }
}

export interface IRenameSolutionOrBranchRequest extends IBaseRenameRequest {
}

export class RenameSolutionGroupRequest extends BaseRenameRequest implements IRenameSolutionGroupRequest {
    override init(_data?: any) {
        super.init(_data);
    }

    static override fromJS(data: any): RenameSolutionGroupRequest {
        data = typeof data === 'object' ? data : {};
        let result = new RenameSolutionGroupRequest();
        result.init(data);
        return result;
    }

    override toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        super.toJSON(data);
        return data;
    }
}

export interface IRenameSolutionGroupRequest extends IBaseRenameRequest {
}