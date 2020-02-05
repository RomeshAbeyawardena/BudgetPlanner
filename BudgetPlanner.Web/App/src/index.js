import 'babel-polyfill';
import $ from "jquery";
import "bootstrap";
import popup from "./popup";
import asyncLoader from "./async-loader";
import expanderForm from "./expander-form";

require("./scss/index.scss");

$(() => {
    $('[data-toggle="tooltip"]').tooltip();
    const modalPopup = new popup("#popup", "#dialogModeDataHiddenField");
        modalPopup
            .configureMode("modal", "#modalDialog", "#content")
            .init();

    const expander = new expanderForm()
        .init();

    const loader = new asyncLoader("data-src","data-parameters")
        .init()
        .then(() => { modalPopup.init(); expander.init(true); });
});