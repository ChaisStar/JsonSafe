import Vue from "vue";
import { Component, Provide, Watch } from "vue-property-decorator";
import { Action, Getter } from "vuex-class/lib";

import { LoginUserRequestDto } from "../../models/dtos/userDtos/LoginUserRequestDto";
import { IVuexActions } from "../../vuex/interfaces/IVuexActions";
import { IVuexGetters } from "../../vuex/interfaces/IVuexGetters";
import { ILogin } from "./ILogin";

@Component
export default class Login extends Vue implements ILogin {
  @Provide() public username: string = "";

  @Provide() public password: string = "";

  @Action(nameof<IVuexActions>((a) => a.login)) private vuexLogin!: (s: LoginUserRequestDto) => void;

  @Getter(nameof<IVuexGetters>((g) => g.isLoggedIn)) private vuexIsLoggedInGetter!: boolean;

  @Watch(nameof<Login>((l) => l.vuexIsLoggedInGetter))
  public redirect(): void {
    if (this.vuexIsLoggedInGetter) {
      this.$router.replace("/jsonList");
    }
  }

  public login(): void {
    this.vuexLogin(new LoginUserRequestDto(this.username, this.password));
  }
}
