import $ from 'jquery';

export default function (dymamicPanelSelector) {
    this.dymamicPanelSelector = dymamicPanelSelector;
    this.elements = [];
    this.modes = {};
    this.init = function() {
        this.elements = $("a[popup]");
        for (var element of this.elements) {
            setupElement(element);
        }
    };
    this.configureMode = function (mode, templateSelector, contentPlaceholder) {
        if (this.modes[mode]) 
            throw 'Mode ' + mode + ' already exists';

        this.modes[mode] = { templateSelector: templateSelector, contentPlaceholder: contentPlaceholder };
        return this;
    };
    function setupElement(element) {
        const $element = $(element);
        const mode = $element.attr("popup");
        const href = $element.attr("href");
        const context = this;
        $element.attr("href", "#");
        $element.attr("data-href", href);
        $element.on("click", (e) => {
            var modeData = context.modes[mode];
            if(!modeData)
                throw 'Mode' + mode + ' not found';

            const $dynamicPanel = $(context.dymamicPanelSelector);
            const $template = $(modeData.templateSelector);
            $dynamicPanel.html($template.html());
            console.log(e);
            e.preventDefault();
            return false;
        });
        
    }
}