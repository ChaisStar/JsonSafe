import Vue from "vue";
import { Component, Provide } from "vue-property-decorator";
import { IDefault } from "./IDefault";

@Component
export default class Default extends Vue implements IDefault {
  @Provide() public name: string = "";
}
