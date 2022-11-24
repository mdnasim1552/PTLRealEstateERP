<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpEntry01.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EmpEntry011" %>

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
            //$('.datepicker').datepicker({
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
                <div class="card-header mt-3 mb-0 pb-0">
                    <div class="row mb-0 pb-0">

                        <asp:Label ID="lbl" runat="server" CssClass="col-1 col-form-label">Employee List  </asp:Label>

                        <div class="col-4">

                            <asp:DropDownList ID="ddlEmpName" data-placeholder="Choose Employee.." runat="server"
                                CssClass="select2" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <asp:LinkButton ID="lnkCreate" OnClick="lnkCreate_Click" runat="server" CssClass="btn  btn-secondary  btn-sm float-right" ToolTip="Create New Employee"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>

                        <asp:Label ID="Label1" runat="server" CssClass="col-1 col-form-label">Information</asp:Label>
                        <div class="col-2">

                            <asp:DropDownList ID="ddlInformation" data-placeholder="Choose Information.." ClientIDMode="Static" runat="server"
                                CssClass="select2" AutoPostBack="true" OnSelectedIndexChanged="ddlInformation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <asp:Label ID="lblLastCardNo" runat="server" Visible="false" CssClass=" btn btn-info col-2 btn-sm mr-2 ml-2"></asp:Label>
                        <asp:HyperLink ID="addOcupation" Visible="False" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_82_App/HRCodeBook.aspx" CssClass=" btn btn-warning col-1 btn-sm mr-1 ml-1" Style="padding: 0 10px">Add Occupation</asp:HyperLink>

                    </div>


                </div>

                <div class="card-body">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewPersonal" runat="server">

                                <div class="col-6">
                                    <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnRowDataBound="gvPersonalInfo_RowDataBound">
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

                                                    <asp:Label ID="lgcResDesc1" runat="server" CssClass="d-block" Height="16px">
                                                        <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>
                                                        <asp:LinkButton ID="ibtngrdEmpList" runat="server" ToolTip="New Field" Visible="false" CssClass="badge badge-info float-right" OnClick="ibtngrdEmpList_Click">
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

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdatPerInfo" runat="server" Visible="false" CssClass="btn btn-danger  btn-xs" OnClick="lUpdatPerInfo_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
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
                                                            <asp:DropDownList ID="ddlval" runat="server" OnSelectedIndexChanged="ddlval_SelectedIndexChanged"
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
                                        OnRowDataBound="gvPersonalInfo2_RowDataBound">
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
                                                        <asp:LinkButton ID="ibtngrdEmpList" runat="server" Visible="false" CssClass="badge badge-info float-right" OnClick="ibtngrdEmpList_Click">
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
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdatPerInfo" runat="server" Visible="false" CssClass="btn btn-danger  btn-xs" OnClick="lUpdatPerInfo_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
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
                                                        <div class="mb-0" style="width:260px;">

                                                            <asp:DropDownList ID="ddlval" runat="server" OnSelectedIndexChanged="ddlval_SelectedIndexChanged"
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
                                <div class="col-sm-6 col-md-2 col-lg-2">
                                    <div class="row" id="UploadCV" runat="server" >

                                        <div class="col-md-12 ">
                                            <asp:Label ID="lblUploadCV" runat="server" >Upload CV</asp:Label>

                                        </div>
                                        <div class="col-md-12 ">
                                            <asp:FileUpload ID="FileUploadControl" CssClass="form-control" runat="server" />

                                        </div>
                                        <div class="col-md-12 ">
                                            <asp:Button runat="server" ID="btnUpload" Text="Upload CV" CssClass="btn btn-sm btn-warning" OnClick="btnUpload_Click"  />
                                        </div>
                                    </div>
                                </div>

                            </asp:View>
                            <asp:View ID="ViewDegree" runat="server">
                                <div class="row">
                                    <div class="col-4 d-none">
                                        <fieldset>
                                            <legend>Academic Information</legend>
                                            <!-- .form-group -->
                                            <div class="form-group">
                                                <label for="sel3">Degree Name</label>
                                                <asp:DropDownList ID="ddlDegreeList" runat="server" CssClass="form-control select2" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlDegreeList_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <!-- /.form-group -->
                                            <!-- .form-group -->
                                            <div class="form-group">
                                                <label for="sel4">Exam/Degree Title	</label>

                                                <asp:DropDownList ID="ddlAcadegreeList" runat="server" CssClass="form-control select2"  OnSelectedIndexChanged="ddlAcadegreeList_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>
                                            <!-- /.form-group -->
                                            <!-- .form-group -->
                                            <div class="form-group">
                                                <label for="sel5">Major Subject	</label>
                                                <asp:DropDownList ID="ddlMajorSubjList" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlMajorSubjList_SelectedIndexChanged"  AutoPostBack="True">
                                                </asp:DropDownList>
                                                
                                            </div>
                                            <!-- /.form-group -->
                                            <!-- /.form-group -->
                                            <!-- .form-group -->
                                            <div class="form-group">
                                                <label for="sel5">Result</label>
                                                <asp:DropDownList ID="ddlResultList" runat="server" CssClass="form-control select2"   AutoPostBack="True">
                                                </asp:DropDownList>
                                                
                                            </div>
                                              <div class="form-group">
                                                <label for="sel5">Institution</label>
                                                 <asp:TextBox ID="txtInstitue" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                 <asp:LinkButton ID="lnkaccAdd" runat="server" CssClass="btn btn-sm btn-primary float-right">Add</asp:LinkButton>
                                                
                                            </div>
                                            <!-- /.form-group -->

                                        </fieldset>
                                    </div>
                                    <div class="col-12">
                                          <legend>Academic Information</legend>
                                        <asp:GridView ID="gvDegree" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True"   OnRowDeleting="gvDegree_RowDeleting">
                                            <RowStyle />
                                            <Columns>
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Degree Name">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" Width="150" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Exam/Degree Title">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlAcadegree" runat="server" CssClass="form-control" Width="100" OnSelectedIndexChanged="ddlAcadegree_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Major Subject">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlMajorSubj" runat="server" CssClass="form-control" Width="100" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtgvEngMedium" runat="server" BackColor="Transparent" Visible="False"
                                                            BorderColor="#660033" BorderStyle="None" CssClass="form-control" Width="150px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Institution">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lUpdateDegree" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false" OnClick="lUpdateDegree_Click">Update</asp:LinkButton>
                                                    </FooterTemplate>

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvInstituee" runat="server" BackColor="Transparent"
                                                            BorderColor="#660033" BorderStyle="None" CssClass="form-control" Width="300px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "institute")) %>'></asp:TextBox>

                                                        <asp:DropDownList ID="ddlinstitute" runat="server" Visible="false" CssClass="chzn-select form-control">
                                                            <asp:ListItem>University of Dhaka</asp:ListItem>
                                                            <asp:ListItem>Rajshahi University</asp:ListItem>
                                                            <asp:ListItem>Bangladesh Agricultural University</asp:ListItem>

                                                            <asp:ListItem>Bangladesh University of Engineering and Technology</asp:ListItem>

                                                            <asp:ListItem>Chittagong University</asp:ListItem>

                                                            <asp:ListItem>Chittagong University of Engineering and Technology (CUET)</asp:ListItem>

                                                            <asp:ListItem>Comilla University</asp:ListItem>

                                                            <asp:ListItem>Dhaka University of Engineering and Technology (DUET)</asp:ListItem>

                                                            <asp:ListItem>Jahangirnagar University</asp:ListItem>

                                                        </asp:DropDownList>

                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Result">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlResult" runat="server" AutoPostBack="True"
                                                            CssClass="form-control" Width="150" OnSelectedIndexChanged="ddlResult_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">

                                                    <ItemTemplate>
                                                        <table style="width: 23%; height: 17px;">
                                                            <tr>
                                                                <td class="style49">
                                                                    <asp:Label ID="lblgvMarks" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        Text="Marks:" Width="62px"></asp:Label>
                                                                </td>
                                                                <td class="style52">
                                                                    <asp:TextBox ID="txtgvmarkorgrade" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "markorgrade")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        BackColor="Transparent"
                                                                        BorderStyle="None" Width="30px" Font-Size="11px" Style="text-align: right;">
                                                                              
                                                                              
                                                                              
                                                                    </asp:TextBox>
                                                                </td>
                                                                <td class="style51">
                                                                    <asp:Label ID="lblgvScale" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        Text="Scale :" Width="40px" Style="text-align: center;"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtgvScale" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "scale")).ToString("#,##0;(#,##0); ")  %>'
                                                                        BorderStyle="None" Width="30px" Font-Size="11px" Style="text-align: center;"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Passing Year">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlPassingYear" runat="server"
                                                            CssClass="form-control" Width="70">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgval" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                    </ItemTemplate>
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


                            </asp:View>
                            <asp:View ID="ViewCompany" runat="server">
                                <div class="row">
                                    <asp:GridView ID="gvEmpRec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="831px" OnRowCreated="gvEmpRec_RowCreated">

                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgcvCompany" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) %>'
                                                        Width="300px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="300px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvesDurationfrm" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%#   (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "frmdur")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "frmdur")).ToString("dd-MMM-yyyy")) %>'
                                                        Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtgvesDurationfrm_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvesDurationfrm" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="To">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateEmprecord" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateEmprecord_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvesDurationto" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%#   (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "todur")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "todur")).ToString("dd-MMM-yyyy")) %>'
                                                        Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtgvesDurationto_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvesDurationto" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewAssociation" runat="server">
                                <div class="row">
                                    <asp:GridView ID="gvAssocia" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="701px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Organization Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgcvOrgName" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orgname")) %>'
                                                        Width="300px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Position">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateEmpAssocia" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateEmpAssocia_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvPostion" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "position")) %>'
                                                        Width="300px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewRef" runat="server">
                                <div class="row">
                                    <asp:GridView ID="gvRef" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="831px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgcvName" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refname")) %>'
                                                        Width="150px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Organazation">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvOrgname" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orgname")) %>'
                                                        Width="200px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateRef" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateRef_Click">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvPhone" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMobile" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewJobRespo" runat="server">
                                <div>
                                    <asp:GridView ID="grvJobRespo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="700px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo42" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode1" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Responsibilities">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgcvJobRes" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobresp")) %>'
                                                        Width="350px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateJobRes" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateJobRes_Click">Update</asp:LinkButton>
                                                </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>



                            <asp:View ID="View1" runat="server">
                                <div>
                                    <asp:GridView ID="gvFamilyInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvFamilyInfo_OnRowDataBound" OnRowDeleting="gvFamilyInfo_OnRowDeleting"
                                        ShowFooter="True" Width="530px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:CommandField ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNopa" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvFamilyCode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblgvEmpname" runat="server" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                        Width="150px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Age">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvAge" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "age")) %>'
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Education (Last Degree)">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddllastDegree" runat="server" CssClass="chzn-select form-control" Width="150">
                                                    </asp:DropDownList>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateFamilyInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateFamilyInfo_OnClick">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Occupation">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlOcupation" runat="server" CssClass="chzn-select form-control" Width="150">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Organization">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtorg" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "org")) %>'
                                                        Width="250px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgvalfam" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>


                            <asp:View ID="View2" runat="server">
                                <div>
                                    <asp:GridView ID="gvCTCDetails" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvFamilyInfo_OnRowDataBound"
                                        ShowFooter="True" Width="500px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNoct" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgCodect" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Particulars">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvParticulars" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "particular")) %>'
                                                        Width="120px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lTotalClick" runat="server" CssClass="btn btn-success primaryBtn" OnClick="lTotalClick_OnClick">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Salary">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvsal" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:TextBox>

                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transport Allw.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvtrnsamt" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnsamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bonus(10% Monthly)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvbonus" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bonus")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateCtcInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateCtcInfo_OnClick">Update</asp:LinkButton>
                                                </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Provident Fund">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvpfamt" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile Allowance">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvmobamt" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mobalw")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CTC Total (TK.)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvtotalamt" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctctotal")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Last Update">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvlastupd" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%#   (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastupd")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastupd")).ToString("dd-MMM-yyyy")) %>'
                                                        Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtgvlastupd_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvlastupd" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgvalctc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>

                            <asp:View ID="View3" runat="server">
                                <div>
                                    <asp:GridView ID="gvSALDetails" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="500px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNosal" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgCodesal" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Starting Salary">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvstrtsal" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "strtsal")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:TextBox>

                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lTotalClickSal" runat="server" CssClass="btn btn-success primaryBtn" OnClick="lTotalClickSal_OnClick">Total</asp:LinkButton>
                                                </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Present Salary">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvprstsal" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presntsal")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Salary Diff.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvsaldiff" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saldif")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateSalInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateSalInfo_OnClick">Update</asp:LinkButton>
                                                </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Avg. Incr.(Amount)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvavgincr" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avgincr")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Avg. Incr.(%)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvavgincrper" runat="server" BackColor="Transparent" Style="text-align: right"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avgincrper")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Last Update On">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvlastupdt" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%#   (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastupdt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastupdt")).ToString("dd-MMM-yyyy")) %>'
                                                        Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtgvlastupdt_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvlastupdt" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgvalctc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="View4" runat="server">
                                <div>
                                    <asp:GridView ID="GvNominee" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="700px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo42" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Field">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCode" Visible="false" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                        Width="100px"></asp:Label>
                                                    <asp:Label ID="lblgvField" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Information">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvNomi" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nominfo")) %>'
                                                        Width="350px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdateInfoNomi" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateInfoNomi_Click" >Update</asp:LinkButton>
                                                </FooterTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                           

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>


                        </asp:MultiView>
                    </div>
                </div>
            </div>

            <div id="EmployeeEntry" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static" aria-hidden="true">
                <div class="modal-dialog ">
                    <div class="modal-content col-md-12 col-sm-12 ">
                        <div class="modal-header hedcon">

                            <h4>Employee Name Entry</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12 col-sm-12 col-lg-12">


                                <div class="form-group">
                                    <label for="txtEmpName">
                                        Employee Name 
                            
                             <asp:CheckBox ID="chkNewEmp" Checked="true" Text=" New Employee" runat="server" CssClass="d-none badge badge-secondary badgechk"
                                 AutoPostBack="True" />
                                    </label>
                                    <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" OnClientClick="CloseModal();" CssClass="btn btn-danger btn-sm">Add</asp:LinkButton>
                                    <asp:Label ID="lblEmplastId" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>


                <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>


    </asp:UpdatePanel>
</asp:Content>
