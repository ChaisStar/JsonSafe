import { BaseFormatter } from "./BaseFormatter";
import { IFormatter } from "./IFormatter";

export class ArrayFormatter implements IFormatter {
  public GetHtml(value: any, path: string): string {
    if (value.length === 0) {
      return "[ ]";
    }

    let output = "";
    for (let i = 0; i < value.length; i++) {
      const subPath = `${path}[${i}]`;
      output += "<li>" + new BaseFormatter().GetHtml(value[i], subPath);
      if (i < value.length - 1) {
        output += ",";
      }
      output += "</li>";
    }
    return (value.length === 0 ? "" : '<span class="collapser"></span>') +
      `[<ul class="array collapsible">${output}</ul>]`;
  }
}
