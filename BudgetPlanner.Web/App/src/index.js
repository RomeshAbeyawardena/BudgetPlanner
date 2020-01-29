import 'babel-polyfill';
import $ from "jquery";
import "bootstrap";
import popup from "./popup";
require("./scss/index.scss");

$(() => {
    var popup1 = new popup();
    popup1.init();
    
});