import Vue from "vue";
import { Component, Provide } from "vue-property-decorator";
import { Action } from "vuex-class/lib";
import { CreateJsonRequestDto } from "../../models/dtos/jsonDtos/CreateJsonRequestDto";
import { IVuexActions } from "../../vuex/interfaces/IVuexActions";
import { IJsonEditor } from "./IJonEditor";

@Component
export default class JsonEditor extends Vue implements IJsonEditor {
  @Provide() public name: string = "";

  @Provide() public json: string = "";

  @Action(nameof<IVuexActions>((a) => a.createJson))
  private vuexCreateJson!: (s: CreateJsonRequestDto) => void;

  public send(): void {
    this.vuexCreateJson(new CreateJsonRequestDto(this.name, this.json));
  }
}
