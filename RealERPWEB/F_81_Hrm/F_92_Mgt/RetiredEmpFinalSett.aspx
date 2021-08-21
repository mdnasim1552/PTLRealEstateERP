<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RetiredEmpFinalSett.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RetiredEmpFinalSett" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

        };
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
                    <asp:Panel ID="panelHead" runat="server">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-3">
                                            <asp:Label ID="lblsetNo" runat="server" CssClass="lblTxt lblName">Emp Sett. No :</asp:Label>
                                            <asp:Label ID="lblCurNo1" runat="server" CssClass="smLbl_to"></asp:Label>
                                            <asp:Label ID="lblCurNo2" runat="server" CssClass="smLbl_to"></asp:Label>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblCurdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                            <asp:TextBox ID="txtCurdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurdate"></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="lbtnPrevsetNo" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevsetNo_OnClick">Prev. Sett. No:</asp:LinkButton>
                                            <asp:DropDownList ID="ddlPrevsetNo" runat="server" Width="200" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class=" col-md-3  pading5px asitCol3">

                                            <asp:Label ID="lblEmpList" CssClass="lblTxt lblName " runat="server" Text="Employee Name:"></asp:Label>
                                            <asp:TextBox ID="txtSrcEmp" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="imgbtnFindEmp" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindEmp_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>


                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlEmpName" runat="server" OnSelectedIndexChanged="ddlEmpName_OnSelectedIndexChanged" CssClass="form-control inputTxt chzn-select" TabIndex="13" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>


                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="PanelEmpinfo" runat="server" Visible="False">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblempname" CssClass="lblTxt " runat="server" Text="Name Of Employee:"></asp:Label>
                                            <asp:Label ID="lblname" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblempdesig" CssClass="lblTxt " runat="server" Text="Designation:"></asp:Label>
                                            <asp:Label ID="lbldesig" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblwrk" CssClass="lblTxt" runat="server" Text="Work Station:"></asp:Label>
                                            <asp:Label ID="lblwrkst" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lbljoind" CssClass="lblTxt" runat="server" Text="Joining Date:"></asp:Label>
                                            <asp:Label ID="lbljdate" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblrd" CssClass="lblTxt" runat="server" Text="Date Of Release:"></asp:Label>
                                            <asp:Label ID="lblrsd" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                        <div class=" col-md-4  pading5px">

                                            <asp:Label ID="lblsl" CssClass="lblTxt" runat="server" Text="Service Length:"></asp:Label>
                                            <asp:Label ID="lblslen" CssClass="lblTxt" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </asp:Panel>

                    <asp:GridView ID="gvRtrSalSett" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False"
                        ShowFooter="True">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn  btn-success primarygrdBtn" OnClick="lnkTotal_OnClick">Total</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                 <asp:LinkButton ID="lnkUpdate" runat="server" OnClick="lnkUpdate_OnClick" CssClass="btn  btn-primary primarygrdBtn">Update</asp:LinkButton>
                                    <asp:Label ID="lgvfTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right" Text="Net Payable Amount"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvgdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salary">
                                <ItemTemplate>
                                    <asp:TextBox ID="lgvnetamtcash" runat="server" Style="text-align: right" BorderStyle="None" BackColor="Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sal")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFTNetmtcash" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
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

