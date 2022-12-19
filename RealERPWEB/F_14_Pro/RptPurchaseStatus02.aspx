<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPurchaseStatus02.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurchaseStatus02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <style type="text/css">
        .multiselect {
            width: 300px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 300px !important;
        }

        .multiselect-container {
            height: 350px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 300px !important;
        }

        .form-control {
            height: 34px;
        }
    </style>
    <style type="text/css">
        #Panel2 {
            width: 300px;
        }

        #DropCheck1 {
            width: 200px;
        }

        #ctl00$ContentPlaceHolder1$DropCheck1Parent {
            top: 0 !important;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $(function () {
                $('[id*=chkPrjName]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                });
                $('[id*=chkSupName]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                });
            });
            var gvPurStatus = $('#<%=this.gvPurStatus.ClientID%>');
            gvPurStatus.gridviewScroll({
                width: 1160,
                height: 420,
                barsize: 8,
                startHorizontal: 3,
                wheelstep: 10,
                arrowsize: 30,
                railsize: 16,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
            });
        };
    </script>

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblProjectName" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control inputTxt"
                                                OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblMaterial" runat="server" CssClass="lblTxt lblName">Material</asp:Label>
                                            <asp:TextBox ID="txtSrcMaterials" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnFindMaterials" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindMaterials_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black"
                                            MaxDropDownHeight="200" TabIndex="7" TransitionalMode="True" Width="390px">
                                        </cc1:DropCheck>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Visible="False">Page</asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage inputTxt"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                                Width="80px" TabIndex="8">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                                <asp:ListItem Value="600">600</asp:ListItem>
                                                <asp:ListItem Value="900">900</asp:ListItem>
                                                <asp:ListItem Value="1500">1500</asp:ListItem>
                                                <asp:ListItem Value="3000">3000</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>

                        <asp:Panel ID="pnldaywpur" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblFdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                            <asp:TextBox ID="txtFdate" runat="server" CssClass="inputTxt inputName inpPixedWidth" autocomplete="off" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFdate"></cc1:CalendarExtender>
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblTodate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                            <asp:TextBox ID="txtTodate" runat="server" CssClass="inputTxt inputName inpPixedWidth" autocomplete="off" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-4">

                                            <asp:Label ID="lblproj" runat="server" CssClass="form-label" Text="Project   : " Font-Size="Medium"></asp:Label>
                                            <asp:ListBox ID="chkPrjName" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>

                                        <%--<div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblProjectName2" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                            <asp:TextBox ID="txtSrcProject2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnFindProject2" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindProject2_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlProjectName2" runat="server" CssClass="chzn-select form-control inputTxt" Visible="false"
                                                OnSelectedIndexChanged="ddlProjectName2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <cc1:DropCheck ID="drpchkproj" runat="server" BackColor="Black"
                                                MaxDropDownHeight="200" TabIndex="7" TransitionalMode="True" Width="390px">
                                            </cc1:DropCheck>
                                        </div>--%>
                                        <div class="col-md-3">
                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="ltbnOk2" runat="server" CssClass="btn btn-primary okBtn" OnClick="ltbnOk2_Click">Ok</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-4">

                                            <asp:Label ID="lblSupplier" runat="server" CssClass="form-label" Text="Supplier : " Font-Size="Medium"></asp:Label>
                                            <asp:ListBox ID="chkSupName" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>

                                        <%--<div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblSupName" runat="server" CssClass="lblTxt lblName">Supplier</asp:Label>
                                            <asp:TextBox ID="txtSrcSup" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="imgbtnFindSup" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindSup_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <cc1:DropCheck ID="drpchksup" runat="server" BackColor="Black"
                                            MaxDropDownHeight="200" TabIndex="7" TransitionalMode="True" Width="390px">
                                        </cc1:DropCheck>--%>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage2" runat="server" CssClass="lblTxt lblName" Visible="False">Page</asp:Label>
                                            <asp:DropDownList ID="ddlpagesize2" runat="server" AutoPostBack="True" CssClass="ddlPage inputTxt"
                                                OnSelectedIndexChanged="ddlpagesize2_SelectedIndexChanged" Visible="False"
                                                Width="80px" TabIndex="8">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                                <asp:ListItem Value="600">600</asp:ListItem>
                                                <asp:ListItem Value="900">900</asp:ListItem>
                                                <asp:ListItem Value="1500">1500</asp:ListItem>
                                                <asp:ListItem Value="3000">3000</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewResSummary" runat="server">
                                <asp:GridView ID="gvPurSum" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvPurSum_PageIndexChanging"
                                    ShowFooter="True" Width="734px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectdesc0" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtS" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="White" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="viewSupplier" runat="server">

                                <asp:GridView ID="gvPurStatus" runat="server" AllowPaging="false" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvPurStatus_PageIndexChanging" ShowFooter="True" Width="831px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Desc." Width="110px"></asp:Label>

                                                <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                                </asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectdesc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Chalan No ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvchalano" Style="word-break: break-all" runat="server" Width="120px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chalanno"))  %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MRR Date ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrrDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="72px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrfNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%--  <asp:TemplateField HeaderText="Chalan No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgchlno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno")) %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Req No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdref" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBillNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Bill Ref">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbillref" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="135px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFUnit" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px">Total : </asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpecifi" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrsumpname" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry User">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>


                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




