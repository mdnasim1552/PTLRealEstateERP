<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpBankSalary.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.EmpBankSalary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
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
            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">

                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lbldate" runat="server">Month</asp:Label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="yyyyMM" TargetControlID="txtDate"
                                    PopupButtonID="Image2"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">Type</asp:Label>

                                <asp:RadioButtonList ID="rbtBankSt" runat="server" CssClass="form-control" RepeatColumns="6" RepeatDirection="Horizontal">
                                    <asp:ListItem>&nbsp;Bank Statement</asp:ListItem>
                                    <asp:ListItem>&nbsp;Forwarding Letter</asp:ListItem>
                                    <asp:ListItem>&nbsp;Account Transfer</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6" id="divDDLComp" runat="server" visible="false">
                            <asp:Label ID="lblcompany" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control  chzn-select" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 col-md-2 col-sm-6 " id="divDDLBranch" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">Branch</asp:Label>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control  chzn-select" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-4 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server">Bank Name
                                    <asp:LinkButton ID="ibtnFindBanK" runat="server" CssClass="fas fa-search"></asp:LinkButton></asp:Label>
                                <asp:TextBox ID="txtBankName" runat="server" CssClass=" form-control d-none"></asp:TextBox>
                                <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6  ">
                            <div class="form-group">
                                <asp:Label ID="Label3" CssClass="d-block" runat="server">Type of Catagory</asp:Label>

                                <asp:CheckBox ID="chkBonus" runat="server" CssClass="checkBox" Style="height: 25px !important;" Text="Festival Bonus" />
                                <asp:CheckBox ID="chklksalary" runat="server" CssClass="checkBox" Style="height: 25px !important;" Text="Previous" />
                                <asp:CheckBox ID="ChkAll" runat="server" CssClass="checkBox" Style="height: 25px !important;" Text="Bank Wise" />
                            </div>

                            <div class="form-group">
                                <asp:Label ID="lblBankLock" runat="server" CssClass="form-control " Visible="False" Width="233"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-5 col-sm-6  ">
                            <div class="form-group">
                            <asp:Label ID="Label4" runat="server">Type of Employee</asp:Label>

                            <asp:RadioButtonList ID="rbtnlistsaltype" runat="server" CssClass=" d-done"
                                Font-Size="14px"  RepeatColumns="14" 
                                Visible="false">
                            </asp:RadioButtonList>
                                </div>
                        </div>


                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem Selected="True">600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblasdate" runat="server" Visible="false">Date</asp:Label>
                                <asp:TextBox ID="txtasdate" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtasdate" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtasdate"
                                    PopupButtonID="Image2"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" OnClick="lnkbtnShow_Click" CssClass="btn btn-primary mt-3 okBtn">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvBankPayment" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvBankPayment_PageIndexChanging" OnRowDataBound="gvBankPayment_RowDataBound"
                        ShowFooter="True">
                        <PagerSettings Position="Top" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl #">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Card #">
                                <FooterTemplate>
                                    <asp:CheckBox ID="chkBankLock" runat="server" CssClass=" btn btn-info btn-sm checkbox" Text="Lock" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgIdCard" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="empid" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgempid" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Name">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtSalUpdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtSalUpdate_Click">Update</asp:LinkButton>
                                </FooterTemplate>

                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td class="style58">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Text="Name" Width="180px"></asp:Label>
                                            </td>
                                            <td class="style60">&nbsp;</td>
                                            <td>
                                                <asp:HyperLink ID="hlbtnCBdataExel" runat="server" BackColor="#000066" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lgvEmpName" runat="server"
                                        Text='<%#"<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                        Width="300px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank AC No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBACNo" runat="server" BackColor="Transparent"
                                        BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acno")) %>'
                                        Width="120px">
                                                        
                                    </asp:Label>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvTotal" Font-Bold="true" runat="server">Total :</asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Left" />

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <%--                                 <asp:TemplateField HeaderText="Transfer">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chksaltrns" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "saltrn"))=="True" %>'
                                                Width="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chksaltrns" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "saltrn"))=="True" %>'
                                        Width="30px" />
                                </ItemTemplate>

                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chkAllfrm_CheckedChanged" Text="Transfer" Width="60px" />
                                </HeaderTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFBamt" runat="server" ForeColor="#000" Font-Size="12px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvAmt" runat="server" BackColor="Transparent" Font-Size="11px"
                                        BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

