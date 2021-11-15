
/*Vue.use(apiList);*/
Vue.use(apiPlugin);

new Vue({
    el: '#layoutApp',
    //delimiters: ['${', '}'],
    data: {
        menu: {
            logo: '',
            logoPath: '',
            list: [],
            logInType: [],
        },
        session: {

        }
    },
    methods: {
        getMenuData: function () {
            var vm = this;
            var userAccount = document.getElementById("user-account").innerText;
            //¨úMenu
            vm.API_GetAppMenu(userAccount).then(function (response) {
                if (response.data) {
                    vm.menu = response.data;
                }
                //})
                //    .catch(function (error) {
                //    vm.ErrorProcess(error);
            }).finally(function () {
                //vm.showLoading(false);
            });
        },
    },
    created() {
        var vm = this;
        vm.getMenuData();
    },
})