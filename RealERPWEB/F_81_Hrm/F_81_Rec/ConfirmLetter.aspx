<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ConfirmLetter.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.ConfirmLetter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                    <div class="col-md-7">
                                        <asp:Label ID="lblconfmNo" runat="server" CssClass="lblTxt lblName">Confirm Let. No :</asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <asp:Label ID="lblCurNo2" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                        <asp:Label ID="lblRef" runat="server" CssClass="lblTxt lblName">Ref:</asp:Label>
                                        <asp:TextBox ID="txtconfmRef" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>
                                    </div>

                                    <div class="col-md-5 pull-right">
                                        <asp:LinkButton ID="lbtnPrevconfmltNo" runat="server" CssClass="smLbl_to" OnClick="lbtnPrevconfmltNo_OnClick">Prev. Confm Lt. No:</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevconfmlttNo" runat="server" Width="180" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                        <asp:Panel ID="pnlEmp" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label1" runat="server" CssClass="smLbl_to">Employee List</asp:Label>
                                            <asp:DropDownList ID="ddlEmpName" runat="server" Width="220" CssClass="form-control inputTxt pull-left" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary primaryBtn pull-left" OnClick="lbtnSelect_OnClick">Select</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>

                        <asp:GridView ID="gvConfmltr" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" ShowFooter="True" Width="599px" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDeleting="gvConfmltr_OnRowDeleting">
                            <PagerSettings Position="TopAndBottom" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Emp Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Emp Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Confirmation Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcondate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "condate")).ToString("dd-MMM-yyyy") %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Department">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkupdate_OnClick">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsec" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="left" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Bal.Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbalqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtisuqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Use of location">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtlocation" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "useoflocation")) %>'
                                            Width="100px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtisurmk" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="100px" BackColor="Transparent" BorderColor="#660033"
                                            BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


