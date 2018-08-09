import Vue from "vue";
import Vuex, { StoreOptions } from "vuex";

import { IVuexState } from "./interfaces/IVuexState";
import { actions } from "./VuexActions";
import { getters } from "./VuexGetters";
import { mutations } from "./VuexMutations";
import { state } from "./VuexState";

Vue.use(Vuex);

export const UserStore: StoreOptions<IVuexState> = {
  actions,
  getters,
  mutations,
  state,
};

export const store = new Vuex.Store<IVuexState>(UserStore);
