import { MutationTree } from "vuex";
import { GetJsonResponseDto } from "../../models/dtos/jsonDtos/GetJsonResponseDto";
import { IVuexState } from "./IVuexState";

export interface IVuexMutations extends MutationTree<IVuexState> {
  changeUsername: (state: IVuexState, username: string) => void;

  changeUserToken: (state: IVuexState, token: string) => void;

  updateUserJsons: (state: IVuexState, jsons: GetJsonResponseDto[]) => void;
}
