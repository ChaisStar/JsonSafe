export interface ISignup {
  username: string;
  password: string;
  confirmPassword: string;
  email: string;
  register(): void;
}
