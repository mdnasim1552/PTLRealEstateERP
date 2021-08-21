<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpMonthWiseEva.aspx.cs" Inherits="RealERPWEB.F_47_Kpi.RptEmpMonthWiseEva" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../Content/jquery-ui.css" rel="stylesheet" />
   
    <script src="../Scripts/Chart.js"></script>
    <script src="../Scripts/Chart.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Script_own/S_05_MyPage/RptEmpMonthWiseEva03.js"></script>
    <script src="../Script_own/print.js"></script>
    <script type="text/javascript">
        var sumtamt1 = 0.00;
        var sumttamt2 = 0.00;
        var sumttamt3 = 0.00;
        var sumttamt4 = 0.00;
        var sumttamt5 = 0.00;
        var sumttamt6 = 0.00;
        var sumttamt7 = 0.00;
        var sumttamt8 = 0.00;

        var sumtaamt1 = 0.00;
        var sumtaamt2 = 0.00;
        var sumtaamt3 = 0.00;
        var sumtaamt4 = 0.00;
        var sumtaamt5 = 0.00;
        var sumtaamt6 = 0.00;
        var sumtaamt7 = 0.00;
        var sumtaamt8 = 0.00;


        var url = "../S_05_MyPage/RptEmpMonthWiseEva03.asmx/PrintRptMgt";
        var prntVal = 'PDF';
        var empid = '';
        var frmdate = '';
        var todate = '';
        var qryString = '';

        $(document).ready(function () {

            $('#<%=this.txtSrchSalesTeam.ClientID%>').focus();
            qryString = '<%=Request.QueryString["Type"].ToString()%>';
            Search();

            $('#imgSearchSalesTeam').click(function () {
               
                
                GetfilterEmployee();
                return false;
            });


            $("[id$=lnkPrint]").click(function () {
                PrintAction(url, prntVal, empid, frmdate, todate);
                return false;

            });


            $("[id$=DDPrintOpt]").change(function () {
                prntVal = ($("[id$=DDPrintOpt]").val());
                return false;

            });



        });
        function GetEmpMonEva() {
            empid = $('#<%=this.ddlEmpid.ClientID%>').val();
            frmdate = $('#<%=this.txtfrmdate.ClientID%>').val();
            todate = $('#<%=this.txttodate.ClientID%>').val();
            var objmonempeva = new RealERPScript();
            var tblmoneeva = objmonempeva.GetEmpMonEva(empid, frmdate, todate);
            displayTable(tblmoneeva);
            CreateBarChart(tblmoneeva);
        }

        
        function displayTable(tblmoneeva) {
            var i = 0, selper=0.00, collper=0.00, meetEx=0.00, meetIn=0.00, offper=0.00, visitper=0.00, callper=0.00, othper=0.00
            var ttamt1 = 0.00;
            var ttamt2 = 0.00;
            var ttamt3 = 0.00;
            var ttamt4 = 0.00;
            var ttamt5 = 0.00;
            var ttamt6 = 0.00;
            var ttamt7 = 0.00;
            var ttamt8 = 0.00;

            var taamt1 = 0.00;
            var taamt2 = 0.00;
            var taamt3 = 0.00;
            var taamt4 = 0.00;
            var taamt5 = 0.00;
            var taamt6 = 0.00;
            var taamt7 = 0.00;
            var taamt8 = 0.00;

            $("#grvMonthempeva").html('');
            $("#grvMonthempeva").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + ">" +
                "<tr><th style='width:30px;'></th><th style='width:70px;'></th><th  colspan='8' >Target</th>" +
                 "<th colspan='8' >Actual</th><th style='width:70px;'></th><th style='width:70px;'></th></tr>" +
                 "<tr><th style='width:30px;'>SL</th><th style='width:70px;'>MONTH</th><th style='width:80px;'>SALES</th><th style='width:80px;'>COLLECTION</th>" +
                        "<th style='width:70px;'>Call</th><th style='width:70px;'>Meeting Ext</th><th style='width:70px;'>Meeting In</th><th style='width:70px;'>Visit</th><th style='width:70px;'>Offer</th><th style='width:70px;'>Others</th> " +
                        "<th style='width:80px;'>SALES</th><th style='width:80px;'>COLLECTION</th><th style='width:70px;'>Call</th><th style='width:70px;'>Meeting Ext</th><th style='width:70px;'>Meeting In</th><th style='width:70px;'>Visit</th> <th style='width:70px;'>Offer</th> <th style='width:70px;'>Others</th>" +
                        "<th style='width:70px;'>Percent</th> <th style='width:120px;'>GPA</th></tr>" +
                "</thead>");
            $.each(tblmoneeva, function (index, arval) {

                ttamt1 += arval.tamt1;
                ttamt2 += arval.tamt2;
                ttamt3 += arval.tamt3;
                ttamt4 += arval.tamt4;
                ttamt5 += arval.tamt5;
                ttamt6 += arval.tamt6;
                ttamt7 += arval.tamt7;
                ttamt8 += arval.tamt8;

                taamt1 += arval.amt1;
                taamt2 += arval.amt2;
                taamt3 += arval.amt3;
                taamt4 += arval.amt4;
                taamt5 += arval.amt5;
                taamt6 += arval.amt6;
                taamt7 += arval.amt7;
                taamt8 += arval.amt8;

                selper = (ttamt1 == 0) ? 0.00 : ((taamt1 / ttamt1) * 100).toFixed(2);
                collper = (ttamt2 == 0) ? 0.00 : ((taamt2 / ttamt2) * 100).toFixed(2);
                callper = (ttamt3 == 0) ? 0.00 : ((taamt3 / ttamt3) * 100).toFixed(2);
                meetEx = (ttamt4 == 0) ? 0.00 : ((taamt4 / ttamt4) * 100).toFixed(2);
                meetIn = (ttamt5 == 0) ? 0.00 : ((taamt5 / ttamt5) * 100).toFixed(2);
                visitper = (ttamt6 == 0) ? 0.00 : ((taamt6 / ttamt6) * 100).toFixed(2);
                offper = (ttamt7 == 0) ? 0.00 : ((taamt7 / ttamt7) * 100).toFixed(2);
                othper = (ttamt8 == 0) ? 0.00 : ((taamt8 / ttamt8) * 100).toFixed(2);

                

                $("#grvMonthempeva").append(" <tbody><tr><td style=text-align:center;" + ">" + "&nbsp" + (i + 1) + "</td>"
                       + "<td style=text-align:left;" + ">"  + arval.yearmon+ "</td>"
                       + "<td style=text-align:right;" + ">" +  ((arval.tamt1 == 0) ? '' : (arval.tamt1).toLocaleString('en-US', { minimumFractionDigits: 0 }))+ "</td>"
                       + "<td style=text-align:right;" + ">"  + ((arval.tamt2 == 0) ? '' : (arval.tamt2).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">"  + ((arval.tamt3 == 0) ? '' : (arval.tamt3).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.tamt4 == 0) ? '' : (arval.tamt4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.tamt5 == 0) ? '' : (arval.tamt5).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.tamt6 == 0) ? '' : (arval.tamt6).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.tamt7 == 0) ? '' : (arval.tamt7).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.tamt8 == 0) ? '' : (arval.tamt8).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"

                       + "<td style=text-align:right;" + ">" + ((arval.amt1 == 0) ? '' : (arval.amt1).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">"  + ((arval.amt2 == 0) ? '' : (arval.amt2).toLocaleString('en-US', { minimumFractionDigits: 0 }))  + "</td>"
                       + "<td style=text-align:right;" + ">"  +  ((arval.amt3 == 0) ? '' : (arval.amt3).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.amt4 == 0) ? '' : (arval.amt4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.amt5 == 0) ? '' : (arval.amt5).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.amt6 == 0) ? '' : (arval.amt6).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.amt7 == 0) ? '' : (arval.amt7).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.amt8 == 0) ? '' : (arval.amt8).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"

                       + "<td style=text-align:right;" + ">"  +  ((arval.tper == 0) ? '' : (arval.tper).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:left;" + ">"  +arval.gpa + "</td></tr></tbody>");

               
                i++;

            });

            sumtamt1 = Math.ceil(ttamt1).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtamt2 = Math.ceil(ttamt2).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtamt3 = Math.ceil(ttamt3).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtamt4 = Math.ceil(ttamt4).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtamt5 = Math.ceil(ttamt5).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtamt6 = Math.ceil(ttamt6).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtamt7 = Math.ceil(ttamt7).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtamt8 = Math.ceil(ttamt8).toLocaleString('en-US', { minimumFractionDigits: 0 });

            sumtaamt1 = Math.ceil(taamt1).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtaamt2 = Math.ceil(taamt2).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtaamt3 = Math.ceil(taamt3).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtaamt4 = Math.ceil(taamt4).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtaamt5 = Math.ceil(taamt5).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtaamt6 = Math.ceil(taamt6).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtaamt7 = Math.ceil(taamt7).toLocaleString('en-US', { minimumFractionDigits: 0 });
            sumtaamt8 = Math.ceil(taamt8).toLocaleString('en-US', { minimumFractionDigits: 0 });

            $("#grvMonthempeva").append(" <tfoot class=grvHeader><tr><td style=text-align:center;></td>" + "<td style=text-align:right;font-weight:bold;>"
                    + "TOTAL" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtamt1 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtamt2 + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtamt3 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtamt4 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtamt5 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtamt6 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtamt7 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtamt8 + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtaamt1 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtaamt2 + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtaamt3 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtaamt4 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtaamt5 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtaamt6 + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtaamt7 + "</td>"+ "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + sumtaamt8 + "</td><td></td><td></td></tr>"
                    +"<tr><td style=text-align:center;></td>" + "<td style=text-align:right;font-weight:bold;>"
                    + "%" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "</td>" 
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + selper + " %" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + collper + " %" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + callper + " %" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + meetEx + " %" + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + meetIn + " %" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + visitper + " %" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + offper + " %" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + othper + " %" + "</td><td></td><td></td></tr>"
                    
                    +"</tfoot>");

            $("#grvMonthempeva").show();

        }
        function CreateBarChart(tblmoneeva) {
            InitCanvas();
            // var empevachrt =document.getElementById('empevachrt').getContext('2d');
            var empevachrt= document.getElementById('empevachrt').getContext('2d');
            var data = {

                labels: [],
                datasets: [
                    {
                        label: "Target",
                        fillColor: "rgba(51,255,47,0.5)",
                        strokeColor: "rgba(220,220,220,0.8)",
                        highlightFill: "rgba(0,204,0,0.75)",
                        highlightStroke: "rgba(220,220,220,1)",
                        data: []
                    },
                    {
                        label: "Actual",
                        fillColor: "rgba(151,187,205,0.5)",
                        strokeColor: "rgba(151,187,205,0.8)",
                        highlightFill: "rgba(151,187,205,1)",
                        highlightStroke: "rgba(151,187,205,1)",
                        data: []
                    }
                ]
            };
            var i = 0;
            $.each(tblmoneeva, function (index, arval) {
                data.labels[i] = arval.yearmon;
                data.datasets[0].data[i] = arval.tmark;
                data.datasets[1].data[i] = arval.tper;
                i++;

            });
            empevachrt = new Chart(empevachrt).Bar(data);



         
          
        }
        function InitCanvas() {
            $('#MonEmpCanDiv').html('');
            $('#MonEmpCanDiv').html('<canvas id="empevachrt" width="750" height="290"></canvas>');

        }


        <%--$('#<%=this.imgSearchSalesTeam.ClientID%>').click(function () {
                alert("some");
                return false;
        });--%>

        //$("[id$=imgSearchSalesTeam]").click(function () {
        // //   prntVal = ($("[id$=DDPrintOpt]").val());
        //    alert("some");
        //    return false;

        //});
    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDatefrm" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>

                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                        </cc1:CalendarExtender>

                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>

                                    </div>


                                    <div class="col-md-2 pading5px asitCol3">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                        </div>

                                    </div>



                                </div>

                                <div class="form-group">

                                    <div class="col-md-2 pading5px asitCol3">
                                        <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Employee Name:"></asp:Label>



                                        <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass=" inputtextbox" ClientIDMode="Static"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgSearchSalesTeam" runat="server"  TabIndex="4" CssClass="btn btn-primary srearchBtn" ClientIDMode="Static"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <%--  <button id="imgSearchSalesTeam" onclick="javascript:SearchSalesTeam()"  tabindex="4"  class="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></button>--%>
                                        </div>
                                    </div>



                                    <div class="col-md-4 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlEmpid" runat="server" CssClass=" form-control"
                                            TabIndex="5" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-1 pading5px">

                                        <input type="button" id="okbtn" onclick="javascript: GetEmpMonEva();" value="OK" class="btn btn-primary okBtn" />


                                    </div>


                                    
                                    <div class="col-md-1 pading5px">
                                        <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/F_47_Kpi/RptEmpMonthWiseEvaDet.aspx?Type=Mgt")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>
                                        <asp:Label ID="lbluseid" runat="server" CssClass="lblTxt lblName" Style="display: none;"></asp:Label>


                                    </div>


                                </div>

                            </div>
                        </fieldset>

                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-4 ">
                                        <asp:Label ID="lblName" runat="server" CssClass=" lblTxt lblName220"></asp:Label>



                                    </div>
                                    <div class="col-md-4 ">
                                        <asp:Label ID="lblDesg" runat="server" CssClass="lblTxt lblName220"></asp:Label>



                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblJoin" runat="server" TabIndex="4" CssClass="lblTxt lblName220"></asp:Label>



                                    </div>




                                </div>
                                <div class="form-group">
                                    <div class="col-sm-8 col-md-8 col-lg-8">
                                        <div id="MonEmpCanDiv">
                                            <canvas id="empevachrt" width="750" height="290"></canvas>
                                        </div>

                                    </div>
                                </div>
                        </fieldset>
                        <table id="grvMonthempeva" class="table-striped table-hover table-bordered grvContentarea"></table>


                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

