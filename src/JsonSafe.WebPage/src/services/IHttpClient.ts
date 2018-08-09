export interface IHttpClient {
  getAsync<T>(URL: string): Promise<T>;

  getAuthorizedAsync<T>(url: string, token: string): Promise<T>;

  postAsync<TRequest, TResponse>(
    URL: string,
    request: TRequest,
  ): Promise<TResponse>;

  postAuthorizedAsync<TRequest, TResponse>(url: string, request: TRequest, token: string): Promise<TResponse>;
}
