<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="YearlyHoliday.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.YearlyHoliday" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Scripts/gridviewScrollHaVertworow.min.js"></script>


    <script type="text/javascript">


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
            <div class="card">
                <div class="card-header">
                                   <asp:DropDownList ID="ddlholidayType" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlholidayType_SelectedIndexChanged">
                                    <asp:ListItem Value="h">Gov</asp:ListItem>
                                    <asp:ListItem Value="st">Special</asp:ListItem>
                                </asp:DropDownList>
                </div>

                       <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"
                                            PopupButtonID="Image2">
                                        </cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">From</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>

                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"
                                            PopupButtonID="Image2">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="pull-left">
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" OnClick="lnkbtnShow_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                                    </div>
                           </div>
                <div class="card-body">



                    <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvholiday" runat="server" AutoGenerateColumns="False" ClientIDMode="Static"
                                        OnPageIndexChanging="gvLeaveRule_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        PageSize="15">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreason" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reason")) %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus")) %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
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
                            </div>
                       
        
                </div>
            </div>

             

             

                            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
