import { LoginUserRequestDto } from "../models/dtos/userDtos/loginUserRequestDto";
import { LoginUserResponseDto } from "../models/dtos/userDtos/loginUserResponseDto";
import { RegisterUserRequestDto } from "../models/dtos/userDtos/RegisterUserRequestDto";

export interface IUserApiService {
  loginAsync(request: LoginUserRequestDto): Promise<LoginUserResponseDto>;

  registerAsync(request: RegisterUserRequestDto): Promise<LoginUserResponseDto>;
}
