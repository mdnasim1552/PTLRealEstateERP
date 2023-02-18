<%@ Page Title="" Language="C#"   MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="DataSyncSystem.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.DataSyncSystem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
           function ShowModal() {
            $('#exampleModalDrawerRight').modal('show');
        }
        function SelectAllCheckboxes(chk) {
            var tblData1 = document.getElementById("<%=gvAccVoucher.ClientID %>");

            var i = 0
                $('#<%=gvAccVoucher.ClientID %>').find("input:checkbox").each(function () {
                    // console.log(tblData1.rows[i].style.display);
                    if ((this).disabled == false && tblData1.rows[i].style.display != "none") {
                        if (this != chk) {
                            this.checked = chk.checked;
                        }
                    }
                    i = i + 1;
                });
            }

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
           


            }
            catch (e) {
                alert(e.mesage);
            }
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


            <div class="card card-fluid">
                <div class=" card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label for="Label5" runat="server" class="control-label"><span class="text-red small">Last Sync Date</span></asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" ToolTip="(dd.mmm.yyyy)" autocomplete="off"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class=" form-group">
                                <asp:Label for="Label6" runat="server" class=" control-label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"
                                    TabIndex="1"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>

                            </div>

                        </div>        

                        <div class="col-md-1">
                            <div class=" form-group" style="margin-top:22px">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click"><span class="fa fa-search-plus"></span> Search</asp:LinkButton>
                           
                            </div>

                        </div>
                          <div class="col-md-1">
                            <div class=" form-group">
                                <asp:Label for="Label6" runat="server" class=" control-label">Res. Missing</asp:Label>
                                <asp:TextBox for="TxtResMiss" ID="TxtResMiss" runat="server" class="form-control form-control-sm bg-twitter"></asp:TextBox>
                                
                                </div>
                              </div>
                           
                          <div class="col-md-1">
                            <div class=" form-group">
                                <asp:Label for="Label6" runat="server" class=" control-label">Acc. Missing</asp:Label>
                                <asp:TextBox for="TxtAccMiss" ID="TxtAccMiss" runat="server" class="form-control form-control-sm bg-twitter"></asp:TextBox>
                                
                                </div>
                              </div>
                          <div class="col-md-1">
                            <div class=" form-group" style="margin-top:22px">
                                <asp:LinkButton ID="LbtnImport" runat="server" CssClass="btn btn-sm btn-success" OnClick="LbtnImport_Click"><span class="fa fa-sync"></span> Import</asp:LinkButton>
                           
                            </div>

                        </div>
                        <div class="col-md-2 d-none">
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
                   
                    



                </div>
            </div>
                 <div class="card card-fluid" style="min-height: 600px">
                <div class=" card-body">
                    <div class="row">
                        <div class="col-md-12">                             
                            <div class="table-responsive">                                 
                               
                                <asp:GridView ID="gvAccVoucher" runat="server" AutoGenerateColumns="False" PageSize="500"
                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvAccVoucher_RowDataBound"
                                    ShowFooter="True" AllowSorting="True" OnSorting="gvAccVoucher_Sorting">
                                    <RowStyle />
                                    <Columns>
                                         <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCol" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>                                             
                                                <asp:CheckBox ID="chkhead" onclick="javascript:SelectAllCheckboxes(this);" CssClass="checkbox" ClientIDMode="Static" runat="server" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
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
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Voucher">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchvounum1" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Voucher No" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblvounum" runat="server" BackColor="Transparent" OnClick="lblvounum_Click"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="90px"></asp:LinkButton>
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
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy") %>'
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


                                        <%--<asp:TemplateField HeaderText="Project Name">
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
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Narration">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchvenar" BackColor="Transparent" BorderStyle="None" runat="server" Width="350px" placeholder="Narration" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNarration" runat="server" BackColor="Transparent" Style="word-break: break-all"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar1")) %>'
                                                    Width="350px"></asp:Label>
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




                                     <%--   <asp:TemplateField HeaderText="">
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
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>                                              
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkVoucherPrint" runat="server" Target="_blank" ToolTip="Voucher Print" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                        </asp:TemplateField>



<%--                                        <asp:TemplateField HeaderText="">
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
                                               </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkPrint" runat="server" Checked="false" CssClass="input-control" Text="" />

                                            </ItemTemplate>
                                            <FooterTemplate>

                                                
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
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

                    </div>
                    </div>
                     </div>
             <%--<a href="#" class="btn btn-sm btn-warning" data-toggle="modal" data-target="#exampleModalDrawerRight" style="font-size:smaller">modal? <%--<i class="fa fa-file-alt">--%></i></a>--%>


            <div class="modal modal-drawer fade has-shown" data-backdrop="static" id="exampleModalDrawerRight" tabindex="-1" role="dialog" aria-labelledby="exampleModalDrawerRightLabel" style="display: none;" aria-hidden="true">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-drawer-right" role="document" style="max-width: 700px !important;">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header modal-body-scrolled">
                            <h5 id="exampleModalDrawerRightLabel" class="modal-title">Voucher Details Information</h5>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body">
                          <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea "
                                        ShowFooter="True" Width="650px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid" runat="server"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccCod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResCod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpclCod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Head of Accounts" Width="200px"></asp:Label>

                                               
                                                </HeaderTemplate>
                                              
                                                <ItemTemplate>



                                                    <asp:HyperLink ID="hlnkAccdesc1" runat="server" Target="_blank" Font-Size="10px"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") +                                                                           
                                                                        " &nbsp;&nbsp;&nbsp;&nbsp;"+Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                                        Width="250px" Font-Names="Verdana"></asp:HyperLink>

                                                    <asp:Label ID="lblAccdesc" runat="server"
                                                        Font-Size="11px" Visible="False"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="50px" Font-Names="Verdana"></asp:Label>
                                                </ItemTemplate>

                                                
                                                <FooterStyle HorizontalAlign="Right" Width="325px" />
                                                <HeaderStyle HorizontalAlign="Left" Width="325px" />
                                                <ItemStyle Width="325px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Details Description" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResdesc" runat="server"
                                                        Font-Size="10px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpcldesc" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'
                                                        Width="80px" TabIndex="78"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgvunit" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                        Width="30px" TabIndex="78"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Qty">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Visible="False" Font-Size="12px" Style="text-align: right"
                                                        ReadOnly="True"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None"
                                                        onkeypress="return isNumberKey(this, event);" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="79"></asp:TextBox>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvRate" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Visible="False" Width="60px" Font-Size="12px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px" Font-Size="12px" ForeColor="Black"
                                                        onkeypress="return isNumberKey(this, event);"
                                                        Style="text-align: right" TabIndex="80"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dr.Amount" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="0px"
                                                        onkeypress="return isNumberKey(this, event);"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="81"></asp:TextBox>

                                                    <asp:HiddenField ID="hntrndram" runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" ForeColor="Black"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                                        Width="70px" Style="text-align: right"></asp:TextBox>
                                                </FooterTemplate>
                                                
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr.Amount" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        onkeypress="return isNumberKey(this, event);"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="82"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" ForeColor="Black"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                                        Width="70px" Style="text-align: right"></asp:TextBox>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                        Width="80px" ForeColor="Black" TabIndex="83"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reconcilation" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrecndat" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                        Width="80px" TabIndex="78"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="RpCode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrpcode" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>'
                                                        Width="80px" TabIndex="60"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill/Chalan No" Visible="true">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblgvBillno" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="GridTextboxL" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                        Width="100px" ForeColor="Black" TabIndex="99"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cost Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcostcenter" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectcode")) %>'
                                                        Width="80px" TabIndex="60"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvssbalamt" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ssbalamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" TabIndex="60"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp: ID="HiddenField1" runat="server" Value='<%# Bind("ProductId") %>' />--%>

                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                        </div>
                        <!-- /.modal-body -->
                        <!-- .modal-footer -->
                        <div class="modal-footer modal-body-scrolled">
                            <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                        </div>
                        <!-- /.modal-footer -->
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
