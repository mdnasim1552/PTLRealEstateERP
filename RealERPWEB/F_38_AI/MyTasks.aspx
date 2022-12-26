<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MyTasks.aspx.cs" Inherits="RealERPWEB.F_38_AI.MyTasks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .customeGV tr td, .customeGV tr th {
            font-size: 11px !important;
        }
    </style>

    <script>
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        }
        function HoldtaskNoteModal() {
            $('#myModal').modal('toggle');
        }
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
            <div class="card mt-4">
                <div class="card-header">
                    <div class="row">
                        <div class=" col-md-2">
                            <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                <asp:RadioButtonList ID="btnMyTasks" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="btnMyTasks_SelectedIndexChanged">

                                    <asp:ListItem Value="2" Selected="True"><a class="nav-link" href="#">Board</a></asp:ListItem>

                                    <asp:ListItem Value="3"> <a class="nav-link" href="#">Files</a></asp:ListItem>

                                </asp:RadioButtonList>
                            </div>

                        </div>
                        <div class="col-md-10" runat="server" id="mgtenplist" visible="false">
                            <asp:Label ID="Label3" CssClass="col-lg-1 col-form-label" runat="server">Employee List</asp:Label>
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <asp:DropDownList ID="ddemplist" runat="server" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddemplist_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
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
                                    <h5>Recently assigned</h5>

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
                                                           <%-- <asp:Label ID="lblordertype" runat="server" Height="16px"
                                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype"))%>'
                                                                ForeColor="Black"></asp:Label>--%>
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


                                                    <asp:TemplateField HeaderText="Role Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tblvelocitytype" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="Assigned <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvelocityqty" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Hour">
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
                                    <%--            <p>Ongoing Job</p>
                                    <div class="card" style="height: 100px;">
                                    </div>
                                    <p>Hold Job</p>
                                    <div class="card" style="height: 100px;">
                                    </div>--%>
                                    <h5>Completed Job</h5>
                                    <div class="card" style="height: 300px; background-color: #F6F6F6;">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gv_Completejob" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea customeGV"
                                                ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Job ID ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: center;"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid1")) %>' Width="40px"
                                                                ForeColor="Black"></asp:Label>
                                                            <asp:Label ID="lblgvtimetaskid" runat="server" Text="0" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblgvempid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser")) %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblgvjobid1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid")) %>' Visible="false" ></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tblgvbatchname" runat="server" Font-Bold="true" Width="150px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="Lblgvtasktitle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Complate <br> date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tblgvcreatedate" runat="server" Width="80px"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "worktime")).ToString("dd-MMM-yyyy hh:mm tt")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "worktime")).ToString("dd-MMM-yyyy hh:mm tt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="Role <br> Type">
                                                        <ItemTemplate>

                                                            <asp:Label ID="tblgvvelocitytype" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletypedesc")) %>'></asp:Label>
                                                            <asp:Label ID="lblcgvroletypecode" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Done <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdoneqty" runat="server" Width="30px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvjobstatus" runat="server" Width="75px" CssClass="badge badge-pill badge-success"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />

                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>

                                    </div>
                                    <div class="card" style="height: 100px;">
                                    </div>

                                </div>
                                <div class="col-md-5">
                                    <h5>Working Progress</h5>
                                    <div class="card" style="height: 100%; background-color: #F6F6F6;">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvTodayList" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea customeGV"
                                                ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnRowDataBound="gvTodayList_RowDataBound">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Job ID ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: center;"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid1")) %>' Width="40px"
                                                                ForeColor="Black"></asp:Label>
                                                            <asp:Label ID="lbltimetaskid" runat="server" Text="0" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblempid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser")) %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbljobid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid")) %>' Visible="false"></asp:Label>
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
                                                        <ItemStyle VerticalAlign="Middle" />

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Role <br> Type">
                                                        <ItemTemplate>

                                                            <asp:Label ID="tblvelocitytype" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletypedesc")) %>'></asp:Label>
                                                            <asp:Label ID="lblgvroletypecode" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start <br> date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="tblcreatedate" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "worktime")).ToString("dd-MMM-yyyy hh:mm tt")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "worktime")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assigned <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvelocityqty" runat="server" Width="50px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Done <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldoneqty" runat="server" Width="30px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rmarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvtdremarks" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pending Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpendingqty" runat="server" CssClass="badge badge-pill badge-danger"
                                                                Visible='<%#  
                                                                     Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pendingqty")) > 0
                                                                     ? true:false %>'
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pendingqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobstatus" runat="server" Width="75px"
                                                                CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99220") ? "badge badge-pill badge-success"://done
                                                                    (Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99215") ? "badge badge-pill badge-warning"://hold
                                                                    (Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99217") ? "badge badge-pill badge-primary"://start
                                                                    (Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99204") ? "badge badge-pill badge-info":"badge badge-pill badge-info"//in progress
                                                                    %>'
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkHoldJob" runat="server" CssClass="text-danger pr-1 pl-1" Font-Size="20px" ToolTip="Hold Create Note"
                                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99215" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99220" ? false:true %>' OnClick="HoldCreateNote_Click"><i class="fa fa-pause-circle"></i></asp:LinkButton>



                                                            <asp:LinkButton ID="lnkStartJobByID" runat="server" CssClass="text-success pr-1 pl-1" Font-Size="20px" ToolTip="Start Job"
                                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99204" ||
                                                                     Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99217" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99220"
                                                                     ? false:true %>'
                                                                OnClick="lnkStartJobByID_Click"><i class="fa fa-toggle-off"></i></asp:LinkButton>


                                                            <asp:LinkButton ID="lnkJObDone" runat="server" CssClass="text-green pr-1 pl-1" Font-Size="20px" ToolTip="Hold Job" OnClick="lnkJObDone_Click" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99220" ? false:true %>'><i class="fa fa-check-square"></i></asp:LinkButton>

                                                           <asp:LinkButton runat="server" ID="tblworkedit" Visible="false" OnClick="tblworkedit_Click" CssClass="text-success  btn-sm btn" ToolTip="Edit Job" ><i class="fa fa-edit"></i></asp:LinkButton>


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
                                    <h5>Activities</h5>

                                    <div class="card" style="height: 100%; background-color: #F6F6F6;">
                                        <asp:GridView ID="gvActivities" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea customeGV"
                                            ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15">

                                            <Columns>
                                                <asp:TemplateField HeaderText="Job ID " Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: center;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>' Width="40px"
                                                            ForeColor="Black"></asp:Label>

                                                        <asp:Label ID="lbltimetaskid" runat="server" Text="0" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblempid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser")) %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbljobid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid")) %>' Visible="false"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Job Name">
                                                    <ItemTemplate>

                                                        <asp:Label ID="Lbltasktitle" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" />

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Role <br> Type" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tblvelocitytype" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assigntype")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Start <br> date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="tblcreatedate" runat="server" Width="80px"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "worktime")).ToString("dd-MMM-yyyy hh:mm tt")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "worktime")).ToString("dd-MMM-yyyy hh:mm tt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assigned <br> QTY" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvelocityqty" runat="server" Width="50px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" HorizontalAlign="center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Done <br> QTY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldoneqty" runat="server" Width="30px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##;(#,##); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px" HorizontalAlign="center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rmarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvtdremarks" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbljobstatus" runat="server" Width="75px"
                                                            CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99220") ? "badge badge-pill badge-success"://done
                                                                    (Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99215") ? "badge badge-pill badge-warning"://hold
                                                                    (Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99217") ? "badge badge-pill badge-primary"://start
                                                                    (Convert.ToString(DataBinder.Eval(Container.DataItem, "trackertype"))=="99204") ? "badge badge-pill badge-info":"badge badge-pill badge-info"//in progress
                                                                    %>'
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>



                                                <%--            <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobstatus" runat="server" CssClass="badge badge-pill badge-info"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>
                                            </Columns>
                                            <%--<FooterStyle CssClass="grvFooter" />--%>

                                            <PagerStyle CssClass="gvPagination" />

                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
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

            <div class="modal" id="myModal" data-backdrop="false" tabindex="-1">
                <div class="modal-dialog modal-md">
                    <div class="modal-content" style="background: #f3f7f9">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title text-primary">
                                <asp:Label runat="server" ID="lbltaskmodal"></asp:Label></h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <asp:Label ID="taskdec" runat="server"></asp:Label>
                        <!-- Modal body -->
                        <div class="modal-body">

                            <asp:Label ID="Mdl_lblempid" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Mdl_jobid" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="notetaskid" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="donestatus" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="holdstatus" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="startstatus" runat="server" Visible="false"></asp:Label>


                            <div class="row" id="divDoneQty" runat="server">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="form-group">
                                        <label for="username">Done Qty</label>
                                        <asp:TextBox ID="txtDoneQty" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divSkiQty" runat="server">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="form-group">
                                        <label for="username">Skip Qty</label>
                                        <asp:TextBox ID="txtSkippqty" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divRetQty" runat="server">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="form-group">
                                        <label for="username">Return Qty</label>
                                        <asp:TextBox ID="txtreturnqty" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divRejQty" runat="server">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="form-group">
                                        <label for="username">Reject Qty</label>
                                        <asp:TextBox ID="textrejectqty" runat="server" TextMode="Number" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="form-group">
                                        <label for="username">Note Description </label>
                                        <asp:TextBox ID="noteDescription" runat="server" class="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="holdreason" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="form-group">
                                        <label id="lblreason" runat="server">Hold Reason</label>
                                        <asp:DropDownList ID="ddlholdreason" runat="server" CssClass="form-control chzn-select">
                                            <asp:ListItem Value="89001"> Pray Break</asp:ListItem>
                                            <asp:ListItem Value="89002">Lunch Break</asp:ListItem>
                                            <asp:ListItem Value="89003">Snacks Break</asp:ListItem>
                                            <asp:ListItem Value="89004">Meeting Break</asp:ListItem>
                                            <asp:ListItem Value="89005">Tea Break</asp:ListItem>
                                            <asp:ListItem Value="89006">Discussion Break</asp:ListItem>
                                            <asp:ListItem Value="89007">Gossip Break</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <button type="button" runat="server" id="SaveNote" data-dismiss="modal" aria-hidden="true" onserverclick="SaveNote_ServerClick" class="btn btn-primary btn-sm">Save Note</button>
                            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Close</button>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
