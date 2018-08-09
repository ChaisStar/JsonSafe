import Vue from "vue";
import { Component, Emit } from "vue-property-decorator";
import { Action, Getter } from "vuex-class/lib";

import { IVuexActions } from "../../vuex/interfaces/IVuexActions";
import { IVuexGetters } from "../../vuex/interfaces/IVuexGetters";
import { INavbar } from "./INavbar";

@Component
export default class Navbar extends Vue implements INavbar {
  @Getter(nameof<IVuexGetters>((g) => g.isLoggedIn)) public isLoggedIn!: boolean;

  @Getter(nameof<IVuexGetters>((g) => g.username)) public username!: string;

  @Action(nameof<IVuexActions>((a) => a.logout)) private vuexLogout!: () => void;

  @Emit()
  public register(): void {
    console.log("register");
  }
  @Emit()
  public login(): void {
    console.log("login");
  }

  public logout(): void {
    this.vuexLogout();
  }
}
