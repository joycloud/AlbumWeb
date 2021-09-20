
/*Vue.use(apiPlugin);*/
Vue.use(apiPlugin);
new Vue({
    el: '#app',
    //delimiters: ['${', '}'],
    data: {
        formData: {
            name: '',
            path: '',
            remark: '',
        },
    },
    methods: {
        getIndexData: function () {
            var vm = this;
            vm.API_GetIndexData().then(function (response) {
                if (response.data) {
                    vm.formData = response.data;
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

        vm.getIndexData();
        ////刪除泡泡遮罩
        //$().ready(function () {
        //    $('#bubbles-back').remove();
        //});
    },
})