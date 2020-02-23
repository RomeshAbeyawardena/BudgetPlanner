import Vue from "vue";
import Tagify from '@yaireo/tagify';

const template = require("./index.html");

const defaultComponent = Vue.component("tags-input", {
    template: template,
    props: {
        inputTextId: String,
        inputTextName: String,
        inputTextValue: String
    },
    data() {
        return {
            inputId: this.inputTextId,
            inputName: this.inputTextName,
            inputValue: this.inputTextValue
        };
    },
    mounted() {
        console.log(this);
        new Tagify(this.$el);
    }
});

export default defaultComponent;