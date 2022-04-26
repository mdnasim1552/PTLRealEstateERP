<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpDeducOther.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_85_Lon.EmpDeducOther" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        
div#ContentPlaceHolder1_ddlEmpName_chzn{
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label runat="server">
                                    <asp:LinkButton ID="lbtnPrevLoanList" runat="server" OnClick="lbtnPrevLoanList_Click">Prev. Loan List:</asp:LinkButton>
                                </asp:Label>
                                <asp:DropDownList ID="ddlPrevLoanList" runat="server" Width="233" CssClass="form-control form-control-sm" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server">Loan Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>


                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">

                                <asp:Label ID="lblResList" runat="server" OnDataBinding="lblResList_DataBinding">Employee List</asp:Label>


                                <asp:DropDownList ID="ddlEmpList" runat="server" Width="233" CssClass="form-control chzn-select" TabIndex="2">
                                </asp:DropDownList>
                                <asp:Label ID="lblEmpName" runat="server" Visible="false" CssClass="form-control form-control-sm "></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                
                                 <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt lblName">Loan No</asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" CssClass="smLbl_to"></asp:Label>
                                <asp:Label ID="lblCurNo2" runat="server" CssClass="smLbl_to"></asp:Label>
                                                           <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm mt20">Ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True" CssClass=" btn btn-primary primaryBtn  chkBoxControl"
                                    OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment"
                                    Visible="False" />


                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                                    Text="Add.Installment" CssClass=" btn btn-primary primaryBtn  chkBoxControl"
                                    Visible="False" OnCheckedChanged="chkAddIns_CheckedChanged" />
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnAddInstallment" runat="server" OnClick="lbtnAddInstallment_Click"
                                    Visible="False" CssClass="btn btn-primary btn-sm mt20">Add</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:Panel ID="pnlloan" runat="server" Visible="False">
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server">Total Amount</asp:Label>
                                        <asp:TextBox ID="txtToamt" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server">Ins. Amount</asp:Label>
                                        <asp:TextBox ID="txtinsamt" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server">Start Date</asp:Label>
                                        <asp:TextBox ID="txtstrdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtstrdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtstrdate"></cc1:CalendarExtender>

                                    </div>
                                </div>

                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server">Duration</asp:Label>
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control form-control-sm" AppendDataBoundItems="True"
                                            Width="100px">
                                            <asp:ListItem Value="1">1 Month</asp:ListItem>
                                            <asp:ListItem Value="2">2 Month</asp:ListItem>
                                            <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                            <asp:ListItem Value="4">4 Month</asp:ListItem>
                                            <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                            <asp:ListItem Value="6">6 Month</asp:ListItem>
                                            <asp:ListItem Value="7">7  Month</asp:ListItem>
                                            <asp:ListItem Value="8">8  Month</asp:ListItem>
                                            <asp:ListItem Value="9">9  Month</asp:ListItem>
                                            <asp:ListItem Value="10">10  Month</asp:ListItem>
                                            <asp:ListItem Value="11">11  Month</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                        </asp:Panel>
                    </div>


                </div>
                <div class="card-body">
                    <asp:GridView ID="gvloan" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No." FooterText="Total ">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />

                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Installment Date.">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvinstdate" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lndate")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtgvinstdate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvinstdate"></cc1:CalendarExtender>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True"
                                        CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>
                                </FooterTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Installment Amt.">
                                <FooterTemplate>
                                    <asp:Label ID="gvlFToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right" Width="120px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="gvtxtamt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lnamt")).ToString("#,##0;(#,##0); ") %>'
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

            <%--            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-4 pading5px asitCol4">
                                <asp:LinkButton ID="lbtnPrevLoanList" runat="server" OnClick="lbtnPrevLoanList_Click" CssClass="lblTxt lblName">Prev. Loan List:</asp:LinkButton>

                                <asp:DropDownList ID="ddlPrevLoanList" runat="server" Width="233" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-5 pading5px asitCol5">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Loan Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>

                                <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt lblName">Loan No</asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" CssClass="smLbl_to"></asp:Label>
                                <asp:Label ID="lblCurNo2" runat="server" CssClass="smLbl_to"></asp:Label>
                                <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>
                            </div>

                        </div>
                        <div class="form-group">

                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName" OnDataBinding="lblResList_DataBinding">Employee List</asp:Label>
                                <asp:TextBox ID="txtsrchEmp" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-5 pading5px asitCol5">
                                <asp:DropDownList ID="ddlEmpList" runat="server" Width="233" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                </asp:DropDownList>
                                <asp:Label ID="lblEmpName" runat="server" Visible="false" Width="233" CssClass="form-control inputTxt pull-left"></asp:Label>


                            </div>
                            <div class="col-md-3 pading5px">
                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                            </div>


                        </div>
                    </div>
                </fieldset>
            </div>--%>
            <%--        <div class="row">
               <asp:Panel ID="pnlloan" runat="server" Visible="False">

                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <div class=" form-group">
                                <div class="col-md-12 pading5px">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Total Amount</asp:Label>
                                    <asp:TextBox ID="txtToamt" runat="server" CssClass="inputTxt inputName inpPixedWidth text-right"></asp:TextBox>
                                    <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">Ins. Amount</asp:Label>
                                    <asp:TextBox ID="txtinsamt" runat="server" CssClass="inputTxt inputName inpPixedWidth text-right"></asp:TextBox>
                                    <asp:Label ID="Label3" runat="server" CssClass=" smLbl_to">Start Date</asp:Label>
                                    <asp:TextBox ID="txtstrdate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtstrdate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtstrdate"></cc1:CalendarExtender>

                                    <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Duration</asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddlPage" AppendDataBoundItems="True"
                                        Width="100px">
                                        <asp:ListItem Value="1">1 Month</asp:ListItem>
                                        <asp:ListItem Value="2">2 Month</asp:ListItem>
                                        <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                        <asp:ListItem Value="4">4 Month</asp:ListItem>
                                        <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                        <asp:ListItem Value="6">6 Month</asp:ListItem>
                                        <asp:ListItem Value="7">7  Month</asp:ListItem>
                                        <asp:ListItem Value="8">8  Month</asp:ListItem>
                                        <asp:ListItem Value="9">9  Month</asp:ListItem>
                                        <asp:ListItem Value="10">10  Month</asp:ListItem>
                                        <asp:ListItem Value="11">11  Month</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </fieldset>



                </asp:Panel>
            </div>--%>
            <%--            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True" CssClass=" btn btn-primary primaryBtn  chkBoxControl"
                            OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment"
                            Visible="False" />
                        <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                            Text="Add.Installment" CssClass=" btn btn-primary primaryBtn  chkBoxControl"
                            Visible="False" OnCheckedChanged="chkAddIns_CheckedChanged" />
                        <asp:LinkButton ID="lbtnAddInstallment" runat="server" OnClick="lbtnAddInstallment_Click"
                            Visible="False" CssClass="btn btn-primary primaryBtn">Add</asp:LinkButton>
                    </div>
                </div>
            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

