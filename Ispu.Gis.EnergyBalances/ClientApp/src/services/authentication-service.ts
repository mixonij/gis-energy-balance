import axios, {AxiosInstance, AxiosRequestConfig, AxiosResponse} from "axios";
import {isAxiosError, throwException} from "../helpers/common-helpers";
import {AuthenticateRequest, AuthenticationResponse, User} from "../models/authentication";
import {LocalKey, LocalStorage} from "ts-localstorage";

export class UserException extends Error {
    response: string;

    constructor(response: string) {
        super();
        this.response = response;
    }
}

export interface IAuthenticationService {
    /**
     * Аутентификация пользователя
     * @param request (optional) Параметры аутентификации
     */
    authenticateUser(request: AuthenticateRequest | undefined): Promise<AuthenticationResponse | null>;

    /**
     * Выход из профиля
     */
    signout(): void;

    /**
     * Проверка или обновление токена
     */
    checkOrUpdateToken(): Promise<void>

    /**
     * Пользователь
     */
    user: User | undefined;
}

const userEmailKey = new LocalKey("userEmail", "");
const userPasswordHashKey = new LocalKey("userPasswordHash", "");

export class AuthenticationService implements IAuthenticationService {
    
    private instance: AxiosInstance;

    constructor(baseUrl?: string, instance?: AxiosInstance) {
        this.instance = instance ? instance : axios.create();
    }

    private _user: User | undefined = undefined;
    get user(): User | undefined {
        return this._user;
    }

    /**
     * Аутентификация пользователя
     * @param request (optional) Параметры аутентификации
     */
    public authenticateUser(request: AuthenticateRequest | undefined): Promise<AuthenticationResponse | null> {
        let url_ = "api/authentication/authenticate";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(request);

        let options_: AxiosRequestConfig = {
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
            }
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processAuthenticateUser(_response, request?.login!, request?.passwordHash!);
        });
    }

    private processAuthenticateUser(response: AxiosResponse, login: string, passwordHash: string): Promise<AuthenticationResponse | null> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any;
            let resultData200 = _responseText;
            result200 = resultData200 ? AuthenticationResponse.fromJS(resultData200) : null as any;
            this._user = new User();
            this._user.login = login;
            this._user.isAdministrator = result200.isAdministrator;
            this._user.token = result200.token;
            this._user.passwordHash = passwordHash;

            LocalStorage.setItem(userEmailKey, login);
            LocalStorage.setItem(userPasswordHashKey, passwordHash);

            return Promise.resolve<AuthenticationResponse | null>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<AuthenticationResponse | null>(null as any);
    }

    /**
     * Выход из профиля
     */
    public signout(): void {
        this._user = undefined;

        LocalStorage.setItem(userEmailKey, "");
        LocalStorage.setItem(userPasswordHashKey, "");
    }

    /**
     * Проверка или обновелние токена
     */
    public async checkOrUpdateToken(): Promise<void> {
        if (!this._user) {
            throw new UserException("Пользователь не задан");
        }

        await this.checkToken().catch(async() => await this.generateToken());
    }

    /**
     * Генерация токена пользователя
     */
    private generateToken(): Promise<string | null> {
        let url_ = "api/authentication/token/generate";
        url_ = url_.replace(/[?&]$/, "");

        const payload = new AuthenticateRequest();
        payload.initFromValues(this._user?.login!, this._user?.passwordHash!);

        const content_ = JSON.stringify(payload);

        let options_: AxiosRequestConfig = {
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processGenerateToken(_response);
        });
    }

    private processGenerateToken(response: AxiosResponse): Promise<string | null> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any;
            let resultData200 = _responseText;
            result200 = resultData200 !== undefined ? resultData200 : null as any;
            if (this._user) {
                this._user.token = result200;
            }
            return Promise.resolve<string | null>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<string | null>(null as any);
    }

    /**
     * Проверка токена пользователя
     */
    private checkToken(): Promise<boolean> {
        let url_ = "api/authentication/token/check";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json",
                "Authorization": `Bearer ${this._user?.token}`
            }
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processCheckToken(_response);
        });
    }

    private processCheckToken(response: AxiosResponse): Promise<boolean> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (let k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any;
            let resultData200 = _responseText;
            result200 = resultData200 !== undefined ? resultData200 : null as any;

            return Promise.resolve<boolean>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<boolean>(null as any);
    }
}