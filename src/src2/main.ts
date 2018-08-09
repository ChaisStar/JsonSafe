/**
 * main.ts
 */
// import "@/styles/style.css";
import Vue from "vue";

import NavBar from "./components/navBarComponent/navBar.vue";
import { router } from "./router";

// tslint:disable-next-line:no-unused-new
// tslint:disable-next-line:no-unused-expression
new Vue({
  components: {
    navbar: NavBar,
  },
  el: "#app-main",
  router,
});
