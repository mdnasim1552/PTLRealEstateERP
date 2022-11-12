<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AdvancedSearchFilter.aspx.cs" Inherits="RealERPWEB.F_21_MKT.AdvancedSearchFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">


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

    <style type="text/css">
        .table th, .table td, card-header {
            padding: 4px;
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

            <div class="card mt-4 pb-4">
                <div class="card-body">
                    <div class="row ml-2">

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <asp:Label ID="lblem" runat="server" CssClass="form-label">Associate Name</asp:Label>
                            <asp:DropDownList ID="ddlEmpid" data-placeholder="Choose Employee.." runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label">Search Type</asp:Label>
                            <asp:DropDownList ID="ddlOther" runat="server" ClientIDMode="Static" CssClass="custom-select chzn-select">
                                <asp:ListItem Value="1">Prospect Name</asp:ListItem>
                                <asp:ListItem Value="2">PID</asp:ListItem>
                                <asp:ListItem Value="3">Phone</asp:ListItem>
                                <asp:ListItem Value="4">Email</asp:ListItem>
                                <asp:ListItem Value="5">NID</asp:ListItem>
                                <asp:ListItem Value="6">TIN</asp:ListItem>
                                <%--  <asp:ListItem Value="7">Prefered Area</asp:ListItem>
                                <asp:ListItem Value="8">Profission</asp:ListItem>--%>
                                <asp:ListItem Selected="True" Value="9">Choose Filter Key.........................</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server" CssClass="form-label">Search </asp:Label>
                            <asp:TextBox ID="txtVal" runat="server" CssClass="form-control" TextMode="Search" autocomplete="off"></asp:TextBox>

                        </div>

                        <div class="col-md-1" style="margin-top: 22px">

                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-success" OnClick="lnkbtnOk_Click" AutoPostBack="True">Show</asp:LinkButton>

                        </div>

                    </div>

                </div>
            </div>

            <div class="card" style="background-color: whitesmoke; align-content: center">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-4">
                            <div class="card">
                                <div class="card-header bg-light"><span class="font-weight-bold text-muted">Employee Information</span></div>
                                <div class="card-body" runat="server" id="engst">
                                    <img src="~/../../../Upload/UserImages/3365001.png" style="display: block; margin-left: auto; margin-right: auto; width: 30%;" alt="User Image">
                                    <table class="table table-striped table-hober tblEMPinfo mt-2">
                                        <%--                <thead>
                                        <tr>
                                            <th></th>
                                            <th></th>

                                        </tr>
                                    </thead>--%>
                                        <tbody class="">
                                            <tr>
                                                <td class="font-weight-bold">PID</td>
                                                <td>
                                                    <asp:Label ID="lblname" runat="server">N/A</asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="font-weight-bold ">Contact Person</td>
                                                <td>
                                                    <asp:Label ID="lblconper" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="font-weight-bold">Primary Mobile</td>
                                                <td>
                                                    <asp:Label ID="lblmbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td class="font-weight-bold">Home Address</td>
                                                <td>
                                                    <asp:Label ID="lblhomead" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="font-weight-bold">Profession</td>
                                                <td>
                                                    <asp:Label ID="lblprof" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="font-weight-bold">Status</td>
                                                <td>
                                                    <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>



                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="card mt-3 mb-3">
                                <div class="card-header bg-light"><span class="font-weight-bold text-muted">Follow Up Summary</span></div>
                                <div class="card-body">
                                    <asp:Repeater ID="rpclientinfo" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <div class="col-md-12  col-lg-12 ">
                                                <div class="well">

                                                    <div class="col-sm-12 panel pt-3 b-3">

                                                        <div class=" col-sm-12">

                                                            <p>
                                                                <strong><%# DataBinder.Eval(Container, "DataItem.prosdesc")%></strong> <%# DataBinder.Eval(Container, "DataItem.kpigrpdesc").ToString() %>  on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %><br>




                                                                <strong>Participants:</strong> <%# DataBinder.Eval(Container, "DataItem.partcilist").ToString() %><br>


                                                                <strong>Summary:</strong><span class="textwrap"><%# DataBinder.Eval(Container, "DataItem.discus").ToString() %></span><br>



                                                                <strong>Next Action:</strong> <%# DataBinder.Eval(Container, "DataItem.nfollowup").ToString() %> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy hh:mm tt")%><br>
                                                                <strong>Comments:</strong> <%# DataBinder.Eval(Container, "DataItem.disgnote").ToString() %>





                                                                <br>
                                                            </p>



                                                        </div>
                                                        <div class="row mb-5">
                                                            <div class="col-md-12">

                                                                <a href="#" class="btn btn-sm btn-primary mt-2">Re-schdule</a>
                                                                <a href="#" class="btn btn-sm btn-success mt-2">Delete</a>
                                                                <a href="#" class="btn btn-sm btn-success mt-2">FollowUp</a>
                                                                <a href="#" class="btn btn-sm btn-success mt-2">Addition</a>


                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>



                                            </div>

                                        </ItemTemplate>

                                    </asp:Repeater>
                                </div>


                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
