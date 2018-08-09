import { IVuexMutations } from "./interfaces/IVuexMutations";

export const mutations: IVuexMutations = {
  changeUsername: (state, username) => (state.userModel.username = username),

  changeUserToken: (state, token) => {
    state.userModel.token = token;
    state.userModel.isLoggedIn = token !== "";
  },

  updateUserJsons: (state, jsons) => (state.userJsonsModel = jsons),
};
