import { GetterTree } from "vuex";
import { IVuexState } from "./IVuexState";

export interface IVuexGetters extends GetterTree<IVuexState, any> {
  username: (state: IVuexState) => string;
  isLoggedIn: (state: IVuexState) => boolean;
}
