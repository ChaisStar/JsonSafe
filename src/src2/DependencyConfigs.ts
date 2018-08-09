import "reflect-metadata";

import { Container } from "inversify";

import { SERVICE_IDENTIFIERS } from "./serviceIdentifiers";
import { CustomerService } from "./services/customerService";
import { HttpClient } from "./services/httpClient";
import { ICustomerService } from "./services/ICustomerService";
import { IHttpClient } from "./services/IHttpClient";
import { ITokenStorage } from "./services/ITokenSorage";
import { TokenStorage } from "./services/TokenStorage";

const container = new Container();

container.bind<IHttpClient>(SERVICE_IDENTIFIERS.HTTP_CLIENT).to(HttpClient);
container.bind<ICustomerService>(SERVICE_IDENTIFIERS.CUSTOMER_SERVICE).to(CustomerService);
container.bind<ITokenStorage>(SERVICE_IDENTIFIERS.TOKEN_STORAGE).to(TokenStorage);

export default container;
