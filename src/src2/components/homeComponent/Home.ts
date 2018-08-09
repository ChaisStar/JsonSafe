/**
 * components/home/Home.ts
 */
import Vue from "vue";
import Component from "vue-class-component";

@Component({})
export class Home extends Vue {
  public mounted(): void {
    this.$nextTick(() => console.log("Home mounted"));
  }
}
