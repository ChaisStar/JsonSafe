export interface IHttpClient {
  getAsync<T>(url: string): Promise<T>;

  getAuthorizedAsync<T>(url: string, token: string): Promise<T>;

  postAsync<TRequest, TResponse>(
    url: string,
    request: TRequest,
  ): Promise<TResponse>;

  postAuthorizedAsync<TRequest, TResponse>(url: string, request: TRequest, token: string): Promise<TResponse>;

  deleteAuthorizedAsync(url: string, token: string): Promise<void>;
}
