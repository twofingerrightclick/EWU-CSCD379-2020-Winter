﻿/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.2.3.0 (NJsonSchema v10.1.5.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';

export class AuthorClient {
    private instance: AxiosInstance;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, instance?: AxiosInstance) {
        this.instance = instance ? instance : axios.create();
        this.baseUrl = baseUrl ? baseUrl : "https://localhost:44317";
    }

    getAll(): Promise<Author[]> {
        let url_ = this.baseUrl + "/api/Author";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processGetAll(_response);
        });
    }

    protected processGetAll(response: AxiosResponse): Promise<Author[]> {
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
            let result200: any = null;
            let resultData200 = _responseText;
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(Author.fromJS(item));
            }
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<Author[]>(<any>null);
    }

    post(entity: AuthorInput): Promise<Author> {
        let url_ = this.baseUrl + "/api/Author";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(entity);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processPost(_response);
        });
    }

    protected processPost(response: AxiosResponse): Promise<Author> {
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
            let result200: any = null;
            let resultData200 = _responseText;
            result200 = Author.fromJS(resultData200);
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<Author>(<any>null);
    }

    get(id: number): Promise<Author> {
        let url_ = this.baseUrl + "/api/Author/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processGet(_response);
        });
    }

    protected processGet(response: AxiosResponse): Promise<Author> {
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
            let result200: any = null;
            let resultData200 = _responseText;
            result200 = Author.fromJS(resultData200);
            return result200;
        } else if (status === 404) {
            const _responseText = response.data;
            let result404: any = null;
            let resultData404 = _responseText;
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
        } else {
            const _responseText = response.data;
            let resultdefault: any = null;
            let resultDatadefault = _responseText;
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
        }
    }

    put(id: number, value: AuthorInput): Promise<Author> {
        let url_ = this.baseUrl + "/api/Author/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(value);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "PUT",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processPut(_response);
        });
    }

    protected processPut(response: AxiosResponse): Promise<Author> {
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
            let result200: any = null;
            let resultData200 = _responseText;
            result200 = Author.fromJS(resultData200);
            return result200;
        } else if (status === 404) {
            const _responseText = response.data;
            let result404: any = null;
            let resultData404 = _responseText;
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
        } else {
            const _responseText = response.data;
            let resultdefault: any = null;
            let resultDatadefault = _responseText;
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
        }
    }

    delete(id: number): Promise<void> {
        let url_ = this.baseUrl + "/api/Author/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "DELETE",
            url: url_,
            headers: {
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processDelete(_response);
        });
    }

    protected processDelete(response: AxiosResponse): Promise<void> {
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
            return Promise.resolve<void>(<any>null);
        } else if (status === 404) {
            const _responseText = response.data;
            let result404: any = null;
            let resultData404 = _responseText;
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
        } else {
            const _responseText = response.data;
            let resultdefault: any = null;
            let resultDatadefault = _responseText;
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
        }
    }
}

export class PostClient {
    private instance: AxiosInstance;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, instance?: AxiosInstance) {
        this.instance = instance ? instance : axios.create();
        this.baseUrl = baseUrl ? baseUrl : "https://localhost:44317";
    }

    getAll(): Promise<Post[]> {
        let url_ = this.baseUrl + "/api/Post";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processGetAll(_response);
        });
    }

    protected processGetAll(response: AxiosResponse): Promise<Post[]> {
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
            let result200: any = null;
            let resultData200 = _responseText;
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(Post.fromJS(item));
            }
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<Post[]>(<any>null);
    }

    post(entity: PostInput): Promise<Post> {
        let url_ = this.baseUrl + "/api/Post";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(entity);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processPost(_response);
        });
    }

    protected processPost(response: AxiosResponse): Promise<Post> {
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
            let result200: any = null;
            let resultData200 = _responseText;
            result200 = Post.fromJS(resultData200);
            return result200;
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<Post>(<any>null);
    }

    get(id: number): Promise<Post> {
        let url_ = this.baseUrl + "/api/Post/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processGet(_response);
        });
    }

    protected processGet(response: AxiosResponse): Promise<Post> {
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
            let result200: any = null;
            let resultData200 = _responseText;
            result200 = Post.fromJS(resultData200);
            return result200;
        } else if (status === 404) {
            const _responseText = response.data;
            let result404: any = null;
            let resultData404 = _responseText;
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
        } else {
            const _responseText = response.data;
            let resultdefault: any = null;
            let resultDatadefault = _responseText;
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
        }
    }

    put(id: number, value: PostInput): Promise<Post> {
        let url_ = this.baseUrl + "/api/Post/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(value);

        let options_ = <AxiosRequestConfig>{
            data: content_,
            method: "PUT",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processPut(_response);
        });
    }

    protected processPut(response: AxiosResponse): Promise<Post> {
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
            let result200: any = null;
            let resultData200 = _responseText;
            result200 = Post.fromJS(resultData200);
            return result200;
        } else if (status === 404) {
            const _responseText = response.data;
            let result404: any = null;
            let resultData404 = _responseText;
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
        } else {
            const _responseText = response.data;
            let resultdefault: any = null;
            let resultDatadefault = _responseText;
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
        }
    }

    delete(id: number): Promise<void> {
        let url_ = this.baseUrl + "/api/Post/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <AxiosRequestConfig>{
            method: "DELETE",
            url: url_,
            headers: {
            }
        };

        return this.instance.request(options_).then((_response: AxiosResponse) => {
            return this.processDelete(_response);
        });
    }

    protected processDelete(response: AxiosResponse): Promise<void> {
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
            return Promise.resolve<void>(<any>null);
        } else if (status === 404) {
            const _responseText = response.data;
            let result404: any = null;
            let resultData404 = _responseText;
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
        } else {
            const _responseText = response.data;
            let resultdefault: any = null;
            let resultDatadefault = _responseText;
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
        }
    }
}

export class AuthorInput implements IAuthorInput {
    firstName!: string;
    lastName!: string;
    email!: string;

    constructor(data?: IAuthorInput) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.firstName = _data["firstName"];
            this.lastName = _data["lastName"];
            this.email = _data["email"];
        }
    }

    static fromJS(data: any): AuthorInput {
        data = typeof data === 'object' ? data : {};
        let result = new AuthorInput();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["firstName"] = this.firstName;
        data["lastName"] = this.lastName;
        data["email"] = this.email;
        return data;
    }
}

export interface IAuthorInput {
    firstName: string;
    lastName: string;
    email: string;
}

export class Author extends AuthorInput implements IAuthor {
    id!: number;

    constructor(data?: IAuthor) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.id = _data["id"];
        }
    }

    static fromJS(data: any): Author {
        data = typeof data === 'object' ? data : {};
        let result = new Author();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        super.toJSON(data);
        return data;
    }
}

export interface IAuthor extends IAuthorInput {
    id: number;
}

export class ProblemDetails implements IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;
    extensions?: { [key: string]: any; } | undefined;

    constructor(data?: IProblemDetails) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.type = _data["type"];
            this.title = _data["title"];
            this.status = _data["status"];
            this.detail = _data["detail"];
            this.instance = _data["instance"];
            if (_data["extensions"]) {
                this.extensions = {} as any;
                for (let key in _data["extensions"]) {
                    if (_data["extensions"].hasOwnProperty(key))
                        this.extensions![key] = _data["extensions"][key];
                }
            }
        }
    }

    static fromJS(data: any): ProblemDetails {
        data = typeof data === 'object' ? data : {};
        let result = new ProblemDetails();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["type"] = this.type;
        data["title"] = this.title;
        data["status"] = this.status;
        data["detail"] = this.detail;
        data["instance"] = this.instance;
        if (this.extensions) {
            data["extensions"] = {};
            for (let key in this.extensions) {
                if (this.extensions.hasOwnProperty(key))
                    data["extensions"][key] = this.extensions[key];
            }
        }
        return data;
    }
}

export interface IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;
    extensions?: { [key: string]: any; } | undefined;
}

export class PostInput implements IPostInput {
    title!: string;
    content?: string | undefined;
    slug?: string | undefined;
    isPublished?: boolean | undefined;
    tagIds?: number[] | undefined;
    postedOn!: Date;
    authorId!: number;

    constructor(data?: IPostInput) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.title = _data["title"];
            this.content = _data["content"];
            this.slug = _data["slug"];
            this.isPublished = _data["isPublished"];
            if (Array.isArray(_data["tagIds"])) {
                this.tagIds = [] as any;
                for (let item of _data["tagIds"])
                    this.tagIds!.push(item);
            }
            this.postedOn = _data["postedOn"] ? new Date(_data["postedOn"].toString()) : <any>undefined;
            this.authorId = _data["authorId"];
        }
    }

    static fromJS(data: any): PostInput {
        data = typeof data === 'object' ? data : {};
        let result = new PostInput();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["title"] = this.title;
        data["content"] = this.content;
        data["slug"] = this.slug;
        data["isPublished"] = this.isPublished;
        if (Array.isArray(this.tagIds)) {
            data["tagIds"] = [];
            for (let item of this.tagIds)
                data["tagIds"].push(item);
        }
        data["postedOn"] = this.postedOn ? this.postedOn.toISOString() : <any>undefined;
        data["authorId"] = this.authorId;
        return data;
    }
}

export interface IPostInput {
    title: string;
    content?: string | undefined;
    slug?: string | undefined;
    isPublished?: boolean | undefined;
    tagIds?: number[] | undefined;
    postedOn: Date;
    authorId: number;
}

export class Post extends PostInput implements IPost {
    id!: number;
    author!: Author;

    constructor(data?: IPost) {
        super(data);
        if (!data) {
            this.author = new Author();
        }
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.id = _data["id"];
            this.author = _data["author"] ? Author.fromJS(_data["author"]) : new Author();
        }
    }

    static fromJS(data: any): Post {
        data = typeof data === 'object' ? data : {};
        let result = new Post();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["author"] = this.author ? this.author.toJSON() : <any>undefined;
        super.toJSON(data);
        return data;
    }
}

export interface IPost extends IPostInput {
    id: number;
    author: Author;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}