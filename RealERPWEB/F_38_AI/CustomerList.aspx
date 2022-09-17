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
        function checkCustomerAdd() {
            OpenAddCustomer();
        }
        function OpenAddCustomer() {
            $('#AddCustomerModal').modal('toggle');
        }
        function closeAddCustomer() {
            $('#AddCustomerModal').modal('hide');
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

            <div class="section">
                <div class="card mt-5">
                    <div class="row">
                        <asp:LinkButton ID="tblAddCustomerModal" runat="server" CssClass="btn btn-primary ml-auto  btn-sm mt20 mr-1" OnClick="AddCustomerModal_Click"><i class="fa fa-plus"></i>Add Customer</asp:LinkButton>
                    </div>



                    <!-- The Modal -->
                    <div class="col-md-12">
                        <div id="myModal" class="modal">
                            <span class="close">&times;</span>
                            <img class=" modal-content img img-responsive" id="img01">
                            <div id="caption"></div>
                        </div>
                    </div>
                </div>
            </div>

            </div>





                     <%-- Customer list Create  --%>
            <div id="AddCustomerModal" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-md-width">
                    <div class="modal-content modal-content-md-width">
                        <div class="modal-header">
                            <h4 class="modal-title"><i class="fa fa-hand-point-right"></i>New Customer Add </h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="220px" ForeColor="Black" Font-Size="12px"></asp:Label>
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
                                            <asp:TemplateField HeaderText="Information">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgdatat" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")) %>' Width="250px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
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
                                                    <asp:Panel ID="Panegrd" runat="server">
                                                        <div class="  mb-0">
                                                            <asp:DropDownList ID="ddlval" runat="server" Visible="false"
                                                                CssClass="select2 form-control" AutoPostBack="true" TabIndex="2">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>


                                        <%--<FooterStyle CssClass="grvFooter" />--%>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" Height="30px" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="lnkReset" runat="server" CssClass="btn btn-sm btn-success" Width="100px" visible="false" ><i class="fa fa-spinner"></i> Reset</asp:LinkButton>
                            <%-- <button type="button" class="btn btn-danger  ml-auto  btn-md mt20 mr-1" data-bs-dismiss="modal">Close</button>--%>
                            <asp:LinkButton ID="btnCustomerSave" runat="server" OnClick="btnCustomerSave_Click" CssClass="btn btn-primary btn-sm" OnClientClick="closeAddCustomer()">Save</asp:LinkButton>
                            <button class="btn btn-primary btn-sm" data-dismiss="modal">Close</button>

                        </div>
                    </div>
                </div>
            </div>

            </div>

             </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
