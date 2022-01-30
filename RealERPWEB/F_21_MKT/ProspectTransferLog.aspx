<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ProspectTransferLog.aspx.cs" Inherits="RealERPWEB.F_21_MKT.ProspectTransferLog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


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
                        <h3>Notifications</h3>
                    </div>
                    </div>
                    </div>

                    <div class="row">
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
                            <asp:GridView ID="gvtransLog" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-bordered table-striped display" AllowPaging="True" ViewStateMode="Enabled"
                                AllowSorting="True" PageSize="500">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='1' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prospect Name" SortExpression="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomname" runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="From Assign">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfrom" runat="server" Text='we'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="To Assign" SortExpression="category" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldFrom" runat="server" Text='rdf'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMsg" runat="server" Text='yyy'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer By">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldtime" runat="server" Text='ggg'></asp:Label>
                                        </ItemTemplate>
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