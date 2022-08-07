<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="VehicleInfoEntry.aspx.cs" Inherits="RealERPWEB.F_36_Vehcl.VehicleInfoEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .chzn-drop {
            width: 100% !important;
        }
        .chzn-container{
            width: 100% !important;
        }
        .chzn-search input{
            width: 100% !important;
        }
        .tblborder {
            border: none;
        }
        table{
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
            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="mt-2 card">
                <div class="row">
                    <div class="col-md-4 mt-2">
                        <asp:GridView ID="gvVehicleEntry" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Code" ControlStyle-CssClass="classhidden" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCodeper" ClientIDMode="Static" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgval" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc1" runat="server" Width="170px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgph" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvVal" ClientIDMode="Static" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" AutoPostBack="true"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" TextMode="Time"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>


                                        <asp:Panel ID="Panegrd" runat="server">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlval" runat="server" Width="300px" CssClass="custom-select chzn-select">
                                                </asp:DropDownList>
                                            </div>
                                        </asp:Panel>
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

                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-primary mx-2 my-2" OnClick="lnkSave_Click" Width="100px"
                            OnClientClick="return confirm('Are You Sure?')"><span class="fa fa-save " style="color:white;" aria-hidden="true"  ></span>Save</asp:LinkButton>


                    </div>
                    <div class="col-md-8 table-responsive">
                        <asp:GridView ID="gvVehicleInfo" runat="server" AutoGenerateColumns="False" CssClass=" table table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
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
                                            OnClick="LnkbtnDelete_Click" ToolTip="Delete Item" Width="25px">
                                                <span class="fa fa-sm fa-trash " style="color:red;" aria-hidden="true"  ></span>&nbsp;
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="LnkbtnEdit" OnClick="LnkbtnEdit_Click"
                                            ToolTip="Edit Item">
                                                <span class="fas fa-edit fa-sm" style="color:blue;" aria-hidden="true" Width="25px" ></span>&nbsp;
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vehicle Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvehicleId" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vehicleId")) %>'
                                            Width="150px"></asp:Label>
                                       
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vehicle Name">

                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vName")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Model">

                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vModel")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Registration">

                                    <ItemTemplate>
                                        <asp:Label ID="lblReg" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vReg")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Start Time">

                                    <ItemTemplate>
                                        <asp:Label ID="lblStime" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "VSTime")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Left" />

                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="End Time">

                                    <ItemTemplate>
                                        <asp:Label ID="lblEtime" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "VETime")) %>'
                                            Width="80px"></asp:Label>
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
