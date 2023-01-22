<%@ Page Title="" Language="C#"   MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AllVoucherTopSheet.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AllVoucherTopSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">


        function Search_Gridview(strKey, cellNr) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvAccVoucher.ClientID %>");
            var rowData;
            for (var i = 0; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {

                $("input, select")
                    .bind("keydown",
                        function (event) {
                            var k1 = new KeyPress();
                            k1.textBoxHandler(event);
                        });

                $('.chzn-select').chosen({ search_contains: true });
                $('#<%=this.gvAccVoucher.ClientID%>').tblScrollable();
                $('#<%=this.gvvoucountsum.ClientID%>').tblScrollable();



            }
            catch (e) {
                alert(e.mesage);
            }
        };

    </script>


    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
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


            <div class=" card card-fluid" style="min-height: 600px">
                <div class=" card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="  form-group">
                                <label for="Label5" runat="server" class=" control-label  lblmargin-top9px ">From</label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputDateBox" ToolTip="(dd.mmm.yyyy)" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class=" form-group">
                                <label for="Label6" runat="server" class=" control-label lblmargin-top9px">To</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputDateBox"
                                    TabIndex="1"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>

                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class="  form-group">
                                <label for="lblrefno" runat="server" class=" control-label  lblmargin-top9px">Ref.</label>
                                <asp:TextBox ID="txtrefno" runat="server" CssClass="inputTextBox"></asp:TextBox>



                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="  form-group">
                                <label for="lblPrivVou" runat="server" class=" control-label lblmargin-top9px ">Type</label>
                                <asp:DropDownList ID="ddlvoucher" runat="server" CssClass=" ddlPage120px" AutoPostBack="True">
                                    <asp:ListItem Value="BD">Bank Payment</asp:ListItem>
                                    <asp:ListItem Value="CD">Cash Payment</asp:ListItem>
                                    <asp:ListItem Value="BC">Bank Deposit</asp:ListItem>
                                    <asp:ListItem Value="CC">Cash Deposit</asp:ListItem>
                                    <asp:ListItem Value="CT">Contra Voucher</asp:ListItem>
                                    <asp:ListItem Value="JV">Journal Voucher</asp:ListItem>
                                    <asp:ListItem Value="PV">PDC Issue</asp:ListItem>


                                    <asp:ListItem Value="DV">PDC Received</asp:ListItem>

                                    <asp:ListItem Value="" Selected="True">All Voucher</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="  form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                <label id="chkbod" runat="server" class="switch">
                                    <asp:CheckBox ID="ChboxPayee" runat="server" OnCheckedChanged="ChboxPayee_CheckedChanged" AutoPostBack="true" />
                                    <span class="btn btn-xs slider round"></span>
                                </label>
                                <asp:Label runat="server" Text="A/C Payee" CssClass="btn btn-xs"></asp:Label>

                            </div>

                        </div>



                        <div class="col-md-2">
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HpblnkNew" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/GeneralAccounts.aspx?Mod=Accounts&vounum="> New Voucher</asp:HyperLink>
                                        <asp:HyperLink ID="hylnkpdcIssue" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc ">PDC Issue</asp:HyperLink>

                                        <asp:HyperLink ID="hlnkLedger" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccLedgerAll.aspx">Ledger</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/RptAccDayTransData.aspx">Daily transaction</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLinkTriBal" runat="server" CssClass="dropdown-item" Target="_blank">Proj.Trial Balance</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=">Interface</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccBankRecon.aspx?Type=Acc">Bank Reconcilation</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink5" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/CashBankposition.aspx?Type=casbankpos&actcode=">Cash & Bank(Group Wise)</asp:HyperLink>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3 offset-9">
                            <div class=" form-group">
                                <asp:CheckBox ID="checkpb" runat="server" Visible="false" Text=" Pubali Bank" />
                                <asp:CheckBox ID="withoutchqdate" runat="server" OnCheckedChanged="withoutchqdate_CheckedChanged" Text=" Without Cheque Date" Visible="false" AutoPostBack="true" />

                            </div>
                        </div>



                    </div>

                    <div class="row">

                        <div class="col-md-10">
                             
                            <div class="table-responsive">
                                 
                               
                                <asp:GridView ID="gvAccVoucher" runat="server" AutoGenerateColumns="False" PageSize="500"
                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvAccVoucher_RowDataBound"
                                    ShowFooter="True" AllowSorting="True" OnSorting="gvAccVoucher_Sorting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date" SortExpression="voudat">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvoudat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Voucher">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchvounum1" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Voucher No" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblvounum" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchrefnum" SortExpression="refnum" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Cheque No" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtINgvteamdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchequedat" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Cheque Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblchequedat" runat="server" BackColor="Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Project Name">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchproj" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Project Name" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Name">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchbank" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Bank Name" onkeyup="Search_Gridview(this,6)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcactdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Narration">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchvenar" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Narration" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNarration" runat="server" BackColor="Transparent" Style="word-break: break-all"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchpayto" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Party Name" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblogvpayto" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchamt" BackColor="Transparent" BorderStyle="None" runat="server" Width="75px" placeholder="Vou. Amount" onkeyup="Search_Gridview(this,9)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvouamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">

                                            <HeaderTemplate>
                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                    CssClass="btn  btn-primary  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkVoucherPrint" runat="server" Target="_blank" ToolTip="Voucher Print" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkVoucherEdit" ToolTip="Edit" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-edit"></span>
                                                </asp:HyperLink>

                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkChequePrint" runat="server" Target="_blank" ToolTip="Cheque Print" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearusrname"  BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="User Name" onkeyup="Search_Gridview(this,13)"></asp:TextBox><br />


                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblogvusrname" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Voucher" Visible="False">

                                            <ItemTemplate>
                                                <asp:Label ID="lblvounumh" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Voutype" Visible="False">

                                            <ItemTemplate>
                                                <asp:Label ID="lblvoutype" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voutype")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkTopPrintAll" runat="server" Checked="false" AutoPostBack="True" OnCheckedChanged="checkTopPrint_CheckedChanged" />

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkPrint" runat="server" Checked="false" CssClass="input-control" Text="" />

                                            </ItemTemplate>
                                            <FooterTemplate>

                                                
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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


                        <div class="col-md-2">
                            <div class="table-responsive">

                                <asp:GridView ID="gvvoucountsum" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="true">

                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvINSlNo50" runat="server" Font-Bold="false" Font-Size="9px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUsrname" runat="server" Style="text-align: left" Font-Bold="True" Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFUsrname" runat="server" Font-Bold="true" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right">Total : </asp:Label>
                                            </FooterTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Font-Size="9px" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle Font-Bold="True" Font-Size="10px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PDC">
                                            <ItemTemplate>
                                                <asp:Label ID="gvpdc" runat="server" Style="text-align: right" Font-Size="9px" Font-Bold="false"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdcvou")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpdc" runat="server" Font-Bold="True" Font-Size="9px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle Font-Bold="true" Font-Size="9px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="9px" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cash Vou">
                                            <ItemTemplate>
                                                <asp:Label ID="gvCashvou" runat="server" Style="text-align: right" Font-Size="9px" Font-Bold="false"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashvou")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCash" runat="server" Font-Bold="True" Font-Size="9px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle Font-Bold="True" Font-Size="10px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="9px" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Vou.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvBankvou" runat="server" Style="text-align: right" Font-Size="9px" Font-Bold="false"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankvou")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBank" runat="server" Font-Bold="True" Font-Size="9px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle Font-Bold="True" Font-Size="9px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="9px" />

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Contra Vou.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvContravou" runat="server" Style="text-align: right" Font-Size="10px" Font-Bold="false"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "contravou")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFContra" runat="server" Font-Bold="True" Font-Size="9px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle Font-Bold="True" Font-Size="9px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="9px" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Jour Vou.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvJourvou" runat="server" Style="text-align: right" Font-Size="9px" Font-Bold="false"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "jourvou")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFJour" runat="server" Font-Bold="True" Font-Size="9px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle Font-Bold="True" Font-Size="9px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="9px" />

                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Total Vou.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvtotalvou" runat="server" Style="text-align: right" Font-Size="9px" Font-Bold="false"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tonum")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTotalvou" runat="server" Font-Bold="True" Font-Size="9px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <HeaderStyle Font-Bold="True" Font-Size="9px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="9px" />

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



                    <div class="clearfix"></div>
                    <br />
                    <br />

                    <asp:Panel ID="pnlvouCount" runat="server" Visible="false"></asp:Panel>
                    <table class="table table-striped table-condensed" style="width: 80%">
                        <thead>
                            <tr>
                                <th>PDC Voucher:</th>
                                <th>
                                    <asp:Label ID="lbltoPdcVoucher" runat="server" CssClass="lblTxt lblName"></asp:Label></th>
                                <th>Cash Voucher:</th>
                                <th>
                                    <asp:Label ID="lbltoCashVoucher" runat="server" CssClass="lblTxt lblName"></asp:Label></th>
                                <th>Bank Voucher:</th>
                                <th>
                                    <asp:Label ID="lbltoBankVoucher" runat="server" CssClass="lblTxt lblName"></asp:Label></th>
                                <th>Contra Voucher:</th>
                                <th>
                                    <asp:Label ID="lbltoContraVoucher" runat="server" CssClass="lblTxt lblName"></asp:Label></th>

                                <th>Journal Voucher:</th>
                                <th>
                                    <asp:Label ID="lbltoJournalVoucher" runat="server"></asp:Label></th>

                                <th>Total Voucher :</th>
                                <th>
                                    <asp:Label ID="lbltotalvoucher" runat="server"></asp:Label></th>


                            </tr>
                        </thead>

                    </table>
                    </panel>
                  
                   




                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
