
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccDetailsSchedule.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.AccDetailsSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style18 {
            width: 598px;
        }

        .style21 {
            width: 209px;
        }

        .style22 {
            width: 242px;
        }

        .style24 {
            width: 34px;
        }

        .style25 {
            width: 21px;
        }

        .style26 {
            width: 47px;
        }

        .style27 {
            width: 70px;
        }

        .style28 {
            width: 68px;
        }

        .style29 {
            width: 454px;
        }

        .style30 {
            width: 32px;
        }

        .style31 {
            width: 16px;
        }

        .style32 {
            height: 1px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<tr>
        <td colspan="11">--%>
    
   
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

                            <div class="form-group">
                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="lbldateRange" runat="server" CssClass="lblTxt lblName" Text="Form"></asp:Label>
                                    <asp:TextBox ID="txtFromdat" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtFromdat_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFromdat">
                                    </cc1:CalendarExtender>
                                    <div class="smLbl_to">

                                        <asp:Label ID="lblTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtTodat" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtTodat_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtTodat">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                    </div>

                                </div>



                                <div class="col-md-3 pading5px pull-right">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#FF3300" Text="Please Wait......" Width="157px"
                                                Style="color: #FFFF99"></asp:Label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblAcchead" runat="server" CssClass="lblTxt lblName" Text="Accounts Head"></asp:Label>
                                    <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="imgsearch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgsearch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlAccHeads" runat="server" CssClass="form-control inputTxt">
                                    </asp:DropDownList>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblreshead" runat="server" CssClass="lblTxt lblName" Text="Resource Head"></asp:Label>
                                    <asp:TextBox ID="txtSrcRes" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="imgsrcres" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgsrcres_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlResHead" runat="server" CssClass="form-control inputTxt">
                                    </asp:DropDownList>

                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblreportlevel" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                    <asp:DropDownList ID="ddlRptlbl" runat="server" CssClass=" ddlPage inputTxt">
                                        <asp:ListItem>Main</asp:ListItem>
                                    <asp:ListItem>Sub-1</asp:ListItem>
                                    <asp:ListItem>Sub-2</asp:ListItem>
                                    <asp:ListItem>Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:CheckBox ID="chkWiZeroBal" runat="server" CssClass="checkBox" Text="Without Zero" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2 pading5px asitCol2">
                                    <asp:Label ID="lbldateRange1" runat="server" CssClass="lblTxt lblName">Message</asp:Label>
                                </div>
                                <div class="col-md-5 pading5px">
                                    <asp:Label ID="lblmsg" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                           ShowFooter="True" Width="911px" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "subcode1").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Dr. &lt;br&gt; Cr. &lt;br&gt; Net" FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right">
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Description Of Details Head" Width="190px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdescryption" runat="server" CssClass="GridLebelL"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Amt." ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfopnamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing Amt." ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <div class="form-group">
                             <cc1:ListSearchExtender ID="Lis1" runat="server" QueryPattern="Contains"
                            TargetControlID="ddlAccHeads">
                        </cc1:ListSearchExtender>
                        </div>
                    </div>
                </div>
            </div>


           
        </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Content>

