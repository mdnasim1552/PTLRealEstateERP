<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPrjSchAnaLysis.aspx.cs" Inherits="RealERPWEB.F_07_Ten.RptPrjSchAnaLysis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<style type="text/css">
        .style19
        {
            width: 9px;
            height: 23px;
        }
        .style20
        {
            width: 82px;
            height: 23px;
        }
        .style21
        {
            width: 81px;
            height: 23px;
        }
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 12px;
            font-weight: normal;
            margin-right: 0px;
            text-align: left;
        }
        .style25
        {
            height: 23px;
            width: 10px;
        }
        .style24
        {
            height: 23px;
            width: 656px;
        }
        .style23
        {
            height: 23px;
        }
        .style29
        {
            height: 23px;
            width: 43px;
        }
        .style27
        {
            height: 17px;
        }
        .style30
        {
            height: 23px;
            width: 14px;
        }
        .style31
        {
            height: 23px;
            width: 24px;
        }
        .style33
        {
            width: 357px;
        }
        .style34
        {
            width: 359px;
        }
        .style35
        {
            width: 114px;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="row">
                    <fieldset class="scheduler-border fieldset_A">


                        <div class="form-group">
                            <div class="col-md-5">
                                <asp:Label ID="Label5" runat="server" CssClass="lblTxt smLbl_to lblName" Style="font-size: 11px;"
                                    Text="Project Name:"></asp:Label>

                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inpPixedWidth"
                                    Width="80px"></asp:TextBox>
                                 <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_OnClick" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="smLbl_to chzn-select  ddlPage"
                                    Width="250px" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server" QueryPattern="Contains"
                                    TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>
                                
                            </div>
                           <div class="col-md-1">
                               <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn"
                                    OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                           </div>
                            
                            <div class="col-md-2">
                                 <asp:Label ID="lblPage" runat="server" Font-Bold="True" CssClass="smLbl_to" Font-Size="12px" Style="text-align: right;"
                                    Text="Page Size:" Visible="False" ></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" CssClass="smLbl_to" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                    Font-Bold="True" Font-Size="12px" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Visible="False" Width="80px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                               

                                <asp:Label ID="lblflrlist" runat="server"  CssClass=" smLbl_to"
                                    Text="Floor :"></asp:Label>

                                <asp:DropDownList ID="ddlFloorListRpt" runat="server"  CssClass=" ddlPage" Width="120px">
                                </asp:DropDownList>

                                <asp:Label ID="lblRptGroup" runat="server"   CssClass=" smLbl_to" Text="Group:"
                                  ></asp:Label>

                                <asp:DropDownList ID="ddlRptGroup" runat="server"  CssClass=" ddlPage"   Width="100px">
                                    <asp:ListItem>Main</asp:ListItem>
                                    <asp:ListItem>Sub-1</asp:ListItem>
                                    <asp:ListItem>Sub-2</asp:ListItem>
                                    <asp:ListItem>Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="row">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewScheduleVsAnalysis" runat="server">
                             <div class="table-responsive">
                            <asp:GridView ID="gvSchedule" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                ShowFooter="True" OnPageIndexChanging="gvSchedule_PageIndexChanging" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor Desc.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvflrdesc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcWDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                                Width="230px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" 
                                                Text="Total:"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="False" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scheduel Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvschqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Budgeted Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Schedule Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvschrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Standard Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvanarate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBgdrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Schedule Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvschamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFSchamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Standard Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvanaamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAnaamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvBgdamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdgamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFBgdamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Schedule %" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsccper" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perschamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Analysis %" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvanaper" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Diff. Rate" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdiffrat" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Diff.%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvanaper" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                                 </div>

                            <asp:Panel ID="Panel2" runat="server">

                                <asp:Label ID="lbltxtDiffamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                    Text="Difference (Amount):"></asp:Label>

                                <asp:Label ID="lbltxtvalDiffamt" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="White"></asp:Label>

                                <asp:Label ID="lbltxtvalDiffamt2" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="White"></asp:Label>


                                <asp:Label ID="lbltxtDiffper" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                    Text="Difference (%):"></asp:Label>

                                <asp:Label ID="lbltxtvalDiffper" runat="server" Font-Bold="True" Font-Size="12px"
                                    ForeColor="White" Text="Difference (%):"></asp:Label>

                            </asp:Panel>


                        </asp:View>
                        <asp:View ID="View1" runat="server">
                             <div class="table-responsive">
                            <asp:GridView ID="gvtenprosal" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                OnPageIndexChanging="gvtenprosal_PageIndexChanging" ShowFooter="True" OnRowDataBound="gvtenprosal_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <RowStyle/>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>


                                            <asp:HyperLink ID="hlnkgvWDesc" runat="server" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' Width="300px" Style="font-size: 12px; color: Black; text-decoration: none;">   
                                                                    
                                                                    
                                                                    
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="False" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alternative 1">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtenamt1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alternative 2">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtenamt2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alternative 3">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtenamt3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="right" />
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
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="table-responsive">
                            <asp:GridView ID="gvBenefit" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                 OnPageIndexChanging="gvSubRate_PageIndexChanging" ShowFooter="True"
                                Style="text-align: left" Width="600px" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItemDes" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvFlrDes" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOQ.Qty">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQty" runat="server" Font-Bold="False" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOQ.Rate">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtgvSRate" runat="server" BorderStyle="None" 
                                                                style="text-align: right; background-color:Transparent" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="75px"></asp:TextBox>--%>
                                            <asp:Label ID="lblgvSRate" Width="75px" Style="text-align: right; font-size: 12px;"
                                                runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schrate")).ToString("#,##0.00;(#,##0.00); ") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="75px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOQ.Amt.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ftlblBoqAmt" runat="server" Style="text-align: right; font-size: 12px;"
                                                Text=''></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <%--     <asp:TextBox ID="txtgvSAmt" runat="server" BorderStyle="None" 
                                                                style="text-align: right; background-color:Transparent" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="90px"></asp:TextBox>--%>
                                            <asp:Label Style="text-align: right; font-size: 12px;" Width="90px" ID="lblgvSAmt"
                                                runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0.00;(#,##0.00); ") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Survey Qty.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvSurvQty" runat="server" BorderStyle="None" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sarqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Survey Rate.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvSurvRete" runat="server" BorderStyle="None" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sarrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Differ. Rate.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvdiffrat" runat="server" BorderStyle="None" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Survey Amt.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ftlblSurveyAmt" runat="server" Style="text-align: right; font-size: 12px;"
                                                Text=''></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <%--  <asp:TextBox ID="txtgvSurvAmt" runat="server" BorderStyle="None" 
                                                                style="text-align: right; background-color:Transparent" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saramt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="90px"></asp:TextBox>--%>
                                            <asp:Label ID="lblgvSurvAmt" Font-Size="12px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="100px" Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Benifit Amt.">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ftlbbillamtAmt" runat="server" Style="text-align: right; font-size: 12px;"
                                                Text=''></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillamtAmt" Font-Size="12px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle Width="100px" Font-Size="11px" />
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
                        <asp:View ID="ViewBenit" runat="server">
                                <asp:GridView ID="gvTenVsBudget" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvTenVsBudget_PageIndexChanging" ShowFooter="True" 
                                    onrowdatabound="gvTenVsBudget_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <RowStyle  Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMatDescbn" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mrptdesc")) + "</B>"+
                                                                      (DataBinder.Eval(Container.DataItem, "rptdesc").ToString().Trim().Length > 0 ?
                                                                      (Convert.ToString(DataBinder.Eval(Container.DataItem, "mrptdesc")).Trim().Length > 0 ? "<br>" : "") +                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                   Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")).Trim() : "")
                                                                         
                                                                    %>' Width="250px">
                                                    
                                                    
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Text="Total:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Text="Different Amount:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                   
                                                </table>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="False" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnitbn" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tender">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtenqtybn" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tenqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbgdqtybn" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Tender">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtenratbn" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tenrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbgdratbn" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                 
                                        <asp:TemplateField HeaderText="Tender">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtenamtbn" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tenam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFtenamtbn" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFDifta01bamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                  
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbgdambn" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFbgdambn" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFdifatbam" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                  
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="For Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdifqtybn" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFdiffambn" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFdiffambn02" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                  
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="For Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdiffram" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFdiffram" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFdiffram02" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="80px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                  
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>





                                         <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtoambn" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "difftoam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFtoambn" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lgvFtoambn02" runat="server" Font-Bold="True" Font-Size="12px"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                  
                                                </table>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>




                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
