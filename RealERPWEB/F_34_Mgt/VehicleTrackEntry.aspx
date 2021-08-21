<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="VehicleTrackEntry.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.VehicleTrackEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPartSmall">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtfromdate" Enabled="true"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-3" style="margin-top: 5px">
                            <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="smLbl_to" Width="70" Text="Vehicle :"></asp:Label>
                            <asp:DropDownList ID="ddlvehicle" runat="server" Width="170" CssClass="ddlPage" AutoPostBack="True">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2" style="margin-top: 5px">
                            <div class="colMdbtn pading5px">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            -
                        </div>
                        <div class="col-md-3" style="margin-top: 5px">
                            <asp:Label ID="Label2" runat="server" CssClass="smLbl_to" Width="70" Text="Driver :"></asp:Label>
                            <asp:DropDownList ID="ddldriver" runat="server" Width="170" CssClass="ddlPage" AutoPostBack="True">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-6" style="margin-top: 5px">
                            <asp:Label ID="lblcurVounum" runat="server" CssClass=" lblName lblTxt" Text="Location:"></asp:Label>
                            <asp:TextBox ID="txtcurrentvou" runat="server" AutoCompleteType="Disabled" AutoPostBack="true" CssClass="inputtextbox"></asp:TextBox>
                            <asp:TextBox ID="TextBox1" runat="server" AutoCompleteType="Disabled" AutoPostBack="true" CssClass="inputtextbox"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            -
                        </div>
                        <div class="col-md-3" style="margin-top: 5px">
                            <asp:Label ID="Label1" runat="server" CssClass="smLbl_to" Width="70" Text="Employee:"></asp:Label>
                            <asp:DropDownList ID="ddlemp" runat="server" Width="170" CssClass="ddlPage" AutoPostBack="True">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-6" style="margin-top: 5px">
                            <asp:Label ID="Label3" runat="server" CssClass=" lblName lblTxt" Text="Ref No:"></asp:Label>
                            <asp:TextBox ID="TextBox3" runat="server" AutoCompleteType="Disabled" AutoPostBack="true" CssClass="inputtextbox"></asp:TextBox>
                        </div>

                    </div>
                    <div class="col-md-12">
                        <asp:GridView ID="gvTransaction" runat="server" Width="59%" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" PageSize="15" ShowFooter="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdate" runat="server" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy")                                                                        %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblgvvoucher" Visible="false" runat="server" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))  %>'></asp:LinkButton>

                                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1"))  %>' Width="80px"></asp:LinkButton>


                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="System #">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkrefnum" runat="server" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>' Width="80px" Style="color: black;"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A/C Desription">
                                    <FooterTemplate>


                                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>

                                        <asp:LinkButton ID="lnkAccdesc1" runat="server" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar1"))  %>' Width="550px" Style="color: black;"></asp:LinkButton>

                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source Ref.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblactcode1" runat="server" Font-Names="Verdana" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "srinfo"))  %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="12px" ForeColor="Black" Style="text-align: right" TabIndex="81" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFgvDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="#000" ReadOnly="True" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px" ForeColor="Black" Style="text-align: right" TabIndex="82" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtFgvCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="#000" ReadOnly="True" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" NextPageText="next" PageButtonCount="15" Position="TopAndBottom" PreviousPageText="previous" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                        </asp:GridView>
                        <br />

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-4">
                                    </div>

                                    <div class="col-md-6 formBtn" style="display: none;">
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" NavigateUrl="#"><span class="glyphicon glyphicon-refresh  asitGlyp"></span> Refresh</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" NavigateUrl="#"><span class="glyphicon glyphicon-chevron-left asitGlyp"></span> Previous</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" NavigateUrl="#">Next<span class="glyphicon glyphicon-chevron-right asitGlyp"></span></asp:HyperLink>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="formBtn ">
                                        <div class="pull-left">
                                            <%--<asp:Label ID="lblcontrolAccHead" runat="server" CssClass=" smLbl_to" Text="Batch No."></asp:Label>
                                            <asp:DropDownList ID="ddlBatchGrp" runat="server" Width="170" CssClass="ddlPage" AutoPostBack="True" OnSelectedIndexChanged="ddlBatchGrp_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                                            <%--                                            <asp:TextBox ID="txtHACDESC" CssClass="ddlPage" ForeColor="Black" Style="margin-left: 10px;" runat="server"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class=" clearfix"></div>
                </div>
                <!-- End of contentpart-->
                <fieldset class="scheduler-border fieldset_A" style="align-items: baseline;">

                    <div class="form-horizontal">
                        <div class="form-group">

                            <div class="col-md-12 formBtn ">
                                <div class="pull-right">
                                    <asp:LinkButton ID="btnClose" runat="server" CssClass="button button-red" Style="margin: 0 5px;">Close</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

