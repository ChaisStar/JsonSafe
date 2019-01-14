import { ArrayFormatter } from "./ArrayFormatter";
import { DecorateWithSpanFormatter } from "./DecorateWithSpanFormatter";
import { IFormatter } from "./IFormatter";
import { ObjectFormatter } from "./ObjectFormatter";
import { StringFormatter } from "./StringFormatter";

export class BaseFormatter implements IFormatter {
  public GetHtml(value: any, path: string) {
    const valueType = typeof value;

    if (value === null) {
      return new DecorateWithSpanFormatter().GetHtml("null", "null");
    } else if (Array.isArray(value)) {
      return new ArrayFormatter().GetHtml(value, path);
    } else if (valueType === "object") {
      return new ObjectFormatter().GetHtml(value, path);
    } else if (valueType === "number") {
      return new DecorateWithSpanFormatter().GetHtml(value, "num");
    } else if (valueType === "string") {
      return new StringFormatter().GetHtml(value, path);
    } else if (valueType === "boolean") {
      return new DecorateWithSpanFormatter().GetHtml(value, "bool");
    }

    return "";
  }
}
