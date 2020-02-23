import Axios from "axios";
const template = require("./index.html");

const defaultComponent = {
    template: template,
    props: {
        requestUrl: String,
        reference: String,
        fromDate: Date,
        toDate: Date, 
        pageNumber: Number
    },
    data() {
        return {
            currentPageNumber: this.pageNumber,
            from: this.fromDate,
            to: this.toDate,
            items: []
        };
    },
    //watch: {
    //    //fromDate(newValue) {
    //    //    this.from = newValue;
    //    //    this.getTransactions();
    //    //},
    //    //toDate(newValue) {
    //    //    this.to = newValue;
    //    //    this.getTransactions();
    //    //},
    //    //pageNumber(newValue) {
    //    //    this.currentPageNumber = newValue;
    //    //    this.getTransactions();
    //    //}
    //},
    methods: {
        setPageNumber(pageNumber) {
            this.currentPageNumber = pageNumber;
        },
        getPreviousPage() {
            if(this.currentPageNumber - 1 < 0)
                return this.currentPageNumber;

            return this.currentPageNumber -1;
        },
        getNextPage() {
            if(this.currentPageNumber + 1 > this.items.length)
                return this.currentPageNumber;

            return this.currentPageNumber + 1;
        },
        getTransactions() {
            const context = this;
            Axios.get(this.requestUrl, 
                { params: { 
                    reference: this.reference, 
                    fromDate: this.from, 
                    toDate: this.to,
                    pageNumber: this.currentPageNumber }})
                .then((e) => context.items = e.data);
        },
        getTransactionTypeClass(item) {
            if(item.transactionType === 2)
                return "text-number-negate";

            return "";
        }
    },
    created() {
        this.getTransactions();
    }
};

export default defaultComponent;