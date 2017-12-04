function upload() {

    
    var picUrl ='/upload/';
    var me = this;

    $("#file").upload({

        uploadUrl: '/staffMobile/Home/SavePhoto',
        previewWrap: '.upload-img-list',
        isFile: false,
        previewUrl: picUrl + '{id}?w=120&h=120',		//图片预览路径
        
        //上传开始
        onUploadStart: function (xhr) {
            var $progress = this.find('.progress', true);
            //保存进度节点
            $progress.html('0%').addClass('uploading');
            //上传图片限制为10张
            if ($('.pic-preview').length >= 10) {
                $('.upload-addbtn:not(.fileIdString)').hide();

            } else {
               
                var count = $('.upload-img-list >div').size();

                if (count <= 4) {
                    $('#uploaddiv').css('height', '4rem');


                } else if (count <= 8) {
                    $('#uploaddiv').css('height', '7rem');

                } else if (count <= 12) {
                    $('#uploaddiv').css('height', '10rem');
                }

            }
            
        },

        //上传成功
        onUploadSuccess: function (fileInfo) {
           
            this.getRoot$().data('fileid', fileInfo.id)
								   .addClass('success');
            //隐藏进度
            this.find('.progress', true).html('').removeClass('uploading');
            //显示删除按钮
            this.find('.del', true).fadeIn();

            //计算上传成功图片总大小
            // totalSize += fileInfo.size;
            //图片总大小未换算
            me.data.totalSize += fileInfo.size;

            var totalSize = me.data.totalSize;

            if (totalSize >= 1024 * 1024) {
                $('.totalsize i').text((totalSize / (1024 * 1024)).toFixed(2) + 'MB');
            } else {
                $('.totalsize i').text((totalSize / 1024).toFixed(2) + 'kb');
            }

            //上传图片信息
            me.data.photoInfo.push(fileInfo);
            var photsInfos = me.data.photoInfo;

            var photosInfosLength = photsInfos.length;

            $("#imageCount").html("已添加" + photosInfosLength + "张");


            //上传图片成功保存草稿
            //me.submitDraftData(me.getData());
           
        },
        //上传进度
        onUploadProgress: function (loaded, total) {
            var $progress = this.find('.progress', true);
            //修改进度
            $progress.html(Math.floor(loaded / total * 100) + '%');
            //$('.ceshi').html(loaded);
        },
        //上传失败
        onUploadFail: function (xhr) {
            var $progress = this.find('.progress', true);
            $progress.removeClass('uploading');
        },
        //删除图片 
        onDelImage: function (fileInfo) {
            //上传图片小于11张
            if($('.pic-preview').length <= 10){
                $('.upload-addbtn:not(.fileIdString)').show();
            }

            //删除图片大小
            //me.data.totalSize = me.data.totalSize - fileInfo.size;
            //var totalSize = me.data.totalSize;
            //if(totalSize >= 1024*1024){
            //    $('.totalsize i').text((totalSize / (1024 * 1024)).toFixed(2) + 'MB');
            //}else{
            //    $('.totalsize i').text((totalSize / 1024).toFixed(2) + 'kb');
            //}
            //var photoInfoIds=me.data.photoInfo;
					
            //var photoInfoIdsSize=photoInfoIds.length;
            //var photoInfoIdss = new Array(photoInfoIdsSize);
            //for(var i=0;i<photoInfoIdsSize;i++){
            //    photoInfoIdss[i] = photoInfoIds[i].id;
            //}
					
            //删除图片跟新图片信息数组
            //var i = $.inArray(fileInfo.id,photoInfoIdss);
            //me.data.photoInfo.splice(i,1);
					
            //var  photsInfos=me.data.photoInfo;
						
            //var photosInfosLength= photsInfos.length;
					
            //$("#imageCount").html("已添加"+photosInfosLength+"张");
					

            //删除图片保存草稿
            //me.submitDraftData(me.getData());

        }
        
    });

}


//第一次加载图片绑定删除事件
function firstDelteImg() {

    var me = this;
    mTouch('.pic-preview2 .del').on('tap', function () {
        var fileId = $(this).closest('.pic-preview2').data('fileid');
        //删除图片信息
        me.delteImg(fileId);
        //删除当前图片
        $(this).closest('.smallyulan').remove();
        //上传图片限制为4张
        if ($('.pic-preview').length < 4) {
            $('.upload-addbtn:not(.fileIdString)').show();
        }

        //保存草稿
        me.submitDraftData(me.getData());
        return false;
    })
}


//删除图片
function delteImg(fileId) {
    var me = this;
    $.each(this.data.photoInfo, function (index, val) {
        if (val.photoId == fileId) {
            var i = $.inArray($(this)[0], me.data.photoInfo);
            me.data.photoInfo.splice(i, 1);
        }
    })
}


//获取上传图片路径fileId
function getImgIdArrs() {

    var imgArr = [];
    $('.pic-preview').each(function () {
        var imgId = $(this).data('fileid');

        if (imgId != undefined) {
            imgArr.push(imgId);
        }
    });
    return imgArr;
}


//实例化预览
function initPreview() {

    var me = this, oldSuccess, imgIds, preview;
    if (!this.initPreveiw.inited) {
    
        imgIds = me.getImgIdArrs();
        oldSuccess = imgIds.length;
				
        preview = new Preview({
            'previewCont': '.swiper-container',
            'imgUrlArrs': getImgUrlArrs(imgIds),
            'autoBindUI': false
					
        });
				
        this.initPreveiw.inited = true;
			
        $('.upload-img-list').on('click', '.success', function (e) {
					
            var $this = $(this);
            imgIds = me.getImgIdArrs();
            //判断是否需要更新大图预览
            var needUpdate = false;
            var current='';
            var imgArrs = getImgUrlArrs(imgIds);
				
            current=window.location.host+$this.find("img").attr('src');
            var newArray = current.split('&');
            newArray.pop();
            newArray.pop();				
            imgUrlArrs = [];
            imgUrlArrs.push("http://"+newArray.join('&'));
            XuntongJSBridge.call('previewImage',
                            {current: '', // 当前显示图片的http链接
                                urls: imgUrlArrs // 需要预览的图片http链接列表
                            },
                            function(result){}
                           );
        });
    }

}

//获取数据
function getData(isDraft) {

    isDraft = typeof isDraft === 'undefined' ? true : isDraft;
    var groupId = __globalData.groupId;

    if (groupId != "") {
        isDraft = false;
    }
    //图片数组信息
    var photoInfoArr = [];
    console.log(this.data.photoInfo);
    $.each(this.data.photoInfo, function (index, value) {
        var photoInfo = {
            "photoId": value.id || value.photoId,
            "photoSize": value.size || value.photoSize,
            "photoName": value.name || value.photoName,
            "photoType": value.type || value.photoType
        };
        photoInfoArr.push(photoInfo);
    });
    var data = {
        id: __globalData.id,
        type: this.data.type,
        photos: JSON.stringify(photoInfoArr),
        files: JSON.stringify(fileInfoArr),
        name: this.data.userName,
        date: +new Date(),
        startDate: this.data.startDate,
        endDate: this.data.endDate,
        isDraft: isDraft,
        workContent: Util.escapeToHtmlEntity($('.report-content_input').val())
    };

    //是详情修改的草稿
    if (this.data.isUpdate) {
        data.isUpdate = this.data.isUpdate;
    }
    return data;

}



//判断图片是否正在上传
function isImgLoading() {

    var me = this;
    //如果图片正在上传
    var imgOnLoading = true;
    $('.pic-preview').each(function () {
        if (!$(this).hasClass('success') && !($(this).find('span').hasClass('reload'))) {
            imgOnLoading = false;
            $('.upload-tishi').html('图片正在上传，请稍后...');
            $('.upload-tishi').fadeIn();
            $('.overlayer').show();
            setTimeout(function () {
                $('.upload-tishi').hide();
                $('.overlayer').hide();
            }, 1500);
            return false;
        }
    });
    if (!imgOnLoading) {
        return false;
    }

}

//第一次获取数据
function firstGetData() {

    var deferred = $.Deferred();
    $.ajax({
        //url: '/workreport/rest/query/' + __globalData.id,
        url:'',
        type: 'GET',
        data: '',
        dataType: 'json'
    })
    .done(function (resp) {
        deferred.resolve(resp);

    })
    .fail(function () {
        deferred.reject();
    });
    return deferred.promise();

}



//第一次加载数据
function initFristData() {


    var me = this;
    this.firstGetData().done(function (data) {

        if (Util.getRequest().isDraft == 'true') {

            if (data.report.photos != null) {
                var html = '';
                var html2 = '';
                //console.log(data.report.photos);
                $.each(data.report.photos, function (index, value) {
                    me.data.photoInfo.push($(this)[0]);
                    var fileId = value.photoId;
                    html += '<div class="pic-preview pic-preview2 smallyulan success" data-fileid="' + fileId + '"><div class="del">×</div><div class="img-wrap smallimg"><img src="' + me.data.picUrl + fileId + '&w=120&h=120"></div><span class="progress"></span></div>';
                    html2 += '<div class="swiper-slide"><img src="' + me.data.picUrl + fileId + '"></div>';
                });
                $('.swiper-wrapper').append(html2);
                $(html).prependTo('.upload-img-list');

                //第一次加载图片绑定删除事件
                me.firstDelteImg();


                //上传图片限制为4张
                if ($('.pic-preview').length >= 4) {
                    $('.upload-addbtn:not(.fileIdString)').hide();
                }

                //预览图片
                me.initPreveiw();


                $("#imageCount").html("已添加" + data.report.photos.length + "张");
            };
        }
    });
   

}
