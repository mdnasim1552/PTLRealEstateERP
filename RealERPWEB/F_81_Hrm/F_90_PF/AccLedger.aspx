<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccLedger.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_90_PF.AccLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<tr>
        <td colspan="9">--%>


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
                                <div class="col-md-3 pading5px asitCol3">
                                </div>
                                <div class="col-md-4 pading5px">
                                    <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#155273" ForeColor="White" CssClass="btn btn-primary checkBox"
                                        RepeatColumns="6" RepeatDirection="Horizontal">
                                        <asp:ListItem>With Narration</asp:ListItem>
                                        <asp:ListItem>Without Narattion</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-md-3 pading5px pull-right">
                                    <div class="msgHandSt">
                                        <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                    </div>


                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Get Acc. Heads"></asp:Label>
                                    <asp:TextBox ID="txtAccSearch" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="IbtnSearchAcc" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="IbtnSearchAcc_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-3 pading5px asitCol3">
                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkShowLedger_Click">Show</asp:LinkButton>

                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateFrom"></cc1:CalendarExtender>
                                    <div class="smLbl_to">

                                        <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server" Visible="false" Width="1036px">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccResCode" runat="server" CssClass="lblTxt lblName" Text="Get Resource Heads"></asp:Label>
                                        <asp:TextBox ID="txtSrchRes" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlConAccResHead" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                   

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:CheckBox ID="chkqty" runat="server" CssClass="checkBox" Text="With qty" />
                                    </div>

                                </div>
                            </fieldset>


                        </asp:Panel>

                    </div>

                    <div class="row">
                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-striped table-hover table-bordered grvContentarea"
                            BorderWidth="2px" ShowFooter="True" Width="908px"
                            OnRowDataBound="dgv2_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' widht="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel"
                                            Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChequeNo" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="85px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>'
                                            Width="250px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtrnqty" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtrnrate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle />
                            <EditRowStyle />
                            <SelectedRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            <AlternatingRowStyle BackColor="" />
                        </asp:GridView>
                    </div>
                </div>
            </div>






            
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</td>
    </tr>--%>
</asp:Content>

