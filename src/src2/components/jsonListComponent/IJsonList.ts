export interface IJsonList {
    jsons: string[];

    selectedJson: string;

    message: string;

    getJsonsAsync(): Promise<void>;
}
