import Vue from "vue";
import { Component, Inject, Prop } from "vue-property-decorator";

import { SERVICE_IDENTIFIERS } from "../../ServiceIdentifiers";
import { ICustomerService } from "../../services/ICustomerService";
import { ITokenStorage } from "../../services/ITokenSorage";
import { IJsonList } from "./IJsonList";

@Component({})
export default class JsonList extends Vue implements IJsonList {
    @Prop() public jsons: string[] = [];
    @Prop() public selectedJson: string = "";
    @Prop() public message: string = "";

    @Inject(SERVICE_IDENTIFIERS.CUSTOMER_SERVICE)
    private customerService!: ICustomerService;

    @Inject(SERVICE_IDENTIFIERS.TOKEN_STORAGE)
    private tokenStorage!: ITokenStorage;

    public async getJsonsAsync(): Promise<void> {
        this.message = "";
        try {
            const token = this.tokenStorage.getToken("token");
            const response = await this.customerService.getJsonsAsync(token);
            this.jsons = response.map((x) => x.BsonDocument);
        } catch (error) {
            this.message = error;
        }
    }
}
