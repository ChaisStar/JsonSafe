/**
 * components/list/List.ts
 */
import axios, { AxiosInstance, AxiosResponse } from "axios";
import Vue from "vue";
import Component from "vue-class-component";

import { IUser } from "./Types";

@Component({})
export class List extends Vue {
  public items: IUser[] = [];
  private url: string = "https://jsonplaceholder.typicode.com/users";
  private axios: AxiosInstance;

  constructor() {
    super();
    this.axios = axios;
  }

  public created(): void {
    console.log("List created");
    this.fetchData();
  }

  public mounted(): void {
    this.$nextTick(() => console.log("List mounted"));
  }

  private fetchData(): void {
    if (this.items.length) {
      return;
    }

    this.axios.get(this.url)
      .then((response: AxiosResponse) => {
        this.items = response.data;
      })
      .catch((error: Error) => {
        console.error(error);
      });
  }
}
