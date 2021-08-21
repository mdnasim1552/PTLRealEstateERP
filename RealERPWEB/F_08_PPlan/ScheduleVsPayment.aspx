<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ScheduleVsPayment.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.ScheduleVsPayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblProj" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                    <asp:TextBox ID="txtproj" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindproj" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2"  OnClick="ibtnFindproj_OnClick"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:DropDownList ID="ddlproj" runat="server" Width="320" CssClass="chzn-select form-control  inputTxt" TabIndex="3"  AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblres" runat="server" CssClass="lblTxt lblName" Text="Resource"></asp:Label>
                                    <asp:TextBox ID="txtSrcMat" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindresname" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2" OnClick="ibtnFindresname_OnClick"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:DropDownList ID="ddlres" runat="server" Width="320" CssClass="chzn-select form-control  inputTxt" TabIndex="3"></asp:DropDownList>                                 
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 pading5px">
                                    <asp:Label ID="lblfrmdate" runat="server" CssClass="smLbl_to">Date</asp:Label>
                                    <asp:TextBox ID="txtCurDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                                </div>
                               

                            </div>
                           
                        </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvOwnerPay" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Schedule Date.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvinstdate" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "insdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Schedule Amt.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvinsamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "insamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="120px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Date.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpaytdate" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "insdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Schedule Amt.">

                                    <ItemTemplate>
                                        <asp:TextBox ID="gvpayamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "insamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="120px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

