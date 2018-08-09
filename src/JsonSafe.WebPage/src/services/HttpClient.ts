import { IHttpClient } from "./IHttpClient";

export class HttpClient implements IHttpClient {
  public postAuthorizedAsync<TRequest, TResponse>(
    url: string,
    request: TRequest,
    token: string,
  ): Promise<TResponse> {
    return new Promise<TResponse>((resolve, reject) => {
      const xhttp = new XMLHttpRequest();
      xhttp.onload = () => {
        if (xhttp.status === 200) {
          resolve(JSON.parse(xhttp.response) as TResponse);
        } else {
          console.log(`Request failed.  Returned status of ${xhttp.status}`);
          console.log(xhttp.response);
          reject(new Error(`XMLHttpRequest Error: ${xhttp.statusText}`));
        }
      };
      xhttp.open("POST", `${url}`, true);
      xhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
      xhttp.setRequestHeader("Authorization", `Bearer ${token}`);
      xhttp.send(JSON.stringify(request));
    });
  }
  public getAsync<T>(url: string): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      const xhttp = new XMLHttpRequest();
      xhttp.onload = () => {
        if (xhttp.status === 200) {
          resolve(JSON.parse(xhttp.response) as T);
        } else {
          console.log(`Request failed.  Returned status of ${xhttp.status}`);
          console.log(xhttp.response);
          reject(new Error(`XMLHttpRequest Error: ${xhttp.statusText}`));
        }
      };
      xhttp.open("GET", `${url}`);
      xhttp.send();
    });
  }

  public getAuthorizedAsync<T>(url: string, token: string): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      const xhttp = new XMLHttpRequest();
      xhttp.onload = () => {
        if (xhttp.status === 200) {
          resolve(JSON.parse(xhttp.response) as T);
        } else {
          console.log(`Request failed.  Returned status of ${xhttp.status}`);
          console.log(xhttp.response);
          reject(new Error(`XMLHttpRequest Error: ${xhttp.statusText}`));
        }
      };
      xhttp.open("GET", `${url}`);
      xhttp.setRequestHeader("Authorization", `Bearer ${token}`);
      xhttp.send();
    });
  }

  public postAsync<TRequest, TResponse>(
    url: string,
    request: TRequest,
  ): Promise<TResponse> {
    return new Promise<TResponse>((resolve, reject) => {
      const xhttp = new XMLHttpRequest();
      xhttp.onload = () => {
        if (xhttp.status === 200) {
          resolve(JSON.parse(xhttp.response) as TResponse);
        } else {
          console.log(`Request failed.  Returned status of ${xhttp.status}`);
          console.log(xhttp.response);
          reject(new Error(`XMLHttpRequest Error: ${xhttp.statusText}`));
        }
      };
      xhttp.open("POST", `${url}`, true);
      xhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
      xhttp.send(JSON.stringify(request));
    });
  }
}
