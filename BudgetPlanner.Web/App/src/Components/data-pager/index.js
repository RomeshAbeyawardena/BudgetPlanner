const template = require("./index.html");
import Vue from "vue";

const defaultComponent = Vue.component("data-pager", {
    template: template,
    props: { 
        totalPages: Number,
        itemsPerPage: Number,
        pageNumber: Number
    },
    data() {
        return {
            total: this.totalPages,
            pageSize: this.itemsPerPage,
            currentPageNumber: this.pageNumber,
            pageItems: []
        };
    },
    watch: {
        totalPages(newValue) {
            this.total = newValue;
            this.populatePageList();
        },
        itemsPerPage(newValue) {
            this.pageSize = newValue;
            this.$emit("page-size:changed", newValue);
        },
        pageNumber(newValue) {
            this.setPage(newValue);
        }
    },
    methods: {
        setPage(pageIndex) {
            this.currentPageNumber = pageIndex;
            this.$emit("page-number:changed", pageIndex);
        },
        previous() {
            if(this.currentPageNumber - 1 < 0)
                return;

            this.setPage(this.currentPageNumber - 1);
        },
        next() {
            if(this.currentPageNumber + 1 > this.total)
                return;

            this.setPage(this.currentPageNumber + 1);
        },
        showPrevious() {
            return this.currentPageNumber > 1;
        },
        showNext() {
            return this.currentPageNumber < this.total;
        },
        populatePageList() {
            this.pageItems = [];
            for(var index=0; index < this.total; index++)
                this.pageItems.push(index + 1);

        },
        getPageClass(pageIndex) {
            if(this.currentPageNumber === pageIndex)
                return "page-item active";

            return "page-item";
        }
    },
    created() {
        this.populatePageList();
    }
});

export default defaultComponent;