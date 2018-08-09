import { inject, injectable } from "inversify";

import { GetStorageItemDto } from "../models/dtos/GetStorageItemDto";
import { LoginCustomerRequestDto } from "./../models/dtos/loginCustomerRequestDto";
import { LoginCustomerResponseDto } from "./../models/dtos/loginCustomerResponseDto";
import { SERVICE_IDENTIFIERS } from "./../serviceIdentifiers";
import { HttpClient } from "./httpClient";
import { ICustomerService } from "./ICustomerService";

@injectable()
export class CustomerService implements ICustomerService {
    private readonly relativeUri: string = "api/customers";

    constructor(
        @inject(SERVICE_IDENTIFIERS.HTTP_CLIENT) private httpClient: HttpClient,
    ) {}

    public async loginAsync(request: LoginCustomerRequestDto ): Promise<LoginCustomerResponseDto> {
        return await this.httpClient
            .postAsync<LoginCustomerRequestDto, LoginCustomerResponseDto>(`${this.relativeUri}/login`, request);
    }

    public async getJsonsAsync(token: string): Promise<GetStorageItemDto[]> {
        return await this.httpClient.getAuthorizedAsync<GetStorageItemDto[]>("api/jsons", token);
    }
}
