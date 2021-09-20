new Vue({
    el: '#app',
    //delimiters: ['${', '}'],
    data: {
        title:'Login',
        loginData: {
            account: '',
            password: '',
        },
        remind: {
            account:'Username',
            password: 'Password',
        },
        backPath:'_Layout.html',
    },
    methods: {
        show2: function () {
            this.visible = true;
        }
    },
    created() {
        var vm = this;
        // 如果有Session變更狀態
        // if (Session) {
        //   vm.menu.loginType === '帳號';
        // }
    },
})