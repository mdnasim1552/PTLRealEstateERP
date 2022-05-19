<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptWorkOdrAdvSupplierWise.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptWorkOdrAdvSupplierWise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
          <%--  $('#<%=this.gvsupstatus.ClientID%>').tblScrollable();--%>
            $(function () {
                $('[id*=chkSupCategory').multiselect({
                    includeSelectAllOption: true,

                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,

                });

            });

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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">Date :</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                            </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">Date :</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                            </div>

                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="margin-top30px">
                                    <asp:DropDownList ID="ddlSuppleir" runat="server" CssClass="chzn-select form-control  inputTxt ">
                                    </asp:DropDownList>
                                </div>
                                <%--<asp:ListBox ID="chkSupCategory" runat="server" CssClass="form-control" Style="min-width: 200px !important;" SelectionMode="Multiple"></asp:ListBox>--%>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        



                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 250px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewOtherCharge" runat="server">

                            <asp:GridView ID="gvsupstatus" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnRowCreated="gvsupstatus_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Name">
                                        <HeaderTemplate>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name" Width="160px"></asp:Label>

                                            <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                            </asp:HyperLink>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvssirdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFssirdesc" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon"> Grand Total : </asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMaterialName" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "limit")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvMaterialName" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="P.O Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpodate" runat="server" Font-Size="11px"
                                                Style="text-align: center;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "period")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="PO No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPoNo" runat="server" Font-Size="11px"
                                                Style="text-align: right" Font-Underline="false" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqhamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterTemplate>
                                            <asp:Label ID="lblFgvPoNo" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon" Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Receive Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreceivedate" runat="server" Font-Size="11px"
                                                Style="text-align: center;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "period")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Challan No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChallanNo" runat="server" Font-Size="11px"
                                                Style="text-align: right" Font-Underline="false" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billiccdamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterTemplate>
                                            <asp:Label ID="lblFChallanNo" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon" Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Net Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvNetpayment" runat="server" Font-Size="11px"
                                                Style="text-align: right" Font-Underline="false" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billinpamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterTemplate>
                                            <asp:Label ID="lblFbillinpamt" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon" Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total Bill Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtotalduebill" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalduebill")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterTemplate>
                                            <asp:Label ID="lblFtotalduebill" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon" Width="80px"></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="120 Days Over">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvmon4amt" runat="server" Font-Size="11px"
                                                Style="text-align: right" Font-Underline="false" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mon4amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterTemplate>
                                            <asp:Label ID="lblFmon4amt" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon" Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="90 Days Over">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlmkgvmon34amt" runat="server" Font-Size="11px"
                                                Style="text-align: right" Font-Underline="false" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mon34amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterTemplate>
                                            <asp:Label ID="lblFmon34amt" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon" Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Upto 90 Days">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvmon13amt" runat="server" Font-Size="11px"
                                                Style="text-align: right" Font-Underline="false" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mon13amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterTemplate>
                                            <asp:Label ID="lblFmon13amt" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon" Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Amt Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtotaldaysamt" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totaldaysamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterTemplate>
                                            <asp:Label ID="lblFtotaldaysamt" runat="server" Font-Size="12px" Height="16px"
                                                Style="text-align: right; color: maroon" Width="80px"></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

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
