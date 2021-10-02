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
function PrintActionRDLC(url, prntVal, comcod, zone, dist, thana, mouza, csdhagno) {

    alert("url");
    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: '{comcod:"' + comcod + '", zone:"' + zone + '", dist:"' + dist + '", thana:"' + thana + '", mouza:"' + mouza + '", csdhagno:"' + csdhagno + '"}',
        async: true,
        success: function onSuccess(data) {
            window.open("../RDLCViewer.aspx?PrintOpt=" + prntVal);
        }
    });
}
