import 'babel-polyfill';
import $ from "jquery";
import "bootstrap";
import popup from "./popup";
import asyncLoader from "./async-loader";
import expanderForm from "./expander-form";
import { httpRequest } from "./utility";


require("./scss/index.scss");

$(() => {
    $('[data-toggle="tooltip"]').tooltip();

    const expander = new expanderForm()
        .init();

    const modalPopup = new popup("#popup", "#dialogModeDataHiddenField");
        modalPopup
            .configureMode("modal", "#modalDialog", "#content")
            .init()
            .then(() => { 
                const $estimatedCostPanel = $("#estimatedCost"); 
                const $costDetailsPanel = $("#costDetails");

                const sourceUrl = $estimatedCostPanel.data("src");
                const dataParams = $estimatedCostPanel.data("parameters");

                const getBalancehttpService = new httpRequest(sourceUrl);

                var decodedParams = atob(dataParams);

                var params = JSON.parse(decodedParams);

                if(!$estimatedCostPanel)
                    return;

                if(!$costDetailsPanel)
                    return;

                $costDetailsPanel.find("input[type='number']")
                    .keyup((e) => { 
                        var amount = $(e.target).val();

                        if(!amount)
                            return;

                        params["amount"] = amount;

                        params["transactionTypeId"] = $("#transactionDropDown").val();
                        getBalancehttpService.get(params)
                            .then((e) => { $estimatedCostPanel.removeClass("d-none"); $estimatedCostPanel.html(e); }); 
                    });
            });

    const loader = new asyncLoader("data-src","data-parameters")
        .init()
        .then(() => {
            modalPopup.init();
            expander.init(true);
        });
});