﻿import 'babel-polyfill';
import $ from "jquery";
import "bootstrap";
import popup from "./popup";
import asyncLoader from "./async-loader";

require("./scss/index.scss");

$(() => {
    const modalPopup = new popup("#popup", "#dialogModeDataHiddenField");
        modalPopup
            .configureMode("modal", "#modalDialog", "#content")
            .init();

    const loader = new asyncLoader("data-src","data-parameters")
        .init().then(() => modalPopup.init());
});