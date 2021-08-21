<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccBillAdjustment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccBillAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
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
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-2 pading5px">

                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Receive Date"></asp:Label>
                                        <asp:TextBox ID="txtReceiveDate" runat="server" AutoPostBack="True"
                                            OnTextChanged="txtReceiveDate_TextChanged" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtReceiveDate"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass=" smLbl_to">Adjustment No</asp:Label>
                                        <asp:TextBox ID="txtBillno" runat="server" AutoCompleteType="Disabled" CssClass="smltxtBox"></asp:TextBox>


                                        <asp:Label ID="Label1" runat="server" CssClass="smLbl_to">Adjustment Amount</asp:Label>
                                        <asp:TextBox ID="txtBillAmount" AutoCompleteType="Disabled" runat="server" CssClass="inputtextbox" Style="text-align:right;"></asp:TextBox>

                                    </div>

                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblBankCode" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtsrchProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control inputTxt" TabIndex="2">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Nature Of Bill"></asp:Label>
                                        <asp:LinkButton ID="ibtnnature" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnnature_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <asp:DropDownList ID="ddlBillNature" runat="server" CssClass=" ddlPage" Width="220px" TabIndex="2">
                                        </asp:DropDownList>

                                    </div>
                                  

                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Party Name"></asp:Label>
                                        <asp:TextBox ID="txtSrhParty" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>


                                        <asp:LinkButton ID="ibtnFindParty" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindParty_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="form-control inputTxt" TabIndex="2">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Adjustment Date"></asp:Label>

                                        <asp:TextBox ID="txtadjdate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inpPixedWidth"
                                            TabIndex="12" AutoPostBack="True"
                                            OnTextChanged="txtadjdate_TextChanged"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtadjdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtadjdate"
                                            PopupButtonID="imgpaydate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:LinkButton ID="lbtnAddTable" runat="server"
                                            OnClick="lbtnAddTable_Click" CssClass="btn btn-primary primaryBtn"
                                            TabIndex="13">Add Table</asp:LinkButton>

                                        <asp:LinkButton ID="lbtnRefresh" runat="server"
                                            OnClick="lbtnRefresh_Click" CssClass="btn btn-primary primaryBtn margin5px">Refresh</asp:LinkButton>

                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                        <asp:Label ID="lblslnum" runat="server" Visible="False"></asp:Label>

                                    </div>


                                </div>

                                
                            </div>
                    </div>
                    <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        Width="831px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Issue #">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Received Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Bill Nature">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Project Name">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Party Name">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server"  OnClick="lbtnUpdate_Click" CssClass="btn  btn-danger primarygrdBtn" >Update</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="ref #">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvref" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bill Amt.">
                                <FooterTemplate>
                                    <asp:Label ID="txtFTotal" runat="server"  ForeColor="#000"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvbillamt" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                    VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Appx. Adjustment Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvadjdate" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "appadjdate")) %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
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



            <%--<table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="PnlBill" runat="server" Width="929px">
                            <table style="width: 100%; background: #cccc99">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" CssClass="lbltextColor"
                                            Text="Receive Date:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style82">
                                        <asp:TextBox ID="txtReceiveDate" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            TabIndex="1" Width="100px" AutoPostBack="True"
                                            OnTextChanged="txtReceiveDate_TextChanged"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtReceiveDate"
                                            PopupButtonID="imgrecdate"></cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Image ID="imgrecdate" runat="server" Height="16"
                                            ImageUrl="~/Image/calender.png" Width="16" />
                                    </td>
                                    <td class="style83">
                                        <asp:Label ID="Label22" runat="server" CssClass="lbltextColor" Text="Bill No:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <asp:TextBox ID="txtBillno" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            TabIndex="2" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" CssClass="lbltextColor"
                                            Text="Bill Amount:" Width="100px"></asp:Label>
                                    </td>
                                    <td align="center">&nbsp;</td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtBillAmount" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            TabIndex="3" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style75">&nbsp;</td>
                                    <td class="style75"></td>
                                    <td class="style75"></td>
                                    <td class="style75"></td>
                                </tr>
                                <tr>
                                    <td class="style59">
                                        <asp:Label ID="Label7" runat="server" CssClass="lbltextColor"
                                            Text="Project:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style60">
                                        <asp:TextBox ID="txtsrchProject" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            TabIndex="4" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style59">
                                        <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindProject_Click"
                                            TabIndex="5" />
                                    </td>
                                    <td class="style59" colspan="2">
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="ddl"
                                            Font-Bold="True" Font-Size="12px" TabIndex="6" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style59">
                                        <asp:Label ID="Label30" runat="server" CssClass="lbltextColor"
                                            Text="Nature Of Bill:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style59">
                                        <asp:ImageButton ID="ibtnnature" runat="server" Height="18px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnnature_Click" TabIndex="7" />
                                    </td>
                                    <td class="style61">
                                        <asp:DropDownList ID="ddlBillNature" runat="server" CssClass="ddl"
                                            Font-Bold="True" Font-Size="12px" TabIndex="8" Width="220px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style73">&nbsp;
                                    </td>
                                    <td class="style73">&nbsp;</td>
                                    <td class="style73">&nbsp;</td>
                                    <td class="style73">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style62">
                                        <asp:Label ID="Label29" runat="server" CssClass="lbltextColor"
                                            Text="Party Name:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style63">
                                        <asp:TextBox ID="txtSrhParty" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" CssClass="ddl"
                                            TabIndex="9" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style62">
                                        <asp:ImageButton ID="ibtnFindParty" runat="server" Height="18px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindParty_Click"
                                            TabIndex="10" />
                                    </td>
                                    <td class="style84" colspan="2">
                                        <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="ddl"
                                            Font-Bold="True" Font-Size="12px" TabIndex="11" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style62">
                                        <asp:Label ID="Label26" runat="server" CssClass="lbltextColor"
                                            Text="Appx Pay Date:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style62">
                                        <asp:Image ID="imgpaydate" runat="server" Height="16"
                                            ImageUrl="~/Image/calender.png" Width="16" />
                                    </td>
                                    <td class="style64"></td>
                                    <td class="style74"></td>
                                    <td class="style74">&nbsp;</td>
                                    <td class="style74">&nbsp;</td>
                                    <td class="style74">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style66">
                                        <asp:LinkButton ID="lbtnRefresh" runat="server" BackColor="#003366"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px"  ForeColor="#000"
                                            OnClick="lbtnRefresh_Click" Style="font-size: small; height: 17px;"
                                            TabIndex="14" Width="60px">Refresh</asp:LinkButton>
                                    </td>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style4"></td>
                                    <td class="style43"></td>
                                    <td class="style43"></td>
                                    <td class="style76"></td>
                                    <td class="style75"></td>
                                    <td class="style75"></td>
                                    <td class="style75"></td>
                                    <td class="style75"></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


