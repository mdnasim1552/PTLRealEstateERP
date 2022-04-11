<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccProjectCode.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.AccProjectCode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function openModal() {
            $('#modalEmpInfo').modal('toggle');
        }
        function CloseMOdal() {
            $('#modalEmpInfo').modal('hide');
        }


        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

    <style>
        .isNewprj label {
            margin-bottom: 0;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid container-data mt-5" style="min-height: 1000px;">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2" id="div2" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    <asp:LinkButton ID="imgbtnMainCode" runat="server" OnClick="imgbtnMainCode_Click">
                                                  Main</asp:LinkButton>
                                </label>
                                <asp:DropDownList ID="ddlMainCode" runat="server" CssClass="chzn-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" id="div3" runat="server">

                            <div class="form-group">
                                <label for="ddlLvType">
                                    <asp:LinkButton ID="ingbtnSub1" runat="server" OnClick="ingbtnSub1_Click">
                                                  Sub-1</asp:LinkButton>
                                </label>
                                <asp:DropDownList ID="ddlSub1" OnSelectedIndexChanged="ddlSub1_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="div4" runat="server">
                            <div class="form-group">
                                <label class="d-block" for="ddlLvType">
                                    <asp:LinkButton ID="imgbtnSub2" runat="server" OnClick="imgbtnSub2_Click">
                                                 Sub-2 
                                    </asp:LinkButton>
                                    <asp:CheckBox ID="chkNewProject" runat="server" CssClass="float-right badge badge-success isNewprj" Text=" New Project" OnCheckedChanged="chkNewProject_CheckedChanged"
                                        AutoPostBack="True" />
                                </label>
                                <asp:DropDownList ID="ddlSub2" OnSelectedIndexChanged="ddlSub2_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="prvProjt" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType" class="d-block">
                                    <asp:LinkButton ID="mgbtnPreDetails" runat="server" OnClick="mgbtnPreDetails_Click">
                                                Previous Project</asp:LinkButton>
                                    
                                    <asp:HyperLink ID="lnkBtnPrjDetails" runat="server" Target="_blank" CssClass="float-right badge badge-danger isNewprj" OnClick="lnkBtnShow_Click">Click Project Details</asp:HyperLink>
                                    <asp:LinkButton ID="lnkBtnShow" runat="server" CssClass="float-right badge badge-info isNewprj" OnClick="lnkBtnShow_Click">Set Project Permission</asp:LinkButton>

                                </label>
                                <asp:DropDownList ID="ddlProjectList" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                </asp:DropDownList>
                            </div>


                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2" id="div1" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Project Name
                                </label>
                                <asp:TextBox ID="txtProjectName" runat="server" Placeholder="English Name" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2" id="div6" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Project Name Bangla(if need)
                                </label>
                                <asp:TextBox ID="txtProjectNameBN" runat="server" Placeholder="Bangla Name" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2" id="div5" runat="server">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Short Name
                                </label>
                                <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                        </div>
<<<<<<< HEAD
<<<<<<< HEAD
                        <div class="col-md-2 pading5px">
                                        <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" CssClass="btn btn-danger">Save</asp:LinkButton>
                       </div>
=======
                        <div class="col-md-2 ">
                                        <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" CssClass="btn btn-danger primaryBtn">Save</asp:LinkButton>
                                    </div>
>>>>>>> dc33c2d5de44d03db3435846ac04e5a12c5f62ea
=======
>>>>>>> 21bf3a72ceb838d8a02940178962f789d4e6a4ce
                    </div>
                    <div class="table-responsive d-none">
                        <asp:GridView ID="gvPrjCode" runat="server" AutoGenerateColumns="False" ClientIDMode="Static"
                            OnPageIndexChanging="gvPrjCode_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            PageSize="15">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
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
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="container moduleItemWrpper d-none">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Main</asp:Label>
                                        <asp:TextBox ID="txtsrchMainCode" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ximgbtnMainCode" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnMainCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="xddlMainCode" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Sub-1</asp:Label>
                                        <asp:TextBox ID="txtSrcSub1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="xingbtnSub1" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ingbtnSub1_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="xddlSub1" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlSub1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPreSLNo" runat="server" CssClass="lblTxt lblName">Sub-2:</asp:Label>
                                        <asp:TextBox ID="txtSrcSub2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ximgbtnSub2" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnSub2_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="xddlSub2" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlSub2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 pading5px  asitCol3">

                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Pre. Project</asp:Label>
                                        <asp:TextBox ID="txtSrcDetails" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="xmgbtnPreDetails" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="mgbtnPreDetails_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">

                                        <%-- <asp:DropDownList ID="ddlProjectList" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                                        </asp:DropDownList>--%>

                                        <asp:DropDownList ID="xddlProjectList" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                        <%--<a href="#" id="lnkBtnShow" onclick="ShowEmployeeInfos();">Show<sup><span class="badgei" id="lblShow" runat="server"></span></sup></a>--%></li>
                                        <%--<button class="btn btn-primary primaryBtn" data-target="#modalEmpInfo" data-toggle="modal">Show</button>--%>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-2 pading5px ">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Project Name:</asp:Label>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:TextBox ID="xtxtProjectName" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="margin-left: 5px;" Width="200"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Short Name:</asp:Label>
                                        <asp:TextBox ID="xtxtShortName" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100"></asp:TextBox>



                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                        <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" CssClass="btn btn-danger primaryBtn">Save</asp:LinkButton>
                                    </div>


                                </div>
                            </div>
                        </fieldset>

                    </div>



                </div>
            </div>
            <div id="modalEmpInfo" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-keyboard="false" data-backdrop="static" aria-hidden="true">
                <div class="modal-dialog ">
                    <div class="modal-content col-md-12 col-sm-12 ">
                        <div class="modal-header hedcon">                            
                    <h4>Project Information </h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblprjname" runat="server" Text="Project Name" ForeColor="DodgerBlue" CssClass="d-block" Font-Size="Large"></asp:Label>
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
                            <asp:LinkButton ID="btnSaveEmp" runat="server" OnClick="btnSaveEmp_Click" OnClientClick="CloseMOdal();" CssClass="btn btn-primary">Save</asp:LinkButton>
                            <button class="btn btn-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

