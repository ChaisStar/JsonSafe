import { IVuexGetters } from "./interfaces/IVuexGetters";

export const getters: IVuexGetters = {
  isLoggedIn: (state) => state.userModel.isLoggedIn,
  username: (state) => state.userModel.username,
};
