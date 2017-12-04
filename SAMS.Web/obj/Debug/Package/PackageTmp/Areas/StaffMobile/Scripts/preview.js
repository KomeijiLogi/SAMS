(function (window) {
    function Preview(cfg) {
        this.$target = $(cfg.target);
        this.$previewCont = $(cfg.previewCont);
        this.picSelector = cfg.picSelector;
        this.imgUrlArrs = cfg.imgUrlArrs;
        this.autoBindUI = cfg.autoBindUI === false ? false : true;	//是否自动绑定点击小图预览的控制
        this.hash = cfg.hash || '#perview';
        return this.init();
    }

    Preview.prototype = {
        init: function () {
            var me = this;

            this.renderUI();	//生成页面
            if (this.isPc()) {
                //插件SWIPER初始化
                var mySwiper = new Swiper(me.$previewCont, {
                    // 如果需要分页器
                    //pagination: '.swiper-pagination',
                    simulateTouch: false,
                    // 如果需要前进后退按钮
                    nextButton: '.swiper-button-next',
                    prevButton: '.swiper-button-prev',
                    onSlideChangeEnd: function (swiper) {

                        $('.curPage').html(mySwiper.activeIndex + 1 + '/' + me.imgUrlArrs.length);

                    }
                });
                this.mySwiper = mySwiper;
                $('.gobtn').on('click', function () {
                    return false;
                });
                this.btnShow();

            } else {
                //插件SWIPER初始化
                var mySwiper = new Swiper(this.$previewCont, {
                    // 如果需要分页器
                    //pagination: '.swiper-pagination',

                    onSlideChangeEnd: function (swiper) {
                        $('.curPage').html(mySwiper.activeIndex + 1 + '/' + me.imgUrlArrs.length);
                    }
                });
                this.mySwiper = mySwiper;
            }
            this.bindUI();

            return this;
        },
        getOS: function () {  //获取操作系统平台，IOS或安卓
            var userAgent = navigator.userAgent;
            return userAgent.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/)
		        ? 'ios' : userAgent.match(/Android/i)
		        ? 'android' : '';
        },
        //是否是PC
        isPc: function () {
            var me = this;
            if (me.getOS() == '') {
                return true;
            } else {
                return false;
            }
        },
        //如果是PC端鼠标移入左右箭头出现
        btnShow: function () {
            this.$previewCont.on('mouseover', function () {
                $('.gobtn').show().css('opacity', '.5');
            }).on('mouseleave', function () {
                $('.gobtn').hide();
            });
            this.$previewCont.on('mouseover', '.gobtn', function () {
                $('.gobtn').css('opacity', '0.5');
                $(this).css('opacity', '1');
            }).on('mouseleave', function () {
                $('.gobtn').css('opacity', '0.5');
            });
        },

        renderUI: function () {
            var imgUrlArrs = this.imgUrlArrs;

            var html = '';

            for (var i = 0, len = imgUrlArrs.length; i < len; i++) {
                html += '<div class="swiper-slide"><img src="' + imgUrlArrs[i] + '"/></div>';
            }

            this.$previewCont.find('.swiper-wrapper').html(html);
        },

        bindUI: function () {
            var me = this;

            //点击退出全屏预览
            this.$previewCont.on('click', function () {
                me.hide();
                if (location.hash !== undefined) {
                    history.go(-1);
                }
            });

            //需要自动绑定点击预览大图事件
            if (this.autoBindUI) {
                this.bindPreview();
            }

            //用过hash控制点击返回键关掉图片预览
            if (location.hash !== undefined) {
                window.addEventListener('hashchange', function (e) {
                    if (e.oldURL.indexOf(me.hash) != -1) {
                        me.hide();
                    }
                });
            }
        },

        bindPreview: function () {
            var me = this;

            this.$target.on('click', this.picSelector, function () {
                var curIndex = $(this).index();
                me.preview(curIndex, false);
            });
        },

        //预览函数
        preview: function (index, needUpdate) {
            //需要更新预览
            if (needUpdate) {
                this.renderUI();
            }

            this.$previewCont.show()
						   .find('.curPage')
						   .html(index + 1 + '/' + this.imgUrlArrs.length);
            this.mySwiper.update();

            this.mySwiper.slideTo(index, 10, false);

            location.hash = this.hash;
        },

        hide: function () {
            this.$previewCont.hide();
        }
    };

    window.Preview = window.Preview || Preview;
})(window);