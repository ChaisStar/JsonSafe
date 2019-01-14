import Vue from "vue";
import { Component, Prop, Watch } from "vue-property-decorator";
import { BaseFormatter } from "./Formatters/BaseFormatter";
import { ErrorFormatter } from "./Formatters/ErrorFormatter";
import { IJsonViewEdit } from "./IJsonViewEdit";

@Component
export default class JsonViewEdit extends Vue implements IJsonViewEdit {
  public get jsonDom(): string { return this.jsonHtml; }
  public get jsonError(): string { return this.errorHtml; }

  @Prop() public jsonInput!: string;
  private jsonHtml: string = "";
  private errorHtml: string = "";

  @Watch(nameof<JsonViewEdit>((j) => j.jsonInput))
  public parse(): void {
    try {
      const jsonObject = JSON.parse(this.jsonInput);
      this.jsonHtml = new BaseFormatter().GetHtml(jsonObject, "<root>");
      this.errorHtml = "";
    } catch (error) {
      this.errorHtml = new ErrorFormatter().GetHtml(error, this.jsonInput);
    }
  }
}
