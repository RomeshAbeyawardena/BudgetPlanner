import $ from "jquery";
import promise from "promise";

const hR = function (url, settings) {
    this.settings = settings;
    this.get = function(data) {
        return new promise((resolve, reject) => $.get(url, data)
            .done((e) => resolve(e))
            .fail((e) => reject(e)));
    };
    this.post = function (data) {
        return new promise((resolve, reject) => $.post(url, data).done((e) => resolve(e))
            .fail((e) => reject(e)));
    },
    this.createSettings = function (method, data, async = true, useCache = false, processData = false) {
        return {
            async: async,
            cache: useCache,
            data: data,
            method: method,
            processData: processData,
            contentType: "application/x-www-form-urlencoded"
        };
    };
};

export const httpRequest = hR;

export const form = function (rootElement, formSelector) {
    this.forms = $(rootElement).find(formSelector);
    this.capture = function (formIndex, capture) {
        const form = this.forms[formIndex];
        const forms = this.forms;
        if(capture)
            $(rootElement)
                .find("[type='submit']")
                .on("click", (e) => {
                    const request1 = new httpRequest(form.action);
                    const formData = new FormData(form);
                    
                    request1.post($(forms).serialize())
                        .then((e) => console.log(e));
                    
                    e.preventDefault();
                });
    };
};