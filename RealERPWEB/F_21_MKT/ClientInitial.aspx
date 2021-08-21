<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ClientInitial.aspx.cs" Inherits="RealERPWEB.F_21_MKT.ClientInitial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });




        function loadModal() {

            $('#AddClient').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }

        function CloseModal() {
            $('#AddClient').modal('hide');


        }


        function loadModalAssign() {

            $('#AddAssign').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }


        function CloseModalAssign() {
            $('#AddAssign').modal('hide');


        }


        function pageLoaded() {

            try {

                $('.chzn-select').chosen({ search_contains: true });

                var gv = $('#<%=this.gvAdDetails.ClientID%>')

              
                gv.gridviewScroll({

                    width: 1300,
                    height: 420,
                    arrowsize: 30,
                    railsize: 16,
                    barsize: 8,
                    varrowtopimg: "../Image/arrowvt.png",
                    varrowbottomimg: "../Image/arrowvb.png",
                    harrowleftimg: "../Image/arrowhl.png",
                    harrowrightimg: "../Image/arrowhr.png",
                    freezesize: 0
                });

                $('#btnreset').click(function () {

                    $('#<%=this.txtname.ClientID%>').val("");
                    $('#<%=this.txtmobile.ClientID%>').val("");
                    $('#<%=this.txtemail.ClientID%>').val("");
                    $('#<%=this.txtinfo.ClientID%>').val("");
                });


                $('#AddClient').on('hide.bs.modal', function () {
                    $('#AddClient').removeData();

                });

                $('#datetimepicker1').datetimepicker();
                $('#datetimepicker2').datetimepicker();


                $('#txtmobile').keyup(function () {
                    var mobile = $(this).val();
                    var comcod = <%=this.GetCompCode()%>;              
                  

                    var usrid= $('#<%=this.lbluserid.ClientID%>').val().length==0?<%=this.GetfUsrid()%>:$('#<%=this.lbluserid.ClientID%>').val();
                 
                   
           
                  

                    if (mobile.length != 11) {

                       
                        return false;
                    }

                    if (!($.isNumeric(mobile))) {

                        alert("Mobile Number must be numeric");
                  
                        return false;
                    }

                  

                    $.ajax({
                        
                        url: "ClientInitial.aspx/CheckMobile",
                        type: "POST",
                        data: '{comcod:"'+comcod+'", usrid:"'+usrid+'", mobile:"' + mobile + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        //  async: false,
                        success: function (data) {
                          
                           
                           var jdata=JSON.parse(data.d);
                           console.log(jdata);
                           var  mesult=jdata.result;
                           if(!mesult)
                           {
                           
                               alert("Found Duplicate Mobile");
                           
                           }

                            
                           
                        }

                       

                    });
                });



               



            }
            catch (e) {

                alert(e.message)
            }

        }


        


        function Search_Gridview(strKey, cellNr) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=this.gvAdDetails.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {

                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;

            }
        }

        function funCheckValidation() {
            try {


                var name = $('#<%=this.txtname.ClientID%>').val();
                var mobile = $('#<%=this.txtmobile.ClientID%>').val();
                if (name.length == 0) {

                    alert("Name is not empty");
                    $('#<%=this.txtname.ClientID%>').focus();
                    return false;
                }


                if (mobile.length == 0) {

                    $('#<%=this.txtmobile.ClientID%>').focus();
                    alert("Mobile Number is not empty");
                    return false;
                }

                CloseModal();

                return true;



            }

            catch (e) {



            }



        }

    </script>
    <style>
        .contentPart .form-group {
            margin: 2px 0;
        }

        .ddlclass {
            width: 305px !important;
        }
    </style>
    <asp:UpdatePanel ID="uppnlclint" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
            <div class="container moduleItemWrpper">


                <div class="contentPart">
                    <div class="row">


                        <fieldset class="scheduler-border">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>

                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Cal2" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Cal3" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                        <asp:Label ID="lblmobno" runat="server" CssClass="smLbl_to" Text="Search By"></asp:Label>

                                        <asp:TextBox ID="txtmobno" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-success btn-xs" OnClick="lbtnSearch_OnClick"><i class="fa fa-search"></i></asp:LinkButton>
                                        <button type="button" class="btn  btn-success btn-sm pull-right" data-toggle="modal" data-target="#AddClient">Add Client</button>

                                    </div>



                                    <div class=" col-md-2  pading5px hidden">

                                        <asp:Label ID="lblPage" CssClass="smLbl_to" runat="server" Text="Size"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>



                                </div>

                            </div>
                        </fieldset>
                        <asp:GridView ID="gvAdDetails" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered table-responsive grvContentarea col-md-10" OnRowDataBound="gvAdDetails_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="S.L">

                                    <ItemTemplate>
                                        <asp:Label ID="serialno" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvusrid" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Call Centre">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvbranch" BackColor="Transparent" BorderStyle="None" runat="server" Width="140px" placeholder="Call Centre" onkeyup="Search_Gridview(this,2)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbranch" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brname")) %>'
                                            Width="140px" BackColor="Transparent" BorderStyle="None"></asp:Label>


                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Name">
                                    <HeaderTemplate>

                                        <asp:TextBox ID="txtgvName" BackColor="Transparent" BorderStyle="None" runat="server" Width="130px" SortExpression="name" placeholder="Name" onkeyup="Search_Gridview(this,3)"></asp:TextBox>


                                    </HeaderTemplate>



                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclname" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                            Width="130px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">

                                    <HeaderTemplate>

                                        <asp:TextBox ID="txtgmobile" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" SortExpression="mob" placeholder="Mobile" onkeyup="Search_Gridview(this,4)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclmob" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mob")) %>'
                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvemail" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" SortExpression="email" placeholder="Email" onkeyup="Search_Gridview(this,5)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclemail"
                                            runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                            Width="110px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="Location">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvlocation" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Location" onkeyup="Search_Gridview(this,6)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lbllocat" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locat")) %>'
                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        <%--<asp:DropDownList ID="ddllocat" runat="server" Width="150" CssClass="form-control inputTxt pull-left">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profession">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvprofession" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Profession" onkeyup="Search_Gridview(this,7)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblpro" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pro")) %>'
                                            Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>


                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lead Source">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvadvertise" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Lead Source" onkeyup="Search_Gridview(this,8)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbladvertise" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addesc")) %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="L.Quality">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvleadq" BackColor="Transparent" BorderStyle="None" runat="server" Width="50px" placeholder="L.Quality" onkeyup="Search_Gridview(this,9)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvleadq" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leaddesc")) %>'
                                            Width="50px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lead Status">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvleadst" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Lead Status" onkeyup="Search_Gridview(this,10)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvleadst" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadstatus")) %>'
                                            Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Interested Project">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvproject" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Interested Project" onkeyup="Search_Gridview(this,11)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvproject" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="120px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Description">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvfeedback" BackColor="Transparent" BorderStyle="None" runat="server" Width="140px" placeholder="Description" onkeyup="Search_Gridview(this,12)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvinfo"
                                            runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "info")) %>'
                                            Width="140px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>



                                <%-- <asp:TemplateField HeaderText="Assign">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvassign" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Assign" onkeyup="Search_Gridview(this,13)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvassign"
                                            runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignname")) %>'
                                            Width="100px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>--%>












                                <asp:TemplateField HeaderText="Size" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclsize" runat="server" Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "size")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send To" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclsendto" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sendto")) %>'
                                            Width="130px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnEdit" type="submit" CssClass="btn btn-sm btn-default" OnClick="lbtnEdit_OnClick" ToolTip="Edit"><i class=" fa fa-edit" ></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkassign" runat="server"
                                            Width="20px" />
                                    </ItemTemplate>

                                    <FooterTemplate>

                                        <asp:LinkButton ID="lnkbtnAssign" runat="server" OnClientClick="return FunOrdConfirm();"
                                            OnClick="lnkbtnAssign_Click" ToolTip="Assign"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                        <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                                    </FooterTemplate>





                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="branchcode" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbranchcode"
                                            runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "branch")) %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>
                </div>

                <%--Modal  --%>
                <%-- Add Client --%>
                <div id="AddClient" class="modal animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content  ">
                            <div class="modal-header">

                                <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>Add New Client  </h4>
                            </div>
                            <div class="modal-body form-horizontal">
                                <div class="row-fluid">



                                    <div class="row form-horizontal">
                                        <div class=" col-md-6">

                                            <div class="row hidden">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="txtdate">Date :</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtcurdate" runat="server" BorderStyle="None" Width="80px" CssClass="inputtextbox"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Cal1" runat="server" Format="dd-MMM-yyyy "
                                                            TargetControlID="txtcurdate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lblbranch">Branch Name </label>
                                                    </div>
                                                    <div class="col-md-9">


                                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control chzn-select ddlclass">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="txtname">Customer Name <span class="manField"><sup>*</sup></span> </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtname" class="form-control" runat="server" placeholder="Customer Name"></asp:TextBox>
                                                        <asp:Label ID="lblfuserid" runat="server" CssClass="lblTxt lblName" Style="display: none"></asp:Label>
                                                        <asp:Label ID="lbluserid" runat="server" CssClass="lblTxt lblName" Style="display: none"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="txtmob">Mobile No <span class="manField"><sup>*</sup></span>  </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtmobile" ClientIDMode="Static"    class="form-control" runat="server" placeholder="017XXXXXXXXX" MaxLength="11"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="txtmob">Alt Phone No  </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="TxtAltPHone" class="form-control" runat="server" placeholder="017XXXXXXXXX" MaxLength="11"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="txtmob">NRB Client No   </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="TxtNbrNo" class="form-control" runat="server" placeholder="NBR Registration No" MaxLength="11"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>


                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="txtemail">E-Mail </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtemail" class="form-control" runat="server" placeholder="you@example.com"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="form-group">

                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="ddlpro">Profession </label>
                                                    </div>
                                                    <div class="col-md-9">

                                                        <asp:DropDownList ID="ddlpro" runat="server" CssClass="form-control chzn-select ddlclass">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="ddllocation">Location</label>
                                                    </div>
                                                    <div class="col-md-9">


                                                        <asp:DropDownList ID="ddllocation" runat="server" CssClass="form-control chzn-select ddlclass">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row hidden">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="txtsize">Flat Size </label>
                                                    </div>
                                                    <div class="col-md-9">

                                                        <asp:TextBox ID="txtsize" class="form-control" runat="server" placeholder="2000"></asp:TextBox>


                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" runat="server" visible="False">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="txtsentto">Send To </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtsentto" class="form-control" runat="server" placeholder="Sales Person"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="ddlAd">Lead Source </label>
                                                    </div>
                                                    <div class="col-md-9">


                                                        <asp:DropDownList ID="ddlAd" runat="server" CssClass="form-control chzn-select ddlclass">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lbllq">Lead Quality </label>
                                                    </div>
                                                    <div class="col-md-9">


                                                        <asp:DropDownList ID="ddllead" runat="server" CssClass="form-control chzn-select ddlclass">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lbllst">Lead Status </label>
                                                    </div>
                                                    <div class="col-md-9">


                                                        <asp:DropDownList ID="ddlleadst" runat="server" CssClass="form-control chzn-select ddlclass">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                        <div class="col-md-6">


                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lblproject">Intested Project</label>
                                                    </div>
                                                    <div class="col-md-9">


                                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control chzn-select ddlclass">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lblproduct">Product Type</label>
                                                    </div>
                                                    <div class="col-md-9">


                                                        <asp:DropDownList ID="DdlProductType" runat="server" CssClass="form-control chzn-select ddlclass">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lblproduct">Requirment Size</label>
                                                    </div>
                                                    <div class="col-md-9">

                                                        <asp:TextBox ID="TxtReqSize" class="form-control" runat="server" placeholder="Requirement Size"></asp:TextBox>


                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lblproduct">Metting Date & Time</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <div class='input-group date' id='datetimepicker2'>
                                                            <input type='text' id="TxtMeetingdatetime" style="height: 30px" runat="server" class="form-control" />
                                                            <span class="input-group-addon">
                                                                <span class="small glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lblproduct">Visiting Date & Time</label>
                                                    </div>
                                                    <div class="col-md-9">

                                                        <div class='input-group date' id='datetimepicker1'>
                                                            <input type='text' id="TxtVisitdatetime" style="height: 30px" runat="server" class="form-control" />
                                                            <span class="input-group-addon">
                                                                <span class="small glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>

                                                        <%--<asp:TextBox ID="TxtVisitdatetime" class="form-control" runat="server" placeholder="Visiting Date & Time"></asp:TextBox>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lblproduct">Lead Create Dept Name</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ID="DdlCreateDept" runat="server" CssClass="form-control chzn-select ddlclass">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lblproduct">Company Name</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="TxtCompname" class="form-control" runat="server" placeholder="Company Name"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="lblproduct">Designation</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="TxtDesignation" class="form-control" runat="server" placeholder="Designation"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>



                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-3 field-label-responsive">
                                                        <label for="txtinfo">Description </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtinfo" class="form-control" runat="server" placeholder="Description" Height="45px" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>




                                        </div>
                                    </div>




                                </div>


                            </div>
                            <div class="modal-footer ">




                                <button type="button" id="btnreset" class="btn btn-sm btn-danger"><i class="fa fa-user-minus" aria-hidden="true"></i>Reset</button>


                                <asp:LinkButton runat="server" ID="lbtnAdd" type="submit" OnClientClick="return  funCheckValidation(); " CssClass="btn btn-sm btn-success" OnClick="lbtnAdd_Click"><i class="fa fa-user-plus" aria-hidden="true"></i> Add</asp:LinkButton>


                            </div>
                        </div>
                    </div>
                </div>


                <%-- Assign Team --%>

                <div id="AddAssign" class="modal animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content  ">
                            <div class="modal-header">

                                <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>Assign Team  </h4>
                            </div>
                            <div class="modal-body form-horizontal">
                                <div class="row-fluid">

                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-3 field-label-responsive">
                                                    <label for="ddlAd">Assign Team </label>
                                                </div>
                                                <div class="col-md-9">


                                                    <asp:DropDownList ID="ddlTeam" runat="server" CssClass="form-control chzn-select ddlclass">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>

                                    </div>




                                </div>


                            </div>
                            <div class="modal-footer ">







                                <asp:LinkButton runat="server" ID="lnkbtnAssignTeam" type="submit" OnClientClick="CloseModalAssign();" CssClass="btn btn-sm btn-success" OnClick="lnkbtnAssignTeam_Click"><i class="fa fa-save" aria-hidden="true"></i> Update</asp:LinkButton>


                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

