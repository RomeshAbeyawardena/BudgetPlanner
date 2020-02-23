import Axios from "axios";
import Components from "../.././components";
const template = require("./index.html");

console.log(Components);

const defaultComponent = {
    template: template,
    components: Components,
    props: {
        requestUrl: String,
        reference: String,
        fromDate: Date,
        toDate: Date, 
        itemsPerPage: Number,
        pageNumber: Number
    },
    data() {
        return {
            pageSize: this.itemsPerPage,
            currentPageNumber: this.pageNumber,
            from: this.fromDate,
            to: this.toDate,
            items: []
        };
    },
    watch: {
        fromDate(newValue) {
            this.from = newValue;
            this.getTransactions();
        },
        toDate(newValue) {
            this.to = newValue;
            this.getTransactions();
        },
        pageSize(newValue) {
            this.pageSize = newValue;
            this.getTransactions();
        },
        pageNumber(newValue) {
            this.currentPageNumber = newValue;
            this.getTransactions();
        }
    },
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
                    pageSize: this.pageSize,
                    pageNumber: this.currentPageNumber }})
                .then((e) => context.items = e.data);
        },
        getLedger(item) {
            if(!item)
                return;

            if(!item.transactionLedgers.length)
                return;
            
            return item.transactionLedgers[0];
        },
        getTransactionTypeClass(item) {
            if(item.type === 2)
                return "text-number-negate";

            return "";
        }
    },
    created() {
        this.getTransactions();
    }
};

export default defaultComponent;