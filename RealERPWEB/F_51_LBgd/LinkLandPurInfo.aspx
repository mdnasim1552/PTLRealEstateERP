<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkLandPurInfo.aspx.cs" Inherits="RealERPWEB.F_51_LBgd.LinkLandPurInfo" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });



        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });



            $('.chzn-select').chosen({ search_contains: true });
        }


        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File uploaded";
            
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }



    </script>

    <style>
        #ContentPlaceHolder1_AsyncFileUpload1_ctl04 {
            display: none;
        }

        body {
            font-family: "Century Gothic";
        }


        .grvHeader {
            font-family: "Century Gothic" !important;
        }
    </style>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="page-inner">
                <!-- .page-title-bar -->
                <header class="page-title-bar">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item active"></li>
                        </ol>
                    </nav>
                </header>
                <!-- /.page-title-bar -->
                <!-- .page-section -->
                <div class="page-section">
                    <!-- .section-block -->
                    <div class="section-block">
                        <!-- .board -->
                        <div class="board board-list">
                            <!-- .tasks -->
                            <section class="tasks">
                                <!-- .task-header -->

                                <!-- /.task-header -->
                                <!-- .task-issue -->

                                <!-- .card -->
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row">

                                            <div class="col-md-6 col-lg-6 col-sm-6">
                                                <div class="row">
                                                    <div class="col-md-2 col-lg-2 col-sm-6">
                                                        <div class="form-group">
                                                            <label for="lblfrmdate" class="control-label lblmargin-top9px">Project Name</label>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-10 col-lg-10 col-sm-6">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="lblPactDesc" runat="server" CssClass="form-control inputTxt"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">

                                                    <div class="col-md-2 col-lg-2 col-sm-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="Customer" runat="server" CssClass="lblTxt lblName">Land Owner</asp:Label>

                                                        </div>
                                                    </div>

                                                    <div class="col-md-10 col-lg-10 col-sm-6">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="lblCustcode" runat="server" CssClass="form-control inputTxt"></asp:TextBox>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-md-6 col-lg-6 col-sm-6">
                                                <asp:Panel ID="pnlDocUpload" runat="server">

                                                    <div class="row">
                                                        <div class="col-md-2 col-lg-2 col-sm-6">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Doc Type"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 col-lg-4 col-sm-6 pading5px">
                                                            <asp:DropDownList ID="ddlDocType" runat="server" CssClass="form-control inputTxt">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2 col-lg-2 col-sm-6">
                                                            <cc1:AsyncFileUpload OnClientUploadError="uploadError" Width="100px"
                                                                OnClientUploadComplete="uploadComplete" runat="server"
                                                                ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                CompleteBackColor="White" CssClass=""
                                                                UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                OnUploadedComplete="FileUploadComplete" />
                                                            <asp:Image ID="imgLoader" runat="server" ImageUrl="~/images/Wait.gif" />
                                                        </div>
                                                        <div class="col-md-2 col-lg-2 col-sm-6">

                                                            <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success pull-left" Text="Show" OnClick="btnShowimg_Click" />
                                                            &nbsp;
                                       <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_Click" OnClientClick="return confirm('Really Do You want to Delete This Image?')" Visible="False" CssClass=" btn btn-danger">DELETE</asp:LinkButton>


                                                        </div>
                                                    </div>
                                                    <div class="row mt-3">
                                                        <div class="col-md-2 col-lg-2 col-sm-6">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Doc Name"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 col-lg-4 col-sm-6 pading5px">
                                                            <div class="form-group">

                                                                <asp:TextBox ID="txtdocsName" runat="server" CssClass="form-control"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 col-lg-4 col-sm-6 pading5px">
                                                            <asp:Label ID="lblMesg2" runat="server" ForeColor="red"></asp:Label>
                                                            <asp:Label ID="lblMesg" runat="server" ForeColor="Green"></asp:Label>

                                                        </div>
                                                    </div>

                                                </asp:Panel>
                                                <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>

                                            </div>



                                        </div>





                                    </div>
                                </div>

                                <div class="card">
                                    <!-- .card-body -->
                                    <div class="card-body">

                                        <div class="row">
                                            <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                                <LayoutTemplate>
                                                    <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <div class="col-xs-6 col-sm-1 col-md-1 listDiv" style="padding: 0 5px;">

                                                        <div id="EmpAll" runat="server">
                                                            <asp:Label ID="lblid" Visible="false" runat="server" Text='<%# Eval("id") %>'></asp:Label>

                                                            <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("docfiles") %>'></asp:Label>
                                                            <asp:Label ID="lpactcode" Visible="false" runat="server" Text='<%# Eval("actcode") %>'></asp:Label>
                                                            <asp:Label ID="lgcod" Visible="false" runat="server" Text='<%# Eval("gcod") %>'></asp:Label>
                                                            <asp:Label ID="llsircode" Visible="false" runat="server" Text='<%# Eval("sircode") %>'></asp:Label>

                                                            <a href="<%#this.ResolveUrl(Convert.ToString(DataBinder.Eval(Container.DataItem, "docfiles2")))%>" target="_blank" class="uploadedimg">
                                                                <div class="checkboxcls">
                                                                    <asp:CheckBox ID="ChDel" Style="margin: 0px 80px; padding: 0px;" runat="server" />
                                                                </div>
                                                                <asp:Image ID="GetImg" runat="server" CssClass="pop image img img-responsive img-thumbnail " Height="135px" />



                                                            </a>
                                                        </div>
                                                        <p>
                                                            <%# Eval("docname") %>
                                                        </p>
                                                    </div>
                                                </ItemTemplate>

                                            </asp:ListView>

                                            <div class="col-md-6" runat="server" id="pnlBaIn" visible="false">
                                                <div class="form-group">
                                                    <h5>Basic Information:</h5>
                                                    <asp:GridView ID="GvOwnerLand" runat="server" AutoGenerateColumns="False"
                                                        ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea" OnRowDataBound="GvOwnerLand_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="150px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvgph" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                                        Width="2px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Font-Bold="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvgval" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>

                                                                <ItemTemplate>

                                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Width="250px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1"))%>'></asp:TextBox>
                                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1"))%>'></asp:TextBox>
                                                                    <asp:Panel ID="Panegrd" runat="server">

                                                                        <div class="form-group">

                                                                            <asp:DropDownList ID="ddlval" runat="server" Width="250px" CssClass="custom-select chzn-select">
                                                                            </asp:DropDownList>


                                                                        </div>


                                                                    </asp:Panel>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                            <div class="col-md-6" runat="server" id="pnlPlIn" visible="false">
                                                <div class="form-group">
                                                    <h5>Land information:</h5>
                                                    <asp:GridView ID="gvplot" runat="server" AutoGenerateColumns="False"
                                                        ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvItmCodeplot" runat="server" ClientIDMode="Static"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="150px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvgph" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                                        Width="2px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Font-Bold="True" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvgval" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>

                                                                <ItemTemplate>

                                                                    <asp:TextBox ID="txtgvValplot" runat="server" BackColor="Transparent"
                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1"))%>'></asp:TextBox>
                                                                    <asp:TextBox ID="txtgvdValplot" runat="server" BackColor="Transparent"
                                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1"))%>'></asp:TextBox>

                                                                    <cc1:CalendarExtender ID="txtgvdValplot_CalendarExtender" runat="server"
                                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdValplot"></cc1:CalendarExtender>
                                                                    <asp:Panel ID="Panegrd" runat="server">
                                                                        <div class="form-group">
                                                                            <div class="col-md-12 pading5px">
                                                                                <asp:DropDownList ID="ddlvalZone" runat="server" OnSelectedIndexChanged="ddlvalZone_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnldist" runat="server">

                                                                        <div class="form-group">
                                                                            <div class="col-md-12 pading5px">
                                                                                <asp:DropDownList ID="ddlvald" runat="server" OnSelectedIndexChanged="ddlvald_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlTh" runat="server">

                                                                        <div class="form-group">
                                                                            <div class="col-md-12 pading5px">
                                                                                <asp:DropDownList ID="ddlvalTh" runat="server" OnSelectedIndexChanged="ddlvalTh_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                        </div>


                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pnlMoz" runat="server">

                                                                        <div class="form-group">
                                                                            <div class="col-md-12 pading5px">
                                                                                <asp:DropDownList ID="ddlvalMoz" runat="server" CssClass=" chzn-select form-control" Width="200px" TabIndex="2">
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                        </div>


                                                                    </asp:Panel>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                        </div>

                                        <div class="row text-center">
                                            <div class="col-md-12">
                                                <div class="form-group">

                                                    <asp:LinkButton ID="lUpdatPerInfo" Visible="false" runat="server" CssClass="btn btn-danger  btn-xs" OnClientClick="return confirm('Do You want to Update?');" OnClick="lUpdatPerInfo_Click">Update</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <%-- <div class="table-responsive">
                                            <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                OnRowDataBound="gvPersonalInfo_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                Width="2px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgval" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <FooterTemplate>
                                                            <div class="col-md-2">
                                                                <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatPerInfo_Click">Final Update</asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <input type="button" id="okbtn" onclick="javascript: window.close();" value="Close" class="btn btn-primary okBtn" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                                BorderStyle="Solid" BorderWidth="1px" Height="20px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="510px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>--%>
                                    </div>
                                    <!-- /.card-body -->
                                </div>
                                <!-- .card -->


                            </section>
                            <!-- /.tasks -->
                        </div>
                        <!-- /.board -->
                    </div>
                    <!-- /.section-block -->
                </div>
                <!-- /.page-section -->
            </div>






        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>



