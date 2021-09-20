
Vue.use(apiPlugin);

new Vue({
    el: '#app',
    //delimiters: ['${', '}'],
    systemId: 1,
    data: {
        menu: {
            logo: "Jorgy",
            path: '_Layout.html',
            List: [{
                name: 'Album',
                path: 'album.html'
            }, {
                name: 'Create',
                path: 'admin.html',
            }, {
                name: 'Update',
                path: 'admin.html'
            }]
        },
        logIn: {
            name: 'LOGIN',
            path: 'login.html',
            loginType: "",
        },
        visible: false,
    },
    methods: {
        getData: function () {
            var vm = this;
            //vm.showLoading(true);
            vm.API_GetAppMenu(vm.systemId).then(function (response) {
                if (response.data) {
                    vm.pageData = response.data;
                }
            })
            //    .catch(function (error) {
            //    vm.ErrorProcess(error);
            //}).finally(function () {
            //    vm.showLoading(false);
            //});
        },

        //getData2 = () => {

        //}

        // getNewData: function () {
        //    var vm = this;
        //    vm.isShowLoading = true;
        //    var promise = vm.API_GetInitialForm('WLIAC', Vue.prototype.$UserEmpId)
        //        .then(function (response) {
        //            vm.formModel = response.data;
        //            if (vm.formModel.isReadForm) {
        //                if (response.data.formMaster.applicantEmpId) {
        //                    vm.formModel.formMaster.applicantEmpId = response.data.formMaster.applicantEmpId.toString();
        //                }
        //                vm.formDataModel = vm.GetModelFromFormDataList(vm.formModel.formDataValue, Vue.prototype.$IsPrintPage)
        //                //設定表單參數
        //                vm.formInfoParameters = vm.GetModelFromFormInfoParameters(vm.formModel.formInfoParameters);
        //            }
        //            else {
        //                vm.showError(vm.formModel.readMessage);
        //            }
        //        })
        //        .catch(function (error) {
        //            vm.ErrorProcess(error);
        //        }).finally(function () {
        //            vm.isShowLoading = false;
        //        })
        //    return promise;
        //},

    },
    created() {
        var vm = this;
        vm.getData();
        // 如果有Session變更狀態
        // if (Session) {
        //   vm.menu.loginType === '帳號';
        // }
    },
})