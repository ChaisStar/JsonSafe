export class FormatterHelpers {
  public static htmlEncode(value: any): string {
    return (typeof value !== "undefined" && value !== null) ? value.toString()
      .replace(/&/g, "&amp;")
      .replace(/"/g, "&quot;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;")
      : "";
  }

  public static jsString(s: string): string {
    s = JSON.stringify(s).slice(1, -1);
    return this.htmlEncode(s);
  }
}
