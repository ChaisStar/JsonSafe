import { GetJsonResponseDto } from "../models/dtos/jsonDtos/GetJsonResponseDto";

export interface IJsonApiService {
    getJsonsAsync(token: string): Promise<GetJsonResponseDto[]>;

    createJson(token: string, request: any): Promise<void>;
}
