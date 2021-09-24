import { GetJsonResponseDto } from "../../models/dtos/jsonDtos/GetJsonResponseDto";

export interface IJsonList {
  username: string;
  getJsons: () => void;
  jsons: GetJsonResponseDto[];
}
