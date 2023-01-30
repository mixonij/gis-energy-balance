export class AuthenticationResponse implements IAuthenticationResponse {
    token!: string;
    isAdministrator!: boolean;

    constructor(data?: IAuthenticationResponse) {
        if (data) {
            for (const property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.token = _data["token"];
            this.isAdministrator = _data["isAdministrator"];
        }
    }

    static fromJS(data: any): AuthenticationResponse {
        data = typeof data === 'object' ? data : {};
        let result = new AuthenticationResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["token"] = this.token;
        data["isAdministrator"] = this.isAdministrator;
        return data;
    }
}

export interface IAuthenticationResponse {
    token: string;
    isAdministrator: boolean;
}

export class AuthenticateRequest implements IAuthenticateRequest {
    login?: string | undefined;
    passwordHash?: string | undefined;

    constructor(data?: IAuthenticateRequest) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.login = _data["login"];
            this.passwordHash = _data["passwordHash"];
        }
    }

    initFromValues(login: string, passwordHash: string) {
        this.login = login;
        this.passwordHash = passwordHash;
    }

    static fromJS(data: any): AuthenticateRequest {
        data = typeof data === 'object' ? data : {};
        let result = new AuthenticateRequest();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["login"] = this.login;
        data["passwordHash"] = this.passwordHash;
        return data;
    }
}

export interface IAuthenticateRequest {
    login?: string | undefined;
    passwordHash?: string | undefined;
}

export class User implements IUser {
    private _isAdministrator: boolean = false;
    private _login: string = "";
    private _passwordHash: string = "";
    private _token: string = "";

    get token(): string {
        return this._token;
    }

    set token(value: string) {
        this._token = value;
    }

    get login(): string {
        return this._login;
    }

    set login(value: string) {
        this._login = value;
    }

    get passwordHash(): string {
        return this._passwordHash;
    }

    set passwordHash(value: string) {
        this._passwordHash = value;
    }

    get isAdministrator(): boolean {
        return this._isAdministrator;
    }

    set isAdministrator(value: boolean) {
        this._isAdministrator = value;
    }

    isNormalUser(): boolean {
        return this._login !== "" && this._passwordHash !== "";
    }
}

export interface IUser {
    login: string,
    isAdministrator: boolean,
    token: string,
    passwordHash: string
    isNormalUser(): boolean
}
