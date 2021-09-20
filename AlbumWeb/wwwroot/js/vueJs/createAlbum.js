
const STATUS_INITIAL = 0, STATUS_SAVING = 1, STATUS_SUCCESS = 2, STATUS_FAILED = 3;

new Vue({
    el: '#app',
    props: ['name', 'field', 'path'],
    components: { InputText },
    data() {
        return {
            currentStatus: null,
            filePath: this.path
        }
    },
    computed: {
        isInitial() {
            return this.currentStatus === STATUS_INITIAL;
        },
        isSaving() {
            return this.currentStatus === STATUS_SAVING;
        },
        isSuccess() {
            return this.currentStatus === STATUS_SUCCESS;
        },
        isFailed() {
            return this.currentStatus === STATUS_FAILED;
        }
    },
    mounted() {
        this.reset();
    },
    methods: {
        reset() {
            // 重置状态字段值
            this.currentStatus = STATUS_INITIAL;
            this.$emit('clear');  //  清理父级作用域报错信息
        },
        fileChange() {
            // 上传文件后才会执行后续步骤
            if (this.$refs.image.files.length === 0) {
                return;
            }

            // 清理父级作用域错误信息
            this.$emit('clear');

            // 填充表单数据
            const formData = new FormData();
            formData.append(this.name, this.$refs.image.files[0]);

            // 开始上传
            this.currentStatus = STATUS_SAVING;
            axios.post('/image/upload', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            }).then(resp => {
                this.currentStatus = STATUS_SUCCESS;
                this.filePath = resp.data.path;
                this.$emit('success', this.field, this.filePath);
            }).catch(error => {
                this.currentStatus = STATUS_FAILED;
                let errors = {};
                errors[this.field] = error.response.data.errors[this.name];
                this.$emit('error', errors);
            });
        }
    }
});