import {DbServerConfiguration, IDbServerConfiguration} from "../models/database-settings";
import {isAxiosError, throwException} from "../helpers/common-helpers";
import axios, {AxiosInstance, AxiosRequestConfig, AxiosResponse} from "axios";
import {DatabaseSettingsEnum} from "../enums/database-settings-enum";
import {DocumentConfiguration, IDocumentConfiguration} from "../models/document-settings";
import {DocumentsSettingsEnum} from "../enums/documents-settings-enum";

export interface ISettingsService {
    getDbServerConfiguration(): Promise<IDbServerConfiguration | null>;

    changeDbServerConfiguration(prevState: IDbServerConfiguration | null, setting: DatabaseSettingsEnum, value: string | number): IDbServerConfiguration | null;

    saveDbServerConfiguration(configuration: IDbServerConfiguration | null): Promise<void>;

    getDocumentsConfiguration(): Promise<IDocumentConfiguration | null>;

    saveDocumentsConfiguration(configuration: IDocumentConfiguration | null): Promise<boolean>;

    changeDocumentsConfiguration(prevState: IDocumentConfiguration | null, setting: DocumentsSettingsEnum, value: number): IDocumentConfiguration | null;

}

export class SettingsService implements ISettingsService {
    private instance: AxiosInstance;

    constructor(instance?: AxiosInstance) {
        this.instance = instance ? instance : axios.create();
    }

    changeDocumentsConfiguration(prevState: IDocumentConfiguration | null, setting: DocumentsSettingsEnum, value: number): IDocumentConfiguration | null {
        if (prevState === null) {
            return null;
        }

        let config = {...prevState} as IDocumentConfiguration;
        switch (setting) {
            case DocumentsSettingsEnum.CheckPeriod: {
                config.documentCheckPeriod = value;
                break;
            }
            case DocumentsSettingsEnum.Lifetime: {
                config.documentLifetime = value;
                break;
            }
        }

        return config;
    }

    changeDbServerConfiguration(prevState: IDbServerConfiguration | null, setting: DatabaseSettingsEnum, value: string | number): IDbServerConfiguration | null {
        if (prevState === null) {
            return null;
        }

        let config = {...prevState} as IDbServerConfiguration;
        switch (setting) {
            case DatabaseSettingsEnum.Host: {
                config.host = value as string;
                break;
            }
            case DatabaseSettingsEnum.Port: {
                config.port = value as number;
                break;
            }
            case DatabaseSettingsEnum.User: {
                config.user = value as string;
                break;
            }
            case DatabaseSettingsEnum.Password: {
                config.password = value as string;
                break;
            }
        }

        return config;
    }

    getDbServerConfiguration(): Promise<IDbServerConfiguration | null> {
        let url_ = "api/settings/get/configuration/database";
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
            return this.processGetDbServerConfiguration(_response);
        });
    }

    protected processGetDbServerConfiguration(response: AxiosResponse): Promise<IDbServerConfiguration | null> {
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
            result200 = resultData200 ? DbServerConfiguration.fromJS(resultData200) : null as any;
            return Promise.resolve<IDbServerConfiguration | null>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<IDbServerConfiguration | null>(null as any);
    }

    saveDbServerConfiguration(configuration: IDbServerConfiguration | null): Promise<void> {
        let url_ = "api/settings/save/configuration/database";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(configuration);

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
            return this.processSaveDbServerConfiguration(_response);
        });
    }

    protected processSaveDbServerConfiguration(response: AxiosResponse): Promise<void> {
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

    getDocumentsConfiguration(): Promise<IDocumentConfiguration | null> {
        let url_ = "api/settings/get/configuration/documents";
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
            return this.processGetDocumentsConfiguration(_response);
        });
    }

    protected processGetDocumentsConfiguration(response: AxiosResponse): Promise<IDocumentConfiguration | null> {
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
            result200 = resultData200 ? DocumentConfiguration.fromJS(resultData200) : null as any;
            return Promise.resolve<IDocumentConfiguration | null>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<IDocumentConfiguration | null>(null as any);
    }

    saveDocumentsConfiguration(configuration: IDocumentConfiguration | null): Promise<boolean> {
        let url_ = "api/settings/save/configuration/documents";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(configuration);

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
            return this.processSaveDocumentsConfiguration(_response);
        });
    }

    protected processSaveDocumentsConfiguration(response: AxiosResponse): Promise<boolean> {
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
            result200 = resultData200 !== undefined ? resultData200 : null as any;

            return Promise.resolve<boolean>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<boolean>(null as any);
    }
}