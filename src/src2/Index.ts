import Vue from "vue";

import HelloComponent from "./components/helloComponent/hello.vue";
import container from "./DependencyConfigs";
import { SERVICE_IDENTIFIERS } from "./serviceIdentifiers";
import { ICustomerService } from "./services/ICustomerService";
import { ITokenStorage } from "./services/ITokenSorage";

const v = new Vue({
    components: {
        HelloComponent,
    },
    data: { name: "World" },
    el: "#app",
    provide: {
         [SERVICE_IDENTIFIERS.CUSTOMER_SERVICE]: container.get<ICustomerService>(SERVICE_IDENTIFIERS.CUSTOMER_SERVICE),
         [SERVICE_IDENTIFIERS.TOKEN_STORAGE]: container.get<ITokenStorage>(SERVICE_IDENTIFIERS.TOKEN_STORAGE),
    },
    template: `
    <div>
        Name: <input v-model="name" type="text">
        <hello-component :name="name" :initialEnthusiasm="5"/>
    </div>
    `,
});
