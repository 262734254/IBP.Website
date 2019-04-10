
jQuery.fn.countdown = function (userOptions) {
    var sh;
    var fresh = function () {
        var endtime = new Date(userOptions.endTime);
        var nowtime = new Date();
        var leftsecond = parseInt((endtime.getTime() - nowtime.getTime()) / 1000);
        d = parseInt(leftsecond / 3600 / 24);
        h = parseInt((leftsecond / 3600) % 24);
        m = parseInt((leftsecond / 60) % 60);
        s = parseInt(leftsecond % 60);

        $('#countdown').html(h + ":" +  m + ":" +  s);

        if (leftsecond <= 0) {
            clearInterval(sh);
        }
    }
    fresh();

    sh = setInterval(fresh, 1000);
};
