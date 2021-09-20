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
        photoList: [{
            name: 'DSC08062',
            img_sm: '../Album/20210115_XX八/smail/DSC08062.jpg',
            img_big: '../Album/20210115_XX八/big/DSC08062.jpg',
        }, {
            name: 'DSC08063',
            img_sm: '../Album/20210115_XX八/smail/DSC08063.jpg',
            img_big: '../Album/20210115_XX八/big/DSC08063.jpg',
        }, {
            name: 'DSC08064',
            img_sm: '../Album/20210115_XX八/smail/DSC08064.jpg',
            img_big: '../Album/20210115_XX八/big/DSC08064.jpg',
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
