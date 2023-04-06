﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccCodeBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });
        });


        function loadModal() {
            $('#AddAccCode').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }




        function CloseModal() {
            $('#AddAccCode').modal('hide');


        }

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });




            $('#Chboxchild').change(function () {

                var result = $('#Chboxchild').is(':checked');
                var description = result ? "Add Child" : "Add Group";
                $('#lblchild').html(description);


            });

            $('.chzn-select').chosen({ search_contains: true });
        };





    </script>



    <style>
        .mt20 {
            margin-top: 20px;
        }
        .mt22 {
            margin-top: 22px;
        }
     .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
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
           
            <div class="card card-fluid mt-4">
                <div class="card-header">
                    <div class="row">
                        <div class="col-sm-6 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="LblBookName1" runat="server" CssClass="lblTxt lblName" Text="Select Code Book:"></asp:Label>
                                <asp:DropDownList ID="ddlCodeBook" runat="server" CssClass="form-control form-control-sm chzn-select" >
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="LalSubCode" runat="server" CssClass="lblTxt lblName" Text="Level"></asp:Label>
                                <asp:DropDownList ID="ddlCodeBookSegment" CssClass="form-control form-control-sm chzn-select" runat="server" >
                                    <asp:ListItem Value="2">Main Code</asp:ListItem>
                                    <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Value="8">Sub Code-2</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary btn-sm mt20"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-1.5 col-md-1.5 col-lg-1.5">
                            <div class="form-group">
                                 <asp:Label ID="LblBookName2" runat="server" CssClass="lblTxt lblName" Text="Search" Visible="False"></asp:Label>
                                 <asp:TextBox ID="txtsrch" runat="server" CssClass="form-control form-control-sm" Visible="False"></asp:TextBox>
                                
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-1 col-lg-1">
                            <div class="form-group">
                                 <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-info btn-sm mt20" Visible="False"><i class="fas fa-search"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size" Visible="False"></asp:Label>                                 
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" Width="80px"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem Selected="True">600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                </asp:DropDownList>                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card-body" style="min-height: 350px;">
                <div class="row mb-2">
                    <div class="table-responsive">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="600"
                            OnPageIndexChanging="grvacc_PageIndexChanging" BorderStyle="None" OnRowDataBound="grvacc_RowDataBound" OnDataBound="grvacc_DataBound">

                            <FooterStyle BackColor="#5F9467" />



                            <%--                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="15"
                            OnPageIndexChanging="grvacc_PageIndexChanging"  CssClass="table table-striped table-hover table-bordered grvContentarea"  BorderStyle="None" OnRowDataBound="grvacc_RowDataBound" OnDataBound="
                                " >

                            --%>







                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="+">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent" Visible="false" OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>

                                        <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Center" />
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle ForeColor="#0000C0" HorizontalAlign="Center"/>
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" " >
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server" CssClass="disabled"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" CssClass="disabled"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Code">

                                    <ItemTemplate>

                                        <asp:Label ID="txtgrcode" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>

                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode3")) %>'
                                            Visible="False"></asp:Label>


                                    </ItemTemplate>
                                    <ItemStyle Width="85px"/>
                                    <HeaderStyle />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" CssClass="form-control inputTxt"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>                                    
                                    <ItemTemplate>
                                         <asp:HyperLink ID="hlnkgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))  %>'>                                             
                                            
                                        </asp:HyperLink>
                                        <%--<asp:Hyperlink ID="Label3" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:Hyperlink>--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="130px"/>
                                    <HeaderStyle HorizontalAlign="Left" />                                     
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Head of Accounts BN">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDescbn" runat="server" CssClass="form-control inputTxt"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdescbn")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Labelf3" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdescbn")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="125px"/>
                                    <HeaderStyle HorizontalAlign="Left" />                                    
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Level">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgridlevel" runat="server" MaxLength="10" CssClass="form-control inputTxt"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvType" runat="server" CssClass="form-control inputTxt"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;"
                                            
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccType" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvTypeDesc" runat="server" CssClass="form-control inputTxt" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblacttdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Entry User Name" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUsr" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref Id" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvShortDesc" runat="server" CssClass="form-control inputTxt" MaxLength="30"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wodesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvwodesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wodesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Department" Visible="false">
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtSerachProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="ibtnSrchProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchProject_Click1" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProName" runat="server" CssClass="ddlistPull" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>

                                        <asp:Label ID="lblgvProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="150px"></asp:Label>


                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Serial">
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
                                        <asp:Label ID="lblgvcatdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Project">
                                    <EditItemTemplate>
                                        <asp:Panel ID="pnlProject" runat="server" BorderColor="Yellow" BorderStyle="Solid" Visible="false"
                                            BorderWidth="1px">


                                            <table style="width: 100%;">
                                                <tr>

                                                    <td>
                                                        <asp:DropDownList ID="ddlpro" runat="server" CssClass=" chzn-select ddlPage" Width="180px" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlaiProject" runat="server" BorderColor="Yellow" BorderStyle="Solid" Visible="false"
                                            BorderWidth="1px">


                                            <table style="width: 100%;">
                                                <tr>

                                                    <td>
                                                        <asp:DropDownList ID="ddlaiproject" runat="server" CssClass=" chzn-select ddlPage" Width="180px" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>
                            </Columns>




                            <PagerTemplate>
                                <table id="pagerOuterTable" class="pagerOuterTable" runat="server">
                                    <tr>
                                        <td>
                                            <table id="pagerInnerTable" cellpadding="2" cellspacing="1" runat="server">
                                                <tr>
                                                    <td class="pageCounter">
                                                        <asp:Label ID="lblPageCounter" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="pageFirstLast">
                                                        <img src="../Image/firstpage.gif" align="absmiddle" />&nbsp;<asp:LinkButton ID="lnkFirstPage" CssClass="pagerLink" runat="server" CommandName="Page" CommandArgument="First">First</asp:LinkButton>
                                                    </td>
                                                    <td class="pagePrevNextNumber">
                                                        <asp:ImageButton ID="imgPrevPage" runat="server" ImageAlign="AbsMiddle" ImageUrl="../Image/prevpage.gif" CommandName="Page" CommandArgument="Prev" />
                                                    </td>

                                                    <td class="pagePrevNextNumber">
                                                        <asp:ImageButton ID="imgNextPage" runat="server" ImageAlign="AbsMiddle" ImageUrl="../Image/nextpage.gif" CommandName="Page" CommandArgument="Next" />
                                                    </td>
                                                    <td class="pageFirstLast">
                                                        <asp:LinkButton ID="lnkLastPage" CssClass="pagerLink" CommandName="Page" CommandArgument="Last" runat="server">Last</asp:LinkButton>&nbsp;<img src="../Image/lastpage.gif" align="absmiddle" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td visible="false" class="pageGroups">Pages:&nbsp;<asp:DropDownList ID="ddlPageGroups" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageGroups_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </PagerTemplate>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%--Modal  --%>

            <div id="AddAccCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Add New Code  </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row-fluid">
                                <asp:Label ID="lblactcode" runat="server" Visible="false"></asp:Label>

                                <div class="form-group" runat="server">
                                    <label class="col-md-4">Accounts Code</label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtacountcode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group" runat="server">
                                    <label class="col-md-4">Accounts Head</label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtaccounthead" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Level </label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtlevel" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>




                                    <div class="col-md-3">
                                        <label id="chkbod" runat="server" class="switch">
                                            <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" />
                                            <span class="btn btn-xs slider round"></span>
                                        </label>
                                        <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Type </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txttype" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Short Description </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtshort" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Ref. ID</label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtrefid" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4">Serial</label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtserial" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label id="lblddlproject" runat="server" class="col-md-4">Project</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>


                            </div>


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModal();" OnClick="lbtnAddCode_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>


                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                        </div>
                    </div>
                </div>
            </div>




           <%-- <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    

                                    <div class="col-md-4 pading5px">
                                        


                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        
                                        <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="form-group">
                                   

                                    <div class="col-md-2 pading5px">
                                        
                                    </div>
                                    <div class="col-md-1 pading5px">
                                       
                                    </div>
                                    


                                </div>

                                <div class="form-group">
                                </div>


                            </div>
                        </fieldset>



                        


                    </div>
                </div>
            </div>--%>


            




        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>


