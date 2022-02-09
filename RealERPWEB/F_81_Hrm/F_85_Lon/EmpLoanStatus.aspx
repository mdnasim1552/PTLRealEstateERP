<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpLoanStatus.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_85_Lon.EmpLoanStatus1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            //$('.datepicker').datepicker({
            //    format: 'mm/dd/yyyy',
            //});
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
        };

        function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvEmpLoanStatus.ClientID %>");
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
                    <div class="row mb-0 pb-0">
                        <asp:Label ID="lcomp" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Company</asp:Label>
                        <asp:DropDownList ID="ddlComp" data-placeholder="Choose Company.." runat="server" OnSelectedIndexChanged="ddlComp_SelectedIndexChanged"
                            CssClass="chzn-select form-control col-3" AutoPostBack="true">
                        </asp:DropDownList>

                        <asp:Label ID="lbldep" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Department</asp:Label>
                        <asp:DropDownList ID="ddlDepartment" data-placeholder="Choose Department.." runat="server"
                            CssClass="chzn-select form-control col-3">
                        </asp:DropDownList>
                        <div class="col-4" runat="server" id="comlist" visible="False">
                            <asp:Label CssClass="smLbl_to" runat="server">Companies</asp:Label>
                            <asp:DropDownList ID="ddlComName" class="ComName form-control ClCompAndMod" runat="server" TabIndex="2" Width="224">
                            </asp:DropDownList>
                        </div>
                        <asp:LinkButton ID="lnkbtnShow" runat="server" OnClick="lnkbtnShow_Click" CssClass="btn btn-success btn-sm ml-1 col-1">Ok</asp:LinkButton>

                    </div>

                    <div class="row mt-2  pb-0">
                        <asp:CheckBox ID="Chkbalance" runat="server" Text="loan Balance" CssClass="btn btn-info btn-sm ml-1 col-1" AutoPostBack="True" />
                        <asp:Label ID="lbldate" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Date</asp:Label>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm col-1 "></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                        <asp:Label ID="Label5" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Loan Type</asp:Label>

                        <asp:DropDownList ID="ddlLoantype" data-placeholder="Choose loan.." runat="server"
                            CssClass="chzn-select form-control col-2" AutoPostBack="true">
                        </asp:DropDownList>

                        <asp:Label ID="lblPage" runat="server" Visible="false" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Page Size</asp:Label>
                        <asp:DropDownList ID="ddlpagesize" runat="server" Visible="false" AutoPostBack="True" CssClass="chzn-select form-control col-1" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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

                        <asp:Label ID="lblser" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Search</asp:Label>

                        <asp:TextBox ID="inputtextbox" Style="height: 29px" runat="server" CssClass="form-control col-2" placeholder="Search here..." onkeyup="Search_Gridview(this)"></asp:TextBox>

                    </div>
                </div>

                <div class="card-body">

                    <div class="row table table-responsive">
                        <asp:GridView ID="gvEmpLoanStatus" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" OnPageIndexChanging="gvEmpLoanStatus_PageIndexChanging">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvEmpId" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSection" runat="server" Font-Bold="true" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCardno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name & Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loan Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblloantype" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "loanname"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Loan Amt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvLoanamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tloan")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFLoanamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upto Paid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpPaidAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptopaid")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Paid Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPaidamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFPaidamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bal. Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFbalamt" runat="server"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
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
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
