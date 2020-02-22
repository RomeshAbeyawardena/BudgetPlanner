require("./scss/index.scss");
import $ from "jquery";
import 'babel-polyfill';
import Tagify from '@yaireo/tagify';
import Vue from "vue";
import Components from "./components";
import VueAxios from "vue-axios";
import Axios from "axios";
require("./filters/currency-filter");
$(() => {
    
    const vue = new Vue({
        el: "#app",
        data:  { value: "hello world" },
        components: Components
    });

    Vue.use(VueAxios, Axios);
});
