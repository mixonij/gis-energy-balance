import type {AxiosInstance, AxiosRequestConfig, AxiosResponse} from 'axios';
import {ISolution, Solution} from "../models/solution";
import {isAxiosError, throwException} from "../helpers/common-helpers";
import axios from "axios";
import {IAuthenticationService} from "./authentication-service";

export interface ISolutionService {
    /**
     * Получения списка всех решений
     * @return Список решений
     */
    getAllSolutions(): Promise<ISolution[] | null>;

    /**
     * Получение решения по идентификатору
     * @param id Идентификатор решения
     * @return Решение с указанным идентификатором
     */
    getSolutionById(id: string): Promise<Solution | null>;
}

export class SolutionService implements ISolutionService {
    private instance: AxiosInstance;
    private authService: IAuthenticationService;

    constructor(authService: IAuthenticationService, instance?: AxiosInstance) {
        this.authService = authService;
        this.instance = instance ? instance : axios.create();
    }

    /**
     * Получения списка всех решений
     * @return Список решений
     */
    async getAllSolutions(): Promise<ISolution[] | null> {
        let url_ = "api/solution/get/all";
        url_ = url_.replace(/[?&]$/, "");
        
        await this.authService.checkOrUpdateToken();

        let options_: AxiosRequestConfig = {
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json",
                "Authorization": `Bearer ${this.authService.user?.token}`
            }
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processGetAllSolutions(_response);
        });
    }

    protected processGetAllSolutions(response: AxiosResponse): Promise<Solution[] | null> {
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
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(Solution.fromJS(item));
            } else {
                result200 = null as any;
            }
            return Promise.resolve<Solution[] | null>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<Solution[] | null>(null as any);
    }

    /**
     * Получение решения по идентификатору
     * @param id Идентификатор решения
     * @return Решение с указанным идентификатором
     */
    getSolutionById(id: string,): Promise<Solution | null> {
        let url_ = "api/solution/get/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
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
            return this.processGetSolutionById(_response);
        });
    }

    protected processGetSolutionById(response: AxiosResponse): Promise<Solution | null> {
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
            result200 = resultData200 ? Solution.fromJS(resultData200) : null as any;
            return Promise.resolve<Solution | null>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<Solution | null>(null as any);
    }
}