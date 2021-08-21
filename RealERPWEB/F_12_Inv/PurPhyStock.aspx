
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurPhyStock.aspx.cs" Inherits="RealERPWEB.F_12_Inv.PurPhyStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            var gv1 = $('#<%=this.gvRptResBasis.ClientID %>');
            gv1.Scrollable();
        }
    </script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

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
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtProjectSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindImpNo" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindImpNo_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control  inputTxt" style="width:336px">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblProjectName" runat="server" CssClass="form-control inputTxt" Visible="False" style="width:336px"></asp:Label>
                                    </div>
                                    <div class="col-md-1 pading5px">

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" Style="margin-left:-50px;">Ok</asp:LinkButton>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lgvPage" runat="server" CssClass="lblTxt lblName" Visible="false" Text="Page"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" Visible="false" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
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
                                    <div class="col-md-3 pading5px asitCol3">

                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" Width="600px"
                        AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnPageIndexChanging="gvRptResBasis_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                        Width="350px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkbtnUpdate_Click">Final Update</asp:LinkButton>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnTotal_Click">Total</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock Qty">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvStkQty" runat="server" CssClass="form-control"
                                        BorderWidth="1px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="100px"></asp:TextBox>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="100px" />
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







        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


