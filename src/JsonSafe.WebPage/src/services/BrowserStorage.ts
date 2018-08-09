import { IBrowserStorage } from "./IBrowserStorage";

export class BrowserStorage implements IBrowserStorage {
  private readonly storage = window.localStorage;

  public clear(): void {
    this.storage.clear();
  }

  public writeKeyValue(key: string, value: string): void {
    console.log(`Write ${key} ${value}`);
    this.storage.setItem(key, value);
  }

  public getValue(key: string): string {
    console.log(`Get ${key}`);
    const token = this.storage.getItem(key);
    if (!token) {
      throw Error("Token not found");
    }
    return token;
  }
}
