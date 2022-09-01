<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="VehicleApply.aspx.cs" Inherits="RealERPWEB.F_36_Vehcl.VehicleApply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
         body {
            font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
            font-size: 12px;
        }
        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-search input {
            width: 100% !important;
        }

        .tblborder {
            border: none;
        }

        table {
            width: 100% !important;
        }

        .tblborder td {
            border: none;
        }

        .visibleshow .grvHeader, .visibleshow .grvFooter {
            display: none;
        }

        .card {
            height: 600px;
        }
        .form-group{
            margin-bottom:0.2rem;
        }
    </style>
    <script type="text/javascript">   
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true }); var today = new Date().toISOString().slice(0, 16);
            document.getElementsByClassName("datetimemin")[0].min = today;
            document.getElementsByClassName("datetimemin")[1].min = today;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="mt-5 card">
                <div class="row">
                    <div class="col-md-3 mt-2">
                        <asp:Label runat="server" ID="lblTransportId" Visible="false"></asp:Label>

                        <div class="form-group">
                            <asp:Label runat="server" ID="lblApplicantName">Applicant Name</asp:Label>
                            <asp:DropDownList ID="ddlApplicantName" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlApplicantName_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="Label1">Designation</asp:Label>
                            <asp:Label ID="lblDesignation" CssClass="form-control" runat="server"></asp:Label>
                        </div>
                        <div class="form-group">
                             <asp:Label runat="server" ID="Label2">Department</asp:Label>
                            <asp:Label ID="lblDept" CssClass="form-control" runat="server"></asp:Label>
                        </div>
                         <div class="form-group">
                             <asp:Label runat="server" ID="Label4">Preferred Vehicle</asp:Label>
                              <asp:DropDownList ID="ddlPrefVehicle" CssClass="chzn-select form-control" runat="server" AutoPostBack="True"
                               ></asp:DropDownList>
                        </div>
                         <div class="form-group">
                            <asp:Label runat="server" ID="Label3">Destination:</asp:Label>
                            <asp:TextBox ID="txtDestination" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                         <div class="form-group">
                             <asp:Label runat="server" ID="Label6">From DateTime:</asp:Label>
                            <asp:TextBox ID="txtFromDatetime" runat="server" CssClass="form-control datetimemin" TextMode="DateTimeLocal"></asp:TextBox>
                        </div>
                        <div class="form-group">
                             <asp:Label runat="server" ID="Label7">To DateTime:</asp:Label>
                            <asp:TextBox ID="txtToDatetime" runat="server" CssClass="form-control datetimemin" TextMode="DateTimeLocal"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="Label8">Purpose:</asp:Label>
                            <asp:TextBox ID="txtPurpose" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" OnClick="lnkSave_Click" Width="100px"
                            OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-save " style="color:white;" aria-hidden="true"  ></span>&nbsp; Save</asp:LinkButton>

                        <asp:LinkButton ID="lnkClear" runat="server" CssClass="btn btn-sm btn-warning mx-2 my-2" OnClick="lnkClear_Click" Width="100px"
                            OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-redo" style="color:white;" aria-hidden="true"  ></span> &nbsp; Clear</asp:LinkButton>

                    </div>
                    <div class="col-md-9 table-responsive">
                        <asp:GridView ID="gvTransportInf" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="LnkbtnDelete"
                                            ToolTip="Delete Item" Width="10px">
                                                <span class="fa fa-sm fa-trash " style="color:red;" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LnkbtnEdit" runat="server" CssClass="btn btn-default btn-xs" ToolTip="Edit Item" 
                                            OnClick="LnkbtnEdit_Click"><span class="fas fa-edit fa-sm" style="color:blue;" aria-hidden="true" Width="25px" ></span></asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transport Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvehicleId" runat="server" Visible="false"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trpid")) %>'
                                            Width="150px"></asp:Label>
                                        <asp:Label ID="lblvehicleId1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trpid1")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Applicant Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVehicleGrp" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "aplname")) + "</B>"+ "<br>"+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")).Trim()+ "<br>"+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim() %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Preferred">

                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vehtypedesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Destination">

                                    <ItemTemplate>
                                        <asp:Label ID="lblDestination" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "destination")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date and Time">

                                    <ItemTemplate>
                                        <asp:Label ID="lbldatetime" runat="server"
                                            Text='<%# "<b>FROM:</b>&nbsp;&nbsp;"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "fdate"))+"<br>"+
                                                "<b>TO:</b>&nbsp;&nbsp;" +Convert.ToString(DataBinder.Eval(Container.DataItem, "tdate")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purpose">

                                    <ItemTemplate>
                                        <asp:Label ID="lblpurpose" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apppurpose")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                            </Columns>


                            <FooterStyle CssClass="gvPagination" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
