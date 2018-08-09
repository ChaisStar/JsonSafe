export class RegisterUserRequestDto {
  constructor(
    public username: string,
    public password: string,
    public confirmPassword: string,
    public email: string,
  ) {}
}
