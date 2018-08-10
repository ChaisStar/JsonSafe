import { GetJsonResponseDto } from "../../models/dtos/jsonDtos/GetJsonResponseDto";

export interface IJsonListItem {
  json: GetJsonResponseDto;
  deleteJson: () => void;
}
