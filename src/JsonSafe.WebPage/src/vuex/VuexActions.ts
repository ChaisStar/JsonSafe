import {} from "ts-nameof";
import { ActionContext } from "vuex";
import { LoginUserResponseDto } from "../models/dtos/userDtos/loginUserResponseDto";
import { BrowserStorage } from "../services/BrowserStorage";
import { HttpClient } from "../services/HttpClient";
import { IBrowserStorage } from "../services/IBrowserStorage";
import { IJsonApiService } from "../services/IJsonApiService";
import { IUserApiService } from "../services/IUserApiService";
import { JsonApiService } from "../services/JsonApiService";
import { UserApiService } from "../services/UserApiService";
import { IVuexActions } from "./interfaces/IVuexActions";
import { IVuexMutations } from "./interfaces/IVuexMutations";
import { IVuexState } from "./interfaces/IVuexState";
import { IUserModel } from "./models/IUserModel";

const httpClient = new HttpClient();
const userApiService = new UserApiService(httpClient) as IUserApiService;
const jsonApiService = new JsonApiService(httpClient) as IJsonApiService;
const browserStorage = new BrowserStorage() as IBrowserStorage;
const browserStorageUsername = nameof<IUserModel>((um) => um.username);
const browserStorageToken = nameof<IUserModel>((um) => um.token);

const updateState = (
  context: ActionContext<IVuexState, any>,
  username: string,
  token: string,
): void => {
  context.commit(nameof<IVuexMutations>((m) => m.changeUsername), username);
  context.commit(nameof<IVuexMutations>((m) => m.changeUserToken), token);
};

const userResponseToState = (
  context: ActionContext<IVuexState, any>,
  response: LoginUserResponseDto,
): void => {
  browserStorage.writeKeyValue(browserStorageUsername, response.username);
  browserStorage.writeKeyValue(browserStorageToken, response.token);
  updateState(context, response.username, response.token);
};

export const actions: IVuexActions = {
  getJsons: async (context) => {
    const response = await jsonApiService.getJsonsAsync(
      context.state.userModel.token,
    );
    console.log(response);
    context.commit(nameof<IVuexMutations>((m) => m.updateUserJsons), response);
  },
  initializeStore: (context) => {
    try {
      const username = browserStorage.getValue(browserStorageUsername);
      const token = browserStorage.getValue(browserStorageToken);
      updateState(context, username, token);
    } catch {
      console.log("there is no user info");
    }
  },
  login: async (context, loginUserRequestDto) => {
    const response = await userApiService.loginAsync(loginUserRequestDto);
    userResponseToState(context, response);
  },
  logout: async (context) => {
    browserStorage.clear();
    context.commit(nameof<IVuexMutations>((m) => m.changeUserToken), "");
  },

  signup: async (context, registerUserRequestDto) => {
    const response = await userApiService.registerAsync(registerUserRequestDto);
    userResponseToState(context, response);
  },

  createJson: async (context, request) => {
    await jsonApiService.createJson(context.state.userModel.token, request);
  },
  deleteJson: async (context, jsonId) => {
    await jsonApiService.deleteJson(context.state.userModel.token, jsonId);
  },
};
