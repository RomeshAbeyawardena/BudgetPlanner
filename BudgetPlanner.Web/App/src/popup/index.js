import $ from 'jquery';
import { httpRequest, form } from "../utility";

export default function (dymamicPanelSelector, modesHiddenFieldSelector) {
    this.dymamicPanelSelector = dymamicPanelSelector;
    this.modesHiddenFieldSelector = modesHiddenFieldSelector;
    this.elements = [];
    this.modes = {};
    this.init = function () {
        this.elements = $("a[popup]");
        var promises = [];
        console.log(promises);
        for (var element of this.elements) {
            console.log(promises);
            promises.push(setupElement(this, element));
        }

        return Promise.all(promises);
    };
    this.configureMode = function (mode, templateSelector, contentPlaceholder) {
        if (this.modes[mode])
            throw 'Mode ' + mode + ' already exists';

        this.modes[mode] = { templateSelector: templateSelector, contentPlaceholder: contentPlaceholder };
        return this;
    };
    this.getMode = function (mode) {
        const modeData = this.modes[mode];
        if (!modeData)
            throw 'Mode' + mode + ' not found';
        return modeData;
    };
    function setupElement(context, element) {
        const $element = $(element);
        const mode = $element.attr("popup");
        const href = $element.attr("href");
        
        if($element.attr("data-setup"))
            return;

        $element.attr("data-setup", "true");
        $element.attr("href", "#");
        $element.attr("data-href", href);

        $element.on("click", (e) => {
            const $element = $(e.target);
            const href = $element.attr("data-href");
            var args = $element.attr("data-args");
            const modeData = context.getMode(mode);
            const $dynamicPanel = $(context.dymamicPanelSelector);
            const $template = $(modeData.templateSelector);
            
            $element.text("Saving...");
            $element.attr("disabled", "disabled");

            $dynamicPanel.html($template.html());
            
            const request = new httpRequest(href);

             if(args)
                 args = JSON.parse(args);

            return request.get(args).then((e) => {
                const contentPlaceholder = $dynamicPanel.find(modeData.contentPlaceholder);
                contentPlaceholder.html(e);
                $dynamicPanel.find(".modal").show();
                const defaultForm = new form(contentPlaceholder, "form", "DismissModals");
                defaultForm.capture(0, true);
            });
        });

    }
}