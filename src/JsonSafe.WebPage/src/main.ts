import Vue from "vue";

import Navbar from "./components/navbar/navbar.vue";
import router from "./router";
import { IVuexActions } from "./vuex/interfaces/IVuexActions";
import { store } from "./vuex/vuexConfig";

Vue.config.errorHandler = (err, vm, info) => {
  console.log("Vue error: ", err);
  console.log("Vue vue: ", vm);
  console.log("Vue info: ", info);
};

export const main = new Vue({
  components: {
    Navbar,
  },
  el: "#app",
  router,
  store,
  template: ` <div>
      <navbar></navbar>
      <div name="router" mode="out-in">
        <router-view class="view router-view"></router-view>
      </div>
    </div>`,
    beforeCreate() {
      console.log(this.$store);
      this.$store.dispatch(nameof<IVuexActions>((m) => m.initialiseStore));
    },
});
