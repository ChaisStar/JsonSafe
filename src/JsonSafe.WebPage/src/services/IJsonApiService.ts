import { CreateJsonRequestDto } from "../models/dtos/jsonDtos/CreateJsonRequestDto";
import { GetJsonResponseDto } from "../models/dtos/jsonDtos/GetJsonResponseDto";

export interface IJsonApiService {
    getJsonsAsync(token: string): Promise<GetJsonResponseDto[]>;

    createJson(token: string, request: CreateJsonRequestDto): Promise<void>;

    deleteJson(token: string, jsonId: string): Promise<void>;
}
