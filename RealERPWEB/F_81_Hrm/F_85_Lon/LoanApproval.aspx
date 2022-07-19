<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LoanApproval.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_85_Lon.LoanApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        }



        function checkEmptyNote() {
            OpenApplyLoan();
        }

        function sum() {
            var txtFirstNumberValue = document.getElementById("<%=txtLoanAmt.ClientID%>").value;


            var txtSecondNumberValue = parseInt(document.getElementById("<%=txtInstNum.ClientID%>").value);
            var result = txtFirstNumberValue.replace(/,(?=\d{3})/g, '') / txtSecondNumberValue;

            if (result < 1) {
                alert("Amount Per  Installment can not be 0")
                return;
            }
            if (!isNaN(result)) {

                document.getElementById("<%=txtAmtPerIns.ClientID%>").value = result.toFixed(2);
            }
        }


        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
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
            <div class="card mt-4" id="warning" runat="server" visible="false">
                    <div class="row">

                    <div class="col-12 col-lg-12 col-xl-12">
                        <div class="section-block">


                            <div class="alert alert-danger has-icon" role="alert">
                                <div class="alert-icon">
                                    <span class="fa fa-bullhorn"></span>
                                </div>
                                Page Not found! Please Contact with Administrator

                                <br />



                            </div>



                        </div>
                    </div>
                </div>
                </div>
            <div class="card mt-4" id="CardBody" runat="server">
                
                <div class="card-header pt-2 pb-2">
                    <div class="row">
                        <h5>Loan Approval</h5>
                    </div>
                </div>
                <div class="card-body">

                    <div class="row mt-2">
                        <div class="col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblLoanId" runat="server">Loan Id</asp:Label>
                                <asp:TextBox ID="txtLoanId" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblcreateDate" runat="server">Create Date 
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ValidationGroup="one"
                                                   ControlToValidate="txtcreateDate" ErrorMessage="Required" Font-Size="8" Font-Italic="true"></asp:RequiredFieldValidator>
                                </asp:Label>
                                <asp:TextBox ID="txtcreateDate" runat="server" CssClass="form-control form-control-sm  mr-2" ValidationGroup="one"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtcreateDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server">Employee Name</asp:Label>
                                <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblLoanAmt" runat="server">Loan Amount *
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="8" Font-Italic="true" runat="server" ForeColor="Red" ValidationGroup="one"
                                                ControlToValidate="txtLoanAmt" ErrorMessage="Required">
                                            </asp:RequiredFieldValidator>
                                </asp:Label>
                                <asp:TextBox ID="txtLoanAmt" runat="server" CssClass="form-control form-control-sm" onKeyUp="sum()" onkeypress="return isNumberKey(this, event);" ValidationGroup="one"></asp:TextBox>
                            </div>

                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="">Instalment No *
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ValidationGroup="one"
                                            ControlToValidate="txtInstNum" ErrorMessage="Required" Font-Size="8" Font-Italic="true"></asp:RequiredFieldValidator>
                                </asp:Label>
                                <asp:TextBox ID="txtInstNum" runat="server" CssClass="form-control form-control-sm" onKeyUp="sum()" Text="1" onkeypress="return isNumberKey(this, event);" ValidationGroup="four"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblAmtPerIns" runat="server">Amount Per Inst</asp:Label>
                                <asp:TextBox ID="txtAmtPerIns" runat="server" CssClass="form-control form-control-sm" Text="0" Enabled="false" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblStd" runat="server" CssClass="">Statutory Deduction</asp:Label>
                                <asp:TextBox ID="txtStd" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblPloanAmt" runat="server">Previous loan Amount</asp:Label>
                                <asp:TextBox ID="txtPloanAmt" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server">Gross Monthly Salary</asp:Label>
                                <asp:TextBox ID="txtGMS" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblOI" runat="server">Other Income</asp:Label>
                                <asp:TextBox ID="txtOI" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblrt" runat="server">Intrest Rate(%)</asp:Label>
                                <asp:TextBox ID="txtrt" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblPFAmt" runat="server">Provident Fund</asp:Label>
                                <asp:TextBox ID="txtPFAmt" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group ">
                                <asp:Label ID="lblTax" runat="server">Income Tax</asp:Label>
                                <asp:TextBox ID="txtTax" runat="server" CssClass="form-control form-control-sm" Enabled="false" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblOD" runat="server">Other Deduction</asp:Label>
                                <asp:TextBox ID="txtOD" runat="server" CssClass="form-control form-control-sm" onkeypress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblEffDate" runat="server">Effective Date *
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Font-Size="8" Font-Italic="true" ForeColor="Red" ValidationGroup="one"
                                             ControlToValidate="txtEffDate" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </asp:Label>
                                <asp:TextBox ID="txtEffDate" runat="server" CssClass="form-control form-control-sm  mr-2" ValidationGroup="one"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEffDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblLoanType" runat="server">Loan Type *</asp:Label>
                                <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>



                    </div>


                    <div class="row">
                        <div class="col-lg-6" runat="server" id="dibNote">
                            <div class="form-group">
                                <asp:Label ID="lblnote" runat="server">Approved Note</asp:Label>
                                <asp:TextBox ID="txtnote" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="3" Style="min-height: 70px;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Label ID="lblLoanDesc" runat="server">Purpose of loan</asp:Label>
                                <asp:TextBox ID="txtLoanDescc" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="3" Style="min-height: 70px;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row" id="pnlApprovalStatus">
                        <div class="col-md-6">
                            <asp:Label ID="Label1" runat="server">Approval Logs</asp:Label>

                            <div class="table table-sm table-responsive">
                                <asp:GridView CssClass="table-striped table-hover table-bordered grvContentarea" ID="gvLoanApprovalLog" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <asp:Label ID="Loglblslpend" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved</br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Loglblcreatedate" runat="server" Text='<%# Convert.ToDateTime( Eval("posteddat")).ToString("dd-MMM-yyyy")%>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Approved by">
                                            <ItemTemplate>
                                                <asp:Label ID="Loglblempnamepend" runat="server" Text='<%#Eval("empname")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="Loglbldesigpend" runat="server" Text='<%#Eval("desig")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="LoglblRemarks" runat="server" Text='<%#Eval("remarks")%>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="d-flex justify-content-center">

                                <asp:LinkButton ID="lnkCancel" CssClass="btn btn-danger btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkCancel_Click" ValidationGroup="one">Request Cancel</asp:LinkButton>



                                <asp:LinkButton ID="lnkApprov" CssClass="btn btn-success btn-sm m-2 p2  bw-100" runat="server" OnClick="lnkApprov_Click" ValidationGroup="one">Approve</asp:LinkButton>

                                <%--<asp:LinkButton ID="lnkCancel" CssClass="btn btn-danger btn-sm m-2 p2  bw-100" runat="server" Visible="false" data-dismiss="modal" ValidationGroup="one">Cancel</asp:LinkButton>--%>
                                <asp:HiddenField ID="stepIDNEXT" runat="server" />
                                <asp:HiddenField ID="loanID" runat="server" />

                            </div>
                        </div>
                    </div>



                </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
