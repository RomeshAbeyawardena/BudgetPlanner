import Axios from "axios";
const template = require("./index.html");

const defaultComponent = {
    template: template,
    props: {
        requestUrl: String,
        reference: String,
        fromDate: Date,
        toDate: Date
    },
    data() {
        return {
            from: this.fromDate,
            to: this.toDate,
            items: []
        };
    },
    watch: {
        fromDate(newValue) {
            this.from = newValue;
            getTransactions();
        },
        toDate(newValue) {
            this.to = newValue;
            getTransactions();
        }
    },
    methods: {
        getTransactions() {
            constcontext = this;
            return Axios.get(this.requestUrl, { params: { reference: this.reference, fromDate: this.from, toDate: this.to }}).then((e) => context,items);
        }
    },
    created() {
        this.getTransactions();
    }
};

export default defaultComponent;