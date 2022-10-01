<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="RealERPWEB.F_38_AI.Projects" %>

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

            <div class="card mt-2">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-3">
                            <h5><i class=" text-primary fa fa-list"></i>Demo Project (Batch) &nbsp;</h5>

                        </div>
                        <div class="col-md-2">

                            <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-success ml-auto bw-100 btn-sm mt20 mr-2 dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Set Status</button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="dropdown-item" Style="padding: 0 10px">On Track</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" CssClass="dropdown-item" Style="padding: 0 10px">At risk</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" CssClass="dropdown-item" Style="padding: 0 10px">Off track</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink5" runat="server" CssClass="dropdown-item" Style="padding: 0 10px">On hold</asp:HyperLink><hr />
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="dropdown-item text-primary color-white" Style="padding: 0 10px">Complate</asp:HyperLink>
                                        <%--<asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/CreateTask" CssClass="dropdown-item" Style="padding: 0 10px">Create Task</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/StepofOperationNew?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Assign Team Leader</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" NavigateUrl="~/StepofOperationNew?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Creating Invoice</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/StepofOperationNew?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Day Wise Work Status</asp:HyperLink>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                          <div class="form-horizontal">
                                <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                    <asp:RadioButtonList ID="TasktState" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1"><a class="nav-link active" href="#">OverView</a></asp:ListItem>
                                        <asp:ListItem Value="2"><a class="nav-link" href="#">List </a></asp:ListItem>
                                        <asp:ListItem Value="3"><a class="nav-link" href="#">Board</a></asp:ListItem>
                                        <asp:ListItem Value="4"><a class="nav-link" href="#">Timeline</a></asp:ListItem>
                                        <asp:ListItem Value="5"> <a class="nav-link" href="#">Calendar</a></asp:ListItem>
                                        <asp:ListItem Value="6"> <a class="nav-link" href="#">Workflow</a></asp:ListItem>
                                        <asp:ListItem Value="7"> <a class="nav-link" href="#">Dashboard</a></asp:ListItem>
                                        <asp:ListItem Value="8"> <a class="nav-link" href="#">Files</a></asp:ListItem>                                                                          
                                    </asp:RadioButtonList> 
                                </div>
                            </div>                       
                    </div>

                </div>
                <div class="card-body">
                    
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View runat="server" ID="OverView">
                                <h4>OverView</h4>
                            </asp:View>
                            <asp:View runat="server" ID="ListView">
                                <h4>ListView</h4>
                            </asp:View>
                            <asp:View runat="server" ID="BoardView">
                                <h4>BoardView</h4>
                            </asp:View>
                            <asp:View runat="server" ID="TimelineView">
                                <h4>TimelineView</h4>
                            </asp:View>
                            <asp:View runat="server" ID="CalendarView">
                                <h4>CalendarView</h4>
                            </asp:View>
                            <asp:View runat="server" ID="WorkflowView">
                                <h4>WorkflowView</h4>
                            </asp:View>
                            <asp:View runat="server" ID="DashboardView">
                                <h4>DashboardView</h4>
                            </asp:View>
                            <asp:View runat="server" ID="FileView">
                                <h4>FileView</h4>
                            </asp:View>                           

                        </asp:MultiView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
