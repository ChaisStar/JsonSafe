import Vue from "vue";
import { Component, Provide } from "vue-property-decorator";
import { Action } from "vuex-class/lib";
import { CreateJsonRequestDto } from "../../models/dtos/jsonDtos/CreateJsonRequestDto";
import { IVuexActions } from "../../vuex/interfaces/IVuexActions";
import JsonViewEdit from "../jsonViewEdit/JsonViewEdit.vue";
import { IJsonTextEditor } from "./IJsonTextEditor";

@Component({components: {
  JsonViewEdit,
},
})
export default class JsonTextEditor extends Vue implements IJsonTextEditor {
  @Provide() public name: string = "";

  @Provide() public json: string = "{}";

  @Provide() public invalidCharacterError: string = "";

  @Provide() public parsedJson: string = "";

  @Action(nameof<IVuexActions>((a) => a.createJson))
  private vuexCreateJson!: (s: CreateJsonRequestDto) => void;

  public send(): void {
    this.vuexCreateJson(new CreateJsonRequestDto(this.name, this.json));
  }

  public prettify(): void {
    console.log("pressed");
  }
}
