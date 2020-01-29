import $ from 'jquery';
import promise from 'promise';

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
        var modeData = context.modes[mode];
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
            const modeData = context.getMode(mode);
            const $dynamicPanel = $(context.dymamicPanelSelector);
            const $template = $(modeData.templateSelector);

            $template.find(modeData.contentPlaceholder).html();
            $dynamicPanel.html($template.html());
        });

    }
}