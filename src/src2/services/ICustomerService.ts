import { GetStorageItemDto } from "../models/dtos/GetStorageItemDto";
import { LoginCustomerRequestDto } from "../models/dtos/loginCustomerRequestDto";
import { LoginCustomerResponseDto } from "../models/dtos/loginCustomerResponseDto";

export interface ICustomerService {
     loginAsync(request: LoginCustomerRequestDto ): Promise<LoginCustomerResponseDto>;

     getJsonsAsync(token: string): Promise<GetStorageItemDto[]>;
}
