import { BaseFormatter } from "./BaseFormatter";
import { FormatterHelpers } from "./FormatterHelpers";
import { IFormatter } from "./IFormatter";

export class ObjectFormatter implements IFormatter {
  public GetHtml(value: any, path: string): string {
    let numProps = Object.keys(value).length;
    if (numProps === 0) {
      return "{ }";
    }

    let output = "";
    for (const prop in value) {
      if (!value.hasOwnProperty(prop)) {
        continue;
      }
      let subPath = "";
      let escapedProp = JSON.stringify(prop).slice(1, -1);
      const bare = this.isBareProp(prop);
      if (bare) {
        subPath = `${path}.${escapedProp}`;
      } else {
        escapedProp = `"${escapedProp}"`;
      }
      // tslint:disable-next-line:max-line-length
      output += `<li><span class="prop${(bare ? "" : " quoted")}" title="${FormatterHelpers.htmlEncode(subPath)}"><span class="q">&quot;</span>${FormatterHelpers.jsString(prop)}<span class="q">&quot;</span></span>: ${new BaseFormatter().GetHtml(value[prop], subPath)}`;
      if (numProps > 1) {
        output += ",";
      }
      output += "</li>";
      numProps--;
    }

    return `<span class="collapser"></span>{<ul class="obj collapsible">${output}</ul>}`;
  }

  private isBareProp = (prop: string): boolean => /^[A-Za-z_$][A-Za-z0-9_\-$]*$/.test(prop);
}
