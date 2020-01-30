﻿import $ from 'jquery';
import { httpRequest } from "../utility";

export default function (dymamicPanelSelector, modesHiddenFieldSelector) {
    this.dymamicPanelSelector = dymamicPanelSelector;
    this.modesHiddenFieldSelector = modesHiddenFieldSelector;
    this.elements = [];
    this.modes = {};
    this.init = function () {
        this.elements = $("a[popup]");
        for (var element of this.elements) {
            setupElement(this, element);
        }
    };
    this.configureMode = function (mode, templateSelector, contentPlaceholder) {
        if (this.modes[mode])
            throw 'Mode ' + mode + ' already exists';

        this.modes[mode] = { templateSelector: templateSelector, contentPlaceholder: contentPlaceholder };
        return this;
    };
    this.getMode = function (mode) {
        var modeData = this.modes[mode];
        if (!modeData)
            throw 'Mode' + mode + ' not found';
        return modeData;
    };
    function setupElement(context, element) {
        const $element = $(element);
        const mode = $element.attr("popup");
        const href = $element.attr("href");

        $element.attr("href", "#");
        $element.attr("data-href", href);

        $element.on("click", (e) => {
            const $element = $(e.target);
            const href = $element.attr("data-href");
            const args = $element.attr("data-args");
            const modeData = context.getMode(mode);
            const $dynamicPanel = $(context.dymamicPanelSelector);
            const $template = $(modeData.templateSelector);
            
            $dynamicPanel.html($template.html());
            
            var request = new httpRequest(href);
            request.get(JSON.parse(args)).then((e) => {
                var contentPlaceholder = $dynamicPanel.find(modeData.contentPlaceholder);
                contentPlaceholder.html(e);
                $dynamicPanel.find(".modal").show();
            });
        });

    }
}