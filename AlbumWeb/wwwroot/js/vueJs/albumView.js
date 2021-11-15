Vue.use(apiPlugin);

new Vue({
    el: '#app',
    //delimiters: ['${', '}'],
    data: {
        albumList: [{
            name: '20210925_XXC',
            path: '/album/20210925_XXC/smail/DSC00907.jpg',
            href: '/Home/PicView?name=20210925_XXC'
        }, {
            name: '20210925_AAC',
            path: '/album/20210925_AAC/smail/DSC01353.jpg',
            href: '/Home/PicView?name=DSC01353'
            }],
    },
    methods: {
        //取相簿清單
        getAlbumList: function () {
            var vm = this;
            //取得Session
            vm.API_GetSession().then(function (response) {
                if (response.data) {
                    vm.userInfo = response.data;
                }
                //})
                //    .catch(function (error) {
                //    vm.ErrorProcess(error);
            }).finally(function () {
                //vm.showLoading(false);
            });

            //取Menu
            vm.API_GetAlbumList(userAccount).then(function (response) {
                if (response.data) {
                    vm.albumList = response.data;
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
        //取相簿清單
        vm.getAlbumList();
    },
})