
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccPaySlip.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccPaySlip" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName"  Text="Accounts Head:"></asp:Label>
                                        <asp:TextBox ID="txtAccSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="IbtnSearchAcc" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="IbtnSearchAcc_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkShowLedger_Click" >Show</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                            <div class="msgHandSt">


                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                    DisplayAfter="30">
                                                    <ProgressTemplate>
                                                        <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar"
                                                            Text="Please Wait.........."></asp:Label>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                            </div>
                                        </div>

                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDetailsHead" runat="server" CssClass="lblTxt lblName"  Text="Details Head::"></asp:Label>
                                        <asp:TextBox ID="txtSearchRes" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="IbtnResource" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="IbtnResource_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlResource" runat="server" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="inputtextbox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="Label1" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>


                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:GridView ID="gvpayslip" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" Width="786px" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                        OnRowDataBound="gvpayslip_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Group Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvGrpDesc" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' Width="120px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vou.Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebelL"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                        widht="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voucher No.">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HLgvVounum1" runat="server" __designer:wfdid="w1"
                                        CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                        Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                        Width="100px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbldescription" runat="server" CssClass="GridLebelL"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>'
                                        Width="400px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCrAmount" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Payment">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDrAmount" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                        Width="85px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

