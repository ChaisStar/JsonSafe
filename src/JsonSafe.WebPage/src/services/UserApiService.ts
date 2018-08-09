import { LoginUserRequestDto } from "../models/dtos/userDtos/loginUserRequestDto";
import { LoginUserResponseDto } from "../models/dtos/userDtos/loginUserResponseDto";
import { RegisterUserRequestDto } from "../models/dtos/userDtos/RegisterUserRequestDto";
import { RegisterUserResponseDto } from "../models/dtos/userDtos/RegisterUserResponseDto";
import { IHttpClient } from "./IHttpClient";
import { IUserApiService } from "./IUserApiService";

export class UserApiService implements IUserApiService {
  private readonly relativeUri: string = "api/users";

  constructor(private httpClient: IHttpClient) {}

  public loginAsync(
    request: LoginUserRequestDto,
  ): Promise<LoginUserResponseDto> {
    return this.httpClient.postAsync<LoginUserRequestDto, LoginUserResponseDto>(
      `${this.relativeUri}/login`,
      request,
    );
  }

  public registerAsync(
    request: RegisterUserRequestDto,
  ): Promise<RegisterUserResponseDto> {
    return this.httpClient.postAsync<
      RegisterUserRequestDto,
      LoginUserResponseDto
    >(`${this.relativeUri}/register`, request);
  }
}
