<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccChqueDeposit.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccChqueDeposit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script language="javascript" type="text/javascript">
                $(document).ready(function () {
                    //For navigating using left and right arrow of the keyboard
                    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
                    $('.chzn-select').chosen({ search_contains: true });
                });
                function pageLoaded() {

                    $("input, select").bind("keydown", function (event) {
                        var k1 = new KeyPress();
                        k1.textBoxHandler(event);
                    });

                    $('.chzn-select').chosen({ search_contains: true });

                };

                function Confirmation() {
                    if (confirm('Are you sure you want to save?')) {
                        return;
                    } else {
                        return false;
                    }
                }
            </script>
            <style>
                .chzn-container-single .chzn-single {
                    height: 29px !important;
                    line-height: 29px !important;
                }
           div#ContentPlaceHolder1_ddlChequeNo_chzn {
                width: 100%!important;
            }
                .mt20 {
                    margin-top:20px;
                }
            </style>

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
            <div class="card card-fluid mb-1">
            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">

                        <asp:Panel ID="PnlDeposit" runat="server" Visible="false">                          
                            <div class="row">
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt lblName" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblBankName" runat="server" CssClass="lblTxt lblName" Text="Accounts Head"></asp:Label>
                                        <asp:LinkButton ID="ibtnSrchBank" CssClass="srearchBtn" runat="server" OnClick="ibtnSrchBank_Click" TabIndex="5"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtSerchBank" runat="server" Visible="false" CssClass="form-control form-control-sm" TabIndex="4"></asp:TextBox>
                                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3" id="divreshead" runat="server" Visible="false">
                                    <div class="form-group">
								        <asp:Label ID="lblreshead" runat="server" CssClass="lblTxt lblName" Text="Resource Head"></asp:Label>
                                        <asp:LinkButton ID="lbtnreshead" CssClass="srearchBtn" runat="server" OnClick="lbtnreshead_Click" TabIndex="5"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtsrchres" runat="server" Visible="false" CssClass="form-control form-control-sm " TabIndex="4"></asp:TextBox>                                                            
                                        <asp:DropDownList ID="ddlresource" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblProName" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:LinkButton ID="ibtnSrchProject" CssClass="srearchBtn" runat="server" OnClick="ibtnSrchProject_Click" TabIndex="8"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtSerchProject" runat="server" Visible="false" CssClass="form-control form-control-sm" TabIndex="7"></asp:TextBox>
                                        <asp:DropDownList ID="ddlProName" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="9">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblfrmdate0" runat="server" CssClass="lblTxt lblName" Text="Cheque No"></asp:Label>
                                        <asp:LinkButton ID="imgSearchCheque" CssClass="srearchBtn" runat="server" OnClick="imgSearchCheque_Click" TabIndex="8"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtSrchChequeno" runat="server" CssClass="form-control form-control-sm" TabIndex="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblDepositDate" runat="server" CssClass="lblTxt lblName" Text="Deposit Date"></asp:Label>
                                        <asp:TextBox ID="txtDepositDate" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDepositDate"></cc1:CalendarExtender>
                                    </div>
                                </div>                                                    
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkOk_Click" TabIndex="2">Ok</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblPage" runat="server" Text="Page" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" Width="95px" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="13">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>                                          
                        </asp:Panel>

                        <asp:Panel ID="Panel1" runat="server" Visible="false">                                    
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfDate" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttDate" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text=" Cheque No"></asp:Label>
                                            <asp:LinkButton ID="imgBtnChq" CssClass="srearchBtn" runat="server" OnClick="imgBtnChq_Click" TabIndex="8"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtChqno" runat="server" CssClass="form-control form-control-sm" TabIndex="7"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkBtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkBtnOk_Click" TabIndex="2">Ok</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" Text="Page" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:DropDownList ID="ddlPageSize1" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlPageSize1_SelectedIndexChanged" TabIndex="13">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>                                    
                    </asp:Panel>

                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-md-6 col-sm-6 col-lg-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblchequeno" runat="server" CssClass="lblTxt lblName" Text=" Issue/Cheque No"></asp:Label>
                                        <asp:LinkButton ID="ibtnFindChequeno" CssClass="srearchBtn" runat="server" OnClick="ibtnFindChequeno_Click" TabIndex="8"><i class="fas fa-search"></i></asp:LinkButton>
                                        <asp:TextBox ID="txtIssSch" runat="server" Visible="false" CssClass="form-control form-control-sm" TabIndex="7"></asp:TextBox>
                                        <asp:DropDownList ID="ddlChequeNo" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="9">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="col-md-4 col-sm-4 col-lg-4">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkorcheqnoasc" runat="server" AutoPostBack="True" OnCheckedChanged="chkorcheqnoasc_CheckedChanged" Text="Assending Order" CssClass="btn btn-primary btn-sm mt20" />
                                    </div>
                                </div> 
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnSelectChequeNo" runat="server" OnClick="lbtnSelectChequeNo_Click" CssClass="btn btn-primary btn-sm mt20"
                                            TabIndex="10">Select</asp:LinkButton>
                                    </div>
                                </div>
                            </div>                                                            
                        </asp:Panel>

                    </div>
                </div>                                            
            </div>
            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewEntry" runat="server">
                                
                                <asp:GridView ID="dgv1" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" OnPageIndexChanging="dgv1_PageIndexChanging"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAccCod" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcUcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Deposit Slip">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtdepositslip" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "depositslip")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acc. Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcPactdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                           <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Description" >

                                            <ItemTemplate>
                                                <asp:Label ID="lgcUdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrmrks" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MR. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>' Width="70"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBaName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque No">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotal" runat="server" Text="Total :" CssClass="btn btn-primary btn-sm" Width="70"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCheNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCheDate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdramt" runat="server" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkdep" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkdep"))=="True" %>'
                                                    Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>

                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="MgtChqDepEntry" runat="server">
                                
                                <asp:GridView ID="grvMgtChqDep" runat="server" AllowPaging="True" CssClass="table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="grvMgtChqDep_PageIndexChanging"
                                    ShowFooter="True" OnRowCancelingEdit="grvMgtChqDep_RowCancelingEdit"
                                    OnRowUpdating="grvMgtChqDep_RowUpdating" OnRowEditing="grvMgtChqDep_RowEditing">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBaName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtSerachBank" runat="server" BorderStyle="Solid"
                                                                    BorderWidth="1px" Height="18px" TabIndex="5" Width="86px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnSrchBank1" runat="server" Height="16px"
                                                                    ImageUrl="~/Image/find_images.jpg" OnClick="ibtnSrchBank1_Click" TabIndex="6"
                                                                    Width="16px" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlgvBankName" runat="server" Width="200px" TabIndex="7">
                                                                </asp:DropDownList>
                                                                <cc1:ListSearchExtender ID="ddlgvBankName_ListSearchExtender" runat="server"
                                                                    Enabled="True" QueryPattern="Contains" TargetControlID="ddlgvBankName">
                                                                </cc1:ListSearchExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>

                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deposited Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDepDate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "depositdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvDepDate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvDepDate"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Pay/Received Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdepositDate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "depositdat1")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deposit Slip">
                                            <ItemTemplate>
                                                <asp:Label ID="txtdepositslip" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "depositslip")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acc. Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcPactdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcUdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MR. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCheNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay/Received Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPayDate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="PayChqCleared" runat="server">

                                
                                <asp:GridView ID="grvRegChqCl" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" Width="963px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="grvRegChqCl_PageIndexChanging"
                                    OnRowDeleting="grvRegChqCl_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />

                                        <asp:TemplateField HeaderText="Issue #">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Received Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Bill Nature">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billnat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                           <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnregUpdate" runat="server" CssClass="btn btn-danger primaryBtn"  OnClick="lbtnregUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Name of Party">
                                           <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnDeleteAll" runat="server" Font-Bold="True"
                                                    Font-Size="12px"  ForeColor="#000" OnClick="lbtnDeleteAll_Click">Delete All</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref No">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvRef" runat="server" Font-Bold="True" Font-Size="12px"
                                                     ForeColor="#000" Style="text-align: right" Width="80px">Total</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                     ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Appx. payment Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCdat" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                            </asp:View>

                        </asp:MultiView>
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="col-md-1 pading5px pull-right">
                                <a class=" btn btn-primary btn-sm nextPrev " href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=22")%>' style="margin: 0 0 0 5px;">Next</a>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


