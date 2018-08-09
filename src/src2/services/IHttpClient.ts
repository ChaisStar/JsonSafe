export interface IHttpClient {
    getAsync<T>(URL: string): Promise<T>;

    getAuthorizedAsync<T>(url: string, token: string): Promise<T>;

    postAsync<TRequest, TResponse>(URL: string, Request: TRequest): Promise<TResponse>;
}
