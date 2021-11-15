
//API清單
var apiPlugin = {
    install: function (Vue, options) {
        //取Menu資料
        Vue.prototype.API_GetAppMenu = function (userAccount) {
            var uri = "/api/Common/GetAppMenuList";
            return axios.get(uri, { params: { 'systemName': 'AlbumWeb', 'userAccount': userAccount } });
        };
        //取Index資料
        Vue.prototype.API_GetIndexData = function () {
            var uri = "/api/Common/GetIndexData";
            return axios.get(uri, { params: { 'systemName': 'AlbumWeb' } });
        }
        //取Session資料
        Vue.prototype.API_GetSession = function () {
            var uri = "/api/Home/GetSession";
            return axios.get(uri);
        }
        //取相簿清單資料
        Vue.prototype.API_GetAlbumList = function (userAccount) {
            var uri = "/api/Common/GetAlbumList";
            return axios.get(uri, { params: {'userAccount': userAccount } });
        }
    }
}




