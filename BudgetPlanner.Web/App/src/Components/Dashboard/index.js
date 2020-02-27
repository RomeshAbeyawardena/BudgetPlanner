import Axios from "axios";
const template = require("./index.html");

const defaultComponent = {
    props: {
        createRequestUrl: String,
        editRequestUrl: String,
        requestUrl: String,
        detailsUrl: String,
        dashboardMode: Number,
        lastUpdated: Date,
        maximumItems: Number,
        maxmimumItemsPerRow: Number
    },
    data() {
        return {
            items: [],
            maxItemsPerRow: this.maxmimumItemsPerRow,
            emptyContent: "No recent planners available",
            listContent: "Lorem Ipsum"
        };
    },
    template: template,
    methods: {
        getColumnClass(items) {
            if (!this.maxItemsPerRow)
                this.maxItemsPerRow = 3;

            if (items.length >= this.maxItemsPerRow)
                return "col-xs-12 col-sm-6 col-4";

            return "col";
        },
        getDetailsUrl(item) {
            var url = this.detailsUrl
                .replace("!ref", item.reference);

            return url;
        },
        add() {
            this.$emit("item:save", this.createRequestUrl);
        },
        edit() {
            this.$emit("item:save", this.editRequestUrl);
        }
    },
    created() {
        const context = this;
        Axios.get(this.requestUrl, {
            params: { lastUpdated: this.lastUpdated }
        }).then((e) => { context.items = e.data; });
    }
};

export default defaultComponent;