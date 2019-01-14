import { DecorateWithSpanFormatter } from "./DecorateWithSpanFormatter";
import { FormatterHelpers } from "./FormatterHelpers";
import { IFormatter } from "./IFormatter";

export class StringFormatter implements IFormatter {
  public GetHtml(value: any, path: string): string {
    if (value.charCodeAt(0) === 8203 && !isNaN(value.slice(1))) {
      return new DecorateWithSpanFormatter().GetHtml(value.slice(1), "num");
    } else if (/^(http|https|file):\/\/[^\s]+$/i.test(value)) {
      // tslint:disable-next-line:max-line-length
      return `<a href="${FormatterHelpers.htmlEncode(value)}"><span class="q">&quot;</span>${FormatterHelpers.jsString(value)}<span class="q">&quot;</span></a>`;
    } else {
      return `<span class="string">&quot;${FormatterHelpers.jsString(value)}&quot;</span>`;
    }
  }
}
