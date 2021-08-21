<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPendingPayment.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptPendingPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style type="text/css">
        .tblh {
            background: #DFF0D8;
            height: 30px;
            text-align: center;
        }

        .th1 {
            width: 200px;
            text-align: center;
        }

        .th2 {
            width: 80px;
            text-align: center;
        }

        .th3 {
            width: 70px;
            text-align: center;
        }
         .th4 {
            width: 200px;
            text-align: center;
        }

        .th5 {
            width: 60px;
            text-align: center;
        }
        .th6 {
            width: 60px;
            text-align: center;
        }
        .th7 {
            width: 70px;
            text-align: center;
        }
        .th8 {
            width: 60px;
            text-align: center;
        }
        
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            GetData();
        }

        function GetData() {
            try {
                $.ajax({
                    type: "POST",
                    url: "RptPendingPayment.aspx/GetAllData",
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //console.log(JSON.parse(response.d));
                        var data = response.d;

                        //console.log(data['account']);
                        ExecuteGraph(data);
                    },
                    failure: function (response) {
                        //  alert(response);
                        alert("f");
                    }
                });
            } catch (e) {
                alert(e);
            };

        }

        function ExecuteGraph(bgd) {

            try {

                var data = JSON.parse(bgd);

                var pdc = data["pdc"];

                
                var row = '';
                $.each(pdc,
                    function (i, item) {
                        var desc = "<b>" + item.grpdesc + "</b>";

                       // "<b>" + item.grpdesc + "</b>" + item.cactdesc.trim().length > 0 ? item.grpdesc.trim().length > 0 ? +"<br>" : "" + item.cactdesc : "";

                      
                        row += "<tr>";
                        row += "<td>" + desc + "</td>";



                        // row += "<td style=text-align:left;>" + item.cactdesc + "</td>";
                        row += "<td style=text-align:center;>" + item.vounum1 + "</td>";
                        row += "<td style=text-align:center;>" + item.voudat.toString('dd-MMM-yyyy') + "</td>";
                        //row += "<td style=text-align:left;>" + item.isunum + "</td>";
                       // row += "<td>" + item.actdesc + item.resdesc.trim().length > 0 ? item.actdesc.trim().length > 0 ? '' : '' + item.resdesc : '' + "</td>";
                        row += "<td>" + item.actdesc + item.resdesc+ "</td>";
                       // row += "<td style=text-align:left;>" + item.resdesc + "</td>";
                        row += "<td style=text-align:center;>" + item.isunum + "</td>";
                        row += "<td style=text-align:center;>" + item.chequeno + "</td>";
                        row += "<td style=text-align:center;>" + item.chequedat.toString('dd-MMM-yyyy') + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.cramt == 0) ? '' : (item.cramt.toFixed(2)).toLocaleString('en-US')) +
                            "</td>";
                        row += "</tr>";
                        $("#pdc tbody").html(row);
                    });


                //var yearsale = data["yearsale"];
                //var ar1 = '';
                //var ar2 = '';
                //var row = '';
                //$.each(yearsale,
                //    function (i, item) {
                //        ar1 = (item.team == "Total ( In lac )")
                //            ? '<a target=_blank href=' + encodeURI('RptSalSummery.aspx?Type=dSaleVsColl') + '>'
                //            : '';
                //        ar2 = (item.team == "Total ( In lac )") ? '</a>' : '';
                //        row += (item.team == "Total ( In lac )")
                //            ? "<tr style='font-weight:bold;color:maroon;background-color:#C0C0C0;'>"
                //            : "<tr>";
                //        row += "<td>" + ar1 + item.team + ar2 + "</td>";
                //        row += "<td style=text-align:right;>" +
                //            ((item.msaleamt == 0)
                //                ? ''
                //                : (item.msaleamt.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                //            "</td>";
                //        row += "<td style=text-align:right;>" +
                //            ((item.actty == 0) ? '' : (item.actty.toFixed(2)).toLocaleString('en-US')) +
                //            "</td>";
                //        row += "<td style=text-align:right;>" +
                //            ((item.actly == 0) ? '' : (item.actly.toFixed(2)).toLocaleString('en-US')) +
                //            "</td>";
                //        row += "</tr>";
                //        $("#yearsale tbody").html(row);
                //    });
            }
            catch (e) {

            }
        }
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <div class="row" style="margin-left: 15px;">
                            <h2>Post Dated Check</h2>
                            <table id="pdc" class="table-striped table-hover table-bordered">
                                <thead>
                                    <tr class="tblh">
                                        <th class="th1">Bank Name</th>
                                        <th class="th2">Issue</th>
                                        <th class="th3">Date</th>
                                        <th class="th4">Acc. Description</th>
                                        <th class="th5">Issue Ref</th>
                                        <th class="th6">Cheque No</th>
                                        <th class="th7">Cheque Date</th>
                                        <th class="th8">Amount</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

