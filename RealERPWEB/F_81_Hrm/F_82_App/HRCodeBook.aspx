﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HRCodeBook.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.HRCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <style>
        div#ContentPlaceHolder1_ddlOthersBook_chzn{
               width: 100% !important;
        }
        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
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
                        <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="LblBookName1" runat="server" Text="Select Code Book:"></asp:Label>
                                <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                                <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control "></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:Label ID="lbldelcode" runat="server" Text="Details Code"></asp:Label>
                            <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control form-control-sm" runat="server">
                                <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary btn-sm mt20"></asp:LinkButton>

                            <asp:LinkButton ID="lnkcancel" runat="server" Text="Change" Visible="False" OnClick="lnkcancel_Click" CssClass="btn btn-primary btn-sm mt20"></asp:LinkButton>

                        </div>
           

                    </div>
                </div>
                <div class="card-body">
                        <asp:GridView ID="grvacc" runat="server" 
                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" Width="1000px"   ShowFooter="True">
                            

                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="+">
                                 <ItemTemplate>
                                     <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent"
                                            Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "additem"))=="True"?true:false %>' OnClick="lbtnAdd_Click" ><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                 </ItemTemplate>
                                 <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" SelectText=""
                                    ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod2"))+"-" %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod3")) %>'
                                            Width="50px" Style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                            Width="200px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Code BN">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDescBn" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdescbn")) %>'
                                            Width="200px"></asp:TextBox>
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lbldescbn" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdescbn")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvUnit" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Font-Size="16px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvRate" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center"  Font-Size="16px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgval")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtype" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgval")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvtxtpercnt" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px" Style="text-align: right"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpercnt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Font-Size="16px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Serial">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvtslno" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                            Width="30px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvslno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Font-Size="16px" />
                                     <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>





                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                            <AlternatingRowStyle BackColor="" />
                        </asp:GridView>

                </div>
            </div>
           

                     <%--   <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="LblBookName1" runat="server" CssClass="lblTxt lblName160" Text="Select Code Book:"></asp:Label>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary primaryBtn"></asp:LinkButton>

                                        <asp:LinkButton ID="lnkcancel" runat="server" Text="Change" Visible="False" OnClick="lnkcancel_Click" CssClass="btn btn-primary primaryBtn"></asp:LinkButton>

                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>


                            </div>
                        </fieldset>--%>


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
                                <label class="col-md-4">Code  </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txthrgcode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <asp:TextBox Visible="false" ID="hrgcodechk" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Description of Code</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>  
                            <div class="row mb-1">
                                <label class="col-md-4">Description of Code BN</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDescBN" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="row mb-1">
                                <label class="col-md-4">Type </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txttype" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click"  ToolTip="Update Code Info.">
                                <i class="fas fa-save"></i>&nbsp;Update </asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

