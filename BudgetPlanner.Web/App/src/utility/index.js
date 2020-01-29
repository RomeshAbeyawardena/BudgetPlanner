import $ from "jquery";
import promise from "promise";

export const httpRequest = function (url, settings) {
    this.settings = settings;
    this.get = function(data) {
        return new promise((resolve, reject) => { 
            $.ajax(url, this.createSettings("GET", data))
                .then((e) => resolve(e), (e) => reject(e));
        });
    };
    this.createSettings = function (method, data, async = true, useCache = false, processData = true) {
        return {
            async: async,
            cache: useCache,
            data: data,
            method: method,
            processData: processData
        };
    };
}