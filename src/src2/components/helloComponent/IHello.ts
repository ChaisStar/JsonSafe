export interface IHello {
    username: string;

    password: string;

    enthusiasm: number;

    name: string;

    initialEnthusiasm: number;

    resultToken: string;

    resultUsername: string;

    jsons: string;

    increment(): void;

    decrement(): void;

    getExclamationMarks(): string;

    loginAsync(): Promise<void>;

    getJsonsAsync(): Promise<void>;
}
