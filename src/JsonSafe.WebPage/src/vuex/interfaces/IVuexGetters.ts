import { GetterTree } from "vuex";
import { GetJsonResponseDto } from "../../models/dtos/jsonDtos/GetJsonResponseDto";
import { IVuexState } from "./IVuexState";

export interface IVuexGetters extends GetterTree<IVuexState, any> {
  username: (state: IVuexState) => string;
  isLoggedIn: (state: IVuexState) => boolean;
  jsons: (state: IVuexState) => GetJsonResponseDto[];
}
