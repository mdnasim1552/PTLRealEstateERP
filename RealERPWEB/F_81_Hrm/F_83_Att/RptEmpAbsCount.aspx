<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptEmpAbsCount.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.RptEmpAbsCount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .chzn-container-single {
            width: auto !important;
            height: 34px !important;
        }

            .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
            }

        /*  .project-slect  .chzn-container-single{
         width: 100px !important;
            height: 34px !important;
        
        }*/
        .profession-slect .chzn-container-single {
            height: 34px !important;
        }
    </style>

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

            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });


            $('.chzn-select').chosen({ search_contains: true });



           <%-- var gvSummary = $('#<%=this.gvSaleFunnel.ClientID %>');
            gvSummary.Scrollable();--%>


        };
        function OpenModalDeails() {
            //    $('#myModal').modal('show');
            $('#EmpDeatilsAbs').modal('toggle');
        }
        function CloseModal() {
            $('#EmpDeatilsAbs').modal('hide');
        }
    </script>

    <div class="card card-fluid container-data mt-5" id='printarea'>
        <div class="card-body" style="min-height: 600px;">

            <div class="row mb-2">
                <div class="col-md-4 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend ">
                            <button class="btn btn-secondary" type="button">From</button>
                        </div>
                        <asp:TextBox ID="txtfodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txtfodate"></cc1:CalendarExtender>
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" id="lblToDate" runat="server" type="button">To</button>
                        </div>
                        <asp:TextBox ID="txttodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="Cal3" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                    </div>
                </div>

                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary ml-1" type="button">Company</button>
                        </div>
                        <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" data-placeholder="Choose Company" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Department</button>
                        </div>
                        <asp:DropDownList ID="ddldept" ClientIDMode="Static" data-placeholder="Choose Department" runat="server" CssClass="custom-select chzn-select " AutoPostBack="true" OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="col-md-2 p-0">
                    <div class="input-group input-group-alt srDiv">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary pl-0 pr-0" type="button">Section</button>
                        </div>
                        <asp:DropDownList ID="ddlsec" data-placeholder="Choose Section" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 p-0 mt-2">
                    <div class="input-group input-group-alt profession-slect srDiv">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Employee</button>
                        </div>
                        <asp:DropDownList ID="ddlEmp" data-placeholder="Choose Employee" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true">
                        </asp:DropDownList>
                        <div class="input-group-prepend">
                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="col-md-2 p-0 mt-2 pading5px">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Page</button>
                        </div>
                        <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                            <asp:ListItem>150</asp:ListItem>
                            <asp:ListItem>200</asp:ListItem>
                            <asp:ListItem>300</asp:ListItem>
                            <asp:ListItem>400</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>

            <div class="clearfix"></div>
            <br />
            <div class="row">

                <asp:GridView ID="gvabscount" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                    OnPageIndexChanging="gvabscount_PageIndexChanging" ShowFooter="True" Width="800px" CssClass="table-striped table-hover table-bordered grvContentarea">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo66" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Emp ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lgvEmpId" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                    Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <asp:Label ID="lblDepart" runat="server" Width="150px" Height="16px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname"))  %>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section">
                            <HeaderTemplate>
                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                    ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lgvSection" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                    Width="200px" Font-Bold="True" Font-Size="11px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                <asp:Label ID="lblgvEmpName" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                    Width="200px"></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Designation">
                            <ItemTemplate>
                                <asp:Label ID="lblgvEmpDesig" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                    Width="160px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFDesig" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right" Width="160px"> Total :</asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Absent</br>Day">
                            <ItemTemplate>

                                <asp:LinkButton ID="lbltotalabs" runat="server" OnClick="lbltotalabs_Click" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "abscount")).ToString("#,##0;(#,##0); ") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFabsday" runat="server" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: right" Width="80px"></asp:Label>
                            </FooterTemplate>
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

    <div id="EmpDeatilsAbs" class="modal fade animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-full-width modal-lg ">
            <div class="modal-content modal-content-full-width">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <i class="fa fa-hand-point-right"></i>
                        Employee Details Information </h4>

                    <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>


                </div>
                <div class="modal-body">
                    <div class="row">
                        <asp:GridView ID="GvEmpDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo66" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldetilsempid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDetailsDepart" runat="server" Width="150px" Height="16px" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname"))  %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section">

                                    <ItemTemplate>
                                        <asp:Label ID="lblsection" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="100px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpDesig" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDDate" runat="server" Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "absdat")).Year==1900 ? "": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "absdat")).ToString("dd-MMM-yyyy")) %>'
                                            Width="90px"></asp:Label>

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
        </div>
    </div>

</asp:Content>
