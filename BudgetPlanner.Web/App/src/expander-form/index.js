import $ from "jquery";

const expanderForm = function () {
    this.expanderParents = {};
    this.init = function (flushParents) {

        if (flushParents)
            this.flushParents();

        var expanderForms = $("[expander-form]");
        const context = this;
        for (var expanderForm of expanderForms) {
            const expanderParent = $(expanderForm).data("expander-parent");
            const $expanderParent = $(expanderParent);
            const expanderParentId = $expanderParent.attr("id");
            const $expanderForm = $(expanderForm);
            var $expanderFormInput = $expanderForm.find("input");

            $expanderFormInput.on("change", () => {
                var form = context.getExpander(expanderParent);
                if ($expanderFormInput.val().length > 2)
                    context.expandChildren(form);
            });

            context.addExpander(expanderParentId, $expanderForm, []);

            if (expanderParent === 'parent')  //top-level don't hide    
                continue;

            //find approriate parent
            var expander = this.getExpander(expanderParent);

            expander.children.push(expanderForm);
            $expanderForm.hide();
        }

        return context;
    };
    this.flushParents = function () {
        this.expanderParents = {};
    };
    this.addExpander = function (expanderId, expanderParentId, element, children) {
        this.expanderParents[expanderParentId] = { id: expanderId, element: element, children: children };
    };
    this.getExpander = function (value) {
        const expander = this.expanderParents[value];

        if (!expander)
            throw 'Expander "' + expanderParent + '" not found';

        return expander;
    },
        this.expandChildren = function (expanderParent) {
            for (var child in expanderParent.children) {
                $(child).show();
            }
        };
};

export default expanderForm;