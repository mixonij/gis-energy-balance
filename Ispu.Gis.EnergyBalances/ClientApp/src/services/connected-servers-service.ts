import {ConnectedServer, IConnectedServer} from "../models/connected-server";
import axios, {AxiosInstance, AxiosRequestConfig, AxiosResponse} from "axios";
import {isAxiosError, throwException} from "../helpers/common-helpers";

export interface IConnectedServersService {
    /**
     * Получение списка всех подключенных серверов
     * @return Список подключенных серверов
     */
    getAllServers(): Promise<IConnectedServer[] | null>;
    /**
     * Добавить подключенный сервер
     */
    addConnectedServer(url: string): Promise<void>;
    /**
     * Удаление подключенного сервера
     */
    removeConnectedServer(id: string): Promise<void>;
    /**
     * Обновление признака "Выполнять операции" для подключенного сервера
     * @param id Идентификатор сервера
     * @param executeOperations Значение признака "Выполнять операции"
     */
    updateConnectedServer(id: string, executeOperations: boolean): Promise<void>;
}

export class ConnectedServersService implements IConnectedServersService {
    private instance: AxiosInstance;

    constructor(baseUrl?: string, instance?: AxiosInstance) {
        this.instance = instance ? instance : axios.create();
    }

    /**
     * Получение списка всех подключенных серверов
     * @return Список подключенных серверов
     */
    getAllServers(): Promise<IConnectedServer[] | null> {
        let url_ = "api/connected-servers/get/all";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            method: "GET",
            url: url_,
            headers: {
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
            return this.processGetAllServers(_response);
        });
    }

    protected processGetAllServers(response: AxiosResponse): Promise<IConnectedServer[] | null> {
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
            let resultData200  = _responseText;
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(ConnectedServer.fromJS(item));
            }
            else {
                result200 = null as any;
            }
            return Promise.resolve<ConnectedServer[] | null>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<IConnectedServer[] | null>(null as any);
    }

    /**
     * Добавить подключенный сервер
     */
    addConnectedServer(url: string): Promise<void> {
        let url_ = "api/connected-servers/add?";
        if (url === undefined || url === null)
            throw new Error("The parameter 'url' must be defined and cannot be null.");
        else
            url_ += "url=" + encodeURIComponent("" + url) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            method: "POST",
            url: url_,
            headers: {
            }
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processAddConnectedServer(_response);
        });
    }

    protected processAddConnectedServer(response: AxiosResponse): Promise<void> {
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
            return Promise.resolve<void>(null as any);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<void>(null as any);
    }

    /**
     * Удаление подключенного сервера
     */
    removeConnectedServer(id: string): Promise<void> {
        let url_ = "api/connected-servers/remove?";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined and cannot be null.");
        else
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            method: "POST",
            url: url_,
            headers: {
            }
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processRemoveConnectedServer(_response);
        });
    }

    protected processRemoveConnectedServer(response: AxiosResponse): Promise<void> {
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
            return Promise.resolve<void>(null as any);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<void>(null as any);
    }

    /**
     * Обновление признака "Выполнять операции" для подключенного сервера
     * @param id Идентификатор сервера
     * @param executeOperations Значение признака "Выполнять операции"
     */
    updateConnectedServer(id: string, executeOperations: boolean): Promise<void> {
        let url_ = "api/connected-servers/update?";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined and cannot be null.");
        else
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        if (executeOperations === undefined || executeOperations === null)
            throw new Error("The parameter 'executeOperations' must be defined and cannot be null.");
        else
            url_ += "executeOperations=" + encodeURIComponent("" + executeOperations) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            method: "POST",
            url: url_,
            headers: {
            }
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processUpdateConnectedServer(_response);
        });
    }

    protected processUpdateConnectedServer(response: AxiosResponse): Promise<void> {
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
            return Promise.resolve<void>(null as any);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<void>(null as any);
    }
}