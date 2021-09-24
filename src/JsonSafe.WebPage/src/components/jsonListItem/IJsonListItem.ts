import { GetJsonResponseDto } from "../../models/dtos/jsonDtos/GetJsonResponseDto";

export interface IJsonListItem {
  json: GetJsonResponseDto;
  name: string;
  description: string;
  deleteJson: () => void;
}
