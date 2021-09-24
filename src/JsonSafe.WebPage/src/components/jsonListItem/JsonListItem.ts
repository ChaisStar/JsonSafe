import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
import { Action } from "vuex-class";

import { IJsonListItem } from "./IJsonListItem";

import { GetJsonResponseDto } from "../../models/dtos/jsonDtos/GetJsonResponseDto";
import { IVuexActions } from "../../vuex/interfaces/IVuexActions";

@Component
export default class JsonListItem extends Vue implements IJsonListItem {
  public get name(): string {
    return this.json.name || "Unnamed";
  }
  public get description(): string {
    return `Created - ${this.json.created} Updated - {this.json.updated}`;
  }
  @Prop() public json!: GetJsonResponseDto;

  @Action(nameof<IVuexActions>((a) => a.deleteJson)) private delete!: (
    jsonId: string,
  ) => void;
  @Action(nameof<IVuexActions>((a) => a.getJsons)) private getJsons!: () => void;

  public deleteJson(): void {
    this.delete(this.json.id);
    this.getJsons();
  }
}
