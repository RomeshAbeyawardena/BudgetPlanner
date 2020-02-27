import Axios from "axios";
import Components from "../.././components";
const template = require("./index.html");

const defaultComponent = {
    template: template,
    components: Components,
    props: {
        title: String,
        requestUrl: String,
        formEntityType: String,
        parameter: null,
        panelIsVisible: Boolean
    },
    data() {
        return {
            entityType: this.formEntityType,
            dialogTitle: this.title,
            capturedForm: null,
            url: this.requestUrl,
            param: this.parameter,
            content: null,
            isVisible: this.panelIsVisible
        };
    },
    watch: {
        formEntityType(newValue) {
            this.entityType = newValue;
        },
        title(newValue) {
            this.dialogTitle = newValue;
        },
        requestUrl(newValue) {
            this.url = newValue;
            this.getRequestUrl();
        },
        parameter(newValue) {
            this.param = newValue;
            this.getRequestUrl();
        },
        panelIsVisible(newValue) {
            this.isVisible = newValue;
            if(newValue)
                this.getRequestUrl();
        },
        capturedForm(newValue) {
            newValue.addEventListener("submit" ,this.onSubmit);
        }
    },
    methods: {
        dismissModal() {
            this.$emit("dialog:dismiss");
        },
        onSubmit(e) {
            const formAction = this.capturedForm.action;
            const formMethod = this.capturedForm.method;
            const formEncoding = this.capturedForm.encoding;

            Axios({
                method: formMethod,
                url: formAction,
                data: new FormData(this.capturedForm),
                headers: {
                    "content-type": formEncoding
                }
            }).then(e => this.handleResponse(e));

            e.preventDefault();
            return false;
        },
        handleResponse(e) {
            if (e.status === 202) {
                this.$emit("form:submit:successful", e.data);
                this.$emit("form:submit:notify", [ "success", this.entityType + " saved successfully"]);
                return;
            }

            if (e.status === 200) {
                this.content = e.data;
                this.captureForm();
            }
        },
        captureForm() {
            window.setTimeout(() => this.capturedForm = this.$el.parentElement.querySelector("form"), 1000);
        },
        getRequestUrl() {
            const context = this;
            Axios.get(this.url, { params: { isModal: true, id: this.param } })
                .then(e => { 
                    context.content = e.data;
                    this.captureForm();
                });
        }
    }
};

export default defaultComponent;