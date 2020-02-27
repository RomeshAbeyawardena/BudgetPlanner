require("./scss/index.scss");
import $ from "jquery";
import 'babel-polyfill';
import Vue from "vue";
import Components from "./components";
import VueAxios from "vue-axios";
import Axios from "axios";
require("./filters");
$(() => {
    
    const vue = new Vue({
        el: "#app",
        data: {
            value: "hello world", 
            modal: { 
                title: null,
                visible: false, 
                url: null, 
                parameter:null 
            },
            lastSavedValue: null
        },
        methods: {
            dismissModal() {
                this.modal.visible = false;
            },
            setModal(url, title, a) {
                this.modal.title = title;
                this.modal.url = url;
                this.modal.parameter = a;
                this.modal.visible = true;
            },
            onModalSubmit(e) {
                this.lastSavedValue = e;
                this.dismissModal();
            }
        },
        components: Components
    });

    Vue.use(VueAxios, Axios);
});
