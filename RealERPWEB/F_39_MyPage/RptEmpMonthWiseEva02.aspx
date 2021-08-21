<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpMonthWiseEva02.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.RptEmpMonthWiseEva02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../Content/jquery-ui.css" rel="stylesheet" />
   
    <script src="../Scripts/Chart.js"></script>
    <script src="../Scripts/Chart.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
      <script src="../Script_own/print.js"></script>
    <script src="../Script_own/S_05_MyPage/RptEmpMonthWiseEva03.js"></script>
    <script type="text/javascript">
       
        var url = "../S_05_MyPage/RptEmpMonthWiseEva03.asmx/PrintRptEmpEva02";
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

                var srchemp = $('#txtSrchSalesTeam').val();
                GetfilterEmployee();
                return false;
            });
            // alert(qryString);

            $('#<%=this.txtSrchSalesTeam.ClientID%>').focus();


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
            var type = "";
            var tblmoneeva = objmonempeva.GetEmpMonEva02(empid, frmdate, todate, type);
            displayTable(tblmoneeva);
            CreateBarChart(tblmoneeva);
        }

        
        function displayTable(tblmoneeva) {
            var i = 0, tarper = 0.00;
            var ttar = 0.00;
            var tactual = 0.00;
            
            $("#grvMonthempeva").html('');
            $("#grvMonthempeva").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + ">" +
                 "<tr><th style='width:30px;'>SL</th><th style='width:70px;'>MONTH</th><th style='width:80px;'>MONTH TARGET</th><th style='width:80px;'>CUM. TARGET</th><th style='width:80px;'>MONTH ACTUAL</th><th style='width:80px;'>CUM. ACTUAL</th> <th style='width:80px;'>MARKS</th> <th style='width:80px;'>GPA</th></tr>" +
                "</thead>");
            $.each(tblmoneeva, function (index, arval) {

                ttar += arval.tar;
                tactual += arval.act;
                
             

                

                $("#grvMonthempeva").append(" <tbody><tr><td style=text-align:center;" + ">"  + (i + 1) + "</td>"
                       + "<td style=text-align:left;" + ">"  + arval.yearmon+ "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.tar == 0) ? '' : (arval.tar).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.cumtar == 0) ? '' : (arval.cumtar).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.act == 0) ? '' : (arval.act).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.cumact == 0) ? '' : (arval.cumact).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                       + "<td style=text-align:right;" + ">" + ((arval.tper == 0) ? '' : (arval.tper).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>"
                        + "<td style=text-align:left;" + ">" + arval.gpa + "</td>"
                      +"</tr></tbody>");

               
                i++;

            });


            tarper = (ttar == 0) ? 0.00 : ((tactual / ttar) * 100).toLocaleString('en-US', { maximumFractionDigits: 2 });
           
         
            $("#grvMonthempeva").append(" <tfoot class=grvHeader><tr> <td style=text-align:center;></td>"
                    + "<td style=text-align:center;font-weight:bold;"+">" + " TOTAL" + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + ttar + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp"  + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + tactual + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp"  + "</td>"
                    + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + tarper+"%" + "<td></td></td></tr>"
                    
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
                                            <asp:LinkButton ID="imgSearchSalesTeam" runat="server" TabIndex="4" CssClass="btn btn-primary srearchBtn" ClientIDMode="Static"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <%--  <button id="imgSearchSalesTeam" onclick="javascript:SearchSalesTeam()"  tabindex="4"  class="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></button>--%>
                                        </div>
                                    </div>



                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlEmpid" runat="server" CssClass="ddlPage235 inputTxt"
                                            TabIndex="5" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-1 pading5px">

                                        <input type="button" id="okbtn" onclick="javascript: GetEmpMonEva();" value="OK" class="btn btn-primary okBtn" />


                                    </div>
                                    <div class="col-md-1 pading5px">
                                         <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/F_05_MyPage/RptMIS.aspx?Type=IndEmpHistory")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>
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

