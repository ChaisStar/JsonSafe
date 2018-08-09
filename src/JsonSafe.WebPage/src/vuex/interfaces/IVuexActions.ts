import { ActionContext, ActionTree } from "vuex";

import { CreateJsonRequestDto } from "../../models/dtos/jsonDtos/CreateJsonRequestDto";
import { LoginUserRequestDto } from "../../models/dtos/userDtos/LoginUserRequestDto";
import { RegisterUserRequestDto } from "../../models/dtos/userDtos/RegisterUserRequestDto";
import { IVuexState } from "./IVuexState";

export interface IVuexActions extends ActionTree<IVuexState, any> {
  initialiseStore: (context: ActionContext<IVuexState, any>) => void;
  login: (
    context: ActionContext<IVuexState, any>,
    loginUserRequestDto: LoginUserRequestDto,
  ) => void;
  logout: (context: ActionContext<IVuexState, any>) => void;
  getJsons: (context: ActionContext<IVuexState, any>) => void;
  signup: (
    context: ActionContext<IVuexState, any>,
    registerUserRequestDto: RegisterUserRequestDto,
  ) => void;
  createJson: (context: ActionContext<IVuexState, any>, request: CreateJsonRequestDto) => void;
}
