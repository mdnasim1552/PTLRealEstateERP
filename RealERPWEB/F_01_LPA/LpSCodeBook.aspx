<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LpSCodeBook.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LpSCodeBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
        function pageLoaded() {

            $('#Chboxchild').change(function () {
                var result = $('#Chboxchild').is(':checked');
                var description = result ? "Add Child" : "Add Group";
                $('#lblchild').html(description);
            });
            $('.chzn-select').chosen({ search_contains: true });
        };
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
    <style>
        .grvHeader th {
            font-weight: normal;
            text-align: center;
            text-transform: capitalize;
        }
        /*.cald {
             z-index: 1;
        }*/

        .lblmargin {
            margin: 0px !important;
        }

        .lblheadertitle {
            background-color: #346CB0;
            font-size: 14px;
            font-weight: bold;
            color: white;
            padding-left: 5px !important;
        }

        .form-control-sm {
            padding: 0.25rem 0rem !important;
        }

        .grvContentarea {
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card mt-4">
                <div class="card-body">
                    <div class="row mb-4">
                   
                                        <div class="col-md-3 pading5px asitCol3 d-none">

                                            <asp:Label ID="lblItem" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                            <asp:TextBox ID="txtFilter" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnSrch" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrch_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblStep" runat="server" CssClass="form-label" Text="Step :"></asp:Label>
                                            <asp:DropDownList ID="ddlGroupList" runat="server" CssClass="form-control form-control-sm">

                                                <asp:ListItem>Main Group</asp:ListItem>
                                                <asp:ListItem>Sub Group</asp:ListItem>
                                                <asp:ListItem>Sub-2 Group</asp:ListItem>
                                                <asp:ListItem>Sub-3 Group</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>
                                           
                                    </div>
                            <div class="col-md-2" style="margin-top:20px">
                                 <asp:LinkButton ID="lbtnShowData" runat="server" CssClass="btn btn-sm btn-primary "
                                                OnClick="lbtnShowData_Click">Show Data</asp:LinkButton>
                            </div>
                    </div>
                    </div>
                </div>
            <div class="card" style="min-height:480px">
                <div class="card-body">
                    <div class="row">
                    <asp:GridView ID="gvCodeBook" runat="server"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvCodeBook_PageIndexChanging"
                        OnRowCancelingEdit="gvCodeBook_RowCancelingEdit" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowEditing="gvCodeBook_RowEditing" OnRowUpdating="gvCodeBook_RowUpdating"
                        Width="16px" AllowPaging="True" PageSize="30">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl #">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CompCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvComCod" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GroupCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfGrp" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infgrp")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inf Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfCod" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                        Width="49px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="+">
                                 <ItemTemplate>
                                     <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent"
                                            Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "additem"))=="1"?true:false %>' OnClick="lbtnAdd_Click" ><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                 </ItemTemplate>
                                 <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" SelectText=""
                                ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="Code">
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtgvInfCod1" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="16"
                                        Style="font-weight: 700; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod1")) %>'
                                        Width="100px"></asp:TextBox>
                                </EditItemTemplate>--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfCod1" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod1")) %>'
                                        Width="110px" Style="text-align: left; font-weight: 700"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Details Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvInfDesc" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px"
                                        Style="font-weight: 700; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfDesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Short Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvInfDes2" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px"
                                        Style="font-weight: 700; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                        Width="177px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInfDes2" runat="server" Height="20px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc2")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvUnitFPS" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                        Style="font-weight: 700; text-align: left;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitfps")) %>'
                                        Width="55px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvUnitFPS" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitfps")) %>'
                                        Width="58px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Rate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvStdQtyF" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                        Style="font-weight: 700; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("###0.0000;(###0.00); ") %>'
                                        Width="100px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvStdQtyF" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("#,##0.0000;(#,##0.00); ") %>'
                                        Width="100px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Const. Area" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvcarea" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                        Style="font-weight: 700; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("###0.00;(###0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcarea" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Saleable Area" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvsarea" runat="server" BorderColor="Blue"
                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12"
                                        Style="font-weight: 700; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salarea")).ToString("###0.00;(###0.00); ") %>'
                                        Width="80px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgvsarea" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salarea")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px" Style="text-align: right"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Project Name" >
                                <EditItemTemplate>
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table style="width: 100%;">
                                            <tr>
                                                
                                                <td>
                                                    <asp:LinkButton ID="ibtnSrchProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlProName" runat="server" CssClass="ddlistPull"  TabIndex="6">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProNames" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>'
                                        Width="250px"></asp:Label>
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


            <%--<asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                           BorderWidth="1px">
            <table style="width: 98%;">

                <tr>
                    <td class="style18">
                        <asp:TextBox ID="txtFilter" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Height="18px" Width="86px"></asp:TextBox>
                    </td>
                    <td class="style25">
                        <asp:ImageButton ID="ibtnSrch" runat="server" Height="16px" 
                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnSrch_Click" TabIndex="1" 
                            Width="16px" />
                    </td>
                    <td class="style4">
                        <asp:Label ID="lblStep" runat="server" Font-Size="14px" Font-Underline="False" 
                            style="font-weight: 700; text-align:right; color: #FFFFFF;" Text="Step :" 
                            Width="62px"></asp:Label>
                    </td>
                    <td class="style24">
                        <asp:DropDownList ID="ddlGroupList" runat="server" Font-Bold="True" 
                            Font-Size="12px" Height="21px" style="text-transform: capitalize" TabIndex="2" 
                            Width="120px">
                            <asp:ListItem>Main Group</asp:ListItem>
                            <asp:ListItem>Sub Group</asp:ListItem>
                            <asp:ListItem>Sub-2 Group</asp:ListItem>
                            <asp:ListItem>Sub-3 Group</asp:ListItem>
                            <asp:ListItem Selected="True">Details</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style26">
                        <asp:LinkButton ID="lbtnShowData" runat="server" Font-Bold="True" 
                            Font-Size="12px" onclick="lbtnShowData_Click" 
                            style="color: #FFFFFF; " TabIndex="3" Width="60px">Show Data</asp:LinkButton>
                    </td>
                    <td class="style27">
                        <asp:Label ID="ConfirmMessage" runat="server" BackColor="Red" 
                            Font-Italic="False" Font-Size="12px" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
                </asp:Panel>--%>

            <table style="width: 98%;">
                <tr>
                    <td class="style13" colspan="10"></td>
                </tr>
                <tr>
                    <td class="style13">&nbsp;</td>
                    <td class="style18">&nbsp;</td>
                    <td class="style19">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style20">&nbsp;</td>
                    <td class="style14">&nbsp;</td>
                    <td class="style21">&nbsp;</td>
                    <td class="style11">&nbsp;</td>
                    <td class="style11">&nbsp;</td>
                    <td class="style11">&nbsp;</td>
                </tr>
            </table>

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
                                <asp:Label ID="lbgrcod" runat="server" Visible="false"></asp:Label>
                                <label class="col-md-4">Land Code</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtlandcode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <asp:TextBox Visible="false" ID="infgrpchk" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>  
                            <div>
                                <asp:TextBox Visible="false" ID="infcodchk" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div>
                                <asp:TextBox Visible="false" ID="actcodechk" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Description of Code</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtfullDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>  
                            <div class="row mb-1">
                                <label class="col-md-4">Description of Code BN</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtshortDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="row mb-1">
                                <label class="col-md-4">Unit </label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtunit" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <label id="chkbod" runat="server" class="switch">
                                        <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" />
                                        <span class="btn btn-xs slider round"></span>
                                    </label>
                                    <%--<asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>--%>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Unit Rate</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtunitrate" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Project Name</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtprojectname" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click" ToolTip="Update Code Info.">
                                <i class="fas fa-save"></i>&nbsp;Update </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




