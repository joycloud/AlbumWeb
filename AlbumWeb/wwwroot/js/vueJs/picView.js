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

    },
    created() {
        var vm = this;
        //取網址參數
        var getUrlString = location.href;
        var url = new URL(getUrlString);
        var albumName = url.searchParams.get('name');
        //呼叫API取照片清單
        if (albumName) {

        }
    },
})