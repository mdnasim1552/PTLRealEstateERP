<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBankCheque.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptBankCheque" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gvChqdep = $('#<%=this.gvChqdep.ClientID %>');
            var dgvChqHand = $('#<%=this.dgvChqHand.ClientID %>');
            var dgvIsu = $('#<%=this.gvIsuCleared.ClientID %>');
            //            var CashFlow = $('#<%=this.grvCashFlow.ClientID %>');


            gvChqdep.Scrollable();
            dgvChqHand.Scrollable();
            dgvIsu.Scrollable();

            //CashFlow.Scrollable();


            $('.chzn-select').chosen({ search_contains: true });


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
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
            <div class="row">
                <asp:Panel ID="Panel1" runat="server">
                    <fieldset class="scheduler-border fieldset_A rptBankcheque">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:Label ID="lblFdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                    <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtfromdate" Enabled="true"></cc1:CalendarExtender>
                                    <asp:Label ID="lblTdate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                    <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page"></asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                        TabIndex="2" CssClass=" ddlPage">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>150</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-3 pading5px pull-right">
                                    <div class="msgHandSt">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="lblProgressbar" runat="server" CssClass="lblProgressBar" Text="Please Wait.........."></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblOpeningDate" runat="server" CssClass="lblTxt lblName" Text="Opening Date"
                                        Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtOpeningDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"
                                        Visible="false"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtOpeningDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtOpeningDate" Enabled="true"></cc1:CalendarExtender>
                                </div>
                                 <div class="col-md-4 pading5px asitCol4" style="margin-left: -50px;">
                                    <asp:Label ID="lblproj" runat="server" CssClass="lblTxt lblName" Text="Project"
                                        Visible="false"></asp:Label>
                                     <asp:DropDownList ID="ddlproj" runat="server" Width="220px" Visible="False" CssClass="chzn-select form-control inputTxt">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblChqNo" runat="server" CssClass="smLbl_to">Cheque</asp:Label>
                                    <asp:TextBox ID="txtSrchChequeno" runat="server" CssClass="  inputtextbox"></asp:TextBox>
                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="imgSearchCheque" runat="server" CssClass="btn btn-primary srearchBtn"
                                            OnClick="imgSearchCheque_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-2 pading5px asitCol2">
                                    <asp:CheckBox ID="chkDeposit" runat="server" CssClass="btn btn-primary checkBox"
                                        Text="With Deposit" Visible="False" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblgroup" runat="server" CssClass=" lblTxt lblName" Text="Group" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlRptGroup" runat="server" Visible="False" CssClass="ddlPage">
                                        <asp:ListItem Value="2">Level-1</asp:ListItem>
                                        <asp:ListItem Value="4" Selected="True">Level-2</asp:ListItem>
                                        <asp:ListItem Value="8">Level-3</asp:ListItem>
                                        <asp:ListItem Value="12">Details</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </asp:Panel>
            </div>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="ChqDepositRpt" runat="server">
                    <asp:Panel ID="Panel3" runat="server">

                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblBankName" runat="server" CssClass="lblTxt lblName">Bank Name</asp:Label>
                                        <asp:TextBox ID="txtSerchBank" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnSrchBank" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnSrchBank_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                    </div>


                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                    </div>




                                    <div class="col-md-3 pading5px asitCol3">

                                        <asp:Label ID="lblrefNo" runat="server" CssClass="smLbl">Ref. No.:</asp:Label>
                                        <asp:TextBox ID="txtBillRef" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>



                                    </div>

                                </div>

                            </div>
                        </fieldset>

                    </asp:Panel>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvChqdep" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AllowPaging="True" OnPageIndexChanging="gvChqdep_PageIndexChanging1"
                            OnRowDataBound="gvChqdep_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNor" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbankdesc" runat="server" Font-Bold="true" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "depositbank"))    %>'
                                            Width="200px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MR. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrnodep" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "mrno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")).Trim() : "")
                                                                         
                                                                    %>'
                                            Width="120px"> </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrDatedep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccCoddep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUcodedep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcPactdescdep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name Of Client">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcustnamedep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Flat No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvudescdep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrmrksdep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCheNodep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chq. Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchqDatedep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBaNamedep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBranchdep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash Amount">
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcashamtdep" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Amount">
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchqamtdep" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dep. Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdepDatedep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "depositdat"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvretDatedep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dhonrdate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cleared Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvclrDatedep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bcleardate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                <asp:View ID="ChqInHand" runat="server">
                    <div class="table table-responsive">
                        <asp:GridView ID="dgvChqHand" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnPageIndexChanging="dgvChqHand_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Width="1052px" OnRowDataBound="dgvChqHand_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MR. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrno" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "mrno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")).Trim() : "")
                                                                         
                                                                    %>'
                                            Width="100px"> </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccCod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcPactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name Of Client">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcustname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Flat No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvudesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrmrks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCheNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chq. Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchqDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dept. Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdeptname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBaName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBranch" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash Amount">
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcashamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Amount">
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchqamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dep. Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdepDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "depositdat"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvretDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dhonrdate"))%>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                <asp:View ID="ToDayIssueChq" runat="server">

                    <asp:Panel ID="Panel4" runat="server">


                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Bank Name</asp:Label>
                                        <asp:TextBox ID="txtSerchBank1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgBtn" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgBtn_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlBankName1" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                            </div>
                        </fieldset>


                    </asp:Panel>
                    <div class="table table-responsive">
                        <asp:GridView ID="grvToDayIssChq" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea" Width="963px" OnPageIndexChanging="grvToDayIssChq_PageIndexChanging"
                            OnRowDataBound="grvToDayIssChq_RowDataBound" AllowPaging="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccCod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cat.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgcatCod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ResCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbankname" runat="server" Font-Bold="true" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "cactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")).Trim(): "")  %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPVnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvounum1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPVDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgactdesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchnono" runat="server" Width="100px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchdat" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Issued Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvCrAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Cleared Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreconamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFReconAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cleared Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbcldate" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bcldate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvacvounum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acvounum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdramt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvParName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Narration">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvNarration" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vnar")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                <asp:View ID="ClientMod" runat="server">
                    <div class="table table-responsive">
                        <asp:GridView ID="grvRptCliMod" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea" Width="963px" OnPageIndexChanging="grvRptCliMod_PageIndexChanging"
                            AllowPaging="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ADW No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAdwNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "patcdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modification">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Date" FooterText="Total: " FooterStyle-ForeColor="#000">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
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
                <asp:View ID="CleintChqStatus" runat="server">
                    <asp:Panel ID="Panel5" runat="server">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblchequenocl" runat="server" CssClass="lblTxt lblName">Issue/Cheque No</asp:Label>
                                        <asp:TextBox ID="txtIssSchcl" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindChequenocl" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindChequenocl_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlChequeNocl" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-4">
                                        <asp:LinkButton ID="lbtnSelectChequeNocl" runat="server" OnClick="lbtnSelectChequeNocl_Click" CssClass="btn btn-primary primaryBtn"
                                            TabIndex="10">Select</asp:LinkButton>
                                        <asp:CheckBox ID="chkorcheqnoasccl" runat="server" AutoPostBack="True" OnCheckedChanged="chkorcheqnoasccl_CheckedChanged" Text="Assending Order" CssClass="chkBoxControl margin5px" />
                                    </div>
                                </div>

                            </div>
                        </fieldset>


                    </asp:Panel>
                    <div class="table table-responsive">
                        <asp:GridView ID="grvCollChqStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea" Width="963px" OnPageIndexChanging="grvCollChqStatus_PageIndexChanging"
                            AllowPaging="True" OnRowDeleting="grvCollChqStatus_RowDeleting">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Received Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReDat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mr No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMrNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                              <%--      <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdatecl" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" OnClick="lbtnUpdatecl_Click">Update</asp:LinkButton>
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Description">
                                    <%--<FooterTemplate>
                                        <asp:LinkButton ID="lbtnDeleteAllcl" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" OnClick="lbtnDeleteAllcl_Click">Delete All</asp:LinkButton>
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Diposite Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "banname")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Diposite Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDipDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "depositdat")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Dishonour Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDisDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dhonrdate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reconcilation Date" FooterText="Total: " FooterStyle-ForeColor="#000">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRecDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Collection From ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCollfrm" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "collfrm")) %>'
                                            Width="160px"></asp:Label>
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
                    </div>

                </asp:View>
                <asp:View ID="PayChqCleared" runat="server">
                    <asp:Panel ID="Panel2" runat="server">
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblchequeno" runat="server" CssClass="lblTxt lblName">Issue/Cheque No</asp:Label>
                                <asp:TextBox ID="txtIssSch" runat="server" CssClass=" inputTxt inputName inpPixedWidth" TabIndex="7"></asp:TextBox>



                                <div class="colMdbtn">
                                    <asp:LinkButton ID="ibtnFindChequeno" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindChequeno_Click" TabIndex="8"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>

                            </div>

                            <div class="col-md-4 pading5px">
                                <asp:DropDownList ID="ddlChequeNo" runat="server" CssClass="form-control inputTxt" TabIndex="9">
                                </asp:DropDownList>

                            </div>

                            <div class="col-md-1 pading5px">


                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lbtnSelectChequeNo" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectChequeNo_Click" TabIndex="10">Select</asp:LinkButton>

                                </div>



                            </div>

                            <div class="col-md-2 pading5px">
                                <asp:CheckBox ID="chkorcheqnoasc" runat="server" AutoPostBack="True" CssClass="checkbox" OnCheckedChanged="chkorcheqnoasc_CheckedChanged" Text="Assending Order" />
                            </div>



                        </div>



                    </asp:Panel>
                    <div class=" row table table-responsive">
                        <asp:GridView ID="grvPayChqCl" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea" Width="963px" OnPageIndexChanging="grvCollChqStatus_PageIndexChanging"
                            OnRowDeleting="grvPayChqCl_RowDeleting" OnSelectedIndexChanged="grvPayChqCl_SelectedIndexChanged">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvounum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvIssDat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvIssuNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name of Party">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Account">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid For">
                                  <%--  <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server"  CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvNar" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "varnar")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                  <%--  <FooterTemplate>
                                        <asp:LinkButton ID="lbtnDeleteAll" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnDeleteAll_Click">Delete All</asp:LinkButton>
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                            Width="110px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCdat" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reconcilation Date" FooterText="Total: " FooterStyle-ForeColor="#000">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRecDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
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
                <asp:View ID="ViewCashFlow" runat="server">
                

                  
                  <div class="row">
                        <asp:GridView ID="grvCashFlow" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            OnRowDataBound="grvCashFlow_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Width="326px" >
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Font-Bold="true"/>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accountcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvactcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                

                                    <asp:TemplateField HeaderText="Description">
                                               
                                                
                                                    <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblheader" runat="server" Text="Description"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExelsp" runat="server"   CssClass=" btn btn-danger btn-xs"   Font-Bold="true"  BackColor="#000066" BorderColor="White" BorderWidth="1px" BorderStyle="None" Text="Export to Excel"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                
                                                
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnactDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                     
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="350px" Font-Underline="False" Style="color: Black"
                                            OnClick="lbtnactDesc_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>








                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Change During The Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcuamtcf" runat="server" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:HyperLink></ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopnamcf" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Change" ItemStyle-HorizontalAlign="Right"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvclosamcf" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                      
                    </div>
                </asp:View>
                <asp:View ID="ViewPostDatedCheque" runat="server">
                     <div class="table table-responsive">
                    <asp:GridView ID="dgvChqHandPost" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="dgvChqHandPost_PageIndexChanging" OnRowDataBound="dgvChqHandPost_RowDataBound"
                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="1052px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNop" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MR. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmrnop" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno"))      %>'
                                        Width="100px"> </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvmrDatep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate"))%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acc. Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgcPactdescp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="130px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name Of Client">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcustnamep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Flat No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvudescp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lgvrmrksp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cheque No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvCheNop" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chq. Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvchqDatep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBaNamep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBranchp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cash Amount">
                                <FooterTemplate>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvcashamtp" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cheque Amount">
                                <FooterTemplate>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvchqamtp" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                </asp:View>
                <asp:View ID="ViewIssueVsClear" runat="server">
                     <div class="table table-responsive">
                    <asp:GridView ID="gvIsuCleared" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        CssClass="table-striped table-hover table-bordered grvContentarea" Width="963px" OnPageIndexChanging="gvIsuCleared_PageIndexChanging"
                        AllowPaging="True" OnRowDataBound="gvIsuCleared_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCatcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc1")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue #">
                                <ItemTemplate>
                                    <asp:Label ID="lblgVounum1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cheque No">
                                <ItemTemplate>
                                    <asp:Label ID="lgcChq1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno1")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFChq1" runat="server" Font-Bold="True" CssClass="btn btn-default primaryBtn">Total:</asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Issued Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvIsuAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFIsuAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                        Style="text-align: right" Width="70px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPrjName1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvParName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto1")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCatcode2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc2")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voucher #">
                                <ItemTemplate>
                                    <asp:Label ID="lblgVounum2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum2")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cheque No">
                                <ItemTemplate>
                                    <asp:Label ID="lgcChq2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno2")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lgvFChq2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">Total:</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lgvFChq21" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">Grand Total:</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cleared from Current Issue">
                                <ItemTemplate>
                                    <asp:Label ID="lgvCuamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lgvFCuamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px">0</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cleared from Prev. Issue">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lgvFPramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lgvFPramt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="70px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPrjName2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc2")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvParName2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto2")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPVDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isudat")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
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
                <asp:View ID="ViewFundFlow" runat="server">

                    <asp:GridView ID="grvfundFlow" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvfundFlow_RowDataBound"
                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Accountcode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvactcodeff" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnactDescff" runat="server" Font-Underline="False" OnClick="lbtnactDescff_Click"
                                        Style="color: Black" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc1").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                        "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc1")).Trim(): "") + "</B>"  + 
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc1")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "")
                                                                    %>'
                                        Width="300px"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cash Flow Amount">
                                <FooterTemplate>
                                    <asp:HyperLink ID="lgvFCashflowff" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right" Target="_blank" Width="100px"></asp:HyperLink>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvcfamtff" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cfamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fund Flow Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFfundflow" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                        Style="text-align: right" Target="_blank" Width="100px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvffamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ffamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                    </asp:GridView>

                </asp:View>
                <asp:View ID="ViewCashFlow02" runat="server">
                    <div class="row">
                        <asp:GridView ID="grvCashFlow02" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvCashFlow02_RowDataBound"
                            ShowFooter="True" Width="520px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accountcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvactcode0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesccf02" runat="server" Font-Underline="False" Style="color: Black"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="280px">
                                   
                                                                    


                                     
                                                                    
                                                                    
                                                                    
                                                                    
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Change During The Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcuamtcf02" runat="server" Font-Size="10px" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopnamcf02" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Change" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvclosamcf02" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                        <asp:Panel ID="PanelNote02" runat="server" Visible="False">
                            <asp:Label ID="lblBankstatus0" runat="server" BackColor="#000066" BorderColor="White"
                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Yellow" Text="Bank Status:"
                                Width="120px"></asp:Label>
                        </asp:Panel>
                        <asp:GridView ID="gvbankbal02" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvbankbal02_RowDataBound"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNcf02" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcActDescbb02" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc"))
                                                                        
                                                                         
                                                                    %>'
                                            Width="280px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Change">
                                    <ItemTemplate>
                                        <asp:Label ID="lgbalambb02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvopnambb02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvclosambb02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                <asp:View ID="ViewFinancialNote" runat="server">
                </asp:View>
                
                <asp:View ID="vwCashFlowproj" runat="server">
                

                  
                  <div class="row">
                        <asp:GridView ID="gvcashflowprj" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            OnRowDataBound="gvcashflowprj_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Width="326px" >
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNoprj" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Font-Bold="true"/>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accountcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvactcodeprj" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                

                                    <asp:TemplateField HeaderText="Description">
                                               
                                                
                                                    <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblheaderprj" runat="server" Text="Description"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExelspprj" runat="server"   CssClass=" btn btn-danger btn-xs"   Font-Bold="true"  BackColor="#000066" BorderColor="White" BorderWidth="1px" BorderStyle="None" Text="Export to Excel"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                
                                                
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnactDescprj" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                     
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="350px" Font-Underline="False" Style="color: Black"
                                            OnClick="lbtnactDesc_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>








                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Change During The Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcuamtcfprj" runat="server" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:HyperLink></ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopnamcfprj" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Change" ItemStyle-HorizontalAlign="Right"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvclosamcfprj" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                      
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
        <!-- End of contentpart-->
    </div>
    <!-- End of Container-->
     </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>
