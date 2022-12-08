<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPurchaseStatus02.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurchaseStatus02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
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
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
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
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




