//阻止冒泡
function cancelBubble() {
    var e = getEvent();
    if (window.event) {
        //e.returnValue=false;//阻止自身行为
        e.cancelBubble = true;//阻止冒泡
    } else if (e.preventDefault) {
        //e.preventDefault();//阻止自身行为
        e.stopPropagation();//阻止冒泡
    }
}
$(document).ready(function ($) {
    var wr_page = {
        init: function () {
            // 【text focus 效果】
            this.textFocus();
            // 【模拟 select 效果】
            this.initSelect();

        },
        textFocus: function () {
            if ($('.text01').length > 0) {
                var $parent;//text外框
                var tit;//标题内容
                $('.text01 input,.text01 textarea').focus(function (event) {
                    $parent = $(this).parent('.text01');
                    tit = $(this).attr('placeholder');//获取 标题内容
                    $(this).attr('placeholder', '');//清空 text框默认内容
                    $parent.prepend('<span class="text_tit">' + tit + '</span>').addClass('focus');//增加标题 并显示
                }).blur(function (event) {
                    $parent.removeClass('focus').find('.text_tit').remove();//删除标题
                    $(this).attr('placeholder', tit);//恢复默认内容 placeholder
                });
            }
        },
        initSelect: function () {
            if ($('.select').length > 0) {
                // ==根据下拉最长项 设置长度==
                $('.select').each(function (index, el) {
                    var sel_width = $(this).width();
                    $(this).find('.sel-list li').each(function (index, el) {
                        var li_width = $(this).text().length * 8;
                        if (li_width > sel_width) {
                            sel_width = li_width;
                        }
                    });
                    $(this).find('.sel-show').width(sel_width);
                });
                // ==展开和收起 下拉列表==
                $('html').click(function () {
                    //点击任何元素 关闭select
                    $('.select').removeClass('open').find('.sel-list').slideUp('fast');
                    $('.select').each(function (index, el) {
                        // 配件多选
                        if ($(this).hasClass('multiple')) {
                            $(this).find('.sel-show').text('Specification(' + $(this).find('li .checked').length + ')')
                        }
                        //城市多选
                        if ($(this).hasClass('country')) {
                            $(this).find('.sel-show').text('Country(' + $(this).find('li .checked').length + ')')
                        }
                    });
                })
                $('.select').on('click', '.sel-show', function (event) {
                    event.stopPropagation();
                    if (!$(this).parent('.select').hasClass('disable')) {
                        // 关闭其他select
                        $(this).parent('.select').siblings('.select').removeClass('open').find('.sel-list').slideUp('fast');
                        // 展开或关闭当前select
                        $(this).siblings('.sel-list').slideToggle('fast').parent('.select').toggleClass('open');
                    }
                    // 配件多选
                    if ($(this).parent('.select').hasClass('multiple')) {
                        $(this).text('Specification(' + $(this).next('.sel-list').find('li .checked').length + ')')
                    }
                    //城市多选
                    if ($(this).parent('.select').hasClass('country')) {
                        $(this).text('Country(' + $(this).next('.sel-list').find('li .checked').length + ')')
                    }
                });
                // ==模拟选中列表项 单选==
                $('.select').on('click', '.sel-list li', function (event) {
                    event.stopPropagation();
                    if (!$(this).parents('.select').hasClass('multiple')&&!$(this).parents('.select').hasClass('country')) {
                        // 选中列表项值 显示
                        $(this).parents('.select').find('.sel-show').html($(this).html());
                        $(this).parents('.select').find("input[type='hidden']").val($(this).attr("data-value"));
                        // 关闭当前select
                        $(this).parents('.select').removeClass('open').find('.sel-list').slideUp('fast');
                    }
                    else
                    {
                        $(this).find('.checkbox').toggleClass('checked');
                    }
                });
            };
        },
    };
    wr_page.init();
});

// 获取事件
function getEvent() {
    if (window.event) { return window.event; }
    func = getEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {
            if ((arg0.constructor == Event || arg0.constructor == MouseEvent
                || arg0.constructor == KeyboardEvent)
                || (typeof (arg0) == "object" && arg0.preventDefault
                    && arg0.stopPropagation)) {
                return arg0;
            }
        }
        func = func.caller;
    }
    return null;
}
//阻止冒泡
function cancelBubble() {
    var e = getEvent();
    if (window.event) {
        //e.returnValue=false;//阻止自身行为
        e.cancelBubble = true;//阻止冒泡
    } else if (e.preventDefault) {
        //e.preventDefault();//阻止自身行为
        e.stopPropagation();//阻止冒泡
    }
}