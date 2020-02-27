require("./scss/index.scss");
import $ from "jquery";
import 'babel-polyfill';
import Vue from "vue";
import Components from "./components";
import VueAxios from "vue-axios";
import Axios from "axios";
require("./filters");

$(() => {
    $("a[href]").attr("href", "javascript:void(0)");
    const vue = new Vue({
        el: "#app",
        data: {
            value: "hello world", 
            modal: { 
                title: null,
                visible: false, 
                url: null, 
                parameter:null,
                entityType:null
            },
            notification: {
                visible: false,
                type: "success",
                content: ""
            },
            lastSavedValue: null
        },
        methods: {
            dismissModal() {
                this.modal.visible = false;
            },
            dismissNotification() {
                this.notification.visible = false;
            },
            setNotification(type, content, visible = true) {
                this.notification.type = type;
                this.notification.content = content;
                this.notification.visible = visible;
            },
            setModal(entityType, url, title, a) {
                this.modal.title = title;
                this.modal.url = url;
                this.modal.parameter = a;
                this.modal.visible = true;
                this.modal.entityType = entityType;
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
