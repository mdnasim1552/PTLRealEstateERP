<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpEntry02.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EmpEntry02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".select2").select2();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            //$('.datepicker').datepicker({ibtngrdEmpList
            //    format: 'mm/dd/yyyy',
            //});
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
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
            </div>

            <div class="card card-fluid container-data">
d                <p runat="server" id="txtname"></p>
                <div class="card-body">
           
                    <div class="row">
                        

                                <div class="col-6">
                                    <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                       >
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                   
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>

                                                    <asp:Label ID="lgcResDesc1" runat="server" CssClass="d-block" Height="16px">
                                                        <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>
                                                        <asp:LinkButton ID="ibtngrdEmpList" runat="server" ToolTip="New Field" Visible="false" CssClass="badge badge-info float-right" >
                                                                    <i class="fa fa-plus "></i>
                                                        </asp:LinkButton>
                                                    </asp:Label>

                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="230px" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgph" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                        Width="2px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle Font-Bold="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>

                                                <HeaderTemplate>

                                                    <asp:HyperLink ID="hlbtAddnew" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_82_App/HRCodeBook.aspx" CssClass="btn  btn-success btn-sm float-right" ToolTip="Add New"><i class="fa fa-plus" aria-hidden="true"></i></i>
                                                    </asp:HyperLink>
                                                </HeaderTemplate>

                                          

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtgvVal" runat="server"
                                                        CssClass="form-control" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>
                                                    <asp:TextBox ID="txtgvdVal" runat="server" AutoCompleteType="Disabled"
                                                        CssClass="form-control" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>

                                                    <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal" PopupPosition="TopLeft" PopupButtonID="txtgvdVal"></cc1:CalendarExtender>
                                                    <asp:Panel ID="Panegrd" runat="server">
                                                        <div class="  mb-0">
                                                            <asp:DropDownList ID="ddlval" runat="server" 
                                                                CssClass="select2 form-control" AutoPostBack="true" TabIndex="2">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="right" Width="230px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bangla">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvValBn" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdescbn")) %>' autocomplete="off"
                                                        Width="150px"></asp:TextBox>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <div class="col-6">
                                    <asp:GridView ID="gvPersonalInfo2" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                       >
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "RowID"))+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>


                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="230px" CssClass="d-block" Height="16px">
                                                        <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>
                                                        <asp:LinkButton ID="ibtngrdEmpList" runat="server" Visible="false" CssClass="badge badge-info float-right">
                                                                    <i class="fa fa-plus "></i>
                                                        </asp:LinkButton>
                                                    </asp:Label>


                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="230px" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgph" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                        Width="2px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle Font-Bold="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtgvVal" runat="server"
                                                        CssClass="form control" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>
                                                    <asp:TextBox ID="txtgvdVal" runat="server" AutoCompleteType="Disabled"
                                                        CssClass="form control" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>

                                                    <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal" PopupPosition="TopLeft" PopupButtonID="txtgvdVal"></cc1:CalendarExtender>
                                                    <asp:Panel ID="Panegrd" runat="server">
                                                        <div class="  mb-0" style="width:260px;">

                                                            <asp:DropDownList ID="ddlval" runat="server" 
                                                                CssClass="select2 col-12" AutoPostBack="true" TabIndex="2">
                                                            </asp:DropDownList>
                                                        </div>

                                                    </asp:Panel>
                                                </ItemTemplate>
                                                <ItemStyle Width="230px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="230px" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bangla">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvValBn" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdescbn")) %>' autocomplete="off"
                                                        Width="140px"></asp:TextBox>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                    </div>
                </div>
            </div>


                             <%--  <asp:Button runat="server" ID="lnkbtnSave" OnClick="lnkbtnSave_Click" CssClass="btn btn-primary btn-sm" Text="Save" />--%>
           <asp:LinkButton runat="server" ID="lnkbtnSave" OnClick="lnkbtnSave_Click" CssClass="btn btn-primary btn-sm" >Save</asp:LinkButton>

        </ContentTemplate>



    </asp:UpdatePanel>
</asp:Content>
