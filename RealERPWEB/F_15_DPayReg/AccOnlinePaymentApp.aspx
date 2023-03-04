<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccOnlinePaymentApp.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.AccOnlinePaymentApp" %>

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

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
    </script>

    <style>
        .moduleItemWrpper h3 {
            background: #bfe7e7 none repeat scroll 0 0;
            border-top-left-radius: 50px;
            border-top-right-radius: 50px;
            box-shadow: 0 0 0 4px #ddffff, 2px 1px 6px 4px rgba(10, 10, 0, 0.5);
            color: #000;
            font-size: 20px;
            font-weight: 600;
            line-height: 6px;
            margin: 22px;
            padding: 10px 0;
            text-align: center;
        }
    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">


                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server"  Text="Date"></asp:Label>
                                <asp:TextBox ID="txtpaymentdate" OnTextChanged="txtpaymentdate_TextChanged" AutoPostBack="True" AutoCompleteType="Disabled" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtpaymentdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtpaymentdate"></cc1:CalendarExtender>
                            </div>


                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblSearch" runat="server"  Text="Search"></asp:Label>

                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                            
                        </div>
                        <div class="col-md-1" style="margin-top:21px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary primaryBtn"
                                OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                             
                        </div>
                        <div class="col-md-3 pading5px pull-right d-none">
                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                        </div>



                    </div>
                    </div>
                </div>
             <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row  table-responsive">
                        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="782px" CssClass=" table-striped  table-bordered grvContentarea"
                            OnRowDataBound="gvPayment_RowDataBound" OnRowDeleting="gvPayment_RowDeleting">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="Cancel" />

                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnubillm" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Payment Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Received Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved By">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvUsrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%#  "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "<span class=gvdesc>"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")+ "</span>" %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bill Amt." Visible="false">
                                    <FooterTemplate>
                                        <asp:Label ID="lblFTotal" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Amt." Visible="false">
                                    <FooterTemplate>
                                        <asp:Label ID="lblFBalAmt" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblFApamt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvAppramt" runat="server" BorderColor="#99CCFF" BackColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Cheque" HeaderStyle-ForeColor="Red" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvchq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nochq")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <HeaderStyle ForeColor="Red" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Distribution amount" HeaderStyle-ForeColor="Red" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle ForeColor="Red" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Date">

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-sm  btn-success primaryBtn" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>



                                    <ItemTemplate>
                                        <asp:Label ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apppaydate")).ToString("dd-MMM-yyyy")  %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Source Id">

                                    <ItemTemplate>
                                        <asp:Label ID="txtgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkapp" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkapp"))=="True" %>'
                                            Width="20px" />
                                    </ItemTemplate>

                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllapp" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkAllapp_CheckedChanged" Width="20px" />

                                    </HeaderTemplate>

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-sm btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Approved</asp:LinkButton>

                                    </FooterTemplate>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Bill Nature" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                            </Columns>

                             <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="" />
                        <RowStyle CssClass="grvRowsNew" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                        </asp:GridView>
                    </div>

                    <div class="col-md-2 pading5px asitCol2 pull-right">
                        <asp:Label ID="lblbank" runat="server" Visible="false" Style="margin-left: -261px;" CssClass="lblTxt lblName" v
                            TabIndex="3">Bank Name :</asp:Label>
                        <asp:DropDownList ID="ddlbanklist" runat="server" Visible="false" Style="margin-left: -158px;" Width="200px" CssClass=" chzn-select inputTxt" TabIndex="6">
                        </asp:DropDownList>

                    </div>
                    </div>
                 </div>
             <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">
                        <asp:Panel ID="PanelNar" runat="server">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="lblNarration" runat="server" CssClass="lblTxt lblName" Text="Narration"></asp:Label>

                                    <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="4" TextMode="MultiLine" Width="500px" Font-Size="12px"></asp:TextBox>

                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Panel ID="PanelNote" runat="server" Visible="true">
                            <div class="row">


                                <div class="col-md-12">
                                    <asp:HyperLink ID="lbtnBankPos" runat="server" CssClass="btn btn-sm btn-success" Visible="False" Target="_blank"
                                        OnLoad="lbtnBankPos_Load"> Bank Position</asp:HyperLink>
                                    <asp:Label  runat="server" CssClass="btn btn-sm btn-success mb-3" >Bank Position:</asp:Label>
                                    <div class="clearfix"></div>
                                    
                                        <asp:Label ID="lblCl" runat="server" CssClass="lblTxt lblName" Text="Bank Balance"></asp:Label>
                                        <%-- <asp:Label ID="lblClAmt" runat="server" CssClass=" smLbl_to"></asp:Label>--%>
                                        <asp:HyperLink ID="Hyplnk" runat="server" ToolTip="Click Details bank position" CssClass="btn btn-danger btn-sm" NavigateUrl="~/F_17_Acc/AccTrialBalance.aspx?Type=BankPosition&comcod=&Date1=&Date2=" Target="_blank"></asp:HyperLink>

                                        <div class="clearfix"></div>
                                    

                                    <%--                                    <asp:Panel ID="bankpso" Visible="false" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="lbllssue" runat="server" CssClass="lblTxt lblName" Text="Issue Amount"></asp:Label>
                                            <asp:Label ID="lbllssueAmt" runat="server" CssClass=" smLbl_to" Text="Closing Balance"></asp:Label>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblColl" runat="server" CssClass="lblTxt lblName" Text="Coll Amount"></asp:Label>
                                            <asp:Label ID="lblCollAmt" runat="server" CssClass=" smLbl_to" Text="Closing Balance"></asp:Label>
                                            <div class="clearfix"></div>
                                        </div>

                                        <div class="form-group">
                                            <asp:Label ID="lblnet" runat="server" CssClass="lblTxt lblName" Text="Net Balance"></asp:Label>
                                            <asp:Label ID="lblnetBal" runat="server" CssClass=" smLbl_to" Text="Closing Balance"></asp:Label>
                                            <div class="clearfix"></div>
                                        </div>
                                    </asp:Panel>--%>
                                </div>


                            </div>


                        </asp:Panel>
                            </div>
                        </div>
                        
                    </div>

                </div>
            </div>



            <%--<table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Panel ID="pnlMain" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style21">&nbsp;</td>
                                    <td class="style7">
                                        <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Text="Approval Date:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style16">
                                        <asp:TextBox ID="txtpaymentdate" runat="server" AutoCompleteType="Disabled"
                                            AutoPostBack="True" BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px"
                                            OnTextChanged="txtpaymentdate_TextChanged" TabIndex="12" Width="80px"></asp:TextBox>

                                    </td>
                                    <td class="style20">
                                        <asp:Label ID="lblSearch" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Text="Search"></asp:Label>
                                    </td>
                                    <td class="style14">
                                        <asp:TextBox ID="txtSearch" runat="server" BorderStyle="None" TabIndex="1"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style15">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White" OnClick="lbtnOk_Click"
                                            Style="text-align: center;" Width="60px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style11">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White" Style="color: #FFFFFF"></asp:Label>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>--%>


            <script type="text/javascript">
                function HideLabel() {
                    var seconds = 5;
                    setTimeout(function () {
                        document.getElementById("<%=lmsg.ClientID %>").style.display = "none";
                    }, seconds * 1000);
                };
            </script>





        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
