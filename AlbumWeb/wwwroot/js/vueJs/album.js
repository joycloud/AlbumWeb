new Vue({
    el: '#app',
    //delimiters: ['${', '}'],
    data: {
        menu: {
            logo: "Jorgy",
            path: '_Layout.html',
            menuList: [{
                name: 'Home',
                path: 'admin.html'
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
        albumList: [{
            name: '果林',
            img_sm: '../Album/20210115_林/smail/DSC04344.jpg',
            path: '',
        }, {
            name: '小八',
            img_sm: '../Album/20210115_XX八/smail/DSC08180.jpg',
            path: '../view/photo.html',
        }],
    },
    methods: {
        show2: function () {
            this.visible = true;
        }
    },
    created() {
        var vm = this;
    },
})