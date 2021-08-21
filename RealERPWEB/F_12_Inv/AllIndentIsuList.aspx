<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AllIndentIsuList.aspx.cs" Inherits="RealERPWEB.F_12_Inv.AllIndentIsuList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
            var gridview = $('#<%=this.gvPromData.ClientID %>');
            gridview.ScrollableGv();

            $('.chzn-select').chosen({ search_contains: true });
        };

    </script>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                <ContentTemplate>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>

                                    </div>
                                  

                                    <div class="col-md-2">
                                        <%-- <asp:DropDownList ID="ddltype" runat="server" Width="90px"  CssClass="inputTxt chzn-select pull-left">
                                                   <asp:ListItem Value="PRO" Selected="True">Product</asp:ListItem>
                                                      <asp:ListItem Value="CUST">Customer</asp:ListItem>
                                                </asp:DropDownList>--%>
                                        <asp:Label ID="LblMsg" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>

                                    </div>
                                </div>
                            </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                <ContentTemplate>
                    <div class="col-md-12">

                        <asp:GridView ID="gvPromData" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvPromData_RowDataBound"
                                    OnPageIndexChanging="gvPromData_PageIndexChanging" CssClass="table-striped  table-hover table-bordered grvContentarea" AllowPaging="false">
                                    <RowStyle />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "issuedat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Challan No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblissueno" runat="server"  Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTeamCard" runat="server"  Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>                                                                                        
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name of Person">
                                            <HeaderTemplate>
                                                <table style="width: 250px;">
                                                    <tr>


                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>

                                                          
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblrsirdesc" runat="server"  Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDept" runat="server"  Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>                                                                                        
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Area">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvArea" runat="server"  Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "territory")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>                                                                                        
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        

                                             <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblissueqty" runat="server"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFissueqty" runat="server" Font-Bold="True" Font-Size="10px"
                                                    ForeColor="#000" Width="60px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                            <HeaderStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrate" runat="server"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFrate" runat="server" Font-Bold="True" Font-Size="10px"
                                                    ForeColor="#000" Width="60px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                            <HeaderStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblissueamt" runat="server"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFissueamt" runat="server" Font-Bold="True" Font-Size="10px"
                                                    ForeColor="#000" Width="80px"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                            <HeaderStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnChalan" OnClick="lbtnChalan_Click" CssClass="btn btn-xs btn-success" runat="server" Text="<span class='glyphicon glyphicon-hand-right'></span>"></asp:LinkButton>
                                               </ItemTemplate>                                  
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                         <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                 
                                                <asp:HyperLink ToolTip="Edit And Approve" ID="BtnEdit" CssClass="btn btn-xs btn-success" runat="server"><span class="glyphicon glyphicon-ok"></span></asp:HyperLink>
                                                  <asp:HyperLink ID="HypRDDoPrint" runat="server" Target="_blank"  CssClass="btn btn-xs btn-danger"><span class="glyphicon glyphicon-print"></span>
                                                                        </asp:HyperLink>
                                                <asp:LinkButton ID="LbtnDelete" CssClass="btn btn-warning btn-xs" OnClick="LbtnDelete_Click" OnClientClick="return confirm('Do You want to delete this item?');" runat="server"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                            </ItemTemplate>                                  
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />

                                </asp:GridView>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

