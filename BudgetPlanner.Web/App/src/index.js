﻿import 'babel-polyfill';
import $ from "jquery";
import "bootstrap";
import popup from "./popup";
require("./scss/index.scss");

$(() => {
    var modalPopup = new popup("#popup", "#dialogModeDataHiddenField")
        .configureMode("modal", "#modalDialog", "#content")
        .init();
    
});