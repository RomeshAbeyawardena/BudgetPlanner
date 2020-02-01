import $ from "jquery";
import { httpRequest } from "../utility";

const asyncLoader = function () {
    this.init = function () {
        const dynamicPanels = $("[data-href]");

        console.log(dynamicPanels);
        for (var dynamicPanel of dynamicPanels) {
            const $dynamicPanel = $(dynamicPanel);
            console.log($dynamicPanel);
            var dynamicUrl = $dynamicPanel.attr("data-href");
            var dynamicParameters = $dynamicPanel.attr("data-parameters");
            console.log(dynamicUrl);
            console.log(dynamicParameters);

            var parameters = JSON.parse(atob(dynamicParameters));
            console.log(parameters);
            new httpRequest(dynamicUrl)
                .get(parameters)
                .then((e) => $dynamicPanel.html(e));
        }
    };
};

export default asyncLoader;