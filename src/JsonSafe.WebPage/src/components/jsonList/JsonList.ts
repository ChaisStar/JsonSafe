import Vue from "vue";
import { Component } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";

import { IJsonList } from "./IJsonList";

import { GetJsonResponseDto } from "../../models/dtos/jsonDtos/GetJsonResponseDto";
import { IVuexActions } from "../../vuex/interfaces/IVuexActions";
import { IVuexGetters } from "../../vuex/interfaces/IVuexGetters";
import JsonListItem from "../jsonListItem/JsonListItem.vue";

@Component(
  {
    components: {
      JsonListItem,
    },
  },
)
export default class JsonList extends Vue implements IJsonList {
  @Getter(nameof<IVuexGetters>((g) => g.username)) public username!: string;

  @Action(nameof<IVuexActions>((a) => a.getJsons)) public getJsons!: () => void;

  @Getter(nameof<IVuexGetters>((g) => g.jsons)) public jsons!: GetJsonResponseDto[];

  public mounted(): void {
    console.log(this);
  }
}
