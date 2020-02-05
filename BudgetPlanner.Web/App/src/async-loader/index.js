import $ from "jquery";
import { httpRequest } from "../utility";
import promise from "promise";

const asyncLoader = function (sourceAttribute, parametersAttribute) {
    this.init = function () {
        const dynamicPanels = $("[" + sourceAttribute + "]");

        var promises = [];

        for (var dynamicPanel of dynamicPanels) {
            const $dynamicPanel = $(dynamicPanel);
            
            var dynamicUrl = $dynamicPanel.attr(sourceAttribute);
            var dynamicParameters = $dynamicPanel.attr(parametersAttribute);
            
            var parameters = JSON.parse(atob(dynamicParameters));
            
            promises.push(new httpRequest(dynamicUrl)
                .get(parameters)
                .then((e) => $dynamicPanel.html(e)));
        }

        return promise.all(promises);
    };
};

export default asyncLoader;