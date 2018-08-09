export interface IBrowserStorage {
  writeKeyValue(key: string, value: string): void;
  getValue(key: string): string;
  clear(): void;
}
