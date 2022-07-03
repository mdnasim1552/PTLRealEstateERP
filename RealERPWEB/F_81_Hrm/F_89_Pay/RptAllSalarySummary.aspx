<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptAllSalarySummary.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptAllSalarySummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .fsize {
            font-size: 15px !important;
            font-weight: bold !important;
        }

        .rbt {
            padding: 8px 2px 0px 3px;
        }
    </style>

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

            <div class="card card-fluid container-data" style="min-height: 500px;">
                <div class="card-header mt-3 mb-0 pb-0">
                    <div class="row mb-0 pb-0">
                        <asp:RadioButtonList ID="rbtnAtten" runat="server" AutoPostBack="True"
                            BackColor="#DFF0D8" BorderColor="#000" CssClass="custom-control custom-control-inline custom-checkbox rbt"
                            Font-Bold="True" Font-Size="12px" ForeColor="Black"
                            OnSelectedIndexChanged="rbtnAtten_SelectedIndexChanged"
                            RepeatDirection="Horizontal">
                            <asp:ListItem>Summary For Bank Advice</asp:ListItem>
                            <asp:ListItem>Mode Of Payment</asp:ListItem>
                            <asp:ListItem>Net Comparison</asp:ListItem>
                            <asp:ListItem>Gross Comparison</asp:ListItem>
                            <asp:ListItem>Gross Recon</asp:ListItem>
                            <asp:ListItem>Total</asp:ListItem>
                            <asp:ListItem>Department wise</asp:ListItem>

                        </asp:RadioButtonList>

                        <asp:Label ID="lcomp" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Month</asp:Label>
                        <asp:DropDownList ID="ddlmon" data-placeholder="Choose Mon.." runat="server"
                            CssClass="chzn-select form-control col-2" AutoPostBack="true">
                        </asp:DropDownList>

                        <asp:LinkButton ID="lnkOk" runat="server" OnClick="lnkOk_Click" CssClass="btn btn-success btn-sm ml-1 col-1">Show</asp:LinkButton>
                    </div>
                    <div class="row mb-0 pb-0">
                    </div>
                </div>

                <div class="card-body">
                    <asp:Panel runat="server" ID="PnlBankSumary" Visible="false">
                        <div class="table-responsive" runat="server">
                            <asp:GridView ID="GvBankSummary" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" Width="40%">
                                <RowStyle />
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="SL.No.">

                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="18px" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Particualars">

                                        <HeaderTemplate>

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Particualars"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExcelbank" runat="server"
                                                CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><i  class="fa fa-file-excel-o"></i></asp:HyperLink>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvParticular" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                BackColor="Transparent" BorderStyle="None"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Font-Size="16px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="18px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Salary">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAmount" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="16px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="18px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblremarks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="16px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="18px" />
                                    </asp:TemplateField>


                                </Columns>

                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="PnlModPayment" Visible="false">
                        <div class="table-responsive" runat="server">
                            <asp:GridView ID="GvModPayment" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" Width="65%">
                                <RowStyle />
                                <Columns>
                                    <%--  <asp:TemplateField HeaderText="Sl.No.">

                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Particualars">
                                        <HeaderTemplate>

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Particualars"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o"></i></asp:HyperLink>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvParticular" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                runat="server" Style="font-size: 12px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                BackColor="Transparent" BorderStyle="None"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtoAmount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SCB">
                                        <ItemTemplate>
                                            <asp:Label ID="lblscbgvAmount" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0;(#,##0); ") %>'></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OBL(Advice)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbloblgvAmount" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OBL(Cash)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbloblcashAmount" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OBL CTG-Cash">
                                        <ItemTemplate>
                                            <asp:Label ID="lbloblctgAmount" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DBL STD">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldblstdAmount" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DBL SICOL">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldblsclAmount" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DBL GPL">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldblgplAmount" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                </Columns>

                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="PnlNetComparison" Visible="false">
                        <div class="table-responsive" runat="server">
                            <asp:GridView ID="GvNetComparison" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" Width="100%" ShowFooter="true">
                                <RowStyle />
                                <Columns>
                                    <%--  <asp:TemplateField HeaderText="Sl.No.">

                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Particualars">
                                        <HeaderTemplate>

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Particualars"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o"></i></asp:HyperLink>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvParticular" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                runat="server" Style="font-size: 12px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                BackColor="Transparent" BorderStyle="None"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPretoAmount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayprev")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurtoAmount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Diff.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeff" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffrent")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblper" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblremark" Width="100%" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="btn btn-xs btn-primary" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                </Columns>

                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="PnlGrossSummary" Visible="false">
                        <div class="table-responsive" runat="server">
                            <asp:GridView ID="GvgrossSalSummary" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                CssClass="table-striped table-hover table-bordered grvContentarea" Width="100%">
                                <RowStyle />
                                <Columns>

                                    <asp:TemplateField HeaderText="Particualars">
                                        <HeaderTemplate>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Particualars"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o"></i></asp:HyperLink>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvParticular" CssClass='<%#  
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                runat="server" Style="font-size: 12px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                BackColor="Transparent" BorderStyle="None"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrsssal" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblincamt" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinper" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inper")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrssfinal" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssalfinal")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgsprev" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssalprev")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Diff.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeff" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffrent")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvper" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblremark" Width="100%" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkGrossUpdate" runat="server" CssClass="btn btn-xs btn-primary" OnClick="lnkGrossUpdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>


                    <asp:Panel runat="server" ID="PnlGrossRecon" Visible="false">
                        <div class="table-responsive" runat="server">
                            <asp:GridView ID="GvGrossRecon" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" Width="100%">
                                <RowStyle />
                                <Columns>
                                    <%--  <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Id Card">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcardno" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "grp"))=="A"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "grp"))=="1"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))=="000000000000"?"fsize":""%>'
                                                runat="server" Style="font-size: 12px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name">
                                        <HeaderTemplate>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Name"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o"></i></asp:HyperLink>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "grp"))=="A"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "grp"))=="1"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))=="000000000000"?"fsize":""%>'
                                                runat="server" Style="font-size: 12px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                BackColor="Transparent" BorderStyle="None"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Department" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldepartment" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesign" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rotation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesign" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "replacement")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Gross">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGross" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))=="AAAAAAAAAAAA"?"fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgsprev" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "grp"))=="A"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))=="AAAAAAAAAAAA"?" fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))=="000000000000"?"fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                </Columns>

                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="PnlTotal" Visible="false">
                        <div class="table-responsive" runat="server">
                            <asp:GridView ID="GvTotalSumm" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" Width="100%">
                                <RowStyle />
                                <Columns>

                                    <asp:TemplateField HeaderText="Particualars">
                                        <HeaderTemplate>

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Particualars"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o"></i></asp:HyperLink>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvParticular" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                runat="server" Style="font-size: 12px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refdesc")) %>'
                                                BackColor="Transparent" BorderStyle="None"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp1Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp2Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp3Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp4Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp5Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp6Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp7Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp8Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp9Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp10Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp11Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblp12Amount" runat="server"
                                                CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="00000000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="AAAAAAAAAAAA"?"bg-yellow d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "refno"))=="BBBBBBBBBBBB"?"bg-danger d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                    </asp:TemplateField>
                                </Columns>

                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="deptwise" Visible="false">
                        <div class="table-responsive" runat="server">
                            <asp:GridView ID="gvdeptwise" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" Width="100%" ShowFooter="true">
                                <RowStyle />
                                <Columns>

                                    <asp:TemplateField HeaderText="Particualars">
                                        <HeaderTemplate>

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Particualars"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o"></i></asp:HyperLink>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdeptParticular"
                                                runat="server" Style="font-size: 12px;" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcodedesc")) %>'
                                                BackColor="Transparent" BorderStyle="None"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>

                                    <%-- Dhaka Last Two(2) Months//  ffff--%>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPretoAmountdept" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperdept" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perncur")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountdeptNpay" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayprev")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperdeptnpay" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pernprev")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Diff.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeffdept" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffrent")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperdeptdhk" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <%-- Chittagaon Last Two(2) Months//  ffff--%>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurtoAmountdeptcht" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "net2pay")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperdeptcht" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per2ncur")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountdeptNpaycht" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayprev2")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperdeptchtcurmon" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pern2prev")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Diff.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeffdept" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffrent2")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurtoAmountdeptcht" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per2")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                            <%--   Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per2")).ToString("#,##0;(#,##0); ") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>


                                    <%-- Dhaka &  Chittagaon Last Two(2) Months//  ffff--%>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurtoAmountdeptchtdhk" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "net2paycurr")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperdeptchtdhk" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per2cumon")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountdeptNpaychtdhk" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "net2payprev")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperdeptchtcurmondhk" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per2monprev")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Diff.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldeffdeptchtdhk" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diff2mon")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurtoAmountdeptchtdhk" runat="server" CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="00000000"?"bg-green d-block fsize":
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "acgcode"))=="99999999"?"bg-yellow d-block fsize":""%>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per2mon")).ToString("#,##0.00;(#,##0.00);") +"%" %>'></asp:Label>
                                            <%--   Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "per2mon")).ToString("#,##0;(#,##0); ") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="16px" />
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>


                                </Columns>

                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
