<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="RealERPWEB.F_38_AI.Projects" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

            <div class="card mt-2">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-6">
                            <h5><i class=" text-primary fa fa-list"></i>&nbsp;<asp:Label runat="server" ID="lbltitleprjectname"></asp:Label>
                                &nbsp;  / &nbsp;<asp:Label runat="server" ID="lblbatchid"></asp:Label>
                                &nbsp;</h5>

                            <asp:HiddenField ID="hiddnbatchID" runat="server" />
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
                                    <asp:ListItem Value="1" Selected="True"><a class="nav-link active" href="#">OverView</a></asp:ListItem>
                                    <asp:ListItem Value="2"><a class="nav-link" href="#">Board</a></asp:ListItem>
                                    <%-- <asp:ListItem  Value="3"><a class="nav-link" href="#">Timeline</a></asp:ListItem>
                                    <asp:ListItem  Value="4"> <a class="nav-link" href="#">Calendar</a></asp:ListItem>
                                    <asp:ListItem  Value="5"> <a class="nav-link" href="#">Workflow</a></asp:ListItem>--%>
                                    <asp:ListItem Value="6"> <a class="nav-link" href="#">Dashboard</a></asp:ListItem>
                                    <asp:ListItem Value="7"> <a class="nav-link" href="#">Files</a></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View runat="server" ID="OverView">
                            <div class="row">
                                <div class="col-md-3" id="prjinfo" runat="server">
                                    <div class="card">
                                        <div class="card-header bg-light text-center p-1">
                                            <span class="font-weight-bold text-muted">Project Information</span>
                                        </div>
                                        <div class="card-body">
                                           <%-- <img src="~/../../../Images/noimageavl.png" style="display: block; margin-left: auto; margin-right: auto; width: 50%;" alt="User Image">--%>

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
                                <div class="col-md-9" id="gvbatch" runat="server">
                                    <div class="card">
                                        <div class="card-header bg-light p-1">
                                            <span class="font-weight-bold text-muted">Batch Information</span>
                                            <span class="float-right">
                                                <asp:LinkButton runat="server" ID="btntaskadd" OnClick="btntaskadd_Click" CssClass="btn btn-primary btn-sm text-white"><i class=" fa fa-plus"></i>&nbsp;New Task</asp:LinkButton>
                                            </span>
                                        </div>
                                        <div class="card-body mb-2">

                                            <asp:GridView ID="gv_BatchName" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="False" AllowPaging="false" Visible="True" Width="100%">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" CssClass="font-weight-bold" ID="lblprjName" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Work Type">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblprjwork" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktype")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Data Set">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblprjdatatype" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "datasettype")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Data Qty">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblprjdatasetqty" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "datasetqty")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Hour">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblprjtotalhour" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalhour")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Processing Time" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblprjpwrkperhour" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pwrkperhour")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Emp. Capacity">
                                                        <ItemTemplate>
                                                            <%--comcod,batchid,prjid,startdate , deliverydate,worktype,datasetqty,datasettype, totalhour,pwrkperhour,empcapacity--%>

                                                            <asp:Label runat="server" ID="lblprjempcapacity" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "empcapacity")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblprjstartdate" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delevery Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblprjdeliverydate" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deliverydate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>

                                        </div>


                                    </div>

                                    <div class="row" runat="server" id="taskoverview" >
                                        <div class="col-md-8">
                                            <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                                <asp:RadioButtonList ID="btnbatchtask" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="btnbatchtask_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Selected="True"><a class="nav-link" href="#">Assigned Task</a></asp:ListItem>
                                                    <asp:ListItem Value="2"> <a class="nav-link" href="#">Pendding Task</a></asp:ListItem>
                                                    <asp:ListItem Value="3"> <a class="nav-link" href="#">Retrun Task</a></asp:ListItem>
                                                    <asp:ListItem Value="4"> <a class="nav-link" href="#">Reject Task</a></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                        </div>
                                    </div>
                                    <div class="card" id="penddingtask" runat="server" visible="false">
                                        <div class="card-header bg-light p-1">
                                            <span class="font-weight-bold text-muted">Pendding Assign</span>
                                        </div>
                                        <div class="card-body">
                                            <asp:Label runat="server" ID="lblpenddingid" Visible="false"></asp:Label>
                                            <asp:GridView ID="gv_PenddingAssign" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL # ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right; font-size: 12px;"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Tasktitle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvAsignid" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'></asp:Label>
                                                            <asp:Label ID="lblgvtaskid" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskid")) %>'></asp:Label>

                                                            <asp:Label ID="lblgvptasktitle" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>' Width="200px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Role <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvproletypedesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletypedesc")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                            <asp:Label ID="lblgvproletype" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Annotor <br> ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpannoid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assign <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpassigntype" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assigntype")) %>'></asp:Label>
                                                            <asp:Label ID="lblgvpassigndesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assigndesc")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                           

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assign <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpassignqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0;(#,##0); ")  %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complete">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpdoneqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ")  %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skip <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpskipqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skipqty")).ToString("#,##0;(#,##0); ")  %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Return <br> QTY" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpreturnqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "returnqty")).ToString("#,##0;(#,##0); ") %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="tbnAssignuser" runat="server" Width="80px" OnClick="tbnAssignuser_Click"
                                                                Visible='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")) > 0 ? true:false %>'
                                                                CssClass="text-primary pr-2"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="card" id="assigntask" runat="server" visible="false">
                                        <div class="card-header bg-light p-1">
                                            <span class="font-weight-bold text-muted">Assigned Tasks</span>
                                        </div>
                                        <div class="form-group ">

                                            <asp:Label runat="server" ID="lbltaskbatchid" Visible="false"></asp:Label>
                                            <asp:GridView ID="gv_BatchInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL # ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right; font-size: 12px;"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvjobid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid")) %>' Width="150px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task Title">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvtasktitle" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>' Width="120px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>' Width="200px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblgvempname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' Width="180px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Batch Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbatchname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchname")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Role <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrolettpcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletypcode")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblgvroletype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Annotor <br> ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvannoid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assigned  <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvassigntype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assigntype")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Role <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvassignqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "assignqty")).ToString("#,##0;(#,##0); ") %>' Width="40px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Work <br> Hour">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvwrkhour" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "workhour")).ToString("#,##0;(#,##0); ") %>' Width="40px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Work <br> Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvworkrate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "workrate")).ToString("#,##0;(#,##0); ") %>' Width="40px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>

                                                            <asp:Label ID="tblStatus" runat="server" class='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "workstatus"))=="99220") ? "badge badge-pill badge-success":"badge badge-pill badge-info" %>' Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "workstatus"))=="99220") ? "Done":"In progress" %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Complete <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdoneqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")).ToString("#,##0;(#,##0); ") %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Skip <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvskipqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skipqty")).ToString("#,##0;(#,##0); ") %>' Width="40px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pendding <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpenddingqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "penddingqty")).ToString("#,##0;(#,##0); ") %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btntaskEdit" runat="server" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "workstatus"))=="99220" ? false:true %>' CssClass="text-primary" OnClick="btntaskEdit_Click" ToolTip="edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btntaskQC" runat="server" OnClick="btntaskQC_Click" Visible='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "doneqty")) > 0) & (Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype"))=="Annotator")  ? true:false %>' CssClass="text-primary"  ToolTip="assgin for QC"><i class="fa fa-user-plus"></i></asp:LinkButton>

                                                               <asp:LinkButton ID="removeRow" runat="server" OnClientClick="return confirm('Are You Sure?')"
                                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus"))=="00000" ? true:false %>'
                                                                OnClick="removeRow_Click" CssClass="text-danger pr-2"><i class="fa fa-trash"></i></asp:LinkButton>
                                                           <%-- <asp:LinkButton runat="server" ID="removeRow" CommandName="Delete"
                                                                ClientIDMode="Static"
                                                                ToolTip="Delete"
                                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobstatus"))=="00000" ? true:false %>'
                                                                CssClass="text-danger pr-2 isdeleteRow" CausesValidation="false"
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "jobid") %>'
                                                                OnClientClick="return sweetAlertConfirm(this);"><i class="fa fa-trash"></i></asp:LinkButton>--%>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <%--<FooterStyle CssClass="grvFooter" />--%>
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>

                                    </div>
                                     <div class="card" id="returntask" runat="server" visible="false">
                                         <div class="card-header bg-light p-1">
                                            <span class="font-weight-bold text-muted">Return Tasks</span>
                                        </div>
                                         <div class="from-group">
                                              <asp:GridView ID="gv_ReturnTask" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL # ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right; font-size: 12px;"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrejobid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid")) %>' Width="150px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task Title">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvretasktitle" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>' Width="120px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreempid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser")) %>' Width="200px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblgvreempname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' Width="180px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Role <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrerolettpcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletypecode")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblgvreroletype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Annotor <br> ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblregvannoid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annotortype")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assigned  <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreassigntype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assigntype")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Role <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreroletype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>' Width="40px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Return <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrereturnqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "returnqty")).ToString("#,##0;(#,##0); ") %>' Width="40px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>                                               
                                                  
                                                  
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                          
                                           

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <%--<FooterStyle CssClass="grvFooter" />--%>
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>


                                         </div>
                                     </div>
                                     <div class="card" id="rejecttask" runat="server" visible="false">
                                         <div class="card-header bg-light p-1">
                                            <span class="font-weight-bold text-muted">Reject Tasks</span>
                                        </div>
                                         <div class="from-group">
                                              <asp:GridView ID="gv_Rejecttask" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL # ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right; font-size: 12px;"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                                ForeColor="Black"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrejobid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid")) %>' Width="150px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task Title">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrejtasktitle" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tasktitle")) %>' Width="120px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrejempid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuser")) %>' Width="200px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblgvrejempname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' Width="180px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Role <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrejrolettpcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletypecode")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="lblgvrejroletype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Annotor <br> ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrejgvannoid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annotortype")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assigned  <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrejassigntype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assigntype")) %>' Width="50px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Role <br> Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrejroletype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>' Width="40px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Reject <br> QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrejrejectqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejectqty")).ToString("#,##0;(#,##0); ") %>' Width="40px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>                                               
                                                  
                                                  
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                          
                                           

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <%--<FooterStyle CssClass="grvFooter" />--%>
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>


                                         </div>
                                     </div>

                                    <div class=" card bg-light">
                                        <div class="col-md-12" visible="false" id="task" runat="server">
                                            
                                            <div class="form-group row">
                                                <asp:HiddenField ID="HiddinTaskid" runat="server" Value="0" />
                                                <div class="d-flex w-100" style="padding: 10px 8px 4px 0px;">
                                                    <asp:Label ID="Label11" runat="server" CssClass="float-left">Task Name &nbsp;</asp:Label>
                                                    <span class="text-danger">&nbsp;PendingAnnotator( <asp:Label runat="server" ID="lblcountannotid"></asp:Label>)</span>
                                                     <span class="text-danger">&nbsp; &nbsp;PendingQC( <asp:Label runat="server" ID="lblcountQC"></asp:Label>)</span>
                                                     <span class="text-danger">&nbsp; &nbsp; PendingQA( <asp:Label runat="server" ID="lblcountQA"></asp:Label>)</span>
                                                    <asp:LinkButton runat="server" type="button" ID="LinkButton1" OnClick="removefield_Click" class="ml-auto text-danger"><i class="fa fa-times-circle" style="font-size: 20px;"></i></asp:LinkButton>

                                                </div>

                                                <asp:TextBox ID="txttasktitle" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>


                                            <div class="form-group row d-none">
                                                <asp:Label ID="Label12" runat="server">Task Description</asp:Label>
                                                <asp:TextBox ID="txtdesc" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group row d-none">
                                                <asp:Label ID="Label13" runat="server">Remakrs</asp:Label>
                                                <asp:TextBox ID="txtremaks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-lg-4 col-md-3 col-sm-12 pl-0">
                                                    <asp:Label ID="Label23" runat="server">Assigne Team Members</asp:Label>
                                                    <asp:DropDownList ID="ddlassignmember" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-3 col-md-3 col-sm-12">
                                                    <asp:Label ID="Label16" runat="server">Role Type</asp:Label>
                                                    <asp:DropDownList ID="ddlUserRoleType" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlUserRoleType_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-lg-3 col-md-4 col-sm-12">
                                                    <asp:Label ID="Label17" runat="server">Annotation ID</asp:Label>
                                                    <asp:DropDownList ID="ddlAnnotationid" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-2 col-md-3 col-sm-12 pl-0">
                                                    <asp:Label ID="Label8" runat="server">Assigned Type</asp:Label>
                                                    <asp:DropDownList ID="ddlassigntype" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="form-group row">


                                                <div class="col-lg-3 col-md-3 col-sm-12">
                                                    <asp:Label ID="Label9" runat="server"> Assigned QYT</asp:Label>
                                                    <asp:TextBox ID="txtquantity"  min="0" runat="server" TextMode="Number"  CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class=" col-lg-3 col-md-3 col-sm-12">
                                                    <asp:Label ID="Label10" runat="server">Work Hour</asp:Label>
                                                    <asp:TextBox ID="txtworkhour" runat="server" min="0" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class=" col-lg-3 col-md-3 col-sm-12">
                                                    <asp:Label ID="Label2" runat="server">Per Rate</asp:Label>
                                                    <asp:TextBox ID="textrate" runat="server" Text="80" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class=" col-lg-2 col-md-2 col-sm-12 mt-4">
                                                    <asp:CheckBox runat="server" ID="checkinoutsourcing" AutoPostBack="True" ForeColor="red"></asp:CheckBox>

                                                    <asp:Label ID="Label1" runat="server">&nbsp;  Freelancing</asp:Label>
                                                </div>
                                                <div class=" col-lg-1 col-md-1 col-sm-12 mt-4 ">
                                                    <asp:LinkButton ID="btnaddrow" runat="server" OnClick="btnaddrow_Click" CssClass=" btn btn-primary ml-auto btn-sm mt20 mr-1 float-left"><i class="fa fa-plus"></i></asp:LinkButton>

                                                </div>
                                            </div>

                                            <div class="form-group">

                                                <asp:GridView ID="GridVirtual" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" Width="">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL # ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                    Style="text-align: right; font-size: 12px;"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                                    ForeColor="Black"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Member" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljobid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobid")) %>'></asp:Label>

                                                                <asp:Label ID="lblempid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Member">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmember" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' Width="220px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Role Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="tblroleType" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roledesc")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Annotation ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="tblAnnotation" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "annoid")) %>' ForeColor="Black" Font-Size="12px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Assign  <br> Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="tbltype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assigndesc")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Assign <br> QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="tblValoquantity" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignqty")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Work <br> Hour">
                                                            <ItemTemplate>
                                                                <asp:Label ID="tblworkhour" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "workhour")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Work <br> Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="tblworkrate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "workrate")) %>' Width="100px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnvrdelete" OnClick="btnvrdelete_Click" OnClientClick="return confirm('Are You Sure?')" CssClass="text-danger"><i class="fa fa-trash"></i></asp:LinkButton>

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <%--<FooterStyle CssClass="grvFooter" />--%>
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                </asp:GridView>
                                            </div>
                                            <asp:LinkButton runat="server" ID="btntaskSave" OnClick="btntaskSave_Click" CssClass="btn btn-primary btn-sm  float-right">Task Save</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btntaskUpdate" Visible="false" OnClick="btntaskUpdate_Click" CssClass="btn btn-primary btn-sm   float-right"> Update</asp:LinkButton>
                                           

                                        </div>

                                    </div>


                                </div>

                                <div id="pnlSidebar" class="card pnlSidebarCl" runat="server" visible="false">
                                </div>
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
                                        <h2 class="text-center" id="dontask" runat="server">3</h2>
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
                                        <h2 class="text-center" runat="server" id="pendtask">0</h2>
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
                                        <h2 class="text-center" runat="server" id="overduetasks">0</h2>
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
                                        <h2 class="text-center" runat="server" id="ttltask">3</h2>
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

    <script>
        //function sweetAlertConfirm(removeRow) {
        //    if (removeRow.dataset.confirmed) {
        //        removeRow.dataset.confirmed = false;
        //        return true;
        //    } else {
        //        // Ask the user to confirm/cancel the action
        //        event.preventDefault();
        //        swal({
        //            title: 'Are you sure?',
        //            text: "You won't be able to revert this !!",
        //            type: 'warning',
        //            showCancelButton: true,
        //            confirmButtonColor: '#3085d6',
        //            cancelButtonColor: '#d33',
        //            confirmButtonText: 'Ok'
        //        })
        //            .then(function () {
        //                // Set data-confirmed attribute to indicate that the action was confirmed
        //                removeRow.dataset.confirmed = true;
        //                // Trigger button click programmatically
        //                removeRow.click();
        //            }).catch(function (reason) {
        //                // The action was canceled by the user
        //                return false
        //            });
        //    }
        //}

        

    </script>


</asp:Content>
