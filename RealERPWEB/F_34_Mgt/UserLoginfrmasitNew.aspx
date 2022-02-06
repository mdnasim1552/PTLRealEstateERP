<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UserLoginfrmasitNew.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.UserLoginfrmasitNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            //$("input, select").bind("keydown", function (event) {
            //    var k1 = new KeyPress();
            //    k1.textBoxHandler(event);

            //});

            var gv = $('#<%=this.gvPermission.ClientID %>');
            gv.Scrollable();
        }
    </script>




     <style>
        .clm0 {
            font-size: 16px !important;
            color: #a93c1c;
            font-weight: bold;
        }

        .cl0 {
            font-size: 15px !important;
            color: #f6350c;
            font-weight: bold;
            padding-left: 5px;
        }

        .cl1 {
            font-size: 14px !important;
            color: #ff7900;
            font-weight: bold;
            padding-left: 15px;
        }

        .cl2 {
            font-size: 13px !important;
            color: #006fa2;
            padding-left: 25px;
            font-weight: bold;
        }

        .cl3 {
            font-size: 12px !important;
            color: #000;
            font-weight: bold;
            padding-left: 35px;
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
            <div class="page-inner">
                <!-- .page-title-bar -->
                <header class="page-title-bar">


                    <!-- /floating action -->
                    <!-- title and toolbar -->
                    <div class="d-md-flex align-items-md-start">
                        <h1 class="page-title mr-sm-auto">Add Users </h1>
                        <!-- .btn-toolbar -->

                        <!-- /.btn-toolbar -->
                    </div>
                    <!-- /title and toolbar -->
                </header>
                <!-- /.page-title-bar -->
                <!-- .page-section -->
                <div class="page-section">
                    <!-- .card -->
                    <section class="card card-fluid pb-6">
                        <!-- .card-header -->
                        <header class="card-header">

                            <div class="row">
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtSrcName" runat="server" CssClass=" form-control "></asp:TextBox>

                                </div>
                                <div class="col-md-1">
                                    <asp:LinkButton ID="ibtnFindName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindName_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span>Search</asp:LinkButton>

                                </div>
                                <div class="col-md-3">

                                    <asp:Label ID="lblId" CssClass=" lblName" runat="server" Visible="False" Text="User Name"></asp:Label>
                                    <asp:Label ID="txtuserid" CssClass=" lblName" runat="server" Visible="False" Text="User Name"></asp:Label>

                                </div>

                                <div class="col-md-3 pading5px asitCol3 pull-right">
                                    <div class="msgHandSt">
                                        <asp:Label ID="lblMsg" CssClass="btn-danger primaryBtn btn disabled" runat="server" Visible="false"></asp:Label>
                                    </div>


                                </div>
                            </div>

                        </header>

                        <div class="card-body">
                            <div class="row">
                                <asp:GridView ID="gvUseForm" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="918px" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvUseForm_PageIndexChanging"
                                    OnRowCancelingEdit="gvUseForm_RowCancelingEdit"
                                    OnRowEditing="gvUseForm_RowEditing" OnRowUpdating="gvUseForm_RowUpdating"
                                    PageSize="100">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField CancelText="Can" ShowEditButton="True" UpdateText="Up" />
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
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtusrShorName" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Width="120px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'></asp:TextBox>
                                            </EditItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Full Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusrFullName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="120px"></asp:Label>
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
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Width="120px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'></asp:TextBox>
                                            </EditItemTemplate>


                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pass Word" Visible="false">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvpass" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Width="140px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrpass")) %>' TextMode="Password"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Active" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usractive"))=="True" %>' />
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                    
                                                </EditItemTemplate>--%>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrmrk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvgvrmrk" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Width="120px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'></asp:TextBox>
                                            </EditItemTemplate>
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
                            </div>

                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <header class="card-header">
                                        <div class="row">
                                            <div class="col-md-1 col-sm-1">
                                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Page Size" Visible="false"></asp:Label>

                                            </div>
                                            <div class="col-md-1 col-sm-1">

                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" form-control"
                                                    BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                                   
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                    <asp:ListItem Value="900">900</asp:ListItem>
                                                    <asp:ListItem Value="1500">1500</asp:ListItem>
                                                    <asp:ListItem Value="1500">2500</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-3">

                                                 <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="form-control">
                                                    <asp:ListItem Value="0">All </asp:ListItem>
                                                   
                                                     <asp:ListItem Value="2">Component</asp:ListItem>
                                                    <asp:ListItem Value="4">Widget</asp:ListItem>
                                                    <asp:ListItem Value="3">Graph </asp:ListItem>
                                                    

                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlModuleName" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged" CssClass="form-control d-none">
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
                                             <div class="col-md-2">

                                                <asp:LinkButton ID="lnkbtnBack" runat="server" CssClass="btn  btn-primary primaryBtn"
                                                    OnClick="lnkbtnBack_Click">Back</asp:LinkButton>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:CheckBox ID="chkShowall" runat="server" AutoPostBack="True"
                                                    Font-Bold="True"
                                                    OnCheckedChanged="chkShowall_CheckedChanged" Text="Show All" CssClass="btn btn-primary checkBox d-none" />
                                            </div>

                                            

                                           
                                            <div class="col-md-2 pull-right">
                                                <asp:Label ID="lblMsg1" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </header>
                                    <div class="row">

                                        <asp:GridView ID="gvPermission" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnPageIndexChanging="gvPermission_PageIndexChanging"
                                            OnRowDeleting="gvPermission_RowDeleting" ShowFooter="True" OnRowDataBound="gvPermission_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>
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
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qrytype")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" ForeColor="Blue"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "step")) %>'></asp:Label>
                                                        <asp:Label ID="lgvufrmname" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmname")) %>'></asp:Label>

                                                         <asp:Label ID="lblmenuparentid" runat="server" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "menuparentid")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpPer" runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpPer_Click">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                                    </HeaderTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Permission">
                                                    <HeaderTemplate>

                                                        <asp:Label ID="Label3" runat="server" Text="Permission"></asp:Label>

                                                        <asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkallView_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkPermit" runat="server"
                                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                            Width="50px" />
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entry or Edit </Br> or Cancel" Visible="false">
                                                    <HeaderTemplate>
                                                        <table style="width: 90px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text="Entry or Edit </Br> or Cancel"></asp:Label>
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
                                                <asp:TemplateField HeaderText="View & Print" Visible="false">
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
                                                <asp:TemplateField HeaderText="Check All" Visible="false">
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

                    </section>
                    <!-- /.card -->
                </div>
                <!-- /.page-section -->
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

