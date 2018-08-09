/**
 * router.ts
 */
import Vue from "vue";
import Router from "vue-router";

import { About } from "./components/aboutComponent/About";
import { Home } from "./components/homeComponent/Home";
import { List } from "./components/listComponent/List";

Vue.use(Router);

export const router: Router = new Router({
  routes: [
    { path: "/", component: Home },
    { path: "/about", component: About },
    { path: "/list", component: List },
  ],
});
