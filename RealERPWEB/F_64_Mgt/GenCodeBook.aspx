<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="GenCodeBook.aspx.cs" Inherits="RealERPWEB.F_64_Mgt.GenCodeBook" UnobtrusiveValidationMode="None" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

            $('#Chboxchild').change(function () {
                var result = $('#Chboxchild').is(':checked');
                var description = result ? "Add Child" : "Add Group";
                $('#lblchild').html(description);
            });

        }

        function loadModalAddCode() {
            $('#AddResCode').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        };

        function CloseModalAddCode() {
            $('#AddResCode').modal('hide');
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

            <div class="card" style="margin-bottom:6px">
                <div class="card-body mt-2 mb-2">
                    <div class="row">

                        <%---VISIBLE-FALSE-START---%>
                        <asp:Label ID="LblBookName1" Visible="false" runat="server" CssClass="form-label mr-2 mt-2">Select Group:</asp:Label>
                        <asp:DropDownList Visible="false" runat="server" ID="ddlOthersBook" CssClass="form-control chzn-select">
                        </asp:DropDownList>
                        <asp:Label ID="lbalterofddl" Visible="false" runat="server" CssClass="form-label mr-2 mt-2">Employee:</asp:Label>
                        <%---VISIBLE-FALSE-END---%>

                        <div class="col-md-5 mr-3 ">
                            <div cssclass="form-group" style="display: flex">
                                <div class="mr-5" style="display: flex">
                                    <asp:Label ID="Label3" runat="server" CssClass="form-label mt-2 mr-1">Select Type:</asp:Label>
                                    <asp:DropDownList runat="server" OnSelectedIndexChanged="ddlOthersBookSegment_SelectedIndexChanged" ID="ddlOthersBookSegment" CssClass="chzn-select" Width="160px">
                                        <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="display: flex">

                                    <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-label mt-1 mr-1" Style="width: 100px"></asp:Label>
                                    <asp:Label ID="Label2" runat="server" CssClass="form-label mt-2 mr-1">Select Details:</asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlemplist" CssClass="chzn-select" Width="160px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="lnkok" runat="server" OnClick="lnkok_Click" CssClass="btn btn-primary btn-sm primaryBtn">Ok</asp:LinkButton>
                        </div>
                        <div class="col-md-1 mr-4">
                            <div class="form-group" style="display: flex">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label mr-2 mt-1" Text="Size:" Visible="False"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="71px" Visible="False">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300" Selected="True">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body" style="min-height: 500px">

                    <div class="row">

                        <fieldset class="scheduler-border">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="Label1" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblBookName2" Visible="false" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Search Option:"></asp:Label>

                                    <div class="col-md-2 pading5px">
                                        <asp:TextBox ID="txtsrch" Visible="false" runat="server" CssClass="form-control inputTxt"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success srearchBtn" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <div class="table-responsive">
                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" PageSize="15" OnPageIndexChanging="grvacc_PageIndexChanging"
                                OnRowDataBound="grvacc_RowDataBound" CssClass="table-striped table-bordered grvContentarea">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="+">

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent" Visible="false" OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                        <HeaderStyle />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                                MaxLength="13"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                Width="90px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="80px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="250px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="150px"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>



                                            <asp:HyperLink ID="hlnkgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                                Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))  %>'
                                                Width="250px">                                             
                                            
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit" Visible="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirunit" runat="server" MaxLength="100"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="40px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Std.Rate" Visible="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsirval" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgridsirtype" runat="server" Font-Size="12px" MaxLength="10"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                Width="60px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltype" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px" MaxLength="20"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltypedesc" runat="server" Font-Size="12px"
                                                Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="User Name">
                                        <EditItemTemplate>
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                                BorderWidth="1px">


                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtSearchUserName" runat="server" CssClass=" inputtextbox" TabIndex="4" Width="86px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="ibtnSrchUse" runat="server" OnClick="ibtnSrchProject_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlUserName" runat="server" CssClass="ddlPage" Width="200px" TabIndex="6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvempname1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname1")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Team">
                                        <EditItemTemplate>
                                            <asp:Panel ID="pnlTeam" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                                BorderWidth="1px">


                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtSearchteam" runat="server" CssClass=" inputtextbox" TabIndex="4" Width="50px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="ibtnSrchteam" runat="server" OnClick="ibtnSrchteam_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlteam" runat="server" CssClass="ddlPage" Width="130px" TabIndex="6">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode3")) %>'
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeaderNew" />
                                <RowStyle CssClass="grvRowsNew" />
                            </asp:GridView>

                        </div>
                    </div>

                </div>
            </div>

            <div id="AddResCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fas fa-info-circle"></i>&nbsp;Add New Code</h5>
                            <asp:Label ID="lblmobile" runat="server"></asp:Label>
                            <button type="button" class="btn btn-xs btn-danger float-right" data-dismiss="modal" title="Close"><i class="fas fa-times-circle"></i></button>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row mb-1">
                                <asp:Label ID="lblsircode" runat="server" Visible="false"></asp:Label>
                                <label class="col-md-4">Code</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Description Code</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDescCode" runat="server" CssClass="form-control"></asp:TextBox> 
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Short Description</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtShrtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1" runat="server" id="divMobile">
                                <label class="col-md-4">Data Type</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="textDataTyp" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-1">
                                    <label id="chkbod" runat="server" class="switch">
                                        <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" />
                                        <span class="btn btn-xs slider round"></span>
                                    </label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click" ToolTip="Update Code Info.">
                                <i class="fas fa-plus"></i>&nbsp;Add</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

