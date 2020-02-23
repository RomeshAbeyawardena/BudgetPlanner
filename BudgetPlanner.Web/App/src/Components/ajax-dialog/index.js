import Axios from "axios";
import Components from "../.././components";
const template = require("./index.html");

const defaultComponent = {
    template: template,
    components: Components,
    props: {
        requestUrl: String,
        parameter: String,
        panelIsVisible: Boolean
    },
    data() {
        return {
            url: this.requestUrl,
            param: this.parameter,
            content: null,
            isVisible: this.panelIsVisible
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
        },
        panelIsVisible(newValue) {
            this.isVisible = newValue;
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