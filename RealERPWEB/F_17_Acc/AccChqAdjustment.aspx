<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccChqAdjustment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccChqAdjustment" %>

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

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSearch" runat="server" CssClass="lblTxt lblName" Text="Search"></asp:Label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>


                                        <asp:LinkButton ID="ibtnFindRefNo" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindRefNo_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </fieldset>

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
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
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
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Name">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                        OnClick="lbtnUpdate_Click" CssClass="btn btn-danger primarygrdBtn">Update</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                        Width="150px"></asp:Label>
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
                                    <asp:Label ID="txtFTotal" runat="server" ForeColor="#000"></asp:Label>
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
                                    <asp:Label ID="lblgvappadjdate" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appadjdate")).ToString("dd-MMM-yyyy")  %>'
                                        Width="70px"></asp:Label>
                                   
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Adjustment Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvadjdate" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: Left; background-color: Transparent"
                                       Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adjdate")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adjdate")).ToString("dd-MMM-yyyy")) %>'
                                        Width="70px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtgvadjdate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvadjdate"></cc1:CalendarExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvremarks" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: Left; background-color: Transparent"
                                       Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                        Width="120px"></asp:TextBox>
                               
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
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


