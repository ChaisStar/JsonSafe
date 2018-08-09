import Vue from "vue";
import { Component, Inject, Model } from "vue-property-decorator";

import { LoginCustomerRequestDto } from "../../models/dtos/loginCustomerRequestDto";
import { SERVICE_IDENTIFIERS } from "../../ServiceIdentifiers";
import { ICustomerService } from "../../services/ICustomerService";
import { ITokenStorage } from "../../services/ITokenSorage";
import { ILogin } from "./ILogin";

@Component({})
export default class Login extends Vue implements ILogin {
    @Model() public username: string = "";

    @Model() public password: string = "";

    @Model() public message: string = "";

    @Inject(SERVICE_IDENTIFIERS.CUSTOMER_SERVICE)
    private customerService!: ICustomerService;

    @Inject(SERVICE_IDENTIFIERS.TOKEN_STORAGE)
    private tokenStorage!: ITokenStorage;

    public async loginAsync(): Promise<void> {
        this.message = "";
        const login = new LoginCustomerRequestDto();
        login.username = this.username;
        login.password = this.password;
        try {
            const response = await this.customerService.loginAsync(login);
            this.tokenStorage.addToken("token", response.token);
            this.$router.push("jsons");
        } catch (error) {
            this.message = error;
        }
    }
}
