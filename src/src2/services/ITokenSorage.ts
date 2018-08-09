export interface ITokenStorage {
    addToken(key: string, token: string): void;
    getToken(key: string): string;
}
