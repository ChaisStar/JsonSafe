import Vue from "vue";
import { Component, Provide, Watch } from "vue-property-decorator";
import { Action, Getter } from "vuex-class/lib";

import { RegisterUserRequestDto } from "../../models/dtos/userDtos/RegisterUserRequestDto";
import { IVuexActions } from "../../vuex/interfaces/IVuexActions";
import { IVuexGetters } from "../../vuex/interfaces/IVuexGetters";
import { ISignup } from "./ISignup";

@Component
export default class Signup extends Vue implements ISignup {
  @Provide() public username: string = "";

  @Provide() public password: string = "";

  @Provide() public confirmPassword: string = "";

  @Provide() public email: string = "";

  @Action(nameof<IVuexActions>((a) => a.signup)) public vuexSignup!: (s: RegisterUserRequestDto) => void;

  @Getter(nameof<IVuexGetters>((g) => g.isLoggedIn)) private vuexIsLoggedInGetter!: boolean;

  @Watch(nameof<Signup>((l) => l.vuexIsLoggedInGetter))
  public redirect(): void {
    if (this.vuexIsLoggedInGetter) {
      this.$router.replace("/jsonList");
    }
  }

  public register(): void {
    this.vuexSignup(new RegisterUserRequestDto(this.username, this.password, this.confirmPassword, this.email));
  }
}
