<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptIndentIssueStatusSummary.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptIndentIssueStatusSummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
           
            <div class="card mt-4 pb-4">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">

                            <label class="control-label">Store Name</label>
                            <asp:DropDownList ID="ddlStoreName" runat="server" CssClass="chzn-select form-control form-control-sm chzn-single" AutoPostBack="True"></asp:DropDownList>

                        </div>

                        <div class="col-md-2">

                            <label class="control-label">Department</label>
                            <asp:DropDownList ID="ddldpt" runat="server" CssClass="chzn-select form-control form-control-sm form-control-sm chzn-single" AutoPostBack="True"></asp:DropDownList>

                        </div>
                        <div class="col-md-2">

                            <label class="control-label" for="FromDate">From Date</label>
                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control flatpickr-input" autocomplete="off"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                        </div>
                        <div class="col-md-2">

                            <label class="control-label" for="ToDate">To Date</label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control flatpickr-input" autocomplete="off"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                        </div>

                        <div class="col-md-1">

                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnOk_Click"  Style="margin-top: 28px;" AutoPostBack="True">Ok</asp:LinkButton>

                        </div>



                    </div>
                   
                </div>
            </div>

               <div class="card" style="min-height: 480px;">
                <div class="card-body">
                    <div class="">
                        <div class="row">
                             <asp:GridView ID="gvIssuest" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="501px">
                        <Columns>
                         
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="10px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Department Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMatCode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:Label ID="lbIssueqty" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"> Total :</asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right"/>
                            </asp:TemplateField>

                           
                             <asp:TemplateField HeaderText="Opening Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "openqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                               <FooterTemplate>
                                    <asp:Label ID="lgvtotalopnqty" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                             <FooterStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Receive Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:Label ID="lgvtotalrcvqty" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                
                             <FooterStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Issue Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvtotalissueqty" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                             <FooterStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Balance Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "blncqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvtotalblncqty" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
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

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
