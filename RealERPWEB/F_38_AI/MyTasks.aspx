<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MyTasks.aspx.cs" Inherits="RealERPWEB.F_38_AI.MyTasks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
         .customeGV tr td, .customeGV tr th{
             font-size:11px !important;
         }
    </style>


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
            <div class="card mt-4">
                <div class="card-header">
                    <div class="col-md-8">
                        <div class="tbMenuWrp nav nav-tabs rptPurInt">
                            <asp:RadioButtonList ID="btnMyTasks" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="btnMyTasks_SelectedIndexChanged">

                                <asp:ListItem Value="2" Selected="True"><a class="nav-link" href="#">Board</a></asp:ListItem>
                                <asp:ListItem Value="3"> <a class="nav-link" href="#">Calendar</a></asp:ListItem>
                                <asp:ListItem Value="4"> <a class="nav-link" href="#">Files</a></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>

                <div class="card-body">
                    <asp:MultiView runat="server" ID="MultiView1">
                        <asp:View runat="server" ID="ListView"></asp:View>
                        <asp:View runat="server" ID="BordView">

                            <div class="row">
                                <div class="col-md-4">
                                    <p>Recently assigned</p>
                                    <div class="card" style="height: 200px;">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvAssingJob" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea customeGV"
                                                ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL # ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right;"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                     
                                                    <asp:TemplateField HeaderText="Order Type" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblordertype" runat="server" Height="16px"
                                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))%>'
                                                                ForeColor="Black"></asp:Label>
                                                            <asp:Label ID="lbljobid" runat="server" Height="16px" Visible="false"
                                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid"))%>'
                                                                ForeColor="Black"></asp:Label>
                                                             <asp:Label ID="lblbatchid" runat="server" Height="16px" Visible="false"
                                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid"))%>'
                                                                ForeColor="Black"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tblbatchname" runat="server" Font-Bold="true"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Lbltasktitle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>'></asp:Label>

                                                            

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Velocity Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tblvelocitytype" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "velocitytype")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Employee Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HyperLink runat="server" ID="btnMytask" Target="_blank" NavigateUrl="~/F_38_AI/MyTasks">
                                                                <asp:Label ID="tblwrktype" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Create date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tblcreatedate" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Velocity <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvelocityqty" runat="server" Width="80px" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "velocityqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Velocity <br> Hour">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblworkhour" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "workhour")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="center" VerticalAlign="Middle" />

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                <asp:LinkButton ID="lnkStartJob" runat="server" CssClass="text-success  btn-sm btn" ToolTip="Start Job" OnClick="lnkStartJob_Click"><i class="fa fa-play-circle"></i></asp:LinkButton>

                                                            
                                                            <%-- <asp:HyperLink ID="lnkView" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/Projects"  CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                                <asp:LinkButton ID="btnRemove" runat="server" CssClass="text-danger pr-2" ToolTip="delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary" ToolTip="edit"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <%--<FooterStyle CssClass="grvFooter" />--%>

                                                <PagerStyle CssClass="gvPagination" />

                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <p>Ongoing Job</p>
                                     <div class="card" style="height: 100px;">
                                    </div>
                                    <p>Hold Job</p>
                                     <div class="card" style="height: 100px;">
                                    </div>
                                    <p>Completed Job</p>
                                     <div class="card" style="height: 100px;">
                                    </div>

                                    <div class="card" style="height: 100px;">
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <p>Do today</p>
                                    <div class="card" style="height: 100%; background-color: #F6F6F6;">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvTodayList" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea customeGV"
                                                ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL # ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right;"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                     
                                                    
                                                    <asp:TemplateField HeaderText="Job Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tblbatchname" runat="server" Font-Bold="true"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Lbltasktitle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>'></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" VerticalAlign="Middle" />

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Velocity <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tblvelocitytype" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "velocitytype")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                   
                                                    <asp:TemplateField HeaderText="Start <br> date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tblcreatedate" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "worktime")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "worktime")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assigned <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvelocityqty" runat="server" Width="80px" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "velocityqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Done <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvelocityqty" runat="server" Width="80px" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobstatus" runat="server" CssClass="badge badge-pill badge-info"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                <asp:LinkButton ID="lnkStartJob" runat="server" CssClass="text-danger pr-1 pl-1" Font-Size="14px" ToolTip="Hold Job" OnClick="lnkStartJob_Click"><i class="fa fa-pause-circle"></i></asp:LinkButton>

                                                            
                                                            <%-- <asp:HyperLink ID="lnkView" runat="server" Target="_blank" NavigateUrl="~/F_38_AI/Projects"  CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:HyperLink>
                                                <asp:LinkButton ID="btnRemove" runat="server" CssClass="text-danger pr-2" ToolTip="delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary" ToolTip="edit"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <%--<FooterStyle CssClass="grvFooter" />--%>

                                                <PagerStyle CssClass="gvPagination" />

                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <p>Do Next week</p>
                                    <div class="card" style="height: 100%; background-color: #F6F6F6;">
                                    </div>
                                </div>
                                  
                            </div>

                        </asp:View>
                        <asp:View runat="server" ID="CalendarView"></asp:View>
                        <asp:View runat="server" ID="FilesView"></asp:View>
                    </asp:MultiView>


                    <div class="clearfix"></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
