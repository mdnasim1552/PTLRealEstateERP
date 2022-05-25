﻿
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkInflowAndOutflow.aspx.cs" Inherits="RealERPWEB.F_17_Acc.LinkInflowAndOutflow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style29
        {
        }
        .txtboxformat
{
	border-style: none;
    border-color: inherit;
    border-width: medium;
    font-size: 12px;
	font-weight: normal;
	margin-right: 0px;
   text-align: left;
    margin-left: 0px;
}
        .style65
        {
            width: 14px;
        }
        .style64
        {
            width: 18px;
        }
        .style66
        {
            width: 54px;
        }
        .style67
        {
            width: 485px;
        }
        .style68
        {
            width: 221px;
        }
        .style69
        {
            width: 145px;
        }
    </style>
</asp:Content>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv = $('#<%=this.gvDPayment.ClientID %>');
            var gv1 = $('#<%=this.gvPayDetails.ClientID %>');
            var gv2 = $('#<%=this.gvCollDet.ClientID %>');
            var gv3 = $('#<%=this.gvPayDet.ClientID %>');
            var moncollect = $('#<%=this.gvMonCollect.ClientID %>');
            var gvMonPayment = $('#<%=this.gvMonPayment.ClientID %>');

            gv.Scrollable();
            gv1.Scrollable();
            gv2.Scrollable();
            gv3.Scrollable();
            moncollect.Scrollable();
            gvMonPayment.Scrollable();



        }

    </script>
    <%-- <table style="width: 98%;">
        <tr>
            <td class="style67">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="TRANSACTION STATEMENT  INFORMATION VIEW/EDIT" Width="500px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style68">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" 
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>--%>


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
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-12 pading5px ">
                                            <asp:Label ID="lblAsDate" runat="server" CssClass="lblTxt lblName" Width="300" Text="A. Sales"></asp:Label>
                                            <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page size :"></asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RadioButtonList ID="rbtPayment" runat="server" BackColor="#003366"
                                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black" RepeatDirection="Horizontal" Visible="False"
                                                Width="250px">
                                                <asp:ListItem>Summary</asp:ListItem>
                                                <asp:ListItem>Cost Wise</asp:ListItem>
                                                <asp:ListItem>Details</asp:ListItem>
                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="table table-responsive">
                                <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View ID="ViewCollVsClearance" runat="server">
                                        <asp:GridView ID="gvCollVsCleared" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False"
                                            OnPageIndexChanging="gvCollVsCleared_PageIndexChanging"
                                            OnRowDataBound="gvCollVsCleared_RowDataBound" ShowFooter="True"
                                            Style="text-align: left" Width="963px">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvDate" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rarcndate")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mr No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgMrno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCollProName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvParName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cresdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Cheque No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcollChqno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="right" />

                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="70px">Total:</asp:Label>
                                                    </FooterTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCollAmt" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFCollAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                    </FooterTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bank Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCollCactdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ccactdesc")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Bank Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvClCactdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clcactdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Voucher #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgVounum2" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clvounum")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClChqno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clchqno")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvFCTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="black" Style="text-align: right" Width="80px">Total:</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvFCNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px">Net Total:</asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </FooterTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="right" />




                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cleared from Current Received">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvclcuram" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clcuram")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvFCuamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="70px"> </asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </FooterTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cleared from Prev. Received">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClPramt" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clpream")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>

                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvFPreamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </FooterTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClPrjName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clactdesc")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClcutomername" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clresdesc")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MR No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvclmrno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clmrno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText=" Received Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvrecDate" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recdate")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>



                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </asp:View>
                                    <asp:View ID="ViewDailyPayment" runat="server">
                                        <asp:GridView ID="gvDPayment" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False"
                                            OnPageIndexChanging="gvDPayment_PageIndexChanging" ShowFooter="True"
                                            Style="text-align: left">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accounts Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvProCod" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accounts Head">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvProName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                                            Width="230px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgv" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="230px">Total</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Payment Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvPayAmt" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFPayAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                    </FooterTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>




                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </asp:View>
                                    <asp:View ID="ViewDailyPayDetails" runat="server">
                                        <asp:GridView ID="gvPayDetails" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvPayDetails_RowDataBound" ShowFooter="True" Width="658px">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCode" runat="server" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2")) %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText=" Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc2").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc2")).Trim(): "")  %>'
                                                            Width="350px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFTDrAmt" runat="server" ForeColor="Black" Font-Size="12px" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </asp:View>
                                    <asp:View ID="ViewCollDet" runat="server">
                                        <asp:GridView ID="gvCollDet" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" OnPageIndexChanging="gvCollDet_PageIndexChanging" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvCollDet_RowDataBound" ShowFooter="True"
                                            Style="text-align: left" Width="1052px">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MR. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvmrno" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "mrno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")).Trim() : "")
                                                                         
                                                                    %>'
                                                            Width="150px"> </asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvmrDate" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate"))%>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAccCod" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcUcode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acc. Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcPactdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="130px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name Of Client">

                                                    <FooterTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvFCDTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px">Total:</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvFCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px">Net Total:</asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </FooterTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcustname" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Flat No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvudesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvrmrks" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCheNo" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chq. Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvchqDate" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bank">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBaName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBranch" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cash Amount">

                                                    <FooterTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvFCashamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="70px"> </asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </FooterTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcashamt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque Amount">
                                                    <FooterTemplate>

                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvFChqamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lgvCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvchqamt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </asp:View>
                                    <asp:View ID="ViewDailyPayDet" runat="server">
                                        <asp:GridView ID="gvPayDet" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False"
                                            OnPageIndexChanging="gvPayDet_PageIndexChanging" ShowFooter="True"
                                            Style="text-align: left" OnRowDataBound="gvPayDet_RowDataBound">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvResCod" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                            Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accounts Head">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvResName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                            Width="300px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgv" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="300px">Total</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Payment Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvPayAmt" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFPayAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                    </FooterTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>




                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </asp:View>
                                    <asp:View runat="Server" ID="ViewMonCollection">



                                        <table style="width: 100%;">

                                            <tr>
                                                <td colspan="12">&nbsp;</td>
                                            </tr>

                                            <tr>
                                                <td colspan="12">
                                                    <asp:GridView ID="gvMonCollect" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" Width="616px" OnRowDataBound="gvMonCollect_RowDataBound">

                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNomon" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Description">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HygvResDesc" runat="server" Font-Underline="false" ForeColor="Black" Target="_blank"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px"></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvtoamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="75px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFtoamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="amt1">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt1" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt1" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt2" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt2" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt3">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt3" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt3" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt4">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt4" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt4" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt5">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt5" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt5" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt6">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt6" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt6" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt7">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt7" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt7" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt8">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt8" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt8" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt9">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt9" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt9" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt10">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt10" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt10" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt11">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt11" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt11" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt12">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamt12" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt12" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>

                                        </table>



                                    </asp:View>
                                    <asp:View runat="Server" ID="ViewMonPayment">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td colspan="12">
                                                    <asp:GridView ID="gvMonPayment" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" Width="616px" OnRowDataBound="gvMonPayment_RowDataBound">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNomonpay" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" Description">


                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvActdescmpay" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                                                     (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                                                     (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                                                     "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                                                     Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                                                        Width="250px"></asp:Label>
                                                                </ItemTemplate>


                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                                            </asp:TemplateField>






                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvtoamtmpay" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="75px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFtoamtmpay" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt1">
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay1" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay1" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay2" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay2" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt3">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay3" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay3" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt4">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay4" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay4" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt5">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay5" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay5" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt6">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay6" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay6" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt7">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay7" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay7" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt8">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay8" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay8" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt9">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay9" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay9" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt10">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay10" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay10" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt11">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay11" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay11" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="amt12">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvamtmpay12" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lgvFamtmpay12" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#333333" />
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>

                                        </table>
                                    </asp:View>
                                    <asp:View runat="Server" ID="ViewPaySummary">
                                        <asp:GridView ID="gvMonPaymentSumm" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvMonPaymentSumm_RowDataBound" ShowFooter="True"
                                            Width="616px">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlpsum" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Description">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HLgvDescpaysum" runat="server" __designer:wfdid="w38"
                                                            CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                                                     (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                                                     (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                                                     "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                                                     Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                                            Width="300px" Style="font-size: 11px; color: Black;"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvtoamtmpaysum" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtoamtmpaysum" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt1">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum1" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum1" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum2" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum2" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt3">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum3" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum3" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt4">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum4" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum4" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt5">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum5" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum5" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt6">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum6" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum6" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt7">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum7" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum7" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt8">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum8" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum8" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt9">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum9" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum9" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt10">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum10" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum10" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt11">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum11" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum11" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amt12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamtmpaysum12" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFamtmpaysum12" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </asp:View>

                                </asp:MultiView>
                            </div>
                    </div>
                </div>
            </div>


            <%--<tr>
                                    <td class="style29" width="100px">
                                        <asp:Label ID="lblAsDate" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Text="A. Sales" Width="300px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" style="color: #FFFFFF; text-align: right;" Text="Page Size:" 
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style65">
                                       
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" 
                                            style="margin-left: 0px" TabIndex="2" Width="85px">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
                                    <td align="left" class="style64">
                                         &nbsp;</td>
                                    <td align="left" class="style64">
                                         <asp:RadioButtonList ID="rbtPayment" runat="server" BackColor="#003366" 
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Black" RepeatDirection="Horizontal" Visible="False" 
                                            Width="250px">
                                            <asp:ListItem>Summary</asp:ListItem>
                                            <asp:ListItem>Cost Wise</asp:ListItem>
                                            <asp:ListItem>Details</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                    <td align="left" class="style66">
                                       
                                    </td>
                                    <td class="style4">
                                       
                                    </td>
                                    <td class="style69">
                                       
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
                                    <td>
                                        &nbsp;</td>
                                </tr>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

