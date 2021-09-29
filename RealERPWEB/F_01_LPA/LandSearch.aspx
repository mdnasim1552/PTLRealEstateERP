<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="LandSearch.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LandSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <script src="../Script_own/print.js"></script>
    <script src="../Script_own/S_05_MyPage/RptEmpMonthWiseEva03.js"></script>
    <script type="text/javascript">
        var url = "../S_05_MyPage/RptEmpMonthWiseEva03.asmx/PrintLandInfo";
        var prntVal = 'PDF';
        var comcod =<%=this.GetCompCode()%>;
        var zone = $('#ddlZone option:selected').val();
        var dist = $('#ddldistrict option:selected').val();
        var thana = $('#ddlthana option:selected').val();
        var mouza = $('#ddlMouza option:selected').val();
        var csdhagno = $('#<%=this.txtcsdhagno.ClientID%>').val();

        $(document).ready(function () {
            //alert("I m IN");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });



        function pageLoaded() {


            try {




                funZone();
                $('#ddlZone').change(function () {


                    funDistrict();
                });
                $('#ddldistrict').change(function () {

                    funThana();
                });
                $('#ddlthana').change(function () {


                    funMouza();
                });

                $('.chzn-select').chosen({ search_contains: true });


                $("[id$=lnkconPrint]").click(function () {
                    
                    PrintActionRDLC(url, prntVal, comcod, zone, dist, thana, mouza, csdhagno);
                    return false;
                });


            }

            catch (e) {

                alert(e.message)
            }


        }






        function funZone() {

            try {


                var comcod =<%=this.GetCompCode()%>;

                $.ajax({
                    type: "POST",
                    url: "LandSearch.aspx/GetZone",
                    data: '{comcod:"' + comcod + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var data = JSON.parse(response.d);
                        var ddlzone = $('#ddlZone');
                        $(ddlzone).html('');
                        $.each(data, function (key, data) {

                            $(ddlzone).append("<option value='" + data.gcod + "'>" + data.gdesc + "</option>");
                        });


                        //  $('#ddlZone').trigger('change');
                        funDistrict();

                        // console.log(data);
                        //  funDataBind(data);                      



                    },


                    failure: function (response) {

                        alert("failure");
                    }
                });



            }

            catch (e) {

                alert(e.message);

            }


        }

        function funDistrict() {

            try {


                var comcod =<%=this.GetCompCode()%>;
                var zone = $('#ddlZone option:selected').val();

                $.ajax({
                    type: "POST",
                    url: "LandSearch.aspx/GetDistrict",
                    data: '{comcod:"' + comcod + '", zone:"' + zone + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var data = JSON.parse(response.d);
                        var ddldistrict = $('#ddldistrict');
                        $(ddldistrict).html('');
                        $.each(data, function (key, data) {

                            $(ddldistrict).append("<option value='" + data.gcod + "'>" + data.gdesc + "</option>");
                        });

                        funThana();

                    },


                    failure: function (response) {

                        alert("failure");
                    }
                });



            }

            catch (e) {

                alert(e.message);

            }


        }
        function funThana() {

            try {


                var comcod =<%=this.GetCompCode()%>;
                var dist = $('#ddldistrict option:selected').val();

                $.ajax({
                    type: "POST",
                    url: "LandSearch.aspx/GetThana",
                    data: '{comcod:"' + comcod + '", dist:"' + dist + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var data = JSON.parse(response.d);
                        var ddlthana = $('#ddlthana');
                        $(ddlthana).html('');
                        $.each(data, function (key, data) {

                            $(ddlthana).append("<option value='" + data.gcod + "'>" + data.gdesc + "</option>");
                        });
                        funMouza();

                    },


                    failure: function (response) {

                        alert("failure");
                    }
                });



            }

            catch (e) {

                alert(e.message);

            }


        }

        function funMouza() {

            try {


                var comcod =<%=this.GetCompCode()%>;
                var thana = $('#ddlthana option:selected').val();

                $.ajax({
                    type: "POST",
                    url: "LandSearch.aspx/GetMouza",
                    data: '{comcod:"' + comcod + '", thana:"' + thana + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var data = JSON.parse(response.d);
                        var ddlMouza = $('#ddlMouza');
                        $(ddlMouza).html('');
                        $.each(data, function (key, data) {

                            $(ddlMouza).append("<option value='" + data.gcod + "'>" + data.gdesc + "</option>");
                        });

                    },


                    failure: function (response) {

                        alert("failure");
                    }
                });



            }

            catch (e) {

                alert(e.message);

            }


        }

        function funShowData() {

            try {


                var comcod =<%=this.GetCompCode()%>;
                var zone = $('#ddlZone option:selected').val();
                var dist = $('#ddldistrict option:selected').val();
                var thana = $('#ddlthana option:selected').val();
                var mouza = $('#ddlMouza option:selected').val();
                var csdhagno = $('#<%=this.txtcsdhagno.ClientID%>').val();

                $.ajax({
                    type: "POST",
                    url: "LandSearch.aspx/GetLandInfo",
                    data: '{comcod:"' + comcod + '", zone:"' + zone + '", dist:"' + dist + '", thana:"' + thana + '", mouza:"' + mouza + '", csdhagno:"' + csdhagno + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var data = JSON.parse(response.d);
                        //console.log(data);

                        displayTable(data);
                        //var ddlMouza = $('#ddlMouza');
                        //$(ddlMouza).html('');
                        //$.each(data, function (key, data) {

                        //    $(ddlMouza).append("<option value='" + data.gcod + "'>" + data.gdesc + "</option>");
                        //});

                    },


                    failure: function (response) {

                        alert("failure");
                    }
                });



            }

            catch (e) {

                alert(e.message);

            }

        }





        function displayTable(tbllandinfo) {
            var i = 0, tarper = 0.00;

         
            $("#gvlandinfo").html('');

            var hrow = '';
            var row = "<tbody>";
            var frow = '';
            hrow = "<thead class='grvHeader'>" +
                "<tr><th rowspan='2' style='width:30px;vertical-align: middle;'>SL</th><th colspan='4' style='width:240px;'>Dhag No</th><th colspan='2' style='width:140px;'>Acre of Land</th>" +
                "<th colspan='2' style = 'width:140px;' >BS</th ><th rowspan='2' style='width:80px;vertical-align: middle;'>JBL Ref No</th>" +
                "<th rowspan='2' style='width:80px;vertical-align: middle;'>JBL Purchase of Land</th> " +
                "<th rowspan='2' style='width:80px;;vertical-align: middle;'>JBL Total Land</th>" +
                "<th rowspan='2' style='width:80px;vertical-align: middle;'>Rest of Land</th>" +
                "<th rowspan='2' style='width:80px;vertical-align: middle;'>Namzari</th></tr>" +
                "<tr><th style='width:60px;'>CS</th><th style='width:60px;'>SA</th><th style='width:60px;'>RS</th>" +
                "<th style='width:60px;'>BS</th><th style='width:70px;'>CS</th><th style='width:70px;'>BS</th>" +
                "<th style='width:70px;'>Khotian No</th><th style='width:70px;'>Acre of Land</th></tr>" +
                "</thead>";

            $("#gvlandinfo").append(hrow);
            var tcslarea = 0;
            var tbslarea = 0;
            var tbsklarea = 0;
            var tbudarea = 0;
            var trestlarea = 0;
            var tpurarea = 0;
            $.each(tbllandinfo, function (index, arval) {

                tcslarea = tcslarea + arval.cslarea;
                tbslarea = tbslarea + arval.bslarea;
                tbsklarea = tbsklarea + arval.bsklarea;
                tbudarea = tbudarea + arval.budarea;
                trestlarea = trestlarea + arval.restlarea;
                tpurarea = tpurarea + arval.purarea;




                row = row + "<tr class='grvRows'><td style=text-align:center;" + ">" + (i + 1) + "</td>" +
                    "<td style=text-align:left;" + ">" + arval.csdhagno + "</td>" +
                    "<td style=text-align:left;" + ">" + arval.sadhagno + "</td>" +
                    "<td style=text-align:left;" + ">" + arval.rsdhagno + "</td>" +
                    "<td style=text-align:left;" + ">" + arval.bsdhagno + "</td>" +
                    "<td style=text-align:right;" + ">" + ((arval.cslarea == 0) ? '' : (arval.cslarea).toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                    "<td style=text-align:right;" + ">" + ((arval.bslarea == 0) ? '' : (arval.bslarea).toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                    "<td style=text-align:left;" + ">" + arval.bskhotianno + "</td>" +
                    "<td style=text-align:right;" + ">" + ((arval.bsklarea == 0) ? '' : (arval.bsklarea).toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                    "<td style=text-align:left;" + ">" + arval.jblrefno + "</td>" +
                    "<td style=text-align:right;" + ">" + ((arval.budarea == 0) ? '' : (arval.budarea).toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                    "<td style=text-align:right;" + ">" + ((arval.budarea == 0) ? '' : (arval.budarea).toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                    "<td style=text-align:right;" + ">" + ((arval.restlarea == 0) ? '' : (arval.restlarea).toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                    "<td style=text-align:right;" + ">" + ((arval.purarea == 0) ? '' : (arval.purarea).toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td></tr>";



               
                i++;

            });
            row = row + "</tbody>";
            $("#gvlandinfo").append(row);
            frow = "<tfoot class='grvFooter'>";
            frow += "<tr><td></td>" +
                "<td style=text-align:center;font-weight:bold;" + ">" + " Total" + "</td>" +
                "<td></td><td></td><td></td>" +
                "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + ((tcslarea == 0) ? '' : tcslarea.toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + ((tbslarea == 0) ? '' : tbslarea.toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                "<td></td>" +
                "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + ((tbsklarea == 0) ? '' : tbsklarea.toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                "<td></td>" +
                "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + ((tbudarea == 0) ? '' : tbudarea.toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + ((tbudarea == 0) ? '' : tbudarea.toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + ((trestlarea == 0) ? '' : trestlarea.toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td>" +
                "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + ((tpurarea == 0) ? '' : tpurarea.toFixed(4).toLocaleString('en-US', { minimumFractionDigits: 0 })) + "</td></tr>" + "</tfoot>";




            $("#gvlandinfo").append(frow);


        }


       <%-- function funPrint()
        {


            try {

                console.log("print");

                var comcod =<%=this.GetCompCode()%>;
                var zone = $('#ddlZone option:selected').val();
                var dist = $('#ddldistrict option:selected').val();
                var thana = $('#ddlthana option:selected').val();
                var mouza = $('#ddlMouza option:selected').val();
                 var csdhagno = $('#<%=this.txtcsdhagno.ClientID%>').val();

                 $.ajax({
                     type: "POST",
                     url: "LandSearch.aspx/PrintLandInfo",
                     data: '{comcod:"' + comcod + '", zone:"' + zone + '", dist:"' + dist + '", thana:"' + thana + '", mouza:"' + mouza + '", csdhagno:"' + csdhagno + '"}',
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     success: function (response) {

                         var data = JSON.parse(response.d);
                         displayTable(data);

                     },


                     failure: function (response) {

                         alert("failure");
                     }
                 });

             }

             catch (e) {

                 alert(e.message);

             }
        }--%>


    </script>
    <style>
        .grvHeader, .grvFooter {
            font-size: 16px !important;
        }

        .grvRows {
            font-size: 12px !important;
        }
    </style>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            


            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">

                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">Zone</span>
                                        <%--<Label class=" lblmargin-top9px lblleftwidth80" >Zone</Label>--%>
                                        <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblzone" runat="server">Zone</label>--%>
                                    </div>

                                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="ddlPage120px" Style="width: 225px;" ClientIDMode="Static">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">District</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lbldistrict" runat="server">District</label>--%>
                                    <asp:DropDownList ID="ddldistrict" runat="server" ClientIDMode="Static" CssClass="ddlPage120px" Style="width: 225px;">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">Thana</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblthana" runat="server">Thana</label>--%>
                                    <asp:DropDownList ID="ddlthana" runat="server" CssClass="ddlPage120px" Style="width: 225px;" ClientIDMode="Static">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">Mouza</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblmouza" runat="server">Mouza</label>--%>
                                    <asp:DropDownList ID="ddlMouza" runat="server" CssClass="ddlPage120px" Style="width: 225px;" ClientIDMode="Static">
                                    </asp:DropDownList>

                                </div>
                            </div>



                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">C.S Dhagno</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblcsdhagno" runat="server">C.S Dhagno</label>--%>
                                    <asp:TextBox ID="txtcsdhagno" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">S.A Dhagno</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblsadhagno" runat="server">S.A Dhagno</label>--%>
                                    <asp:TextBox ID="txtsadhagno" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">R.S Dhagno</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblrsdhagno" runat="server">R.S Dhagno</label>--%>
                                    <asp:TextBox ID="txtrsdhagno" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">B.S Dhagno</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblbsdhagno" runat="server">B.S Dhagno</label>--%>
                                    <asp:TextBox ID="txtbsdhagno" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">B.S Khatian</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblbskhatina" runat="server">B.S Khatian</label>--%>
                                    <asp:TextBox ID="txtbskhatina" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">Land Owner</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lbllownername" runat="server">Land Owner</label>--%>
                                    <asp:TextBox ID="txtlownername" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">Father Name</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblfathername" runat="server">Father Name</label>--%>
                                    <asp:TextBox ID="txtfather" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">M. Name</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblmothername" runat="server">M. Name</label>--%>
                                    <asp:TextBox ID="txtmothername" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">Address</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lbladdress" runat="server">Address</label>--%>
                                    <asp:TextBox ID="txtaddress" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">

                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">Mobile</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblmobile" runat="server">Mobile</label>--%>
                                    <asp:TextBox ID="txtmobile" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">

                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">NID</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblnid" runat="server">NID</label>--%>
                                    <asp:TextBox ID="txtnid" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">

                            <div class="form-group">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text lblleftwidth80">Passport</span>
                                    </div>
                                    <%--<label class="control-label  lblmargin-top9px lblleftwidth80" id="lblPassport" runat="server">Passport</label>--%>
                                    <asp:TextBox ID="txtPassport" runat="server" CssClass="inputTextBox" Style="width: 225px;"></asp:TextBox>


                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-md-3 offset-9">

                            <div class="form-group">
                                <button id="lbtnShow" class=" form-control btn btn-primary" onclick="funShowData();" style="margin-left: 84px; width: 230px;">Show</button>
                                <button id="lnkconPrint" class=" form-control btn btn-primary"  style="margin-left: 84px; width: 230px;"><i class="fas fa-print"></i></button>                                
                            </div>
                        </div>


                    </div>
                    <div class="row">

                        <table id="gvlandinfo" class="table table-striped table-hover table-bordered grvContentarea">
                            <thead></thead>
                            <tbody></tbody>
                            <tfoot></tfoot>

                        </table>
                    </div>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
