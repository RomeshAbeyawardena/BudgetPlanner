import Axios from "axios"; 
const template = require("./index.html");

const defaultComponent = {
    props: { requestUrl: String, dashboardMode: Number, lastUpdated: Date, maximumItems: Number },
    data() {
        return { items: [], emptyContent: "", listContent: "" };
    },
    template: template,
    created() {
        const context = this;
        Axios.get(this.requestUrl, { params: { lastUpdated: this.lastUpdated } }).then((e) => { console.log(e); context.items = e.data; });
    }
};

export default defaultComponent;