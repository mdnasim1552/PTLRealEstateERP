<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LandSearch.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LandSearch" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            //alert("I m IN");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {


            try {

                $('.chzn-select').chosen({ search_contains: true });
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

            }

            catch (e) {

                alert(e.message)
            }


        }


       

     

        function funZone() {

            try {


                var comcod =<%=this.GetComeCode()%>;

                $.ajax({
                    type: "POST",
                    url: "LandSearch.aspx/GetZone",
                    data: '{comcod:"' + comcod +'"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var data = JSON.parse(response.d); 
                        var ddlzone = $('#ddlZone') ;
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

               
                var comcod =<%=this.GetComeCode()%>;
                var zone = $('#ddlZone option:selected').val();
              
                $.ajax({
                    type: "POST",
                    url: "LandSearch.aspx/GetDistrict",
                    data: '{comcod:"' + comcod + '", zone:"' + zone+ '"}',
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


                var comcod =<%=this.GetComeCode()%>;
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


                var comcod =<%=this.GetComeCode()%>;
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
        

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>


            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblzone" runat="server">Zone</label>
                                <asp:DropDownList ID="ddlZone" runat="server" CssClass="ddlPage120px" Style="width: 230px;" ClientIDMode="Static">
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lbldistrict" runat="server">District</label>
                                <asp:DropDownList ID="ddldistrict" runat="server" ClientIDMode="Static" CssClass="ddlPage120px" Style="width: 230px;" >
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblthana" runat="server">Thana</label>
                                <asp:DropDownList ID="ddlthana" runat="server" CssClass="ddlPage120px" Style="width: 230px;" ClientIDMode="Static">
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblmouza" runat="server">Mouza</label>
                                <asp:DropDownList ID="ddlMouza" runat="server" CssClass="ddlPage120px" Style="width: 230px;" ClientIDMode="Static">
                                </asp:DropDownList>

                            </div>
                        </div>



                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblcsdhagno" runat="server">C.S Dhagno</label>
                                <asp:TextBox ID="txtcsdhagno" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblsadhagno" runat="server">S.A Dhagno</label>
                                <asp:TextBox ID="txtsadhagno" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblrsdhagno" runat="server">R.S Dhagno</label>
                                <asp:TextBox ID="txtrsdhagno" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblbsdhagno" runat="server">B.S Dhagno</label>
                                <asp:TextBox ID="txtbsdhagno" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblbskhatina" runat="server">B.S Khatian</label>
                                <asp:TextBox ID="txtbskhatina" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lbllownername" runat="server">Land Owner</label>
                                <asp:TextBox ID="txtlownername" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblfathername" runat="server">Father Name</label>
                                <asp:TextBox ID="txtfather" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblmothername" runat="server">M. Name</label>
                                <asp:TextBox ID="txtmothername" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>

                    </div>
                    <div class="row">
                         <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lbladdress" runat="server">Address</label>
                                <asp:TextBox ID="txtaddress" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                             </div>
                        <div class="col-md-3">

                              <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblmobile" runat="server">Mobile</label>
                                <asp:TextBox ID="txtmobile" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>
                         <div class="col-md-3">

                              <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblnid" runat="server">NID</label>
                                <asp:TextBox ID="txtnid" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>
                         <div class="col-md-3">

                              <div class="form-group">
                                <label class="control-label  lblmargin-top9px lblleftwidth80" id="lblPassport" runat="server">Passport</label>
                                <asp:TextBox ID="txtPassport" runat="server" CssClass="inputTextBox" Style="width: 230px;"></asp:TextBox>


                            </div>
                        </div>


                    </div>

                      <div class="row">
                          <div class="col-md-3 offset-9">

                              <div class="form-group">
                                 <asp:LinkButton ID="lbtnShow" runat="server" CssClass=" form-control btn btn-primary" Style=" margin-left:84px;width:230px;" OnClick="lbtnShow_Click">Show</asp:LinkButton>


                            </div>
                        </div>
                      
                      
                      </div>

                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
