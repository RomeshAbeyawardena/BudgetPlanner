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
            modal: { visible: false, url:null, parameter:null }
        },
        methods: {
            setModal(url, a) {
                this.modal.url = url;
                this.modal.parameter = a;
                this.modal.visible = true;
            }
        },
        components: Components
    });

    Vue.use(VueAxios, Axios);
});
