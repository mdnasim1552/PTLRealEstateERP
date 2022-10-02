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
                            <h5><i class=" text-primary fa fa-list"></i>&nbsp; Demo Project (Batch) &nbsp;</h5>

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

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-horizontal">
                            <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                <asp:RadioButtonList ID="ProjectDetails" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="ProjectDetails_SelectedIndexChanged1">
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


                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View runat="server" ID="OverView">

                           
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="card">
                                        <div class="card-header bg-light text-center p-1">
                                            <span class="font-weight-bold text-muted">Project Information</span>
                                        </div>
                                        <div class="card-body">
                                            <img src="~/../../../Images/noimageavl.png" style="display: block; margin-left: auto; margin-right: auto; width: 50%;" alt="User Image">
                                             
                                            <asp:GridView ID="gv_projOverView" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="False" ShowHeader="false" AllowPaging="false" Visible="True" Width="100%">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" CssClass="font-weight-bold" ID="lblprjName" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblprjgdesce" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="card">
                                        <div class="card-header bg-light p-1"><span class="font-weight-bold text-muted">Batch Information</span></div>
                                        <div class="card-body"></div>
                                    </div>
                                </div>
                            </div>
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
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                        <div class="text-center">
                                            <h6>Completed Tasks</h6>
                                        </div>
                                        <h2 class="text-center">3</h2>
                                        <div class="text-center">
                                            <p><i class="fa fa-angle-double-down"></i>Filter</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                        <div class="text-center">
                                            <h6>InCompleted Tasks</h6>
                                        </div>
                                        <h2 class="text-center">0</h2>
                                        <div class="text-center">
                                            <p><i class="fa fa-angle-double-down"></i>Filter</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                        <div class="text-center">
                                            <h6>OverDue Tasks</h6>
                                        </div>
                                        <h2 class="text-center">0</h2>
                                        <div class="text-center">
                                            <p><i class="fa fa-angle-double-down"></i>Filter</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="shadow-lg p-3 mb-5 bg-body rounded">
                                        <div class="text-center">
                                            <h6>Total Tasks</h6>
                                        </div>
                                        <h2 class="text-center">3</h2>
                                        <div class="text-center">
                                            <p><i class="fa fa-angle-double-down"></i>No Filter</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="card p-3 mb-5 bg-body rounded" style="height: 200px">
                                        <div class="card-body">
                                            <p>InComplete Tasks by section</p>
                                            <div class="text-center">
                                                <asp:Image runat="server" CssClass="text-center" ID="iamge1" Style="height: 100px; width: 300px;" ImageUrl="https://cdn.pixabay.com/photo/2013/07/12/14/18/productivity-148197_960_720.png" />

                                            </div>

                                        </div>
                                        <div class="card-footer mt-1">
                                            <p><i class="fa fa-angle-double-down"></i>1 Filter</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card p-3 mb-5 bg-body rounded" style="height: 200px">
                                        <div class="card-body">
                                            <p>Total Tasks by Completion status</p>
                                            <div class="text-center">
                                                <asp:Image runat="server" ID="Image1" Style="height: 100px; width: 100px;" ImageUrl="https://cdn.pixabay.com/photo/2016/09/03/14/35/algorithms-1641861_960_720.png" />

                                            </div>

                                        </div>
                                        <div class="card-footer mt-1">
                                            <p><i class="fa fa-angle-double-down"></i>1 Filter</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="card p-3 mb-5 bg-body rounded" style="height: 200px">
                                        <div class="card-body">
                                            <p>Upcoming tasks by assigne</p>
                                            <div class="text-center">
                                                <asp:Image runat="server" CssClass="text-center" ID="Image2" Style="height: 100px; width: 300px;" ImageUrl="https://img.freepik.com/free-photo/flat-lay-statistics-presentation-with-chart-arrows_23-2149023777.jpg?w=740&t=st=1664624137~exp=1664624737~hmac=fbd68a18f560f1656b386a53fab82cd928c55ceb77d143b162a160053d1fdfd7" />

                                            </div>
                                        </div>
                                        <div class="card-footer mt-1">
                                            <p><i class="fa fa-angle-double-down"></i>No Filter</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card p-3 mb-5 bg-body rounded" style="height: 200px">
                                        <div class="card-body">
                                            <p>Task completion over time</p>
                                            <div class="text-center">
                                                <asp:Image runat="server" CssClass="text-center" ID="Image3" Style="height: 100px; width: 300px;" ImageUrl="https://cdn.pixabay.com/photo/2015/10/31/11/59/financial-equalization-1015282_960_720.jpg" />

                                            </div>
                                        </div>
                                        <div class="card-footer mt-1">
                                            <p><i class="fa fa-angle-double-down"></i>2 Filter</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View runat="server" ID="FileView">
                            <h4>FileView</h4>
                        </asp:View>

                    </asp:MultiView>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
