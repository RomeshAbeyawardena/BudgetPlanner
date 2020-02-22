import Axios from "axios";
const template = require("./index.html");

const defaultComponent = {
    props: {
        requestUrl: String,
        detailsUrl: String,
        dashboardMode: Number,
        lastUpdated: Date,
        maximumItems: Number
    },
    data() {
        return {
            items: [],
            emptyContent: "No recent planners available",
            listContent: "Lorem Ipsum"
        };
    },
    template: template,
    methods: {
        getDetailsUrl(item) {
            console.log(item);
            var url = this.detailsUrl.replace("!ref", item.reference);
            console.log(url);
            return url;
        }
    },
    created() {
        const context = this;
        Axios.get(this.requestUrl, {
            params: { lastUpdated: this.lastUpdated }
        })
            .then((e) => { context.items = e.data; });
    }
};

export default defaultComponent;