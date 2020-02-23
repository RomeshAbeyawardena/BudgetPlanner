import Vue from "vue";
import Moment from "moment";

Vue.filter('date', (value, format) => {
    if(!value)
        return value;
    
    if(!format)
        format = "ddd Do MMM YYYY";

    var formattedDate = Moment(value).format(format);
    return formattedDate;
});