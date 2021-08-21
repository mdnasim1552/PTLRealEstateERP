<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AdvertisementEntry.aspx.cs" Inherits="RealERPWEB.F_21_MKT.AdvertisementEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="uppnlclint" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-8">
                                        <asp:Label ID="lblAdNo" runat="server" CssClass="lblTxt lblName">Advertise No :</asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <asp:Label ID="lblCurNo2" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                        <asp:Label ID="lblRef" runat="server" CssClass="lblTxt lblName">Ad. Ref:</asp:Label>
                                        <asp:TextBox ID="txtAdRef" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:LinkButton ID="lbtnPrevAdNo" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevAdNo_OnClick">Prev. Ad. No:</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevAdNo" runat="server" Width="200" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="scheduler-border fieldset_A" runat="server" id="adDetails" visible="False">
                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-4">
                                        <asp:Label ID="lblPaperName" runat="server" CssClass="lblTxt lblName">Paper Name :</asp:Label>
                                        <asp:DropDownList ID="ddlPaperName" runat="server" Width="200" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblPaperDesc" runat="server" CssClass="lblTxt lblName">Advertise Des :</asp:Label>
                                        <asp:DropDownList ID="ddlPaperDesc" runat="server" Width="200" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="lblAmount" runat="server" CssClass="lblTxt lblName">Amount :</asp:Label>
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-success okBtn" OnClick="lbtnAdd_OnClick">Add</asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </fieldset>

                        <div class="col-md-12" style="padding-left: 0">
                            <asp:GridView ID="gvAdDetails" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered table-responsive grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">

                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pap Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtoffcod" runat="server" Style="text-align: left; font-size: 11px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "papcod")) %>'
                                                Width="40px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Des Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtdescod" runat="server" Style="text-align: left; font-size: 11px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descode")) %>'
                                                Width="40px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Media Description">
                                        <ItemTemplate>
                                            <asp:Label ID="txtdesc"
                                                runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "papdesc")) %>'
                                                Width="250px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdate_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Size Of Ad.">
                                        <ItemTemplate>
                                            <asp:Label ID="txtaddesc"
                                                runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addesc")) %>'
                                                Width="150px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" OnClick="lbtnTotal_Click" runat="server" CssClass="btn btn-xs btn-default">Total</asp:LinkButton>
                                            <%--<asp:LinkButton ID="lnkbtnRecalculate" runat="server" OnClick="lnkbtnRecalculate_Click" CssClass="btn btn-xs btn-danger">Calculate</asp:LinkButton>--%>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtamount" runat="server" Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                Width="60px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>

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

                        </div>
                    </div>



                </div>


            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

