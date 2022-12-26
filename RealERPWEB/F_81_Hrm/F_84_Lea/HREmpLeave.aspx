<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HREmpLeave.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.HREmpLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mt20 {
            margin-top: 28px;
        }
        div#ContentPlaceHolder1_ddlEmpNamelApp_chzn{
                        width: 100% !important;
        }
        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 35px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
        .form-group{
            width:100%!important;
        }
    </style>

    <script src="../../Scripts/gridviewScrollHaVertworow.min.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {
                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });
                $('.chzn-select').chosen({ search_contains: true });

           <%-- var gvLeaveRule = $('#<%=this.gvLeaveRule.ClientID %>');
            gvLeaveRule.Scrollable()--%>;

                //var gridViewScroll = new GridViewScroll({
                //    elementID: "gvLeaveRule",
                //    width: 1450,
                //    height: 550,
                //    freezeColumn: false,
                //    freezeFooter: true,
                //   // freezeColumnCssClass: "GridViewScrollItemFreeze",
                //    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                //   // freezeHeaderRowCount: 2,
                //   // freezeColumnCount: 12,
                //});
                //gridViewScroll.enhance();
            }

            catch (e)
            {
                alert(e);
            }
        }

        function loadCreateRuleModal() {
            $('#createRuleModal').modal('toggle');
        }
        function CloseModal() {
            $('#createRuleModal').modal('hide');
        }

        function CloseModal_AlrtMsg() {

            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('.modal').modal('hide');

            loadCreateRuleModal();
        };


    </script>
    <style>
        .FixedHeader {
            position: absolute !important;
            font-weight: bold;
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


            <div class="row justify-content-md-center mt-4 mb-1">
                <div class="col-9"></div>
               
                <div class="col-3" >
                    <asp:HyperLink ID="LinkButton1" runat="server" CssClass="btn btn-denger d-none btn-sm pull-right" NavigateUrl="~/F_81_Hrm/F_84_Lea/UploadLeavExcel.aspx" Target="_blank"><i class="fas fa-plus"></i> Upload Excel </asp:HyperLink>

                    <%--  <asp:LinkButton ID="lnkRule" runat="server" CssClass="btn btn-info btn-sm pull-right" OnClick="lnkRule_Click"><i class="fas fa-plus"></i> Create Rule </asp:LinkButton>  --%>
                    <asp:LinkButton ID="lnkRule" runat="server" CssClass="btn btn-info btn-sm pull-right" data-toogle="modal" data-target="#createRuleModal" OnClick="lnkRule_Click"><i class="fas fa-plus"></i> Create Rule </asp:LinkButton>

                    <asp:LinkButton ID="lnkReset" runat="server" CssClass="btn btn-success btn-sm pull-right" OnClick="lnkReset_Click" ><i class="fa fa-refresh"></i> Apply New Rule</asp:LinkButton>


                </div>          
            </div>
            <div class="card card-fluid container-data" style="min-height: 1000px;">

                <div class="card-header">
                    <div class="row">

                        <div class="col-sm-12 col-md-1" id="divLeaveApp" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Yearly Leave   
                                </label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-2" id="div1" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    <asp:LinkButton ID="imgbtnCompany" runat="server" OnClick="imgbtnCompany_Click">
                                                  Company</asp:LinkButton>
                                </label>
                                <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-3" id="div2" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    <asp:LinkButton ID="imgbtnProSrch" runat="server" OnClick="imgbtnProSrch_Click">
                                                  Section Name</asp:LinkButton>
                                </label>
                                <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-2" id="lblEmpIdSearch" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Emp. Code
                                    <asp:LinkButton ID="imgbtnEmpSeach" runat="server" OnClick="imgbtnEmpSeach_Click"><i class="fas fa-search "></i></asp:LinkButton>
                                </label>
                                <asp:TextBox ID="txtEmpSearch" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-sm-12 col-md-1" id="divPage" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Page Size
                                </label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem Selected="True">600</asp:ListItem>
                                    <asp:ListItem>1000</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-sm-12 col-md-1" id="div5" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Search Type
                                </label>
                                <asp:DropDownList ID="ddlModiType" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlModiType_SelectedIndexChanged">
                                    <asp:ListItem id="All">All</asp:ListItem>
                                    <asp:ListItem id="Updated">Updated</asp:ListItem>
                                    <asp:ListItem id="Notupdate">Notupdate</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-sm-12 col-md-1" id="div6" runat="server">
                            <div class="form-group">

                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary mt20" OnClick="lbtnOk_Click" Text="Ok"></asp:LinkButton>

                            </div>
                        </div>

                    </div>

                </div>
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="leaveRule" runat="server">
                            <asp:Panel ID="pnlleave" runat="server" BackColor="#F8F8F8" Visible="False">
                                <div class="row">
                                    <div class="col-2">
                                        <div class="input-group input-group-alt ">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary ml-1" type="button">Earn Leave</button>
                                            </div>
                                            <asp:TextBox ID="txternleave" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="input-group input-group-alt ">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary ml-1" type="button">Causual Leave</button>
                                            </div>
                                            <asp:TextBox ID="txtcsleave" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="input-group input-group-alt ">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary ml-1" type="button">Sick Leave</button>
                                            </div>
                                            <asp:TextBox ID="txtskleave" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="input-group input-group-alt ">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary ml-1" type="button">Maternity Leave</button>
                                            </div>
                                            <asp:TextBox ID="txtmtleave" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="input-group input-group-alt ">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary ml-1" type="button">Without Pay Leave </button>
                                            </div>
                                            <asp:TextBox ID="txtWPayleave" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="co--2">
                                        <div class="input-group input-group-alt ">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary ml-1" type="button">Adjustment Leave</button>
                                            </div>
                                            <asp:TextBox ID="txtTrainleave" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-2">
                                        <div class="input-group input-group-alt ">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary ml-1" type="button">Paternity Leave</button>
                                            </div>
                                            <asp:TextBox ID="txtptleave" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="input-group input-group-alt ">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary ml-1" type="button">Leave On Probation</button>
                                            </div>
                                            <asp:TextBox ID="txtleaveOnProvi" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="input-group input-group-alt ">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary ml-1" type="button">Leave On Separation</button>
                                            </div>
                                            <asp:TextBox ID="txtleaveOnSepa" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <asp:LinkButton ID="lnkbtnGenLeave" runat="server" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lnkbtnGenLeave_Click" TabIndex="14">Generate</asp:LinkButton>
                                    </div>
                                </div>

                            </asp:Panel>
                            <div class="col-sm-12 d-none">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkLeave" runat="server" CssClass="btn chkBoxControl primaryBtn" AutoPostBack="True" OnCheckedChanged="chkLeave_CheckedChanged" Text="Leave" Visible="False"
                                            TabIndex="15" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="table table-responsivex" style="width: 100%; height: 400px; overflow: auto">
                                    <asp:GridView ID="gvLeaveRule" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="FixedHeader"
                                        OnPageIndexChanging="gvLeaveRule_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea "
                                        PageSize="15">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSection" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secname")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp. ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvempid" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID CARD">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvidcard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnFUpLeave" runat="server" CssClass="btn btn-info btn-xs"
                                                        OnClick="lnkbtnFUpLeave_Click">Final Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Join Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvjoindate" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderText="Confirm Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvconf" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDesig" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Opening Leave">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvoplv" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oplv")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Earned Leave">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvel" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ernleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Casual Leave">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvcl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "csleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sick Leave">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvsl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Maternity Leave">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvml" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Star Leave">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvpl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hajj Leave">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvLOnHajjlv" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lapphajjleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Leave On Probation <br> ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvLOnProba" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lonproidleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Without Pay <br> Leave">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvWPl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Training <br> Leave">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvTrL" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trpleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        
                                            <asp:TemplateField HeaderText="Leave On <br> Separation">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvLOnSepa" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lonsepaleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Leave On <br> APPRENTICE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvLOnApprentice" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lappreleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
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
                            </div>
                        </asp:View>

                        <asp:View ID="LeaveApp" runat="server">


                            <div class="row">
                                <div class="col-8">
                                    <asp:Panel ID="PnlEmp" runat="server">
                                        <div class="row">
                                            <asp:RadioButtonList ID="rblstapptype" runat="server" CssClass="rbtnList1 chkBoxControl" RepeatColumns="6" RepeatDirection="Horizontal"
                                                Width="220px" TabIndex="16" Visible="False">
                                                <asp:ListItem>Type 1</asp:ListItem>
                                                <asp:ListItem>Type 2</asp:ListItem>
                                                <asp:ListItem>Type 3</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group row">
                                            <label for="staticEmail" class="col-2 col-form-label">Emp.  Name</label>
                                            <div class="col-6">
                                                <asp:DropDownList ID="ddlEmpName" runat="server" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged"
                                                    CssClass=" chzn-select form-control" TabIndex="2" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-3">
                                            </div>

                                        </div>

                                    </asp:Panel>
                                    <asp:Panel ID="Pnlapply" runat="server" Visible="False">
                                        <div class="form-group row">
                                            <label for="staticEmail" class="col-2 col-form-label">Apply Date</label>
                                            <div class="col-2">
                                                <asp:TextBox ID="txtaplydate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtaplydate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtaplydate" TodaysDateFormat=""></cc1:CalendarExtender>
                                            </div>
                                            <label for="staticEmail" class="col-2 col-form-label">Appr.Date</label>
                                            <div class="col-2">
                                                <asp:TextBox ID="txtApprdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtApprdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtApprdate" TodaysDateFormat=""></cc1:CalendarExtender>
                                            </div>
                                            <div class="col-4">
                                                <asp:LinkButton ID="lnkbtnRef" runat="server" OnClick="lnkbtnRef_Click" CssClass="btn btn-secondary primaryBtn">Refresh</asp:LinkButton>
                                                <asp:CheckBox ID="chkPreLeave" runat="server" AutoPostBack="True" CssClass="chkBoxControl" OnCheckedChanged="chkPreLeave_CheckedChanged"
                                                    Text="Previous Leave" TabIndex="24" />
                                            </div>
                                        </div>

                                    </asp:Panel>
                                    <asp:Panel ID="PnlPreLeave" runat="server" Visible="False">
                                        <div class="form-group row">
                                            <label for="staticEmail" class="col-2 col-form-label">Pre. Leave</label>
                                            <div class="col-6">
                                                <asp:DropDownList ID="ddlPreLeave" runat="server" CssClass="chzn-select form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-3">
                                                <asp:LinkButton ID="lnkbtnPreLeave" runat="server" CssClass="btn btn-primary primaryBtn"
                                                    OnClick="lnkbtnPreLeave_Click" TabIndex="21">Show</asp:LinkButton>
                                            </div>

                                        </div>


                                    </asp:Panel>

                                </div>
                                <div class="col-4" id="divEmpDetails" visible="false" runat="server">
                                    <div class="form-group row mb-0">
                                        <label for="staticEmail" class="col-3 col-form-label p-0 font-weight-bold">Company</label>
                                        <asp:Label ID="lblComPany" runat="server" CssClass="col-9 p-0 col-form-label"></asp:Label>


                                    </div>

                                    <div class="form-group row  mb-0">
                                        <label for="staticEmail" class="col-3 col-form-label p-0 font-weight-bold">Section</label>
                                        <asp:Label ID="lblSection" runat="server" CssClass="col-9 p-0 col-form-label"></asp:Label>


                                    </div>

                                    <div class="form-group row  mb-0">
                                        <label for="staticEmail" class="col-3 col-form-label p-0 font-weight-bold">Designation</label>
                                        <asp:Label ID="lblDesignation" runat="server" CssClass="col-9 p-0 col-form-label "></asp:Label>

                                    </div>

                                    <div class="form-group row  mb-0">
                                        <label for="staticEmail" class="col-3 col-form-label p-0 font-weight-bold">Joining Date</label>
                                        <asp:Label ID="lblJoiningDate" runat="server" CssClass="col-9 p-0 col-form-label "></asp:Label>
                                        <asp:Label ID="lbltrnleaveid" Visible="false" runat="server" CssClass="form-control"></asp:Label>
                                    </div>






                                </div>
                            </div>

                            <div class="row">
                                <div class="col-4">
                                    <header class="card-header mb-1 mt-1">
                                        <asp:Label ID="lblleaveApp" runat="server">Leave Application</asp:Label>
                                    </header>


                                    <asp:GridView ID="gvLeaveApp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnTotalLeave" runat="server" CssClass="btn btn-xs" OnClick="lnkbtnTotalLeave_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Desription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDescription" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnUpdateLeave" runat="server" Font-Bold="True" Font-Size="12px" CssClass="btn btn-xs btn-danger"
                                                        OnClick="lnkbtnUpdateLeave_Click">Update </asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Applied <br> For">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvlapplied" runat="server" BorderStyle="None" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lapplied")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px" BackColor="Transparent" Font-Size="12px"
                                                        Style="text-align: right"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" Font-Bold="True" Font-Size="12px" CssClass="btn btn-secondary btn-xs"
                                                        ForeColor="#000" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Std. Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvenjoydt1" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px" BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtgvenjoydt1_CalendarExtender" runat="server" Enabled="True"
                                                        Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt1"></cc1:CalendarExtender>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave End Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvenjoydt2" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                        Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                        BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtgvenjoydt2_CalendarExtender" runat="server" Enabled="True"
                                                        Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt2"></cc1:CalendarExtender>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <div class="col-8">
                                    <header class="card-header mb-1 mt-1">
                                        <asp:Label ID="lblleaveStatus" runat="server">Leave Status</asp:Label>
                                    </header>


                                    <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening <br> Bal.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlentitled0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entitlement">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave <br> This Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Leave <br> Adjusted">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlvadjusted" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjfleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Present <br> Bal.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Closing <br> Bal.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Leave <br> Std. Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvenjoydt10" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Leave <br> End Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                        Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Leave <br> Day's">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
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
                            <asp:Panel ID="PnlRmrks" runat="server" Visible="False">

                                <div class="row">

                                    <div class="col-4">
                                        <div class="form-group row">
                                            <label for="staticEmail" class="col-4 col-form-label">Reason(s)</label>
                                            <div class="col-8">
                                                <asp:TextBox ID="txtLeavLreasons" runat="server" BorderWidth="1px"
                                                    TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    TabIndex="25"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-4">

                                        <div class="form-group row">
                                            <label for="staticEmail" class="col-4 col-form-label">Address of enjoing time</label>
                                            <div class="col-8">
                                                <asp:TextBox ID="txtaddofenjoytime" runat="server" BorderWidth="1px"
                                                    TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    TabIndex="25"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-4">
                                        <div class="form-group row">
                                            <label for="staticEmail" class="col-4 col-form-label">Remarks</label>
                                            <div class="col-8">
                                                <asp:TextBox ID="txtLeavRemasrks" runat="server" BorderWidth="1px"
                                                    TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    TabIndex="25"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-4">

                                        <div class="form-group row">
                                            <label for="staticEmail" class="col-4 col-form-label">While on Leave, Duties will Performed by</label>
                                            <div class="col-8">
                                                <asp:TextBox ID="txtdutiesnameandDesig" runat="server" BorderWidth="1px"
                                                    TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    TabIndex="25"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-4">

                                        <div class="form-group row">
                                            <label for="staticEmail" class="col-4 col-form-label">Reason</label>
                                            <div class="col-8">
                                                <asp:TextBox ID="txtLeavRemarks" runat="server" BorderWidth="1px"
                                                    TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    TabIndex="25"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>




                                </div>
                            </asp:Panel>

                            <div class="row">
                                <asp:Label ID="lblleaveInformation" runat="server" Font-Bold="True"
                                    Font-Size="14px" ForeColor="#000" Text="Leave Information" Visible="False"></asp:Label>
                                <asp:GridView ID="gvleaveInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="420px" OnRowDataBound="gvleaveInfo_RowDataBound"
                                    OnRowDeleting="gvleaveInfo_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="trnleaveid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltrnleaveid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" />

                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvledescription" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="120px">
                                                                
                                                                
                                                                
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Apply Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvapplydate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aplydat")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="From Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlstartdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstrtdat")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlenddate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lenddat"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvleavedays" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreason" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreason")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvremarks" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lrmarks")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
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
                        <asp:View ID="LeaveAppform" runat="server">
                            <asp:Panel ID="PnlEmplApp" runat="server" Visible="False">
                                <div class="row">
                                    <div class="col-lg-6 card ">
                                      
                                      
                                        <div class="form-group row mt-2">
                                            <asp:Label ID="Label13" runat="server" CssClass="col-lg-3">Emp.  Name</asp:Label>
                                            <div class="col-lg-9 col-md-9" style="margin:0;padding:0">
                                                             <asp:DropDownList ID="ddlEmpNamelApp" runat="server" OnSelectedIndexChanged="ddlEmpNamelApp_SelectedIndexChanged" CssClass=" chzn-select form-control form-control-sm"  AutoPostBack="True">
                                            </asp:DropDownList>
                                            </div>
                               
                                        </div>
                           

                                
                                         <div class="form-group row">
                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="col-lg-3" >Date</asp:Label>
                                            <asp:TextBox ID="txtformdate" runat="server" CssClass="form-control form-control-sm col-lg-9"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtformdate"></cc1:CalendarExtender>
                                        </div>
                             
                   
                                        <div class="form-group row">
                                            <asp:Label ID="lbltodate" runat="server" CssClass="col-lg-3" >From</asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm col-lg-9"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtformdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                        </div>
                   

                        
                                         <div class="form-group row">
                                            <asp:RadioButtonList ID="rblstFormType" runat="server" CssClass="rbtnList1 chkBoxControl col-lg-6" RepeatColumns="6" RepeatDirection="Horizontal"
                                                Width="220px" TabIndex="16" Visible="False">
                                                <asp:ListItem>Type 1</asp:ListItem>
                                                <asp:ListItem>Type 2</asp:ListItem>
                                                <asp:ListItem>Type 3</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                 
                          
                                      <div class="col-lg-6 card">
                                                        
                                 
                                        <div class="form-group row mt-2">
                                                     <asp:Label ID="Label16" runat="server" CssClass="col-lg-3">Company :</asp:Label>
                                                    <asp:Label ID="lblComPanylApp" runat="server" CssClass="col-lg-9"></asp:Label>
                                        </div>
                           

                           
                                       <div class="form-group row">
                                                    <asp:Label ID="Label21" runat="server" CssClass="col-lg-3  text-bold">Section :</asp:Label>
                                                    <asp:Label ID="lblSectionlApp" runat="server" CssClass=" col-lg-9"></asp:Label>
                                        </div>
                       
              
                                       <div class="form-group row">
                                                    <asp:Label ID="Label23" runat="server" CssClass="col-lg-3  text-bold">Designation :</asp:Label>
                                                    <asp:Label ID="lblDesignationlApp" runat="server" CssClass=" col-lg-9"></asp:Label>
                                        </div>
                           
                                                               
                                       <div class="form-group row">
                                                 <asp:Label ID="Label29" runat="server"  CssClass="col-lg-3 text-bold">Joining Date :</asp:Label>
                                                    <asp:Label ID="lblJoiningDatelApp" runat="server" CssClass=" col-lg-9"></asp:Label>
                                        </div>
            
                     


              
                         

                         

                                      </div>
                        
           


                                </div>


                                     <div class="row mt-1">
                                <asp:GridView ID="gvLeaveStatus01" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="208px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entitlement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave This. Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvltaken1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requested">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavedt21" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy ") %>'
                                                    Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoyday1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                            </asp:Panel>

                            <%--          <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <asp:Panel ID="PnlEmplApp" runat="server" Visible="False">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="rblstFormType" runat="server" CssClass="rbtnList1 chkBoxControl" RepeatColumns="6" RepeatDirection="Horizontal"
                                                        Width="220px" TabIndex="16" Visible="False">
                                                        <asp:ListItem>Type 1</asp:ListItem>
                                                        <asp:ListItem>Type 2</asp:ListItem>
                                                        <asp:ListItem>Type 3</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName">Emp.  Name</asp:Label>
                                                    <asp:TextBox ID="txtlFEmpSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <asp:LinkButton ID="imgbtnlFEmpSeaarch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnlFEmpSeaarch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                                <div class="col-md-4 pading5px asitCol4">
                                                    <asp:DropDownList ID="ddlEmpNamelApp" runat="server" OnSelectedIndexChanged="ddlEmpNamelApp_SelectedIndexChanged" CssClass=" chzn-select form-control inputTxt pull-left" TabIndex="2" AutoPostBack="True">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-5 pading5px asitCol5">
                                                    <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to ">Date</asp:Label>
                                                    <asp:TextBox ID="txtformdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtformdate"></cc1:CalendarExtender>

                                                    <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to">From</asp:Label>
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtformdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-md-4 pading5px asitCol4">
                                                    <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                                    <asp:Label ID="lblComPanylApp" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label21" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                                    <asp:Label ID="lblSectionlApp" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label23" runat="server" CssClass="lblTxt lblName">Designation</asp:Label>
                                                    <asp:Label ID="lblDesignationlApp" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>
                                                <div class="col-md-3 pull-right">
                                                    <asp:Label ID="Label27" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label29" runat="server" CssClass="lblTxt lblName">Joining Date</asp:Label>
                                                    <asp:Label ID="lblJoiningDatelApp" runat="server" CssClass="inputTxt"></asp:Label>

                                                </div>

                                            </div>
                                        </div>
                                    </asp:Panel>
                                </fieldset>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvLeaveStatus01" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="208px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entitlement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave This. Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvltaken1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requested">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavedt21" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy ") %>'
                                                    Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoyday1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>--%>
                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>

            <div id="createRuleModal" class="modal " tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static" aria-hidden="true">
                <div class="modal-dialog ">
                    <div class="modal-content col-md-12 col-sm-12 ">
                        <div class="modal-header hedcon">

                            <h5>Create Leave Rule</h5>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>

                        </div>
                        <div class="modal-body">
                            <div class="col-md-12 col-sm-12 col-lg-12">

                                <div class="row">
                                    <div class="col-md-8 col-sm-8 col-lg-8">
                                        <div class="input-group input-group-alt">
                                            <div class="input-group-prepend">
                                                <button class="btn btn-secondary" type="button">Year</button>
                                            </div>
                                            <asp:DropDownList ID="ddlyear" ClientIDMode="Static" data-placeholder="Choose year" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                                                <asp:ListItem Value="2020">2020</asp:ListItem>
                                                <asp:ListItem Value="2021">2021</asp:ListItem>
                                                <asp:ListItem Value="2022" Selected="True">2022</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                        Visible="False" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod2"))+"-" %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                                    MaxLength="3"
                                                    Style="border-style: none; border-color: midnightblue; font-size: 12px; text-align: left;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod3")) %>'
                                                    Width="50px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod3")) %>'
                                                    Width="50px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgrcod1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                    Visible="False"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                    Width="200px"></asp:TextBox>
                                            </EditItemTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                                    Style="font-size: 12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave">


                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtLeav" runat="server" Font-Size="12px" AutoCompleteType="None" onkeypress="return isNumberKey(this,event);"
                                                    Style="font-size: 12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdescval")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <AlternatingRowStyle BackColor="" />
                                </asp:GridView>

                                <br />



                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:LinkButton ID="lnkUpdateLeaveRule" runat="server" CssClass="btn btn-info btn-sm" OnClientClick="CloseModal();" OnClick="lnkUpdateLeaveRule_Click" Text="Save"></asp:LinkButton>
                            <button type="button" class="btn btn-danger btn-sm " data-dismiss="modal">Close</button>
                        </div>



                    </div>
                </div>
            </div>









        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
