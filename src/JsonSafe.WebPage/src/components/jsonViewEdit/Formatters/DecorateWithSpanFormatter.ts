import { FormatterHelpers } from "./FormatterHelpers";
import { IFormatter } from "./IFormatter";

export class DecorateWithSpanFormatter implements IFormatter {
  public GetHtml(value: any, path: string): string {
    return `<span class="${path}">${FormatterHelpers.htmlEncode(value)}</span>`;
  }
}
