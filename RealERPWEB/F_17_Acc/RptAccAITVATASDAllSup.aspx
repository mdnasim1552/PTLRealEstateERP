<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccAITVATASDAllSup.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccAITVATASDAllSup" %>

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
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>

                                    </div>
                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccResCode" runat="server" CssClass="lblTxt lblName" Text="Resource"></asp:Label>
                                        <asp:TextBox ID="txtSrchRes" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlConAccResHead" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-2 pading5px">

                                        <asp:LinkButton ID="lbtnOk0" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>



                                    </div>


                                </div>

                                

                                


                            </div>
                        </fieldset>                        
                    </div>
                    <asp:GridView ID="gvaitvsd" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="518px" OnRowDataBound="gvaitvsd_RowDataBound">
                            <PagerSettings Position="Top" />
                            <RowStyle />

                            <Columns>
                                <asp:TemplateField HeaderText="Description">
                                      <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblheader" runat="server" Text="Description"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"  CssClass="btn btn-danger btn-xs fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                    
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Text="Total :"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>


                                        <asp:HyperLink ID="HLgvDescription" runat="server" Font-Size="12px" Target="_blank" __designer:wfdid="w38" Style="color: Black; text-decoration: none;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="200px"></asp:HyperLink>



                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Opening">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOpAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFOpAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvClAmount" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFClsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle VerticalAlign="Top" />
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

            <table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <%--<asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="lblDate" runat="server" CssClass="label2" Text="From:"
                                            Width="110px" Style="text-align: left;"></asp:Label>
                                    </td>
                                    <td class="style8">
                                        <asp:TextBox ID="txtDateFrom" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateFrom"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style23">
                                        <asp:Label ID="lbltoDate" runat="server" CssClass="label2" Text="To:"></asp:Label>
                                    </td>
                                    <td class="style26">
                                        <asp:TextBox ID="txtDateto" runat="server" BorderStyle="None" TabIndex="1"
                                            Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style15">
                                        <asp:Label ID="lblcontrolAccResCode" runat="server" CssClass="label2"
                                            Height="16px" Text="Resource:" Width="110px" Style="text-align: left;"></asp:Label>
                                    </td>
                                    <td class="style11">
                                        <asp:TextBox ID="txtSrchRes" runat="server" AutoCompleteType="Disabled"
                                            BorderStyle="None" Height="17px" TabIndex="2" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:ImageButton ID="ibtnFindRes" runat="server" Height="16px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnFindRes_Click"
                                            Style="width: 16px" TabIndex="3" />
                                    </td>
                                    <td class="style27">
                                        <asp:DropDownList ID="ddlConAccResHead" runat="server" Font-Bold="True"
                                            ForeColor="Black" Height="19px" TabIndex="4" Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlConAccResHead_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlConAccResHead">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td class="style13">
                                        <asp:LinkButton ID="lbtnOk0" runat="server" BackColor="#000066"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnOk_Click"
                                            Style="text-align: center" TabIndex="5" Width="50px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style13"></td>
                                    <td class="style13"></td>
                                    <td class="style13"></td>
                                    <td class="style13"></td>
                                    <td class="style13"></td>
                                    <td class="style13"></td>
                                    <td class="style13"></td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        
                    </td>
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
                    <td>`</td>
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

