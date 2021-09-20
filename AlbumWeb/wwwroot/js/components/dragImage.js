Vue.component('dragImage', {
    //mixins: [showLoadingMixin],
    props: {
        formModel: null,
    },
    data: function () {
        return {
            isShow: true,
            showMsgData: [],
        }
    },   
    created: function () {
        var vm = this;
    },
    template:
        `
 <div class="form-group">
        <div class="dropbox">
            <input class="drag-file" type="file" ref="image" :name="name" :disabled="isSaving" @change="fileChange" accept="image/*">
            <p v-if="isInitial">
                拖放图片开始上传<br>或者点击选择图片上传
            </p>
            <p v-if="isSaving">
                图片上传中...
            </p>
        </div>
        <div v-if="isSuccess">
            <div class="alert alert-success" role="alert">
                图片上传成功！<a href="javascript:void(0)" @click="reset()">重新上传</a>
            </div>
        </div>
        <div v-if="isFailed">
            <div class="alert alert-danger" role="alert">
                图片上传失败！<a href="javascript:void(0)" @click="reset()">重新上传</a>
            </div>
        </div>
        <InputText type="hidden" :name="field" v-model="filePath"></InputText>
        <img :src="filePath" class="img-responsive img-thumbnail" style="width: 50%;" v-if="filePath">
    </div>`
});