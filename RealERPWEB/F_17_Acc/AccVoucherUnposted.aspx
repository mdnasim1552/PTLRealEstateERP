
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccVoucherUnposted.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccVoucherUnposted" %>

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





        };

    </script>

    <style>
       
        .grvContentarea tr td:last-child a span{
            margin:0 5px !important;
        }

    </style>



   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-8 pading5px ">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">from</asp:Label>


                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to">To</asp:Label>


                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                        <asp:Label ID="lblrefno" runat="server" CssClass="lblTxt lblName">Ref.</asp:Label>
                                        <asp:TextBox ID="txtrefno" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                       
                                        

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>



                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:CheckBox ID="ChboxPayee" runat="server" Text="A/C Payee" CssClass="btn btn-primary checkBox" />
                                        <asp:Label ID="lblmsg" CssClass="btn-danger btn  primaryBtn" runat="server" Visible="false"></asp:Label>

                                    </div>
                                </div>



                            </div>
                        </fieldset>


                        <asp:GridView ID="gvAccUnPosted" runat="server" AutoGenerateColumns="False"
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvAccUnPosted_RowDataBound"
                            ShowFooter="True" Width="550px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvoudat" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                            Width="80px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Voucher">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvounum" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reference">
                                    <ItemTemplate>
                                        <asp:Label ID="txtINgvteamdesc" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Voucher Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvouamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right"  Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkvmrno" runat="server" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="" >
                                    <ItemTemplate>
                                        <%-- <asp:LinkButton ID="lnkbtnViewIN" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnkbtnPrintIN" runat="server" OnClick="lnkbtnPrintRD_Click"  ><span class="glyphicon glyphicon-print"></span></asp:LinkButton>
                                        <%--<asp:HyperLink ID="hlnkVoucherEdit" ToolTip="Edit"  runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" ><span class="glyphicon glyphicon-pencil"></span>--%>
                                        </asp:HyperLink>

                                        
                                          <asp:LinkButton ID="lbtnVoucherApp" ToolTip="Approved"  runat="server" OnClick="lbtnVoucherApp_Click" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'><span class=" glyphicon glyphicon-ok"></span> </asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDeleteVoucher" ToolTip="Delete" runat="server" OnClick="lbtnDeleteVoucher_Click"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="300px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->

      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>

