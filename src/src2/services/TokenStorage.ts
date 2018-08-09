import { injectable } from "inversify";

import { ITokenStorage } from "./ITokenSorage";

@injectable()
export class TokenStorage implements ITokenStorage {
    private readonly storage = window.localStorage;
    public addToken(key: string, token: string): void {
        this.storage.setItem(key, token);
    }
    public getToken(key: string): string {
        const token = this.storage.getItem(key);
        if (!token) {
            throw Error("Token not found");
        }
        return token;
    }
}
