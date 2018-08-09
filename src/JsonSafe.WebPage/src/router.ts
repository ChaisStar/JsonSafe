import Vue from "vue";
import VueRouter from "vue-router";

import Default from "./components/default/default.vue";
import JsonEditor from "./components/jsonEditor/jsonEditor.vue";
import JsonList from "./components/jsonList/jsonList.vue";
import Login from "./components/login/login.vue";
import Signup from "./components/signup/signup.vue";

Vue.use(VueRouter);

export default new VueRouter({
  linkActiveClass: "active",
  mode: "history",
  routes: [
    {
      component: Login,
      name: nameof<Login>(),
      path: "/login",
    },
    {
      component: JsonEditor,
      name: nameof<JsonEditor>(),
      path: "/jsonEditor",
    },
    {
      component: Signup,
      name: nameof<Signup>(),
      path: "/signup",
    },
    {
      component: JsonList,
      name: nameof<JsonList>(),
      path: "/jsonList",
    },
    {
      component: Default,
      path: "/*",
    },
  ],
});
