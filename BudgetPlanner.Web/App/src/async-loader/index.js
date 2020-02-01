import $ from "jquery";
import { httpRequest } from "../utility";

const asyncLoader = function (sourceAttribute, parametersAttribute) {
    this.init = function () {
        const dynamicPanels = $("[" + sourceAttribute + "]");

        for (var dynamicPanel of dynamicPanels) {
            const $dynamicPanel = $(dynamicPanel);
            
            var dynamicUrl = $dynamicPanel.attr(sourceAttribute);
            var dynamicParameters = $dynamicPanel.attr(parametersAttribute);
            
            var parameters = JSON.parse(atob(dynamicParameters));
            
            new httpRequest(dynamicUrl)
                .get(parameters)
                .then((e) => $dynamicPanel.html(e));
        }
    };
};

export default asyncLoader;