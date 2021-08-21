
function KeyPress() 
{

    this.textBoxHandler = function (e) {
        var keycode;
        if (e.keyCode) keycode = e.keyCode;
        else if (e.which) keycode = e.which;

        if (keycode == 13) { // || keycode == 40) {
            var ti = e.target.tabIndex;
            var vv = ti + 1;
            var selector = "[tabIndex=" + vv.toString() + "]";
            $(selector).focus();
           
            if (!$(selector).hasClass('button')) {
             
                 $(selector).val(vv);
                //$(selector).focus();
            }
           

        }
        
        return vv;
    }



    this.textBoxHandlerPost = function (e) {
        var keycode;
        if (e.keyCode) keycode = e.keyCode;
        else if (e.which) keycode = e.which;

        if (keycode == 13) { // || keycode == 40) {
            var ti = e.target.tabIndex;
            var vv = ti + 1;
            var selector = "[tabIndex=" + vv.toString() + "]";
            $(selector).focus();

            if (!$(selector).hasClass('button')) {

                $(selector).val(vv);
                //$(selector).focus();
            }


        }

        return vv;
    }



    this.TxtTransSearch = function (e) {

        var keycode;
        if (e.keyCode) keycode = e.keyCode;
        else if (e.which) keycode = e.which;

        if (keycode == 13) { // || keycode == 40) {
            var ti = e.target.tabIndex;
            var vv =11
            var selector = "[tabIndex=" + vv.toString() + "]";
            $(selector).focus();
            if (!$(selector).hasClass('button')) {
              
                $(selector).val(vv);
            }


        }

        return vv;

    }


    this.TxtPenBillStatus = function (e) {

        var keycode;
        if (e.keyCode) keycode = e.keyCode;
        else if (e.which) keycode = e.which;

        if (keycode == 13) { // || keycode == 40) {
            var ti = e.target.tabIndex;
            var vv = 20;
            var selector = "[tabIndex=" + vv.toString() + "]";
            $(selector).focus();
            if (!$(selector).hasClass('button')) {
                $(selector).val(vv);
            }


        }

        return vv;


    }


    this.TxtVoucher = function (e) {

        var keycode;
        if (e.keyCode) keycode = e.keyCode;
        else if (e.which) keycode = e.which;

        if (keycode == 13) { // || keycode == 40) {
            var ti = e.target.tabIndex;

            var vv =(ti==22)?21:ti+1;
            var selector = "[tabIndex=" + vv.toString() + "]";
            $(selector).focus();
            if (!$(selector).hasClass('button')) {
                $(selector).val(vv);
            }


        }

        return vv;


    }



    this.KeyPressPageControl = function (e) {

        var keycode;
        if (e.keyCode) keycode = e.keyCode;
        else if (e.which) keycode = e.which;

        if (keycode == 13) { // || keycode == 40) {
            var ti = e.target.tabIndex;

            var vv = ti+1;
            var selector = "[tabIndex=" + vv.toString() + "]";
            $(selector).focus();
            if (!$(selector).hasClass('button')) {
                $(selector).val(vv);
            }


        }

        return vv;


    }


    this.TxtLogin = function (e) {

        var keycode;
        if (e.keyCode) keycode = e.keyCode;
        else if (e.which) keycode = e.which;

        if (keycode == 13) { // || keycode == 40) { Enter=13
            var ti = e.target.tabIndex;

            var vv = (ti == 3) ? 5 : ti + 1;
         
            var selector = "[tabIndex=" + vv.toString() + "]";
            $(selector).focus();
            if (!$(selector).hasClass('button')) {
                $(selector).val(vv);
            }


        }

        return vv;


    }


}

 