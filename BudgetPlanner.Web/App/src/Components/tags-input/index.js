import Axios from "axios";
import Vue from "vue";
import Tagify from '@yaireo/tagify';
import QueryString from "querystring";

const template = require("./index.html");

const defaultComponent = Vue.component("tags-input", {
    template: template,
    props: {
        inputTextId: String,
        inputTextName: String,
        inputTextValue: String,
        inputTagWhiteListRequestUrl: String,
        inputTagWhiteListSaveRequestUrl: String
    },
    data() {
        return {
            tagifyInstance: null,
            tags: [],
            lastUpdated: null,
            inputId: this.inputTextId,
            inputName: this.inputTextName,
            inputValue: this.inputTextValue,
            controller: null
        };
    },
    watch: {
        inputTextValue(newValue) {
            this.inputValue = newValue;
        }
    },
    methods: {
        tagsUpdated(e) {
            const tagName = e.detail.data.value;

            this.saveTag(tagName);
        },
        getTags(value) {
            return Axios.get(this.inputTagWhiteListRequestUrl, { params: { searchTerm: value } });
        },
        saveTag(tagName, tagId) {
            return Axios.post(this.inputTagWhiteListSaveRequestUrl, QueryString.stringify({ name: tagName, id: tagId }),
                { headers: { "content-type":"application/x-www-form-urlencoded" } })
                .then(e => console.log(e));
        },
        updateWhiteList(whiteList, value) {
            if (!whiteList || !whiteList.length)
                return;

            this.tagifyInstance.settings.whitelist.length = 0;

            for (var whiteListItem of whiteList)
                this.tagifyInstance.settings.whitelist.push(whiteListItem.name);
            this.tagifyInstance.loading(false).dropdown.show.call(this.tagifyInstance, value);
            this.tags = whiteList;
        },
        onTagInput(e) {
            
            var value = e.detail.value;

            this.tagifyInstance.loading(true).dropdown.hide.call(this.tagifyInstance);
            const context = this;

            if(this.controller) 
                this.controller.abort();

            this.controller = new AbortController();

            this.getTags(value)
                .then(e => { 
                    context.updateWhiteList(e.data.result, value); 
                    context.lastUpdated = e.data.requested; 
                });

            
        }
    },
    mounted() {
        this.tagifyInstance = new Tagify(this.$el)
            .on('add', this.tagsUpdated)
            .on('input', this.onTagInput);
    }
});

export default defaultComponent;