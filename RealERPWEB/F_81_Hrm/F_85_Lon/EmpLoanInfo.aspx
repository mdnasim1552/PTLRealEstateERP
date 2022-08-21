<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpLoanInfo.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_85_Lon.EmpLoanInfo1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .isFormulaChekcboxdv label{
            margin:0;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded()
        {
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
            var gvloan = $('#<%=this.gvloan.ClientID %>');
            gvloan.Scrollable();
        }
        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (charCode > 31 &&
                    (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
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

            <div class="card card-fluid container-data" style="min-height: 500px;">
                <div class="card-header mt-3 mb-0 pb-0">
                    <div class="row">

                        <div class="col-md-3 col-lg-3 col-xs-12">
                            <asp:Label ID="lblEmplist" runat="server">Employee List 
                                <asp:LinkButton ID="lnkSearcEMP" runat="server" OnClick="lnkSearcEMP_Click"><span class=" fa fa-search"></span></asp:LinkButton></asp:Label></asp:Label>
                            <asp:DropDownList ID="ddlEmpList" data-placeholder="Choose loan.." runat="server"
                                CssClass="chzn-select form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-lg-2 col-xs-12">
                            <asp:Label ID="lbldate" runat="server">Loan Date</asp:Label>
                            <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1 col-lg-1 col-xs-12 p-0">
                            <asp:Label ID="loanNo" CssClass="d-block" runat="server">Loan No</asp:Label>
                            <asp:Label ID="lblCurNo1" runat="server" CssClass="btn btn-sm btn-secsondary ">ELN</asp:Label>
                            <asp:Label ID="lblCurNo2" runat="server" CssClass="btn btn-sm btn-secsondary ">000</asp:Label>
                        </div>

                        <div class="col-md-2 col-lg-2 col-xs-12">
                            <asp:Label ID="Label7" runat="server">Loan Type</asp:Label>
                            <asp:DropDownList ID="ddlLoantype" data-placeholder="Choose loan.." runat="server"
                                CssClass="chzn-select form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-lg-3 col-xs-12">
                            <asp:Label ID="lbtnPrevLoanLists" runat="server">Prev. Loan List
                                <asp:LinkButton ID="lbtnPrevLoanList" runat="server" OnClick="lbtnPrevLoanList_Click"><span class=" fa fa-search"></span></asp:LinkButton></asp:Label>

                            <asp:DropDownList ID="ddlPrevLoanList" data-placeholder="Choose Employee.." runat="server"
                                CssClass="chzn-select form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1 col-lg-1 col-xs-12">
                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-success" Style="margin-top: 20px;">Ok</asp:LinkButton>
                        </div>
                    </div>

                    <div class="row mb-0 pb-0 pt-2" id="pnlloan" runat="server" visible="false">
                        <div class="col-md-2 col-lg-2 col-xs-12">
                            <asp:Label ID="Label5" runat="server">Total Amount 
                               <span class="float-right isFormulaChekcboxdv"><asp:CheckBox ID="isFormulaChekcbox"  runat="server" AutoPostBack="True" ForeColor="red" Visible="false" OnCheckedChanged="isFormulaChekcbox_CheckedChanged" Text="Is Formula" CssClass="margin:0" /></span> 
                            </asp:Label>
                            <asp:TextBox ID="txtToamt" runat="server" onkeypress="return isNumberKey(this, event);" Style="text-align: right" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1 col-lg-1 col-xs-12">
                            <asp:Label ID="Label1" runat="server">Ins. Amount         
                            </asp:Label>
                            <asp:TextBox ID="txtinsamt" runat="server" CssClass="form-control" onkeypress="return isNumberKey(this, event);" Style="text-align: right"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-lg-3 col-xs-12" id="isFormulaDiv" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-3 col-lg-3 col-xs-12">
                                    <asp:Label ID="Label2" runat="server">Type</asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlFormulatype"   CssClass="form-control">                                        
                                        <asp:ListItem Value="P">%</asp:ListItem>
                                        <asp:ListItem Value="F">Flat</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-lg-3 col-xs-12">
                                    <asp:Label ID="Label3" runat="server">Emp. Pay</asp:Label>
                                    <asp:TextBox ID="txtEmployePayment" runat="server" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>                                     
                                </div>
                                 <div class="col-md-3 col-lg-3 col-xs-12">
                                    <asp:Label ID="Label4" runat="server">Comp. Pay</asp:Label>
                                    <asp:TextBox ID="txtCompPaid" runat="server" onkeypress="return isNumberKey(this, event);" CssClass="form-control"></asp:TextBox>                                     
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 col-lg-1 col-xs-12">
                            <asp:Label ID="Label6" runat="server">Start Date</asp:Label>
                            <asp:TextBox ID="txtstrdate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtstrdate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtstrdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1 col-lg-1 col-xs-12">
                            <asp:Label ID="Label9" runat="server">Duration</asp:Label>
                            <asp:DropDownList ID="ddlMonth" runat="server" ata-placeholder="Choose Month.." CssClass="chzn-select form-control">
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
                        <div class="col-md-2 col-lg-2 col-xs-12">
                            <asp:Label ID="Label8" runat="server">Upto Paid</asp:Label>
                            <asp:TextBox ID="txtPaidAmt" runat="server" ReadOnly="true" onkeypress="return isNumberKey(this, event);" CssClass="form-control" Style="text-align: right"></asp:TextBox>
                        </div>
                         <div class="col-md-1 col-lg-1 col-xs-12">
                            <asp:Label ID="Label10" runat="server">Upto Date</asp:Label>
                            <asp:TextBox ID="txtUptoDate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtUptoDate_CalendarExtender1" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtUptoDate"></cc1:CalendarExtender>
                        </div>

                         <div class="col-md-2 col-lg-2 col-xs-12" id="rfuBox" visible="false" runat="server">
                            <asp:Label ID="Label11" ForeColor="Red" runat="server">Refund Amount</asp:Label>
                            <asp:TextBox ID="txtRefunAmt" runat="server" onkeypress="return isNumberKey(this, event);" ReadOnly="true" CssClass="form-control" Style="text-align: right;"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-lg-2 col-xs-12" id="refunNotes" runat="server" visible="false">
                            <asp:Label ID="Label12" ForeColor="Red" runat="server">Refund Notes</asp:Label>
                            <asp:TextBox ID="TextBox1" runat="server"   CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-1 col-lg-1 col-xs-12">
                            <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-info" Style="margin-top: 20px;" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row mb-0 pb-0 pt-2" runat="server" id="xpnlloan" visible="false">
                    </div>
                    <div class="row mb-0 pb-0 pt-2">
                        <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True" CssClass="btn btn-warning btn-sm"
                            OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment"
                            Visible="False" />
                        <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                            Text="Add.Installment" CssClass=" btn btn-info btn-sm ml-1 col-1  chkBoxControl"
                            Visible="False" OnCheckedChanged="chkAddIns_CheckedChanged" />
                        <asp:LinkButton ID="lbtnAddInstallment" runat="server" OnClick="lbtnAddInstallment_Click"
                            Visible="False" CssClass="btn btn-info btn-sm ml-1 col-1">Add</asp:LinkButton>                     
                    </div>
                </div>
                <div class="card-body">
                    <div class="row table table-responsive">
                        <asp:GridView ID="gvloan" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvloan_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl" FooterText="Total ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkCalculation" runat="server" CssClass="btn btn-info btn-sm" OnClick="lnkCalculation_Click">Calculation</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-info btn-sm" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
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
                                            CssClass="btn btn-success btn-sm ml-1" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>

                                     

                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comp. Pay">                                   
                                    <FooterTemplate>
                                        <asp:Label ID="gvlFToamtComp" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtamtComppay" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comppay")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Installment Amt.">
                                    <HeaderTemplate>
                                        Installment Amt.
                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="gvlFToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtamt" runat="server" Style="text-align: right" onkeypress="return isNumberKey(this, event);"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lnamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total">                                  
                                    <FooterTemplate>
                                        <asp:Label ID="gvlFToamtTTL" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtamtttlinsamt" runat="server" Style="text-align: right" AutoPostBack="true" onkeypress="return isNumberKey(this, event);" OnTextChanged="gvtxtamtttlinsamt_TextChanged"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlinsamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server"  Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "paidamt")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is formula" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblisformula" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isformula")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDel" runat="server"  OnClientClick="return confirm('Are you sure to delete this item?');" OnClick="lnkDel_Click" Text="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="IsRefund">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRefund" runat="server" 
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isrefund"))=="True" %>' />
                                            
                                    </ItemTemplate>
                                    <FooterTemplate>
                                           <asp:LinkButton ID="lnkbtnRefund" runat="server" Font-Bold="True"
                                            CssClass="btn btn-success btn-sm ml-1" OnClick="lnkbtnRefund_Click">Update</asp:LinkButton>

                                    </FooterTemplate>
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
