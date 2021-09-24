import { IVuexGetters } from "./interfaces/IVuexGetters";

export const getters: IVuexGetters = {
  isLoggedIn: (state) => state.userModel.isLoggedIn,
  jsons: (state) => state.userJsonsModel,
  username: (state) => state.userModel.username,
};
