import Vue from "vue";
import { Component } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";

import { IJsonList } from "./IJsonList";

import { IVuexActions } from "../../vuex/interfaces/IVuexActions";
import { IVuexGetters } from "../../vuex/interfaces/IVuexGetters";

@Component
export default class JsonList extends Vue implements IJsonList {
  @Getter(nameof<IVuexGetters>((g) => g.username)) public username!: string;

  @Action(nameof<IVuexActions>((a) => a.getJsons)) public getJsons!: () => void;
}
