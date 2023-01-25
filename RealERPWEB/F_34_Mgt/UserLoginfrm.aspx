<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UserLoginfrm.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.UserLoginfrm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.10.0/css/all.css" integrity="sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            $('#inputtextbox').attr('autocomplete', 'off');
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            $('.chzn-select').chosen({ search_contains: true });

            var gv = $('#<%=this.gvPermission.ClientID %>');
            gv.Scrollable();

            var gvUseForm = $('#<%=this.gvUseForm.ClientID %>');
            gvUseForm.Scrollable();


        }
        //User Modal
        function openUserModal() {
            $('#modalAddUser').modal('toggle');
        }
        function closeUserModal() {
            $('#modalAddUser').modal('hide');
        }
        function IsNumberWithOneDecimal(txt, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8)) {
                return false;
            } else {
                var len = txt.value.length;
                var index = txt.value.indexOf('.');
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    if ((len + 1) - index > 3) {
                        return false;
                    }
                }

            }
            return true;
        }

        function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvUseForm.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {

                rowData = tblData.rows[i].innerHTML;
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

       <%-- function Search_Gridview(strKey, cellNr, gvname) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                var tbldata;


                tblData = document.getElementById("<%=this.gvUseForm.ClientID %>");



                var rowData;
                for (var i = 0; i < tblData.rows.length; i++) {
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

            catch (e) {
                alert(e.message);

            }

        }--%>

        function openModal() {

            $('#myModal').modal('toggle');

        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');

        }

    </script>

    <style>
        .cl1 {
            color: blue;
        }

        .cl2 {
            font-size: 14px !important;
            color: #ff006e;
        }

        .modal-open .modal {
            overflow-y: hidden !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

            <div class="card card-fluid mt-2">

                <header class="card-header border-0">
                    <div class="d-flex align-items-center">
                        <span class="mr-auto">Al Users </span>

                        <div class="col-md-3">

                            <div class="input-group">
                                <asp:Label ID="lblId" CssClass="btn btn-info" runat="server" Visible="False" Text="User Name"></asp:Label>

                                <asp:Label ID="txtuserid" class="form-control" runat="server" Visible="False" Text="User Name"></asp:Label>

                            </div>
                        </div>

                        <div class="col-md-3">


                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="Label2" runat="server" CssClass="btn btn-secondary btn-sm">Search</asp:Label>
                                </div>
                                <asp:TextBox ID="inputtextbox" Style="height: 29px" runat="server" autocomplete="off" Text="" CssClass="form-control" placeholder="Search here..." onkeyup="Search_Gridview(this)"></asp:TextBox>

                            </div>
                            <div class="input-group" hidden="hidden">

                                <asp:TextBox ID="txtSrcName" runat="server" class="form-control" placeholder="Username" aria-label="Username"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindName_Click" TabIndex="9">
                                   <i class="fas fa-search"></i></asp:LinkButton>

                            </div>
                        </div>


                        <asp:LinkButton ID="lnkbtnAdd" runat="server" CssClass="btn btn-success" OnClick="lnkbtnAdd_Click" ToolTip="Add New User">
                            <i class="fas fa-plus-octagon"></i>&nbsp;Add New User</asp:LinkButton>

                    </div>
                </header>



                <div class="row table-responsive ">
                    <asp:GridView ID="gvUseForm" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        OnPageIndexChanging="gvUseForm_PageIndexChanging"
                        OnRowCancelingEdit="gvUseForm_RowCancelingEdit"
                        OnRowEditing="gvUseForm_RowEditing" OnRowUpdating="gvUseForm_RowUpdating">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:CommandField CancelText="Can" ShowEditButton="True" UpdateText="Up" Visible="false"  />
                            <asp:TemplateField HeaderText="User Id">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnUserId" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                        Width="50px" OnClick="lbtnUserId_Click"></asp:LinkButton>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvuserid" runat="server" BackColor="Transparent"
                                        BorderStyle="None" MaxLength="7" Width="50px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'></asp:TextBox>
                                </EditItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Short Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvusrShorName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtusrShorName" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Width="80px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'></asp:TextBox>
                                </EditItemTemplate>

                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Full Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvusrFullName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtusrFullName" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Width="120px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lgvDesig" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Width="120px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'></asp:TextBox>
                                </EditItemTemplate>


                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pass Word">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvpass" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Width="80px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrpass")) %>' TextMode="Password"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActive" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usractive"))=="True" %>' />
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                                    
                                                </EditItemTemplate>--%>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Email ID">
                                <ItemTemplate>
                                    <asp:Label ID="lgvWebmailID" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mailid")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtWebmailID" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Width="150px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mailid")) %>'></asp:TextBox>
                                </EditItemTemplate>


                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Web Mail PassWord">



                                <ItemTemplate>
                                    <asp:TextBox ID="TxtWebmailPWD" runat="server" BackColor="Transparent" TextMode="Password"
                                        BorderStyle="None" Width="80px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mailpass")) %>'></asp:TextBox>
                                </ItemTemplate>


                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Graph">
                                <ItemTemplate>
                                    <asp:Label ID="lgvrmrk" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvgvrmrk" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Width="80px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp ID">
                                <EditItemTemplate>
                                    <asp:Panel ID="Panel21" runat="server">
                                        <table style="width: 100%; display: none;">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtSrCentrid" runat="server" CssClass="inputtextbox" Width="30px"></asp:TextBox>
                                                </td>
                                                <td>

                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="ibtnSrchCentr" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnSrchCentr_Click" TabIndex="14"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </div>

                                                </td>
                                                <td>
                                                    <div class="col-md-4 pading5px">
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>
                                        <asp:DropDownList ID="ddlempid" runat="server" CssClass="form-control inputTxt chzn-select " Width="170px">
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCentrName" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                        Width="150px"></asp:Label>
                                    <asp:Label ID="lblempid" runat="server" Visible="false"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Emp" Visible="false">


                                <ItemTemplate>
                                    <asp:Label ID="lblgvempid" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="User Role">
                                <ItemTemplate>
                                    <asp:Label ID="lbgUserlId" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roledesc")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlUserRole" runat="server">
                                        <asp:ListItem Selected Value="3">User</asp:ListItem>
                                        <asp:ListItem Value="1">Admin</asp:ListItem>
                                        <asp:ListItem Value="2">Managment</asp:ListItem>
                                        <asp:ListItem Value="4">HR</asp:ListItem>
                                        <asp:ListItem Value="5">IT</asp:ListItem>


                                    </asp:DropDownList>
                                </EditItemTemplate>


                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <%--   <HeaderTemplate>
                                          <asp:TextBox ID="txtSearchUser" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Copy User Name" onkeyup="Search_Gridview(this,2,'gvUseForm')"></asp:TextBox>

                                       </HeaderTemplate>--%>

                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditUser" OnClick="lnkEditUser_Click" ToolTip="Edit Users" runat="server" CssClass="btn btn-xs btn-success"><i class="fas fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnLink" OnClick="lbtnLink_Click" ToolTip="Copy From " runat="server" CssClass="btn btn-xs btn-info"><i class="fas fa-copy"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" Width="100" />

                            </asp:TemplateField>


                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>
                </div>

                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="row">

                            <asp:Label ID="lblPage" runat="server" CssClass="col-1" Text="Page Size" Visible="false"></asp:Label>

                            <div class="col-1">

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" class="form-control"
                                    BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="400">400</asp:ListItem>
                                    <asp:ListItem Value="600">600</asp:ListItem>
                                    <asp:ListItem Value="900">900</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-1">
                                <asp:CheckBox ID="chkShowall" runat="server" AutoPostBack="True"
                                    Font-Bold="True"
                                    OnCheckedChanged="chkShowall_CheckedChanged" Text="Show All" CssClass="btn btn-primary checkBox" />
                            </div>

                            <div class="col-3">

                                <asp:DropDownList ID="ddlModuleName" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged" CssClass="form-control inputTxt">
                                    <asp:ListItem Value="04">&nbsp; A. Budgetary Control</asp:ListItem>
                                    <asp:ListItem Value="12">&nbsp; B. Inventory Control</asp:ListItem>
                                    <asp:ListItem Value="17">&nbsp; C. General Accounts </asp:ListItem>
                                    <asp:ListItem Value="22">&nbsp; D. Sales</asp:ListItem>
                                    <asp:ListItem Value="32">&nbsp; E. MIS Module</asp:ListItem>
                                    <asp:ListItem Value="35">&nbsp; F. Management Module</asp:ListItem>
                                    <asp:ListItem Value="45">&nbsp; A. Management Interface</asp:ListItem>
                                    <asp:ListItem Value="46">&nbsp; B. Group MIS</asp:ListItem>
                                    <asp:ListItem Value="00" Selected="True">All</asp:ListItem>

                                </asp:DropDownList>
                            </div>

                            <div class="col-2 pading5px">

                                <asp:LinkButton ID="lnkbtnBack" runat="server" CssClass="btn  btn-primary primaryBtn"
                                    OnClick="lnkbtnBack_Click">Back</asp:LinkButton>
                            </div>


                        </div>
                        <div class="row mt-2">

                            <asp:GridView ID="gvPermission" runat="server"
                                AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                OnRowDeleting="gvPermission_RowDeleting" ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Form Name" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvufrmname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmname")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Form id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvufrmid" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmid")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Query Type" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvQrytype" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qrytype")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <%--  <asp:Label ID="lgvDescription" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "dscrption").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "dscrption")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="280px"></asp:Label>--%>

                                            <asp:Label ID="Label1" runat="server"
                                                Text='<%# "<B>"+"<span class=cl2>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")) +"</span>"+ "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "frmdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+"<span class=cl1>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "frmdesc")).Trim() + "</span>" + "</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "dscrption").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "frmdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "dscrption")).Trim(): "")
                                                                    %>'
                                                Width="280px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpPer" runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpPer_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style22">Description</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td class="style23">&nbsp;</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Permission">
                                        <HeaderTemplate>
                                            <table style="width: 90px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text="Permission"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkallView_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPermit" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                Width="50px" />


                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entry or Edit ">
                                        <HeaderTemplate>
                                            <table style="width: 90px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Entry or Edit"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkallEntry" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkallEntry_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEntry" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entry"))=="True" %>'
                                                Width="50px" />
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View & Print">
                                        <HeaderTemplate>
                                            <table style="width: 90px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="View & Print"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkallPrint" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkallPrint_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPrint" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "printable"))=="True" %>'
                                                Width="50px" />
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Delete">
                                        <HeaderTemplate>
                                            <table style="width: 90px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblgvdelete" runat="server" Text="Delete"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkallDelete" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkallDelete_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDelete" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delete"))=="True" %>'
                                                Width="50px" />
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Check All">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkall_CheckedChanged" />
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                            </asp:GridView>
                            <asp:Label ID="lblusrid" runat="server" Visible="False"></asp:Label>





                        </div>
                    </asp:View>
                </asp:MultiView>


            </div>


            <%--Modal Copy Start--%>
            <div id="myModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title"><span class="fa fa-table"></span>&nbsp;Copy From</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" aria-label="Close"><i class="fas fa-times-circle"></i></button>

                        </div>


                        <div class="modal-body">
                            <div class="row-fluid">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="col-md-4" Font-Bold="true" Font-Size="20px">Select From : </asp:Label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlUser" CssClass="form-control chzn-select" runat="server"></asp:DropDownList>
                                        <asp:Label ID="fromUserid" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <br />

                            </div>
                        </div>
                        <div class="modal-footer ">

                            <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" OnClick="lblbtnSave_Click"><span class="glyphicon glyphicon-save-file"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>

            </div>
            <%--Modal Copy End--%>

            <%--Modal User Start--%>
            <div id="modalAddUser" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog ">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><span class="fa fa-table"></span>&nbsp;User Information</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" aria-label="Close"><i class="fas fa-times-circle"></i></button>

                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row mb-1">
                                <asp:Label ID="lblmUserId" runat="server" Visible="false"></asp:Label>
                                <div class="col-md-3">
                                    <label>User ID </label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmUesrId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Short Name</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmShortName" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Full Name</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmFullName" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Designation</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmDesignation" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Password</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmPassword" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="Password" onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Active</label>
                                </div>
                                <div class="col-md-9 mt-2">
                                    <asp:CheckBox ID="chkmUserActive" runat="server" />
                                </div>
                            </div>
                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Email ID</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmUserEmail" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Web Mail Pass.</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmWebMailPass" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="Password" onkeypress="return IsNumberWithOneDecimal(this,event);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Graph</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmGraph" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Employee ID</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlmEmpId" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>User Role</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlmUserRole" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1">Admin</asp:ListItem>
                                        <asp:ListItem Value="2">Managment</asp:ListItem>
                                        <asp:ListItem Selected Value="3">User</asp:ListItem>
                                        <asp:ListItem Value="4">HR</asp:ListItem>
                                        <asp:ListItem Value="5">IT</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


                                 <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Home Menu Link</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlMenuLink" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnSaveUser" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="closeUserModal();" OnClick="lbtnSaveUser_Click"><i class="fas fa-badge-check"></i>&nbsp;Update </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <%-- Usr Modal End--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
