<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptClientModification.aspx.cs" Inherits="RealERPWEB.F_24_CC.RptClientModification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

    <style type="text/css"> 
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;

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
            <div class="card mt-2">
                <div class="card-header">
                    <div class="row mt-4">

                        <div class="col-md-3 pading5px asitCol3 d-none">
                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                            <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                        </div>
                         <div class="col-md-1">
                            <asp:Label ID="lblDatefrom" runat="server" CssClass="form-label" Text="Date:" Font-Bold="True" Width="50px"></asp:Label>
                            <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>

                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="To"></asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>

                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" TabIndex="3" AutoPostBack="True">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                              <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Client"></asp:Label>
                            <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="3">
                            </asp:DropDownList>

                        </div>

                       




                        <div class="col-md-3 d-none">
                   
                            <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                            <asp:LinkButton ID="imgbtnFindCustomer" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindCustomer_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                        </div>
                       

                        <div class="col-md-1">
                            <asp:Label ID="lblPage" runat="server" CssClass="form-label" Text="Page Size"></asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="150">150</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-3 d-none">
                            


                            <asp:LinkButton ID="imgbtnFindMod" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindMod_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                        </div>
                         <div class="col-md-1">
                             <asp:Label ID="lblReq" runat="server" CssClass="form-label" Text="Add. No"></asp:Label>
                                                         <asp:TextBox ID="txtSrcRequisition" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>


                        </div>
                        <div class="col-md-1" style="margin-top:22px;">
                                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>

                        </div>


                        <%--                                <div class="form-group">
                                    

                                </div>--%>


                        <%--<div class="form-group">


                                    

                                </div>--%>
                    </div>
                    </div>
                <div class="card-body">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ClientMod" runat="server">
                            <asp:GridView ID="grvRptCliMod" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Style="text-align: left" Width="487px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="grvRptCliMod_PageIndexChanging" AllowPaging="True"
                                OnRowDataBound="grvRptCliMod_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ADW No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAdwNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno1")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcUDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Modification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issue Date" FooterText="Total: " FooterStyle-ForeColor="#000">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDate" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkgvamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discount Amt">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkgvdisamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDisamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Net Amount">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkgvnetamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFNetamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                        <asp:View ID="FinalbillApproval" runat="server">
                            <asp:GridView ID="gvfbillapproval" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Style="text-align: left" Width="487px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvfbillapproval_PageIndexChanging" AllowPaging="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNof" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ADW No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAdwNof" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno1")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField
                                        HeaderText="Project Name">
                                        <HeaderTemplate>

                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Project Name" Width="160px"></asp:Label>


                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvDesc" runat="server"
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="300px"></asp:HyperLink>
                                        </ItemTemplate>



                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPDescf" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Unit Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcUDescf" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCDescf" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Modification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMdescf" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issue Date" FooterText="Total: " FooterStyle-ForeColor="#000">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDatef" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkgvamtf" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmtf" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Discount Amt">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkgvdisamtf" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDisamtf" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Net Amount">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkgvnetamtf" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFNetamtf" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                    </asp:MultiView>
                    </div>

                    
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



