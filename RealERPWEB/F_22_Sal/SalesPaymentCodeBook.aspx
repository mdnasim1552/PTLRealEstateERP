﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SalesPaymentCodeBook.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SalesPaymentCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });
        function pageLoaded() {

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

        .mt20 {
            margin-top: 29px;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 32px !important;
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

            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row ">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label id="Label1" runat="server" cssclass=" lblName lblTxt">Select Code Book</label>
                                <asp:LinkButton ID="lnkbtnFindProject" runat="server"> <i class="fa fa-search" aria-hidden="true"></i>
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlSalPayment" runat="server" CssClass="form-control form-control-sm chzn-select ">
                                </asp:DropDownList>
                                <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control "></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label for="ddlOthersBookSegment" cssclass=" lblName lblTxt" id="Label3" runat="server">Details Code</label>
                            <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="12">
                                <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control "></asp:Label>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkok" runat="server" CssClass=" btn btn-primary btn-sm mt20" OnClick="lnkok_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label cssclass=" lblName lblTxt" id="lblpaging" runat="server">Page Size</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm " Width="70px"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            </div>
            
            <div class="card card-fluid mb-2" style="min-height: 450px;">
                <div class="card-body">
                    <div class="table-responsive mb-1">
                        <asp:GridView ID="gvPaySch" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Font-Size="12px" OnRowCancelingEdit="gvPaySch_RowCancelingEdit" OnRowEditing="gvPaySch_RowEditing"
                            OnRowUpdating="gvPaySch_RowUpdating" PageSize="15" Width="284px" ShowFooter="True" OnPageIndexChanging="gvPaySch_PageIndexChanging">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Mode="NumericFirstLast" />
                            <FooterStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="+" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent"
                                            Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "additem"))=="True"?true:false %>' OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="20px" Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" SelectText=""
                                    ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod2"))+"-" %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDescgdescbn" runat="server" Font-Size="12px" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdescbn")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescgdescbn" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdescbn")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Mapping">
                                    <EditItemTemplate>
                                         <asp:Panel ID="Panelmapping" runat="server">
                                        <asp:DropDownList ID="ddlFloorCode" runat="server" CssClass=" ddlPage chzn-select" Style="width: 200px;" TabIndex="6">
                                        </asp:DropDownList>
                                             </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmapping" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>





                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                            <AlternatingRowStyle BackColor="" />
                        </asp:GridView>
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
                                <asp:Label ID="lbgrcod" runat="server" Visible="false"></asp:Label>
                                <label class="col-md-4">Payment Code </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtpaymentcode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <asp:TextBox Visible="false" ID="paymentcodchk" runat="server" CssClass="form-control"></asp:TextBox>
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
                                <div class="col-md-4">
                                    <asp:TextBox ID="txttype" runat="server" CssClass="form-control"></asp:TextBox>
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
