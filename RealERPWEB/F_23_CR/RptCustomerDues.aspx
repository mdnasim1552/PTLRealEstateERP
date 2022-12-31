<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptCustomerDues.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptCustomerDues" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            var gv1 = $('#<%=this.gvcustdues.ClientID %>');
            gv1.Scrollable();             
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

            <div class="row">
                <div class="col-lg-12">
                    <div class="card mt-4 mb-2">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-lg-3 col-md-3 col-sm-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblRefNo" runat="server">Project Name 

                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search"> </span></asp:LinkButton>

                                        </asp:Label>
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="True" CssClass="form-control chzn-select" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server">From </asp:Label>
                                        <asp:TextBox ID="txtfrmDate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>

                                    </div>

                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server">To </asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>

                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblPage" runat="server" CssClass="d-block" Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" Width="55px" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged1">
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

                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <div class="form-group mt-3">
                                        <asp:CheckBox ID="chkoverdues" runat="server" Text="Over Dues" CssClass="btn btn-info btn-sm checkBox" AutoPostBack="True" OnCheckedChanged="chkoverdues_CheckedChanged" />
                                        <asp:CheckBox ID="chkCurrentdues" runat="server" Text="Current Dues" CssClass="btn btn-info btn-sm checkBox" />
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click" TabIndex="8">Ok</asp:LinkButton>
                                    </div>
                                </div>


                            </div>

                        </div>
                        <div class="card-body">
                            <asp:GridView ID="gvcustdues" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvcustdues_PageIndexChanging"
                                ShowFooter="True"
                                OnRowDataBound="gvcustdues_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgactdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Cutomer Name">
                                        <HeaderTemplate>
                                            <table style="width: 105px;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                            Text="Particulars" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                            CssClass="btn btn-success btn-xs" ToolTip="Export Excel">X</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lgacuname" runat="server"
                                                Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Desc.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgudesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Concern Person">

                                        <ItemTemplate>
                                            <asp:Label ID="lgCper" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cteam")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Installment">

                                        <FooterTemplate>
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#000"
                                                Text="Grand Total:" Font-Size="12px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvInstallment" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="110px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Schedule Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvschddate" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Dues Inst.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDues" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueins")).ToString("#,##0;(#,##0); ") %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Previous Dues">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDueAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Current Dues">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcurDueAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcurDamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdueamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                                         <HeaderTemplate>

                                              <asp:CheckBox ID="chkAll" runat="server"  AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged"
                                         
                                            Width="20px" />

                                         </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:CheckBox ID="chksms" runat="server" 
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chksms"))=="True" %>'
                                            Width="20px" />
                                    </ItemTemplate>
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


 

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

