const template = require("./index.html");

const defaultComponent = {
    template: template,
    props: {
        notificationContent: String,
        notificationType: String,
        notificationVisible: Boolean
    },
    data() {
        return {
            content: this.notificationContent,
            notificationClass: this.getClassByType(this.notificationType),
            visible: this.notificationVisible
        };
    },
    watch: {
        notificationType(newValue) {
            this.class = this.getClassByType(newValue);
        },
        notificationContent(newValue) {
            this.content = newValue;
        },
        notificationVisible(newValue) {
            this.visible = newValue;
        }
    },
    methods: {
        getClassByType(type) {
            if(!type)
                return;

            if(type === 'primary' 
                || type === 'secondary' 
                || type === 'success'
                || type === 'danger'
                || type === 'warning'
                || type === 'info'
                || type === 'light'
                || type === 'dark')
                this.notificationClass = 'alert alert-' + type;
        }
    }
};

export default defaultComponent;