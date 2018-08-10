import { CreateJsonRequestDto } from "../models/dtos/jsonDtos/CreateJsonRequestDto";
import { GetJsonResponseDto } from "../models/dtos/jsonDtos/GetJsonResponseDto";
import { IHttpClient } from "./IHttpClient";
import { IJsonApiService } from "./IJsonApiService";

export class JsonApiService implements IJsonApiService {
  private readonly relativeUri: string = "api/jsons";

  constructor(private httpClient: IHttpClient) {}

  public createJson(token: string, request: CreateJsonRequestDto): Promise<void> {
    return this.httpClient.postAuthorizedAsync<CreateJsonRequestDto, void>(
      this.relativeUri,
      request,
      token,
    );
  }

  public getJsonsAsync(token: string): Promise<GetJsonResponseDto[]> {
    return this.httpClient.getAuthorizedAsync<GetJsonResponseDto[]>(
      this.relativeUri,
      token,
    );
  }

  public deleteJson(token: string, jsonId: string): Promise<void> {
    return this.httpClient.deleteAuthorizedAsync(`this.relativeUri/${jsonId}`, token);
  }
}
