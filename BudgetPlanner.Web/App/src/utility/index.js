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
        return new promise((resolve, reject) => $.post(url, data).done((e, e1, e2) => { resolve({data: e, response: e2}); })
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
    this.capture = function (formIndex, capture) {
        
        if(capture)
            $(rootElement)
                .find("[type='submit']")
                .on("click", (e) => {
                    const forms = $(rootElement).find(formSelector);
                    const form = forms[formIndex];
                    const request = new httpRequest(form.action);

                    request.post($(forms).serialize())
                        .then((e) => { 
                            if(!e.response.getResponseHeader("DismissModals"))
                                $(rootElement).html(e.data);
                            else
                                window.location.reload();
                            //recapture form.
                            
                            this.capture(formIndex, capture);
                        });
                    
                    e.preventDefault();
                });
    };
};