<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntryAllEmp.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EntryAllEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

        };
        $(document).ready(function () {
            $(".select2").select2();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });

        };
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
            <div class="card mt-5">
                <div class="card-header">

                    <panel runat="server" id="pnlmarketemp" visible="false">
                        <div class="row mt-1">
                            <div class="col-lg-4 col-md-4 col-sm-6">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label">Select Code Book:</asp:Label>
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control select2" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary btn-sm  mt-4" OnClick="lnkbtnOk_OnClick">Add</asp:LinkButton>
                            </div> 

                            
                        </div>
                        <div class="row mt-3">
                            <asp:GridView ID="gvEmpSal" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" ShowFooter="True" Width="850px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo5" runat="server"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btndelete" OnClick="btndelete_OnClick" OnClientClick="return confirm('Are you want to delete?')">
                                                <i class="fa fa-trash-alt "></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                                    <asp:TemplateField HeaderText="Card #">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCardno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblempid" runat="server" Visible="False"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                Width="160px"></asp:Label>
                                            <asp:Label ID="lblgvempname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesig" runat="server"
                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbntUpdateOtherDed" runat="server" CssClass="btn btn-success btn-sm " OnClick="lbntUpdateOtherDed_OnClick">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSection" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            </div>
                    </panel>
                    <panel runat="server" id="pnlplanemp" visible="false">
                        <div class="row  mt-1">
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <asp:Label ID="lblEmpPlan" runat="server" CssClass="smLbl_to">Employee:</asp:Label>
                                <asp:DropDownList ID="ddlEmpPlan" runat="server" CssClass="form-control select2" TabIndex="2" ></asp:DropDownList>
                            </div>
                            <div class="col-lg-1 col-md-1 col-sm-6">
                                <asp:LinkButton ID="lnkbtnOkPlan" runat="server" CssClass="btn btn-primary btn-sm mt-4" OnClick="lnkbtnOkPlan_Click">Add</asp:LinkButton>
                            </div> 
                        </div>
                        <div class="row mt-4">
                            <asp:GridView ID="gvEmpPlan" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" ShowFooter="True" Width="850px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNop" runat="server"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:ImageButton runat="server" ID="btndeletep" ImageUrl="~/assets/images/delete.png" Height="20px" OnClick="btndeletep_Click" OnClientClick="return confirm('Are you want to delete?')"
                                                CommandName="Command" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Card #">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCardnop" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <%-- <asp:CommandField ShowDeleteButton="True" />--%>

                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblempidp" runat="server" Font-Bold="true" Font-Size="11px" Visible="False"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                Width="160px"></asp:Label>
                                            <asp:Label ID="lblgvempnamep" runat="server" Font-Bold="true" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesigp" runat="server"
                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbntUpdateOtherDedp" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbntUpdateOtherDedp_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSectionp" runat="server" Font-Bold="true" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

