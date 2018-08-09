export interface ILogin {
    username: string;

    password: string;

    message: string;

    loginAsync(): Promise<void>;
}
