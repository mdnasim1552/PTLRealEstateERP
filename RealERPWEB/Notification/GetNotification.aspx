<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="GetNotification.aspx.cs" Inherits="RealERPWEB.Notification.GetNotification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <div class="card card-fluid container-data mt-5" id='printarea'>
                <div class="card-body">
                <div class="card-header">
                    <div class="row">
                    <div class="col-md-12">
                        <h3>Notification Details</h3>
                    </div>
                    </div>
                    </div>

                    <div class="row d-none">
                        <div class="col-md-4 col-sm-6 col-lg-4">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">From</button>
                                </div>
                                <asp:TextBox ID="txtFdate" runat="server" autocomplete="off" CssClass="from-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFdate"></cc1:CalendarExtender>
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">To</button>
                                </div>
                                <asp:TextBox ID="txtTdate" runat="server" autocomplete="off" CssClass="from-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtTdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTdate"></cc1:CalendarExtender>

                            </div>
                        </div>
                    </div>

                </div>
                <div class="card-body">
                    <div class="row">
                    <div class="col-md-12">

                        <div class="">
                            <asp:GridView ID="gvNotificaitons" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-bordered table-striped display" AllowPaging="True" ViewStateMode="Enabled"
                                AllowSorting="True" PageSize="500">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sender ID Name" SortExpression="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")).Length==0?Convert.ToString(DataBinder.Eval(Container.DataItem, "username")):Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Subject">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfrom" runat="server" Text='<%# Eval("eventitle") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="From" SortExpression="category" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldFrom" runat="server" Text='<%# Eval("sendid") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Details">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMsg" runat="server" Text='<%# Eval("msgdesc").ToString()+ " "+ Eval("refdesc").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date & Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldtime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "msgtime")).ToString("dddd, dd MMM yyy, HH:mm tt") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluserid" runat="server" Text='<%# Eval("userid") %>'></asp:Label>
                                            <asp:Label ID="lblcomcod" runat="server" Text='<%# Eval("comcod") %>'></asp:Label>
                                            <asp:Label ID="lblrefid" runat="server" Text='<%# Eval("refid") %>'></asp:Label>
                                            <asp:Label ID="lblntype" runat="server" Text='<%# Eval("ntype") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnMesg" Text="Message" runat="server" CssClass="btn-text "><span class="glyphicon glyphicon-envelope" style="margin-top:5px;" ></span> Message</asp:LinkButton>
                                            <asp:LinkButton ID="lnkView" Text="View" runat="server" OnClientClick="NewWindow();" CssClass="btn-text"><span class="glyphicon glyphicon-eye-open" style="margin-top:5px; margin-left:10px"></span> View</asp:LinkButton>
                                            <asp:LinkButton ID="lnkdele" Text="Delete" runat="server" CssClass="btn-text " Visible="false"><span class="glyphicon glyphicon-trash" style="margin-top:5px; margin-left:10px""></span> Delete</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <EmptyDataTemplate>
                                    <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                    </div>
                </div>



            </div>
             <script>

                 $(document).ready(function () {
                     $('#ContentPlaceHolder1_gvNotificaitons').DataTable();
                 });

             </script>
        </ContentTemplate>

       
    </asp:UpdatePanel>
</asp:Content>
