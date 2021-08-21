<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptClientModification.aspx.cs" Inherits="RealERPWEB.F_24_CC.RptClientModification" %>

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
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" TabIndex="3" AutoPostBack="True">
                                        </asp:DropDownList>

                                    </div>
                                    <div>
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>

                                    <div class="col-md-4">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblName" Text="Date:" Font-Bold="True" Width="50px"></asp:Label>
                                        <asp:TextBox ID="txtFDate" runat="server" CssClass=" inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                    </div>

                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Client"></asp:Label>
                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="imgbtnFindCustomer" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindCustomer_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
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

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblReq" runat="server" CssClass="smLbl_to" Text="Add. No"></asp:Label>
                                        <asp:TextBox ID="txtSrcRequisition" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="imgbtnFindMod" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindMod_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                </div>

                                <%--                                <div class="form-group">
                                    

                                </div>--%>


                                <%--<div class="form-group">


                                    

                                </div>--%>
                            </div>
                        </fieldset>
                    </div>

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
                                                Width="80px"></asp:Label>
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
                                                Width="70px"></asp:Label>
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
                    </asp:MultiView>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



