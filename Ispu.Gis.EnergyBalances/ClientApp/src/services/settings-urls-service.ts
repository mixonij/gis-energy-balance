import {IServerUrl, ServerUrl} from "../models/url-settings";
import axios, {AxiosInstance, AxiosRequestConfig, AxiosResponse} from "axios";
import {isAxiosError, throwException} from "../helpers/common-helpers";

export interface IUrlsService {
    /**
     * Получение списка всех адресов сервера
     * @return Упорядоченный список адресов сервера
     */
    getAllUrls(): Promise<IServerUrl[] | null>;
    /**
     * Добавление адреса сервера
     * @param url Строка адреса сервера
     */
    addServerUrl(url: string): Promise<void>;
    /**
     * Удаление адреса сервера
     * @param url Строка адреса сервера
     */
    removeServerUrl(url: string): Promise<void>;
    /**
     * Обновление списка адресов
     * @param urls (optional) Упорядоченный список адресов
     */
    updateServerUrls(urls: IServerUrl[] | null): Promise<void>;
}

export class UrlsService implements IUrlsService {
    private instance: AxiosInstance;

    constructor(instance?: AxiosInstance) {
        this.instance = instance ? instance : axios.create();
    }

    /**
     * Получение списка всех адресов сервера
     * @return Упорядоченный список адресов сервера
     */
    getAllUrls(): Promise<IServerUrl[] | null> {
        let url_ = "api/urls/get/all";
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
            return this.processGetAllUrls(_response);
        });
    }

    protected processGetAllUrls(response: AxiosResponse): Promise<IServerUrl[] | null> {
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
                    result200!.push(ServerUrl.fromJS(item));
            }
            else {
                result200 = null as any;
            }
            return Promise.resolve<ServerUrl[] | null>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<IServerUrl[] | null>(null as any);
    }

    /**
     * Добавление адреса сервера
     * @param url Строка адреса сервера
     */
    addServerUrl(url: string): Promise<void> {
        let url_ = "api/urls/add?";
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
            return this.processAddServerUrl(_response);
        });
    }

    protected processAddServerUrl(response: AxiosResponse): Promise<void> {
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
     * Удаление адреса сервера
     * @param url Строка адреса сервера
     */
    removeServerUrl(url: string): Promise<void> {
        let url_ = "api/urls/remove?";
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
            return this.processRemoveServerUrl(_response);
        });
    }

    protected processRemoveServerUrl(response: AxiosResponse): Promise<void> {
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
     * Обновление списка адресов
     * @param urls (optional) Упорядоченный список адресов
     */
    updateServerUrls(urls: ServerUrl[] | null): Promise<void> {
        let url_ = "api/urls/update";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(urls);

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
            return this.processUpdateServerUrls(_response);
        });
    }

    protected processUpdateServerUrls(response: AxiosResponse): Promise<void> {
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