export interface IJsonTextEditor {
  name: string;
  json: string;
  parsedJson: string;
  send(): void;
}
