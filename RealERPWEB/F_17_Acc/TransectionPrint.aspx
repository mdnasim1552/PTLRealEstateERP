<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TransectionPrint.aspx.cs" Inherits="RealERPWEB.F_17_Acc.TransectionPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            //$("input, select").bind("keydown", function (event) {
            //    var k1 = new KeyPress();
            //    k1.textBoxHandler(event);

            //});
        }



        function PrintRptCheque(vouanptype) {
            window.open('AccPrint.aspx?' + vouanptype + '', '_blank');

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
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="AccVoucher" runat="server">
                            <div class="row">

                                <asp:Panel ID="Panel1" runat="server">
                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-sm-12 rbtnList1">
                                                    <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#155273"
                                                        RepeatColumns="6" RepeatDirection="Horizontal" ForeColor="#ffffff" CssClass="form-control">
                                                        <asp:ListItem>Bank Voucher</asp:ListItem>
                                                        <asp:ListItem>Cash Voucher</asp:ListItem>
                                                        <asp:ListItem>Journal Voucher</asp:ListItem>
                                                        <asp:ListItem>Post Dated Cheque</asp:ListItem>
                                                        <asp:ListItem>All Voucher</asp:ListItem>
                                                        <asp:ListItem>Cancellation Voucher</asp:ListItem>

                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-md-4 pading5px">
                                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                                    </cc1:CalendarExtender>

                                                    <asp:Label ID="Label6" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inpPixedWidth"
                                                        TabIndex="1"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                        Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat="">
                                                    </cc1:CalendarExtender>

                                                  
                                                
                                                    <div class="colMdbtn pading5px">
                                                        <asp:LinkButton ID="lnkbtnVouOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnVouOk_Click">Ok</asp:LinkButton>


                                                    </div>

                                                    

                                                </div>
                              
                                                <div class="col-md-2 pading5px">
                                                      <asp:CheckBox ID="Chkpayment" runat="server" TabIndex="10" Text="Payment" CssClass="btn btn-primary checkBox" />
                                                     <asp:CheckBox ID="chktopsheet" runat="server" TabIndex="10" Text="Top Sheet" CssClass="btn btn-primary checkBox" />
                                                   
                                                </div>

                                                <div class="col-md-3 pading5px pull-right">
                                                    <div class="msgHandSt">
                                                        <asp:Label ID="lmsg01" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName">Cheque No</asp:Label>
                                                    <asp:TextBox ID="txtSearchChequeno" runat="server" CssClass=" inputTxt inputName inpPixedWidth"> </asp:TextBox>

                                                </div>
                                            </div>



                                            <div class="form-group">

                                                <asp:ListBox ID="lstVouname" runat="server" CssClass="col-sm-4 form-control" Font-Bold="True"
                                                    Font-Size="12px" Height="297px" SelectionMode="Multiple" Width="350px"></asp:ListBox>
                                                <asp:LinkButton ID="lnkbtnDelVoucher" runat="server" CssClass="btn btn-danger btn-xs" OnClientClick="javascript:return FunConfirm();" OnClick="lnkbtnDelVoucher_Click"
                                                    Visible="False" Width="60px">Delete</asp:LinkButton>
                                                <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="White"></asp:Label>
                                            </div>
                                        </div>
                                    </fieldset>
                                </asp:Panel>
                            </div>
                        </asp:View>
                        <asp:View ID="AccCheque" runat="server">
                            <div class="row">
                                <asp:Panel ID="Panel2" runat="server">

                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-4 pading5px">
                                                    <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                                    <asp:TextBox ID="txtfromdatec" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfromdatec_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdatec">
                                                    </cc1:CalendarExtender>

                                                    <asp:Label ID="Label8" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                    <asp:TextBox ID="txttodatec" runat="server" CssClass="inputTxt inputName inpPixedWidth"
                                                        TabIndex="1"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txttodatec_CalendarExtender" runat="server"
                                                        Format="dd-MMM-yyyy" TargetControlID="txttodatec" TodaysDateFormat="">
                                                    </cc1:CalendarExtender>

                                                       <asp:CheckBox ID="ChboxPayee" runat="server"  CssClass="pull-right" Text="A/C Payee" />

                                                    <div class="colMdbtn pading5px">
                                                        <asp:LinkButton ID="lnkbtnChkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnChkOk_Click">Ok</asp:LinkButton>

                                                    </div>
                                                    
                                                </div>


                                                <div class="col-md-3 pading5px pull-right">
                                                    <div class="msgHandSt">
                                                        <%--<asp:Label ID="lblmsg1" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>--%>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lblVoucher" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                                                    <asp:TextBox ID="txtSearchCheqno" runat="server" CssClass=" inputTxt inputName inpPixedWidth"> </asp:TextBox>
                                                    <asp:LinkButton ID="imgbtnSearchChq" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnSearchChq_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:DropDownList ID="ddlChkVouNo" runat="server" CssClass="form-control inputTxt">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:RadioButtonList ID="rbtCprintList" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                                                        Visible="False">
                                                        <asp:ListItem>All Company</asp:ListItem>
                                                        <asp:ListItem>Rp Land</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                
                                </asp:Panel>
                                <asp:GridView ID="gvCheque" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="577px" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvCheque_PageIndexChanging" PageSize="15">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Voucher #">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbntUpPayto" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkbntUpPayto_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvounum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum2")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Voucher Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvVouDat" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                         <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvChequeDat" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAmount" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0);  ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pay To">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvPayto" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                                    Width="20px"
                                                    Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="False" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>

                            </div>

                        </asp:View>
                        <asp:View ID="PostDatedCheque" runat="server">
                            <div class="row">
                                <asp:Panel ID="Panel4" runat="server">
                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-4 pading5px">
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                                    <asp:TextBox ID="txtfromdatec1" runat="server" CssClass="inputTxt inputName inpPixedWidth" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Enabled="True" Format="dd.MMM.yyyy" TargetControlID="txtfromdatec1">
                                                    </cc1:CalendarExtender>

                                                    <asp:Label ID="Label2" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                                    <asp:TextBox ID="txttodatec1" runat="server" CssClass="inputTxt inputName inpPixedWidth"
                                                        TabIndex="1"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                                        Format="dd-MMM-yyyy" TargetControlID="txttodatec1" TodaysDateFormat="">
                                                    </cc1:CalendarExtender>

                                                     <asp:CheckBox ID="chkpayee" runat="server"  CssClass="pull-right" Text="A/C Payee" />

                                                    <div class="colMdbtn pading5px">
                                                        <asp:LinkButton ID="btnPostDatChqOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="btnPostDatChqOk_Click">Ok</asp:LinkButton>

                                                    </div>

                                                </div>


                                                <div class="col-md-3 pading5px pull-right">
                                                    <div class="msgHandSt">

                                                        <asp:Label ID="lmsg02" CssClass="btn-danger btn disabled" runat="server"></asp:Label>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                                                    <asp:TextBox ID="txtSearchPCheqno" runat="server" CssClass=" inputTxt inputName inpPixedWidth"> </asp:TextBox>
                                                    <asp:LinkButton ID="imgbtnSearchPChq" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnSearchPChq_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:DropDownList ID="ddlPostDatedCheque" runat="server" CssClass="form-control inputTxt">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-4 pading5px ">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                                                        Visible="False">
                                                        <asp:ListItem>All Company</asp:ListItem>
                                                        <asp:ListItem>Rp Land</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>
                                </asp:Panel>

                                <asp:GridView ID="gvPostDatCheq" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="577px" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvPostDatCheq_PageIndexChanging" PageSize="15">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Voucher #">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbntUpPayto1" runat="server" Font-Bold="True"
                                                    
                                                    CssClass="btn btn-danger primaryBtn" OnClick="lnkbntUpPayto1_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvounum1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Voucher Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvVouDat1" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAmount1" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0);  ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvChqNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvChqDat1" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay To">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvPayto1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                                    Width="20px"
                                                    Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="False" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </div>
                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

