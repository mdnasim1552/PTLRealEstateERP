<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PayrollLink.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.PayrollLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        div#ContentPlaceHolder1_ddlUserList_chzn {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
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

            $('.chzn-select').chosen({ search_contains: true });
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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">

                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">Company</asp:Label>
                                <asp:DropDownList ID="ddlUserList" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                             <asp:Panel ID="Panel2" runat="server" Visible="False">
                <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblConTrolCode" runat="server">Company Code</asp:Label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>


          


                    <%--                  <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblConTrolCode" runat="server" CssClass="lblTxt lblName">Company Code:</asp:Label>
                                    <asp:TextBox ID="txtCompSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <asp:LinkButton ID="ImgbtnFindComp" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindComp_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">
                                    </asp:DropDownList>

                                    <div class="pull-left">
                                        <asp:LinkButton ID="lbtnSelectSupl1" runat="server" CssClass="btn btn-primary primaryBtn pull-left" OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>

                                    </div>

                                </div>

                            </div>
                        </div>
                    </fieldset>--%>
                </div>
                <asp:GridView ID="gvPayrollLinkInfo" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                    AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                    OnRowDeleting="gvPayrollLinkInfo_RowDeleting">
                    <PagerSettings Visible="False" />
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ShowDeleteButton="True" />

                        <asp:TemplateField HeaderText="Company Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblgvCompCod" runat="server" Height="16px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <asp:Label ID="lblgvCompDesc" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                    Width="350px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" CssClass="btn btn-danger primaryBtn" >Final Update</asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF"
                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                    Style="text-align: left; background-color: Transparent"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                    Width="150px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="User Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblgvCompusrid" runat="server" Height="16px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />

                </asp:GridView>





            </asp:Panel>

                </div>
            </div>

            <%--            <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblUser1" runat="server" CssClass="lblTxt lblName">User Name</asp:Label>
                                        <asp:TextBox ID="txtUserSearch1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindUser1" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindUser1_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlUserList" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>

                                        <div class="pull-left">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblmsg1" CssClass="btn btn-danger primaryBtn" runat="server"></asp:Label>
                                    </div>

                                </div>
                            </div>--%>

   



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

