<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptDateWiseBill.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptDateWiseBill" %>
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
                             <asp:GridView ID="gvDWBill" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="501px" AllowPaging="True"  PageSize="20" OnPageIndexChanging="gvDWBill_PageIndexChanging">
                        <Columns>
                         
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Suppliers Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbIssueqty" runat="server" Width="130px" Style="text-align: right" Font-Size="12px" Font-Bold="true"> Total :</asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMatCode" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ERP Bill No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod1" runat="server" Style="text-align: left" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bill No/Ref As per Supp. Ref">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvspcfdesc" runat="server" Style="text-align: left" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                        Width="90px"></asp:Label>
                                      
                                </ItemTemplate>
                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill No/Ref As per Front Desk">
                                <ItemTemplate>
                                   <asp:Label ID="lbgvspcfdesc1" runat="server" Style="text-align: left"
                                       
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved Amount(Taka)">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit" runat="server"
                                        Style="font-size: 12px; text-align: right; font-weight:bold;" Font-Size="12px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                              
                            <FooterTemplate>
                                    <asp:Label ID="lgvtotalappamt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                             <FooterStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Security/Advance/<br>Tax(Taka)">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit1" runat="server"
                                        Style="font-size: 12px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                               
                             <FooterTemplate>
                                    <asp:Label ID="lgvtotaladvamt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                             <FooterStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Net Payable(Taka)">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit2" runat="server"
                                        Style="font-size: 12px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayable")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                               <FooterTemplate>
                                    <asp:Label ID="lgvtotalnetamt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                </FooterTemplate>
                             <FooterStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit3" runat="server"
                                        Style="font-size: 12px; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                               
                             <FooterStyle HorizontalAlign="left" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit4" runat="server"
                                        Style="font-size: 12px; text-align: left;"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                               
                             <FooterStyle HorizontalAlign="left" />

                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                     <asp:Label ID="lblgvunit5" runat="server"
                                        Style="font-size: 12px; text-align: left;"
                                       
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                               

                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Action">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                    </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="isPrint" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isPrint"))=="True" %>' />
                                        
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="true" Font-size="16px" />
                                <ItemStyle HorizontalAlign="center" />
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
