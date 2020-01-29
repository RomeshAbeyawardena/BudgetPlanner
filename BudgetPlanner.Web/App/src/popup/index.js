import $ from 'jquery';

export default function () {
    this.elements = [];
    this.init = function() {
        this.elements = $("a[popup]");
        for (var element of this.elements) {
            console.log($(element).attr("popup"));
        }
    };
}