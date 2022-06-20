<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccVoucherUnposted.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccVoucherUnposted" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function openModalAbs() {
            //    $('#myModal').modal('show');
            // alert("Hello");
            $('#myModal').modal('toggle');
        }

        function CloseMOdal() {

            $('#myModal').modal('hide');
        }



        function pageLoaded() {
            try {
                $("input, select")
                    .bind("keydown",
                        function (event) {
                            var k1 = new KeyPress();
                            k1.textBoxHandler(event);
                        });

                $('.chzn-select').chosen({ search_contains: true });
                $('#<%=this.gvAccUnPosted.ClientID%>').tblScrollable();

            }
            catch (e) {
                alert(e.mesage);
            }
        };

        function Search_Gridview2(strKey) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                var tblData = document.getElementById("<%=this.gvAccUnPosted.ClientID %>");
                var rowData;
                for (var i = 1; i < tblData.rows.length; i++) {
                    rowData = tblData.rows[i].innerHTML;
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
            catch (e) {
                alert(e.message);
            }

        }
        function Search_Gridview(strKey, cellNr) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvAccUnPosted.ClientID %>");
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

    </script>

    <style>
        .grvContentarea tr td:last-child a span {
            margin: 0 5px !important;
        }

        .modal-lg {
    max-width: 70%;
}
    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                <label for="lblfrmdate" runat="server" class=" control-label  lblmargin-top9px ">From</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="inputDateBox" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="  form-group">
                                <label for="lbltodate" runat="server" class=" control-label  lblmargin-top9px ">To</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputDateBox" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
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
                                    <asp:ListItem Value="" Selected="True">All Voucher</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..." onkeyup="Search_Gridview2(this)"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" Style="float: left">Ok</asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            
                            <asp:CheckBox ID="ChboxPayee" runat="server" Text="A/C Payee" CssClass="btn btn-primary" />
                            <asp:Label ID="lblmsg" CssClass="btn-danger btn  primaryBtn" runat="server" Visible="false"></asp:Label>
                        </div>

                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAccUnPosted" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvAccUnPosted_RowDataBound"
                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvvoudat" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Voucher">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchvounum1" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Voucher No" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblvounum" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reference">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchrefnum" SortExpression="refnum" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Cheque No" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtINgvteamdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cheque Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvchequedat" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                Width="80px"></asp:Label>

<%--                                            <asp:Label ID="lblgvchequedat" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy")%>'
                                                Width="80px"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchproj" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Project Name" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpactdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Narration">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchvenar" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Narration" onkeyup="Search_Gridview(this,6)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvounar" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Party Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchpayto" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Party Name" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsirdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Accounts Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtcactdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Accounts Name" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcactdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Voucher Amount">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchamt" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Vou. Amount" onkeyup="Search_Gridview(this,9)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvouamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkvmrno" runat="server" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                Width="30px" CssClass="btn btn-default btn-xs" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnVouDetials" runat="server" ToolTip="View" OnClick="BtnVouDetials_Click" CssClass="btn btn-default btn-xs"><span class="fa fa-eye"></span> </asp:LinkButton>
                                            <%--<asp:LinkButton ID="lnkbtnPrintIN" runat="server" OnClick="lnkbtnPrintRD_Click" ToolTip="Print" CssClass="btn btn-default btn-xs"><span style="color:green" class="fa fa-print"></span> </asp:LinkButton>--%>
                                            <asp:HyperLink ID="hlnkVoucherPrint" runat="server" Target="_blank" ToolTip="Voucher Print" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                            <asp:HyperLink ID="hlnkVoucherEdit" runat="server" ToolTip="Edit" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-edit"></span></asp:HyperLink>
                                            <asp:LinkButton ID="lbtnVoucherApp" runat="server" ToolTip="Approved" OnClick="lbtnVoucherApp_Click" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'
                                                CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span> </asp:LinkButton>
                                           
                                            
                                            
                                            <asp:LinkButton ID="lbtnDeleteVoucher" runat="server" ToolTip="Delete" OnClick="lbtnDeleteVoucher_Click" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle Width="180px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="User Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearusrname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="User Name" onkeyup="Search_Gridview(this,12)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvusrname" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
            </div>
           

            <div class="modal fade bd-example-modal-lg" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLongTitle">
                                    <asp:Label ID="lbmodalheading" runat="server"> Voucher Details Information :</asp:Label></h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>

                            </div>
                            <div class="modal-body">
                                <p id="EmpDeatials" class="m-0" runat="server" > 
                                </p>

                                <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>

                                <div class="table-responsive">
                            <asp:GridView ID="gvdetails" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvINSlNo0d" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvvoudatd" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Voucher">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchvounumd1" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Voucher No" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblvounumd" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reference">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchrefnumd" SortExpression="refnum" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Cheque No" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtINgvteamdescd" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cheque Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvchequedatd" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy")%>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchprojd" BackColor="Transparent" BorderStyle="None" runat="server" Width="200px" placeholder="Project Name" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpactdesc" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Narration">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchvenard" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Narration" onkeyup="Search_Gridview(this,6)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvounard" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Party Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchpaytod" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Party Name" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsirdescd" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Accounts Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtcactdescd" BackColor="Transparent" BorderStyle="None" runat="server" Width="220px" placeholder="Accounts Name" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcactdescd" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                Width="220px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lblgvFcactdescd" runat="server" Style="text-align: right" Width="90px" Font-Bold="true" Font-Size="12px">Totat :</asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Voucher Amount">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearchamtd" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Vou. Amount" onkeyup="Search_Gridview(this,9)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvouamtd" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFvouamtd" runat="server" Style="text-align: right" Width="90px" Font-Bold="true" Font-Size="12px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    </asp:TemplateField>

                                    
                                    



                                    <asp:TemplateField HeaderText="User Name">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtSearusrnamed" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="User Name" onkeyup="Search_Gridview(this,12)"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvusrnamed" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                            <div class="modal-footer">
                              <%--  <asp:LinkButton ID="ModalUpdateBtn" OnClientClick="CloseModal();" OnClick="ModalUpdateBtn_Click"
                                    runat="server" CssClass="btn btn-sm btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>--%>

                             <%--   <asp:LinkButton ID="ModallnkBtnLateAFTER10AM" OnClientClick="CloseModal();" OnClick="ModallnkBtnLateAFTER10AM_Click" Visible="false"
                                    runat="server" CssClass="btn btn-sm btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>--%>

                                   <asp:LinkButton ID="btnVouApproval" OnClientClick="CloseMOdal();" OnClick="btnVouApproval_Click"
                                    runat="server" CssClass="btn btn-sm btn-primary"> <span class="glyphicon glyphicon-saved"></span> Approval</asp:LinkButton>
                                <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal">Close</button>
                            </div>

                        </div>
                    </div>
                </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

