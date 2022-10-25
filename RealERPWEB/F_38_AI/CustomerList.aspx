<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="RealERPWEB.F_38_AI.CustomerList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

        function checkEmptyNote() {
            OpenCustomerView();           

        }
        function OpenCustomerView() {
            $('#CustomarModalView').modal('toggle');
        }

    </script>
    <style>
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

            <div class="section">
                <div class="card mt-5">

                    <div class="card-header">
                        <div class="row">                          
                                
                        <div class="col-lg-1 col-md-2 col-sm-6 mt-4">
                                   <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                           </div>
                             <div class="col-lg-10 col-md-10 col-sm-6 mt-4">
                                <asp:LinkButton ID="tblAddCustomerModal" runat="server" OnClick="tblAddCustomerModal_Click" CssClass="btn btn-primary ml-auto btn-sm mt20 mr-1 float-right"><i class="fa fa-plus"></i>Add Customer</asp:LinkButton>
                         </div>
                            </div>
                        </div>
                
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4 divEntryform d-none" id="none" runat="server">

                                <div class="table-responsive">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <h6>Customer Entry</h6>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:LinkButton runat="server" type="button" ID="removefield" OnClick="removefield_Click" class="close" data-dismiss="modal">&times;</asp:LinkButton>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass="table-bordered gview"
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
                                    <asp:LinkButton ID="btnCustomerSave" runat="server" OnClick="btnCustomerSave_Click" CssClass="btn btn-primary btn-sm  float-right">Save</asp:LinkButton>
                                </div>
                            </div>
                            <div class="divGrid" id="gridcol" runat="server">
                                <h6>Customer List</h6>
                                <div class="table-responsive">
                                     <asp:Label runat="server" ID="lblinfocode" Visible="false"></asp:Label>
                                    <asp:GridView ID="GridcusDetails" runat="server" AutoGenerateColumns="False" Width="100%" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridcusDetails_PageIndexChanging">
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
                                            <asp:TemplateField HeaderText="Customer code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinfcode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' Width="200px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="200px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Phone Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblpnoe" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="tbladdress" runat="server" Width="120px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "address1")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Country">
                                                <ItemTemplate>
                                                    <asp:Label ID="tblcountry" runat="server" Width="120px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "country")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" CssClass="text-primary pr-2 pl-2"><i class="fa fa-eye"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btnRemove" runat="server"  OnClientClick="return confirm('Are You Sure?')" OnClick="btnRemove_Click" CssClass="text-danger pr-2"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CssClass="text-primary"><i class="fa fa-edit"></i></asp:LinkButton>
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
                        </div>


                        <%-- customer view  --%>

                         <div id="CustomarModalView" class="modal " role="dialog" data-keyboard="false" data-backdrop="static">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header bg-light">
                                    <h6 class="modal-title">Customer Details</h6>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body">                              
                                     
                                      <p><strong>Customer Name :</strong> <span id="txtcustname" runat="server"></span></p>
                                    <p><strong>Phone :</strong> <span id="custphn" runat="server"></span></p>
                                    <p><strong>Address :</strong> <span id="custAddress" runat="server"></span></p>
                                    <p><strong>Country :</strong> <span id="custCountry" runat="server"></span></p>
                                                                          
                                   
                                </div>
                                <div class="modal-footer">

                                </div>
                            </div>
                        </div>
                    </div>



                    </div>
                </div>
            </div>

            </div>        

            </div>
             </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
