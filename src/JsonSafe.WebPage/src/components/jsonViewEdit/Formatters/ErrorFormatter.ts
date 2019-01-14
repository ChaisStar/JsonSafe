import { FormatterHelpers } from "./FormatterHelpers";

export class ErrorFormatter {
  public GetHtml(error: Error, data: string): string {
    data = data.replace("\u0000", "\uFFFD");

    const errorInfo = this.massageError(error);

    let output = `<div id="error">{errorParsing}`;
    if (errorInfo.message) {
      output += `<div class="errormessage">${errorInfo.message}</div>`;
    }
    output += `</div><div id="json">${this.highlightError(data, errorInfo.line, errorInfo.column)}</div>`;
    return output;
  }

  private massageError(error: Error): {
    message: string;
    line?: number;
    column?: number;
  } {
    if (!error.message) {
      return error;
    }

    const message = error.message.replace(/^JSON.parse: /, "").replace(/of the JSON data/, "");
    const parts = /line (\d+) column (\d+)/.exec(message);
    if (!parts || parts.length !== 3) {
      return error;
    }

    return {
      column: Number(parts[2]),
      line: Number(parts[1]),
      message: FormatterHelpers.htmlEncode(message),
    };
  }

  private highlightError(data: string, lineNum?: number, columnNum?: number) {
    if (!lineNum || !columnNum) {
      return FormatterHelpers.htmlEncode(data);
    }

    const lines = data.match(/^.*((\r\n|\n|\r)|$)/gm)!;

    let output = "";
    for (let i = 0; i < lines.length; i++) {
      const line = lines[i];

      if (i === lineNum - 1) {
        output += '<span class="errorline">';
        // tslint:disable-next-line:max-line-length
        output += `${FormatterHelpers.htmlEncode(line.substring(0, columnNum - 1))}<span class="errorcolumn">${FormatterHelpers.htmlEncode(line[columnNum - 1])}</span>${FormatterHelpers.htmlEncode(line.substring(columnNum))}`;
        output += "</span>";
      } else {
        output += FormatterHelpers.htmlEncode(line);
      }
    }

    return output;
  }
}
