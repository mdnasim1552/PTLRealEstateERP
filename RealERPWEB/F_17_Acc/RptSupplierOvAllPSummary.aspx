<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSupplierOvAllPSummary.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptSupplierOvAllPSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <%-- <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 300px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 300px !important;
        }

        .multiselect-container {
            height: 350px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 300px !important;
        }

        .form-control {
            height: 34px;
        }
    </style>--%>

    <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .lnkbtmfromtop {
            margin-top: 28px;
            margin-left: 5px;
        }

        .srchbtmfromtop {
            margin-top: 5px !important;
        }

        .grvContentarea {
        }
    </style>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
          <%--  $('#<%=this.gvsupstatus.ClientID%>').tblScrollable();--%>
            $(function () {
                $('[id*=chkSupCategory').multiselect({
                    includeSelectAllOption: true,

                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,

                });

            });

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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">From Date</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">To Date</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="from-group">
                                <label class="control-label">Supplier Name</label>
                                <asp:DropDownList ID="ddlSuplist" runat="server" CssClass="form-control form-control-sm  chzn-select" AutoPostBack="True" Width="320px"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">

                            <asp:Label ID="Label1" runat="server" Font-Size="12px">Type</asp:Label>
                            <asp:RadioButtonList ID="rbtnAtStatus" runat="server" AutoPostBack="True" Style="border-radius: 5px; padding: 0 5px;"
                                CssClass="custom-control custom-control-inline custom-checkbox rbtnAtStatus d-block p-0 mt-3"
                                Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">&nbsp; Summary &nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem>&nbsp; Details</asp:ListItem>

                            </asp:RadioButtonList>

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass=" btn btn-primary btn-sm lnkbtmfromtop" OnClick="lnkbtnOk_Click" AutoPostBack="True">Ok</asp:LinkButton>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="veiwsummary" runat="server">
                   <%-- <div class="card card-fluid" style="min-height: 250px;">
                        <div class="card-body">--%>           
            <asp:GridView ID="gvspaysummary" runat="server" CssClass=" table-striped table-bordered grvContentarea"
                AutoGenerateColumns="False" ShowFooter="True">
                <PagerSettings Visible="False" />

                          <%--  <asp:GridView ID="gvspaysummary" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" AllowPaging="false"  Font-Size="12px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupCodealsasub" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                       <%--             <asp:TemplateField HeaderText="Supplier Name">

                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblheaderalsasub" runat="server" Text="Supplier's Name"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtntbCdataExelalsasub" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White" Style="text-align: center" Width="90px">Export Excel</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvSupDescalsasub" runat="server" __designer:wfdid="w38"
                                                CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="250px"></asp:HyperLink>
                                        </ItemTemplate>


                                        <FooterTemplate>
                                            <asp:Label ID="lgvFTotalalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>


                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>--%>

                                      <asp:TemplateField HeaderText="Supplier Name" >

                                    <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Supplier Name" Width="220px"></asp:Label>

                                    <asp:HyperLink ID="HLgvSupDescalsasub" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>
                                    </asp:HyperLink>
                                </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvResDescd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpnamalsasub" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFOpalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Debit Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmountalsasub" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDrAmtalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Credit Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmtalsasub" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCrAmtalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Closing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClsOwneramalsasub" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFclsOwneramalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Post Dated Cheque">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpostdatedcheque" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFpostdatedcheque" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <%--<asp:TemplateField HeaderText="Pending Bill">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="hlnkLgvpenbill" runat="server"
                                                CssClass="GridLebelL" Font-Size="12px" Font-Underline="False"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:LinkButton>


                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPenBill" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>




                                    <%--<asp:TemplateField HeaderText="Net Closing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClsamalsasub" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isubal")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFclsamalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
                                </Columns>

                                <FooterStyle CssClass="grvFooterNew" />
                               <HeaderStyle HorizontalAlign="Center" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvFooterNew" />
                            </asp:GridView>


                       <%-- </div>
                    </div>--%>

                </asp:View>


                <asp:View ID="ViewDetails" runat="server">

                    <%--<div class="card card-fluid">
                        <div class="card-body">--%>
                            <asp:GridView ID="gvspaymentdetails" runat="server" AutoGenerateColumns="False" 
                                ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-bordered grvContentarea" 
                                OnRowDataBound="gvspaymentdetails_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupCodealsasub" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupName" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="240px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprjName" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="230px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grp" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrp" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpndetails" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFOpalsasub" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Debit Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmount" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDrAmount" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Credit Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmt" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Closing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvClsing" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFclsClsing" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle CssClass="grvFooterNew" />
                                
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvFooterNew" />
                            </asp:GridView>


                       <%-- </div>
                    </div>--%>

                </asp:View>

            </asp:MultiView>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

