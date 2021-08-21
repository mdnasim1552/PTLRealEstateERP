<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccBankLimit.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccBankLimit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
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
                                <asp:Panel ID="pnlMain" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="lblBankName" runat="server" CssClass="lblName lblTxt" Text="Bank Name:"></asp:Label>

                                            <asp:TextBox ID="txtSrchBankName" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                         <asp:GridView ID="gvBankLimit" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="444px" OnRowDeleting="gvBankLimit_RowDeleting">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Bank Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBankCod" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbTotal" runat="server"  OnClick="lbTotal_Click"  CssClass="btn btn-danger primaryBtn"> Total </asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate" runat="server"  OnClick="lbtnFinalUpdate_Click"  CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBank" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loan Limit">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFamt" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbankamt" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
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



                                <%--<tr>
                                    <td class="style7">
                                        <asp:Label ID="lblBankName" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#000" style="text-align: left" Text="Bank Name:" 
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style8">
                                        <asp:TextBox ID="txtSrchBankName" runat="server" BorderStyle="None" 
                                            CssClass="txtboxformat" TabIndex="1" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style11">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" Height="16px" onclick="lbtnOk_Click" 
                                            style="color: #FFFFFF; text-align: center;" TabIndex="2" Width="29px">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblmsg" runat="server" ></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>--%>
                      
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


