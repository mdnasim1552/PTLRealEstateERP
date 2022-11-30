<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AlternativeSupvervisor.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.AlternativeSupvervisor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
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
            <div class="card card-fluid mt-1">
                <div class="card-body">

                  
                        <div class="row mt-1">
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label">Supervisor Employee</asp:Label>
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control select2" TabIndex="2"></asp:DropDownList>
                            </div>
                           
                      
                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <asp:Label ID="Label2" runat="server" CssClass="form-label">Alternative:</asp:Label>
                                <asp:DropDownList ID="ddlaltEmpName" runat="server" CssClass="form-control select2" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary btn-sm  mt-4" OnClick="lnkbtnOk_OnClick">Add</asp:LinkButton>
                            </div> 
                            
                        </div>


                        <div class="row mt-3">
                            <asp:GridView ID="gvEmpCluster" runat="server" CssClass="table-striped table-bordered grvContentarea" AutoGenerateColumns="False" ShowFooter="True" Width="850px">
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
                                                <i class="fa fa-trash-alt " aria-hidden="true" style="color:red;"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Card #">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCardno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sidcardno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblempid" runat="server" Visible="False"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sempid")) %>'
                                                Width="160px"></asp:Label>
                                            <asp:Label ID="lblgvempname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sempname")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesig" runat="server"
                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "sdesig")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbntUpdateOtherDed" runat="server" CssClass="btn btn-success btn-sm " OnClick="lbntUpdateOtherDed_OnClick">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSection" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssection")) %>'
                                                Width="190px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cluster Id" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvclusterid" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asempid")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Alternative Name">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvclustername" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asempname")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblasdesig" runat="server"
                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "asdesig")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                       
                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="16px" />
                                    </asp:TemplateField>
                                   
                                    

                                </Columns>
                                <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPaginationNew" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                            </asp:GridView>
                            </div>
              
                   
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
