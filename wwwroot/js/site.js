(function (root, component) {

    root.CompanyList = component();

})(window, function () {

    var _apiUrl = '/api/companies';

    var app = new Vue({

        template: '#app-list-template',

        data: {
            items: [],
            newItem: ''
        },

        mounted() {
            axios
                .get(_apiUrl)
                .then(response => {
                    this.items = response.data;
                });
        },

        computed: {
            total: function () {
                return this.items.length;
            }
        },

        methods: {
            addItem: function () {
                var value = this.newItem && this.newItem.trim()
                if (!value)
                    return;

                axios
                    .post(_apiUrl, null, { params: { name: value }})
                    .then(response => {
                        this.newItem = '';
                        this.items.push(response.data);
                    })
                    .catch((error, d) => {
                        console.log('error:');
                        console.log(error.response);
                    });
            },

            removeItem: function (item) {
                axios
                    .delete(_apiUrl, { params: { id: item.id }})
                    .then(response => {
                        this.items.splice(this.items.indexOf(item), 1);
                    })
                    .catch(error => {
                        console.log('error:');
                        console.log(error);
                    });
            }
        }
    });

    Vue.component('app-item', {
        props: ['item'],
        template: '#app-item-template'
    });

    return {
        render: function (el) {
            app.$mount(el);
        }
    }
});