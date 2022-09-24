<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="RealERPWEB.F_38_AI.AddProject" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .chzn-select {
            width: 100%;
        }

        .chzn-container {
            width: 100% !important;
        }

        .gview tr td {
            border: 0;
        }

        .gview .form-control {
            height: 25px;
            line-height: 25px;
            padding: 0 12px;
            border-style: solid !important;
            border-color: #c6c9d5 !important;
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



        function checkEmptyNote() {
            OpenAddBatch();
            closeAddBatch();

        }

        function OpenAddBatch() {
            $('#CreateModalBatch').modal('toggle');
        }
        function closeAddBatch() {
            $('#CreateModalBatch').modal('hide');
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
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-4">
                            <div>
                                <h6>Project Entry
                                            <asp:LinkButton ID="tblAddCustomerModal" runat="server" OnClick="tblAddCustomerModal_Click" CssClass="btn btn-primary ml-auto btn-sm mt20 mr-1 float-right"><i class="fa fa-plus"></i>Add Project</asp:LinkButton>

                                </h6>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <h6>Project List</h6>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4 divEntryform d-none" id="none" runat="server">
                            <div class="table-responsive">

                                <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False" CssClass="table-bordered gview"
                                    ShowFooter="False" ShowHeader="false" AllowPaging="false" Visible="True" Width="100%">

                                    <Columns>

                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAdd" Visible="false" runat="server" CssClass="text-primary pr-2 pl-2"><i class="fa fa-plus"></i></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtgvVal" runat="server"
                                                    CssClass="form-control" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")) %>'>
                                                </asp:TextBox>
                                                <asp:TextBox ID="txtgvdVal" runat="server" AutoCompleteType="Disabled"
                                                    CssClass="form-control" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")) %>'>
                                                </asp:TextBox>

                                                <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal" PopupPosition="TopLeft" PopupButtonID="txtgvdVal"></cc1:CalendarExtender>


                                                <asp:DropDownList ID="ddlval" runat="server" Visible="false"
                                                    CssClass="chzn-select form-control" TabIndex="2">
                                                </asp:DropDownList>

                                                <asp:Label ID="lgvgdatat" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")) %>' Width="250px" ForeColor="Black" Font-Size="12px"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle Width="250" />
                                            <ItemStyle Width="250" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <asp:LinkButton ID="btnProjectSave" runat="server" OnClick="btnProjectSave_Click" CssClass="btn btn-primary btn-sm  float-right">Project Save</asp:LinkButton>
                            </div>
                        </div>
                        <div class="divGrid" id="gridcol" runat="server">
                            <div class="table-responsive">
                                <asp:Label runat="server" ID="tblproj" Visible="false"></asp:Label>
                                <asp:GridView ID="GridcusDetails" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" OnPageIndexChanging="GridcusDetails_PageIndexChanging">
                                   
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
                                        <asp:TemplateField HeaderText="ProjectName" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpactcode" runat="server" Text='<%#Eval("pactcode").ToString()%>' Width="10px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ProjectName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                    Text='<%#Eval("projectName").ToString()%>'
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ProjectType">
                                            <ItemTemplate>
                                                <asp:Label ID="tblproj" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typedesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DataSet">
                                            <ItemTemplate>
                                                <asp:Label ID="tbldataset" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dataset")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WorkType">
                                            <ItemTemplate>
                                                <asp:Label ID="tblwrktype" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "worktype")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create date">
                                            <ItemTemplate>
                                                <asp:Label ID="tblcreatedate" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="tbladdress" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "quantity")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hour" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="tblcountry" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalhour")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" runat="server" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnRemove" runat="server" CssClass="text-danger pr-2" ToolTip="delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary" ToolTip="edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AddBatch">
                                            <ItemTemplate>
                                                <%--//<asp:LinkButton ID="lnkVieww" runat="server" OnClick="tblAddBatch_Click" CssClass="text-primary pr-2 pl-2" ToolTip="view"><i class="fa fa-eye"></i></asp:LinkButton>--%>

                                                <asp:LinkButton runat="server" ID="tblAddBatch" OnClick="tblAddBatch_Click" ToolTip="batchadd" CssClass="btn  btn-sm pr-2 pl-2"><i class="fas fa-plus"></i></asp:LinkButton>
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
                    <%--// Batch Add modal--%>
                    <div id="CreateModalBatch" class="modal " role="dialog" data-keyboard="false" data-backdrop="static">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-light">
                                    <h6 class="modal-title">Batch Add</h6>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">

                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server">Project Name</asp:Label>
                                                <asp:TextBox ID="txtproj" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="tblpactcode" runat="server" Visible="false" Text='<%#Eval("pactcode").ToString()%>'></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server">Batch Name</asp:Label>
                                                <asp:TextBox ID="txtBatch" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" runat="server">Start Date</asp:Label>
                                                <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtstartdate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label4" runat="server">Delivery Date</asp:Label>
                                                <asp:TextBox ID="textdelevery" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="textdelevery"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row center">

                                        <asp:LinkButton runat="server" ID="tblAddBatch" OnClick="tblAddBatch_Click1" OnClientClick="closeAddBatch()" CssClass="btn btn-primary   btn-sm pr-2 pl-2">Save</asp:LinkButton>

                                    </div>

                                    <hr />
                                    <div class="row">
                                        <asp:GridView ID="gv_BatchList" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" Width="100%">
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
                                                <asp:TemplateField HeaderText="ProjectName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projname")) %>'
                                                            Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Batch Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbatchid" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchid")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Start Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstartdate" runat="server" Height="16px"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Delivery Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldeliverydate" runat="server" Height="16px"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deliverydate")).ToString("dd-MMM-yyyy") %>' Width="150px"
                                                           ></asp:Label>
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
                                <div class="modal-footer">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
