/**
 * components/navbar/Navbar.ts
 */
import Vue from "vue";
import Component from "vue-class-component";
import * as Router from "vue-router";

import { ILink } from "./Types";

@Component({
  watch: {
    $route(current: Router.Route, previous: Router.Route): void {
      console.log("Changed current path to: " + this.$route.path);
    },
  },
})
export class Navbar extends Vue {
  public links: ILink[] = [
    { name: "Home", path: "/" },
    { name: "About", path: "/about" },
    { name: "List", path: "/list" },
  ];

  public mounted(): void {
    this.$nextTick(() => console.log("Navbar mounted"));
  }
}
