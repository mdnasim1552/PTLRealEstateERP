<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjectCode.aspx.cs" Inherits="RealERPWEB.F_64_Mgt.ProjectCode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container moduleItemWrpper">
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

            };
        </script>
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


                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <asp:Label ID="LblBookName1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Select Code Book:"></asp:Label>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlCodeBook" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>


                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlCodeBookSegment" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Value="2">Main Code</asp:ListItem>
                                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Value="8">Sub Code-2</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="LblBookName2" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Search Option:"></asp:Label>

                                    <div class="col-md-2 pading5px">
                                        <asp:TextBox ID="txtsrch" runat="server" CssClass="form-control inputTxt"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success srearchBtn" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                    </div>
                                    <asp:Label ID="lblPage" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Page Size" Visible="False"></asp:Label>
                                    <div class="col-md-1 pading5px">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
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
                                    <div class="col-md-4 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                </div>

                                <div class="form-group">
                                </div>


                            </div>
                        </fieldset>
                        <div class="table-responsive">
                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" PageSize="15"
                                OnPageIndexChanging="grvacc_PageIndexChanging" ShowFooter="True" BorderStyle="None">
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
                                    <asp:TemplateField HeaderText=" " HeaderStyle-Width="30px">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcode" runat="server" CssClass="disabled"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" CssClass="disabled"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acc. Code" HeaderStyle-Width="100px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" CssClass="form-control inputTxt"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:TextBox>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode3")) %>'
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>


                                            <asp:HyperLink ID="lbgrcod" runat="server" ForeColor="Black" Font-Underline="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Accounts">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" CssClass="form-control inputTxt"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:TextBox>
                                        </EditItemTemplate>
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
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Level" HeaderStyle-Width="40">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgridlevel" runat="server" MaxLength="10" CssClass="form-control inputTxt"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                                Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" HeaderStyle-Width="80px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvType" runat="server" BorderStyle="Solid" CssClass="form-control inputTxt"
                                                TabIndex="4"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'></asp:TextBox>
                                            <br />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAccType" runat="server" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvTypeDesc" runat="server" CssClass="form-control inputTxt" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entry User Name" Visible="false">
                                        <EditItemTemplate>
                                            <asp:Label ID="tlblgvUsr" runat="server" MaxLength="100" CssClass="form-control inputTxt"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

