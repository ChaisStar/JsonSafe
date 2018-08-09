import { IVuexState } from "./interfaces/IVuexState";

export const state: IVuexState = {
  userJsonsModel: [],
  userModel: {
    isLoggedIn: false,
    token: "",
    username: "",
  },
};
