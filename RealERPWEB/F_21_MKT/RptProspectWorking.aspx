<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptProspectWorking.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptProspectWorking" %>

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
                    <div class="row mb-2" id="divFilter">
                        <div class="col-md-4 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button class="btn btn-secondary" type="button">From</button>
                                </div>
                                <asp:TextBox ID="txtfodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfodate"></cc1:CalendarExtender>
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" id="lblToDate" runat="server" type="button">To</button>
                                </div>
                                <asp:TextBox ID="txttodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="Cal3" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" clientidmode="Static" id="lblcondate" runat="server">Con Date</label>                                    
                                </div>
                                <asp:TextBox ID="txtcondate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtcondate" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtcondate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-3 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Team Lead</button>
                                </div>
                                <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" data-placeholder="Choose Employee.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3 p-0 mt-2">
                            <div class="input-group input-group-alt profession-slect srDiv">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Lead Status</button>
                                </div>
                                <asp:DropDownList ID="ddlleadstatus" data-placeholder="Choose Lead Status.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Ok" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 p-0 mt-2 pading5px">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Page</button>
                                </div>
                                <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control" >
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>400</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-12 table-responsive">
                            <asp:GridView ID="gvSaleFunnel" runat="server" AutoGenerateColumns="False"
                                PageSize="10" AllowPaging="true"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                HeaderStyle-Font-Size="11px" Width="800px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField
                                        HeaderText="Project Name">
                                        <HeaderTemplate>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                Text="Project Name" Width="180px"></asp:Label>
                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblgvItmCode" runat="server"
                                                Font-Size="12px" Font-Underline="False" Target="_blank"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projname")) %>'
                                                Width="300px"></asp:HyperLink>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Profession Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProfession" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'
                                                Width="150px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Source">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSource" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sourtxt")) %>'
                                                Width="50px" ForeColor="Black"></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
