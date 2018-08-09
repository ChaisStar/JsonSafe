import { GetJsonResponseDto } from "../../models/dtos/jsonDtos/GetJsonResponseDto";
import { IUserModel } from "../models/IUserModel";

export interface IVuexState {
  userModel: IUserModel;
  userJsonsModel: GetJsonResponseDto[];
}
