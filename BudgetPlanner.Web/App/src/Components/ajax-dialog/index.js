import Axios from "axios";
import Components from "../.././components";
const template = require("./index.html");

const defaultComponent = {
    template: template,
    components: Components,
    props: {
        requestUrl: String,
        parameter: String
    },
    data() {
        return {
            url: this.requestUrl,
            param: this.parameter,
            content: null
        };
    },
    watch: {
        requestUrl(newValue) {
            this.url = newValue;
            this.getRequestUrl();
        },
        parameter(newValue) {
            this.param = newValue;
            this.requestUrl();
        }
    },
    methods: {
        getRequestUrl() {
            const context = this;
            Axios.get(this.url, { params: { isModal: true, id: this.param } })
                .then(e => context.content = e.data);
        }
    },
    created() {
        this.getRequestUrl();
    }
};

export default defaultComponent;