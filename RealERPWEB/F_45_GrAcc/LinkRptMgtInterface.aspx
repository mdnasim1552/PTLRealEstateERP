
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptMgtInterface.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkRptMgtInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
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
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblfrm" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:Label ID="lblfrmdate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                        <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:Label ID="lbltodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:Label>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" B Font-Bold="True" Font-Size="12px" ForeColor="Yellow" CssClass="lblTxt lblName"
                                                    Text="Please wait . . . . . . ." Width="120px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                    </div>

                                </div>

                                 <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblOrderList" runat="server" CssClass="lblTxt lblName">Label Field</asp:Label>

                                        <asp:DropDownList ID="ddlOrder" runat="server" Width="150" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlOrderad1" runat="server" Width="100" CssClass="ddlPage62 inputTxt margin5px">
                                            <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                            <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                        </asp:DropDownList>



                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblOrderList0" runat="server" CssClass=" smLbl_to">Order Field</asp:Label>


                                        <asp:DropDownList ID="ddlReportLevel" runat="server" Width="150" CssClass="ddlPage62 inputTxt margin5px"
                                            OnSelectedIndexChanged="ddlReportLevel_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">Company Wise</asp:ListItem>
                                            <asp:ListItem Value="2">Department Wise</asp:ListItem>
                                            <asp:ListItem Value="3">Section Wise</asp:ListItem>
                                            <asp:ListItem Value="4">Details</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">ok</asp:LinkButton>
                                    </div>




                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            Font-Bold="True" Font-Size="12px" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Width="105px">
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

                                    </div>


                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="table-responsive">
                    <asp:GridView ID="gvEmpList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvEmpList_PageIndexChanging" ShowFooter="True" Width="420px" CssClass="table-striped table-hover table-bordered grvContentarea"
                        OnRowDataBound="gvEmpList_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True"  Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvEmpid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvComp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDept" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "department")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Section Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvEmp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvdesignationemp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvDes" runat="server" ForeColor="Black" Font-Size="12px" Width="70px">Total:</asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No of </Br> Employee">
                                <ItemTemplate>
                                    <asp:Label ID="lgvNoEmp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noemp")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFNoEmp" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="12px"
                                        Width="50px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salary">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnksalary" runat="server" Font-Underline="false" Target="_blank" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:HyperLink>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFsalary" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="12px"
                                        Width="70px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Joining </Br> Date" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "joindate")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvLeave" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leave")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Absent">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvAbst" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tabst")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Late">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvLate" runat="server" Font-Underline="false" Target="_blank"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:HyperLink>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Service Period" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvserperiod" runat="server" Font-Underline="false" Target="_blank"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "serperiod")) %>'
                                        Width="140px"></asp:HyperLink>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
