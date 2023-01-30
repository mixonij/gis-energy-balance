import {Project} from "./project";

export class Solution implements ISolution {
    id!: string;
    name!: string;
    creatorName?: string | undefined;
    dbName!: string;
    branchName!: string;
    solutionGroup?: string | undefined;
    isArchived!: boolean;
    projects!: Project[];

    constructor(data?: ISolution) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property))
                    (this as any)[property] = (data as any)[property];
            }
        }
        if (!data) {
            this.projects = [];
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.creatorName = _data["creatorName"];
            this.dbName = _data["dbName"];
            this.branchName = _data["branchName"];
            this.solutionGroup = this.getSolutionGroupName(_data["solutionGroup"]);
            this.isArchived = _data["isArchived"];
            if (Array.isArray(_data["projects"])) {
                this.projects = [] as any;
                for (let item of _data["projects"])
                    this.projects!.push(Project.fromJS(item));
            }
        }
    }

    static fromJS(data: any): Solution {
        data = typeof data === 'object' ? data : {};
        let result = new Solution();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["creatorName"] = this.creatorName;
        data["dbName"] = this.dbName;
        data["branchName"] = this.branchName;
        data["solutionGroup"] = this.solutionGroup;
        data["isArchived"] = this.isArchived;
        if (Array.isArray(this.projects)) {
            data["projects"] = [];
            for (let item of this.projects)
                data["projects"].push(item.toJSON());
        }
        return data;
    }

    private getSolutionGroupName(data: string | null): string {
        if (!data) {
            return "Без группы";
        }

        data = data.replace(/\\/g, " - ");
        return data.startsWith(" - ") ? data.slice(3) : data;
    }
}

export interface ISolution {
    id: string;
    name: string;
    creatorName?: string | undefined;
    dbName: string;
    branchName: string;
    solutionGroup?: string | undefined;
    isArchived: boolean;
    projects: Project[];
}