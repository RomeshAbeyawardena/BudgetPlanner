const template = require("./index.html");
import Axios from "axios";

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
            this.getBudgetStatistics();
        },
        toDate(newValue) {
            this.to = newValue;
            this.getBudgetStatistics();
        }
    },
    methods: {
        getBudgetStatistics() {
            Axios.get(this.requestUrl, { params: { 
                reference: this.reference, 
                fromDate: this.from, 
                toDate: this.to }
            }).then(e => this.items = e.data.statistics);
        }
    },
    created() {
        this.getBudgetStatistics();
    }
};

export default defaultComponent;