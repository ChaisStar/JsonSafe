import Vue from "vue";
import VueRouter from "vue-router";

import JsonList from "./components/jsonListComponent/JsonList.vue";
import Login from "./components/loginComponent/Login.vue";
import container from "./DependencyConfigs";
import { SERVICE_IDENTIFIERS } from "./serviceIdentifiers";
import { ICustomerService } from "./services/ICustomerService";
import { ITokenStorage } from "./services/ITokenSorage";

const router = new VueRouter({
  base: "",
  linkActiveClass: "active",
  mode: "history",
  routes: [
    {
      component: {
        components: {
          Login,
        },
        provide: {
          [SERVICE_IDENTIFIERS.CUSTOMER_SERVICE]: container.get<ICustomerService>(SERVICE_IDENTIFIERS.CUSTOMER_SERVICE),
          [SERVICE_IDENTIFIERS.TOKEN_STORAGE]: container.get<ITokenStorage>(SERVICE_IDENTIFIERS.TOKEN_STORAGE),
        },
        template: `<login></login>`,
      },
      path: "/login",
    },
    {
      component: {
        components: {
          JsonList,
        },
        provide: {
          [SERVICE_IDENTIFIERS.CUSTOMER_SERVICE]: container.get<ICustomerService>(SERVICE_IDENTIFIERS.CUSTOMER_SERVICE),
          [SERVICE_IDENTIFIERS.TOKEN_STORAGE]: container.get<ITokenStorage>(SERVICE_IDENTIFIERS.TOKEN_STORAGE),
        },
        template: `<json-list-component></json-list-component>`,
      },
      path: "/jsons",
    },
    {
      component: {template: "<h2>Not Found 1</h2>"},
      path: "/",
    },
    {
      component: {template: "<h2>Not</h2>"},
      path: "*",
    },
  ],
});

Vue.use(VueRouter);
Vue.config.devtools = true;

export const app = new Vue({
  el: "#app",
  router,
});
