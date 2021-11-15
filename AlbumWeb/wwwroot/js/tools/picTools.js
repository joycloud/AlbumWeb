var fd = new FormData();
var array = [];


//#region 照片拖拉並顯示
function dragoverHandler(evt) {
    evt.preventDefault();
}
function dropHandler(evt) {//evt 為 DragEvent 物件
    evt.preventDefault();
    let files = evt.dataTransfer.files;//由DataTransfer物件的files屬性取得檔案物件

    for (let i in files) {
        if (files[i].type == 'image/png' || files[i].type == 'image/jpeg') {

            //判斷重複
            var checkPic = array.some(function (item) {
                return item.name == files[i].name
            });
            //沒有重複才匯入
            if (!checkPic) {
                let pics = {
                    name: files[i].name,
                    files: files[i]
                }
                array.push(pics);
                //將圖片在頁面預覽
                let fr = new FileReader();
                fr.onload = openfile(files[i]);
                fr.readAsDataURL;
                //新增上傳檔案，上傳後名稱為圖檔的陣列
                fd.append(files[i].name, files[i]);
            }
        }
    }
}

async function openfile(tr) {
    // await優先跑這段
    let img = await toBase64(tr);//將圖片轉成base64
    let imgx = document.createElement('img');
    imgx.style.margin = "10px";
    imgx.src = img;
    imgx.className = "imgs";
    let listname = tr.name.replace('.', '_');
    imgx.id = listname;

    let photo = $(document.createElement('div')).attr({
        "class": 'photo-list',
        "id": 'list-' + listname,
        "name": tr.name
    });

    $('#imgDIV').append(photo);
    photo.append('<div class="pic-close" id="close-' + listname + '"></div>');
    photo.append(imgx);
    photo.append('<div id="progress-' + tr.name + '">0%</div>');
}

// 抓file的內容
const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});
//#endregion 

//#region 上傳至後端
async function Upload() {
    if (array.length < 1) {
        swal("沒有上傳的照片!", "", "error");
    }
    else {
        // async/await 需搭配return使用，有回傳才往下跑
        let albumResult = await CreateAlbumName($('#albumName').val());

        //相簿建立成功
        if (albumResult.isCheck) {
            for (let pic of array) {
                let progress = 'progress-' + pic.name;
                let f = document.getElementById(progress);
                let listname = 'list-' + pic.name.replace('.', '_');
                let loading = "loading-" + pic.name.replace('.', '_');

                //設定loading
                var node = document.createElement("div");
                node.className = "loading";
                node.id = loading;
                document.getElementById(listname).appendChild(node);

                var picResult = await CreatePicture($('#albumName').val(), pic, albumResult.id);

                //上傳成功
                if (picResult.isCheck) {
                    f.innerHTML = 'OK';
                    document.getElementById(listname).children[1].style.opacity = 1
                    //移除loading
                    document.getElementById(loading).remove();
                }
            }
            //sweet alert
            swal("上傳成功!", "相簿：" + $('#albumName').val() + "已上傳!", "success")
                .then(() => {
                    //導至相簿瀏覽頁
                    location.href = location.protocol + "//" + location.host + "/Home/CreateAlbum";
                });
        }
        //相簿建立失敗
        else {
            swal("相簿建立失敗!", albumResult.msg, "error");
        }
    }
}

let CreateAlbumName = (albumName) => {
    let msg = '';
    $.ajax({
        type: "POST",
        // 預設true，是同步執行狀態，false非同步執行完成才往下跑
        async: false,
        url: "/Home/CreateAlbumApi?albumName=" + albumName,
        processData: false,
        contentType: false,
        success: function (result) {
            msg = result;
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
            msg = 'fales error!!';
        }
    });
    return msg;
};

let CreatePicture = (albumName, pic, id) => {
    let msg = '';
    let fdd = new FormData();
    fdd.append(pic.name, pic.files);

    $.ajax({
        type: "POST",
        // 預設true，是同步執行狀態，false非同步執行完成才往下跑
        async: false,
        url: "/Home/CreatePicApi?albumName=" + albumName + "&id=" + id,
        data: fdd,
        processData: false,
        contentType: false,
        success: function (result) {
            msg = result;
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
            msg = 'fales error!!';
        }
    });
    return msg;
};
//#endregion 

//#region 打叉按鈕刪除照片
$("body").on("click", ".pic-close", function (evt) {
    let id = '#' + evt.target.id;
    let idd = $(id).parent()[0].id;
    let iddname = $('#' + idd).attr('name');
    // indexOf method in object array
    let pos = array.map(function (e) {
        return e.name;
    }).indexOf(iddname);

    // delete array
    array.splice(pos, 1);

    // 依照檔名刪除陣列
    fd.delete(iddname);
    $('#' + idd).remove();
});
//#endregion