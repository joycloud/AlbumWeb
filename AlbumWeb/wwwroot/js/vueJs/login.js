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
        backPath:'/Home/Index',
    },
    methods: {
   
    },
    created() {
        var vm = this;
    },
})