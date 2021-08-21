function PrintAction(prntAddress, prntVal) {
    $.ajax({
        type: "POST",
        url: prntAddress,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: '',
        async: true,
        success: function onSuccess(data) {
            window.open("../RptViewer.aspx?PrintOpt=" + prntVal);
        }
    });
}

