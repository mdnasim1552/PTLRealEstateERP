<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LOMoneyReciptapp.aspx.cs" Inherits="RealERPWEB.F_37_LOwner.LOMoneyReciptapp" %>

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

        }



    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
            <div class=" card card-fluid">
                <div class=" card-body" style="min-height: 300px;">

                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label for="Label5" runat="server" class=" control-label  lblmargin-top9px ">Project Name</label>


                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select" TabIndex="3">
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <asp:GridView ID="gvMoneyRecipt" runat="server" AutoGenerateColumns="False"
                            CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" OnRowDataBound="gvMoneyRecipt_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: center"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)%>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MR Date" SortExpression="voudat">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRDate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MR No">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRNo" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque No">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvChqNo" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pay Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPayDate" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Name">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPactdesc" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                     <FooterTemplate>
                                        <asp:Label ID="lblgvFTotal" runat="server" Style="text-align: right" Text="Total"  ></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit Name">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvunitdesc" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Amount">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRAmt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>' Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFMRAmt" runat="server" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                            CssClass="btn  btn-success   btn-sm" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkMoneyRcptPrint" runat="server" Target="_blank" ToolTip="Money Receipt Print" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Reconcillation">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrecnDate" runat="server" BorderStyle="None" BackColor="Transparent" 
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy") =="01-Jan-1900"? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtgvrecnDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvrecnDate"></cc1:CalendarExtender>
                        


                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnApproved" OnClick="lnkbtnApproved_Click" runat="server" ToolTip="Reconcilled"><span class="fa fa-check" ></span></asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkMoneyRcptEdit" ToolTip="Edit" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-edit"></span>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Pactcode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpactcode" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="" />
                            <RowStyle CssClass="grvRows" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
