import { Component, Inject, Model, Prop, Vue } from "vue-property-decorator";

import { LoginCustomerRequestDto } from "../../models/dtos/loginCustomerRequestDto";
import { SERVICE_IDENTIFIERS } from "../../serviceIdentifiers";
import { ICustomerService } from "../../services/ICustomerService";
import { ITokenStorage } from "../../services/ITokenSorage";
import { IHello } from "./IHello";

@Component({})
export default class HelloDecorator extends Vue implements IHello {
    public jsons: string;
    @Model() public username: string;
    @Model() public password: string;
    @Prop() public resultToken: string;
    @Prop() public resultUsername: string;
    @Prop() public name!: string;
    @Prop() public initialEnthusiasm!: number;
    public enthusiasm = this.initialEnthusiasm;
    @Inject(SERVICE_IDENTIFIERS.CUSTOMER_SERVICE)
    private customerService!: ICustomerService;

    @Inject(SERVICE_IDENTIFIERS.TOKEN_STORAGE)
    private tokenStorage!: ITokenStorage;

    constructor() {
        super();
        this.username = "";
        this.password = "";
        this.resultToken = "";
        this.resultUsername = "";
        this.jsons = "";
    }

    public increment() {
        this.enthusiasm++;
    }

    public decrement() {
        if (this.enthusiasm > 1) {
            this.enthusiasm--;
        }
    }

    public getExclamationMarks(): string {
        return Array(this.enthusiasm + 1).join("!");
    }

    public async loginAsync(): Promise<void> {
        const login = new LoginCustomerRequestDto();
        login.username = this.username;
        login.password = this.password;
        const response = await this.customerService.loginAsync(login);
        this.tokenStorage.addToken(response.username, response.token);
        this.resultToken = response.token;
        this.resultUsername = response.username;
    }

    public async getJsonsAsync(): Promise<void> {
        const token = this.tokenStorage.getToken(this.username);
        const response = await this.customerService.getJsonsAsync(token);
        this.jsons = JSON.stringify(response);
    }
}
