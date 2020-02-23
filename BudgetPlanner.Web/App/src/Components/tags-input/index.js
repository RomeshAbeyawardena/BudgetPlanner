import Axios from "axios";
import Vue from "vue";
import Tagify from '@yaireo/tagify';

const template = require("./index.html");

const defaultComponent = Vue.component("tags-input", {
    template: template,
    props: {
        inputTextId: String,
        inputTextName: String,
        inputTextValue: String,
        inputTagWhiteListRequestUrl: String
    },
    data() {
        return {
            tagifyInstance: null,
            tags: [],
            lastUpdated: null,
            inputId: this.inputTextId,
            inputName: this.inputTextName,
            inputValue: this.inputTextValue
        };
    },
    watch: {
        inputTextValue(newValue) {
            this.inputValue = newValue;
        }
    },
    methods: {
        tagsUpdated(e, tagName) {
            console.log(e);
            console.log(tagName);
        },
        getTags(value) {
            return Axios.get(this.inputTagWhiteListRequestUrl, { params: { searchTerm: value } });
        },
        updateWhiteList(whiteList) {
            if(!whiteList || !whiteList.length)
                return;

            this.tagifyInstance.settings.whitelist.length = 0;

            for(var whiteListItem of whiteList)
                this.tagifyInstance.settings.whitelist.push(whiteListItem);

            this.tags = whiteList;
        },
        onTagInput(e) {
            var value = e.detail.value;
            
            this.tagifyInstance.loading(true).dropdown.hide.call(tagify);
            const context = this;

            if(!this.tags.length || !lastUpdated || lastUpdated > )
                this.getTags(value)
                    .then(e => { context.updateWhiteList(e.data); lastUpdated = new Date(); });
            
            tagify.loading(false).dropdown.show.call(tagify, value);
        }
    },
    mounted() {
        this.tagifyInstance = new Tagify(this.$el)
            .on('add', this.tagsUpdated);
    }
});

export default defaultComponent;