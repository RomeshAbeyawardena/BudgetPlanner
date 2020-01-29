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
    };
    function setupElement(element) {
        const $element = $(element);
        const mode = $element.attr("popup");
        $element.on("click", () => {
            var modeData = this.modes[mode];
            if(!modeData)
                throw 'Mode' + mode + ' not found';

            const $dynamicPanel = $(this.dymamicPanelSelector);
            const $template = $(modeData.templateSelector);

            $dynamicPanel.html($template.html());
        });
        
    }
}