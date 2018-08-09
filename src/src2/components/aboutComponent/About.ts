/**
 * components/about/About.ts
 */
import Vue from "vue";
import Component from "vue-class-component";

@Component({})
export class About extends Vue {
  public mounted(): void {
    this.$nextTick(() => console.log("About mounted"));
  }
}
