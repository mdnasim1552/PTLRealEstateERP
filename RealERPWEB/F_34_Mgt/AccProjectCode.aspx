<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccProjectCode.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.AccProjectCode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .ml5 {
            margin-left: 5px;
        }

        .modal-lg {
            width: 80% !important;
        }

        div#ContentPlaceHolder1_ddlMainCode_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlSub1_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlSub2_chzn {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
            top: 34px !important;
        }

        .chzn-search input[type="text"] {
            width: 100% !important;
        }

        input#ContentPlaceHolder1_txtProjectName {
            width: 100% !important;
        }

        .isNewprj label {
            margin-bottom: 0;
        }
    </style>

    <script type="text/javascript">
        function openModal() {
            $('#modalEmpInfo').modal('toggle');
        }
        function CloseMOdal() {
            $('#modalEmpInfo').modal('hide');
        }

        function newCodebookOpen() {
            $('#newCodeBook').modal('toggle');
        }
        function newCodebookClose() {
            $('#newCodeBook').modal('hide');
        }

        function openDeleteModal() {
            $('#openDeleteModal').modal('toggle');
        }





        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }

        function Search_Gridview2(strKey) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                /*alert()*/
                var tblData = document.getElementById("<%=this.gvPrjCode.ClientID %>");

                var rowData;
                for (var i = 1; i < tblData.rows.length; i++) {
                    rowData = tblData.rows[i].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblData.rows[i].style.display = styleDisplay;
                }
            }

            catch (e) {
                alert(e.message);
            }
        }

    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid container-data mt-5" style="min-height: 1000px;">
                <div class="card-header">
                    <div class="row">


                        <div class="col-lg-2">

                            <div class="form-group">
                                <label for="ddlLvType">
                                    Main
                                </label>
                                <asp:DropDownList ID="ddlMainFilter" runat="server" CssClass="chzn-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMainFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-lg-2">

                            <div class="form-group">
                                <label for="ddlLvType">
                                    Sub-1
                                </label>
                                <asp:DropDownList ID="ddlSubFilter" OnSelectedIndexChanged="ddlSubFilter_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-2">

                            <div class="form-group">
                                <label class="d-block" for="ddlLvType">
                                    Sub-2  
                                </label>
                                <asp:DropDownList ID="ddlSubDetailsFilter" OnSelectedIndexChanged="ddlSubDetailsFilter_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                </asp:DropDownList>
                            </div>

                        </div>



                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="d-block" for="ddlLvType">
                                    Search
                                </label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..." onkeyup="Search_Gridview2(this)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="d-block" for="ddlLvType">
                                    Page Size
                                </label>
                                 <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="5">

                                    <asp:ListItem Selected="True" Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="600">600</asp:ListItem>
                                    <asp:ListItem Value="900">900</asp:ListItem>
                                    <asp:ListItem Value="1200">1200</asp:ListItem>
                                    <asp:ListItem Value="1500">1500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                         
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="lnknewcodebook" OnClick="lnknewcodebook_Click" runat="server" class="btn btn-primary btn-sm mt-4"><i class="fa fa-plus"></i> Create Project</asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive ">
                        <asp:GridView ID="gvPrjCode" runat="server" AutoGenerateColumns="False" ClientIDMode="Static" OnRowDataBound="gvPrjCode_RowDataBound" AllowPaging="true"
                            OnPageIndexChanging="gvPrjCode_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            PageSize="100">
                            <PagerSettings Position="bottom" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>


                                        <asp:Label ID="Label5" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="180px"></asp:Label>
                                        <asp:Label ID="lblgvSection" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="deleteModal" runat="server" Visible="false" CssClass="btn btn-danger" OnClick="deleteModal_Click"> <i class="fa fa-trash"></i>  </asp:LinkButton>
                                        <asp:LinkButton ID="lnkedit" runat="server" CssClass="btn btn-primary" OnClick="lnkedit_Click">  <i class="fa fa-edit"></i> </asp:LinkButton>
                                        <asp:HyperLink ID="lnkBtnPrjDetails" runat="server" Target="_blank" CssClass="btn btn-danger" OnClick="lnkBtnShow_Click"><i class="fa fa-eye"></i></asp:HyperLink>
                                        <asp:LinkButton ID="lnkBtnShow" runat="server" CssClass="btn bg-green" OnClick="lnkBtnShow_Click"><i class="fa fa-lock"></i> User Permission</asp:LinkButton>


                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" />
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

            <div id="modalEmpInfo" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static" aria-hidden="true">
                <div class="modal-dialog modal-lg ">
                    <div class="modal-content col-md-12 col-sm-12 ">
                        <div class="modal-header hedcon">
                            <asp:Label ID="lblprjname" CssClass="titel" Font-Bold="true" Font-Size="Large" runat="server">User Permission Project Wise</asp:Label>

                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddednPactcode" runat="server" />
                            <asp:GridView ID="gvEmployeeInfo" runat="server" AutoGenerateColumns="False"
                                CssClass="table-condensed tblborder grvContentarea ml-3">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="User Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUserId" runat="server" Width="60px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvName" runat="server" Width="170px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDesig" runat="server" Width="170px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'>                                  
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Staus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPerm" runat="server" Width="40px" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "permission")) %>'>                                  
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="chklblall" runat="server">Check All</asp:Label>
                                                        <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" Text="" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>


                                            <asp:CheckBox ID="chkPermission" runat="server" Width="20px" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "permission"))=="True" %>' />
                                            <%--  <asp:CheckBox ID="CheckPermission" runat="server" Checked="false" />--%>
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
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnSaveEmp" runat="server" OnClick="btnSaveEmp_Click" OnClientClick="CloseMOdal();" CssClass="btn btn-danger">Save</asp:LinkButton>
                            <button class="btn btn-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div id="newCodeBook" class="modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content col-md-12 col-sm-12 ">
                        <div class="modal-header hedcon">
                            <h5 class="modal-title">Create New Codebook</h5>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">


                            <div class="row">
                                <div class="col-lg-6">
                                    <div id="div2" runat="server">
                                        <div class="form-group">
                                            <label for="ddlLvType">
                                                Main
                                            </label>
                                            <asp:DropDownList ID="ddlMainCode" runat="server" CssClass="chzn-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-lg-6">
                                    <div id="div1" runat="server">
                                        <div class="form-group">
                                            <label for="ddlLvType">
                                                Project Name
                                            </label>
                                            <asp:TextBox ID="txtProjectName" runat="server" Placeholder="English Name" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="row">
                                <div class="col-lg-6">
                                    <div id="div3" runat="server">
                                        <div class="form-group">
                                            <label for="ddlLvType">
                                                Sub-1
                                            </label>
                                            <asp:DropDownList ID="ddlSub1" OnSelectedIndexChanged="ddlSub1_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                </div>
                                <div class="col-lg-6">

                                    <div id="div5" runat="server">
                                        <div class="form-group">
                                            <label for="ddlLvType">
                                                Short Name
                                            </label>
                                            <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>
                            </div>




                            <div class="row">
                                <div class="col-lg-6">
                                    <div id="div4" runat="server">
                                        <div class="form-group">
                                            <label class="d-block" for="ddlLvType">
                                                Sub-2 
                                
                                    <asp:CheckBox ID="chkNewProject" runat="server" CssClass="float-right badge badge-success isNewprj" Text=" New Project" OnCheckedChanged="chkNewProject_CheckedChanged"
                                        AutoPostBack="True" Visible="false" />
                                            </label>
                                            <asp:DropDownList ID="ddlSub2" OnSelectedIndexChanged="ddlSub2_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div id="div6" runat="server">
                                        <div class="form-group">
                                            <label for="ddlLvType">
                                                Project Name Bangla(if need)
                                            </label>
                                            <asp:TextBox ID="txtProjectNameBN" runat="server" Placeholder="Bangla Name" CssClass="form-control"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row ">
                                <div class="col-lg-6 d-none">
                                    <div id="prvProjt" runat="server">
                                        <div class="form-group">
                                            <label for="ddlLvType" class="d-block">
                                                Previous Project
                                    
                                    <asp:HyperLink ID="lnkBtnPrjDetails" runat="server" Target="_blank" CssClass="float-right badge badge-danger isNewprj" OnClick="lnkBtnShow_Click">Click Project Details</asp:HyperLink>
                                                <asp:LinkButton ID="lnkBtnShow" runat="server" CssClass="float-right badge badge-info isNewprj" OnClick="lnkBtnShow_Click">Set Project Permission</asp:LinkButton>

                                            </label>
                                            <asp:DropDownList ID="ddlProjectList" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" CssClass="btn btn-success float-right mb-2">Save</asp:LinkButton>
                                </div>
                            </div>












                        </div>
                    </div>
                </div>
            </div>


            <div class="modal fade" id="openDeleteModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <h5 class="text-center">Are you sure want to delete?</h5>
                            <p class="text-center">
                                <asp:LinkButton ID="lnkdelete" runat="server" CssClass="btn btn-danger" OnClick="lnkdelete_Click">Yes, Delete</asp:LinkButton>
                            </p>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

