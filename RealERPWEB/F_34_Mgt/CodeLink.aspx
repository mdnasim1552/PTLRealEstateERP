<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CodeLink.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.CodeLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });

        };
    </script>

       <style>
        .moduleItemWrpper{
            overflow:visible;
        }
    </style>

    <%--<div class="row">
            <div class="pagetitelWrp">
                <div class="col-md-8 col-lg-8">
                    <div class="pagetitel">
                        <asp:Label ID="lblTitle" CssClass="lblPageTitel" runat="server"
                            Text="Accounts Code"> 
                        </asp:Label>
                    </div>
                </div>
                <div class="col-md-3 col-lg-3 pull-right">
                    <asp:Label ID="lblprintstk" runat="server"></asp:Label>
                    <div class="input-group">
                        <asp:DropDownList ID="DDPrintOpt" runat="server" CssClass="form-control inputTxt">
                            <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                            <asp:ListItem Value="HTML">HTML</asp:ListItem>
                            <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                            <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                        </asp:DropDownList>

                        <span class="input-group-btn">
                            <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" CssClass="btn btn-success printBtn"><span class="glyphicon glyphicon-print asitGlyp" aria-hidden="true"></span> PRINT</asp:LinkButton>
                        </span>
                    </div>
                    <!-- /input-group -->
                </div>
                <div class="clearfix"></div>
            </div>
        </div>--%>





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">



                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" ddlPage" style="width:120px;"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 pading5px pull-right">
                                        <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>


                                </div>
                            </div>


                        </fieldset>
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="15"
                            OnPageIndexChanging="grvacc_PageIndexChanging" ShowFooter="True" BorderStyle="None" Width="724px">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />
                            <FooterStyle BackColor="#5F9467" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle />
                                    <ItemStyle ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" Account Code" HeaderStyle-Width="100px">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvactcode" runat="server" Style="text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Accounts">

                                    <HeaderTemplate>
                                        <table class="table-responsive">
                                            <tr>
                                                <td class="style63">
                                                    <asp:Label ID="Label8" runat="server" Text="Head of Accounts"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="style61">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <HeaderStyle />
                                </asp:TemplateField>







                                <asp:TemplateField HeaderText="Balance Sheet Description">
                                    <EditItemTemplate>
                                        <asp:Panel ID="pnlTeam" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                            BorderWidth="1px">


                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtSearchteam" Visible="false" runat="server" CssClass=" inputtextbox" TabIndex="4" Width="50px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="ibtnSrchteam" Visible="false" runat="server" OnClick="ibtnSrchteam_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlteam" runat="server" CssClass="chzn-select form-control  inputTxt"  TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcatdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")) %>'
                                            Width="320px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>







                            </Columns>

                            <RowStyle />
                            <EditRowStyle />
                            <SelectedRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            <AlternatingRowStyle BackColor="" />
                        </asp:GridView>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

