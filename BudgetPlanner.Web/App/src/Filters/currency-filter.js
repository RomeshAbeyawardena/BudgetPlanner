import Vue from "vue";

Vue.filter('currency', (value, locale, currency) => {
    if(!value || isNaN(value))
        return;

    if(!locale)
        locale = "en-GB";

    if(!currency)
        currency = "GBP";

    return Intl.NumberFormat(locale, { style: "currency", currency: currency }).format(value);
});