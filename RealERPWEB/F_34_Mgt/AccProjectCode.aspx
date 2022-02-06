<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccProjectCode.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.AccProjectCode" %>

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

            //$("input, select").bind("keydown", function (event) {
            //    var k1 = new KeyPress();
            //    k1.textBoxHandler(event);

            //});
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">


                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Main</asp:Label>
                                        <asp:TextBox ID="txtsrchMainCode" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnMainCode" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnMainCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlMainCode" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Sub-1</asp:Label>
                                        <asp:TextBox ID="txtSrcSub1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ingbtnSub1" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ingbtnSub1_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSub1" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlSub1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPreSLNo" runat="server" CssClass="lblTxt lblName">Sub-2:</asp:Label>
                                        <asp:TextBox ID="txtSrcSub2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSub2" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnSub2_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSub2" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlSub2_SelectedIndexChanged">
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
                                        <asp:LinkButton ID="mgbtnPreDetails" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="mgbtnPreDetails_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">

                                        <%-- <asp:DropDownList ID="ddlProjectList" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                                        </asp:DropDownList>--%>

                                        <asp:DropDownList ID="ddlProjectList" runat="server" CssClass="form-control chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                        <%--<a href="#" id="lnkBtnShow" onclick="ShowEmployeeInfos();">Show<sup><span class="badgei" id="lblShow" runat="server"></span></sup></a>--%></li>
                                         <asp:LinkButton ID="lnkBtnShow" runat="server" CssClass="btn  btn-primary primaryBtn" OnClick="lnkBtnShow_Click">Show</asp:LinkButton>
                                        <%--<button class="btn btn-primary primaryBtn" data-target="#modalEmpInfo" data-toggle="modal">Show</button>--%>
                                    </div>

                                </div>


                                <div class="form-group">
                                    <div class="col-md-2 pading5px ">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Project Name:</asp:Label>
                                        <asp:CheckBox ID="chkNewProject" runat="server" Text="New Project" CssClass=" checkbox-inline" OnCheckedChanged="chkNewProject_CheckedChanged"
                                            AutoPostBack="True" />

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:TextBox ID="txtProjectName" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="margin-left: 5px;" Width="200"></asp:TextBox>
                                    </div>
                                     <div class="col-md-3 pading5px asitCol3">
                                        <asp:TextBox ID="txtProjectNameBN" runat="server" Placeholder="Bangla Name" CssClass="inputTxt inputName inpPixedWidth" Style="margin-left: 5px;" Width="200"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Short Name:</asp:Label>
                                        <asp:TextBox ID="txtShortName" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100"></asp:TextBox>



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



            <div id="modalEmpInfo" class="modal fade   " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-full-width">
                    <div class="modal-content modal-content-full-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>Project Information </h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                            <asp:Label ID="lblprjname" runat="server" Text="Project Name" ForeColor="DodgerBlue" Font-Size="Large">
                            </asp:Label>
                        </div>
                        <div class="modal-body ">
                            <%--<div class="table-responsive">
                        <table id="tblinformation" class="table-striped table-hover table-bordered  grvContentarea">
                            <thead>
                                <tr class="grvHeader">
                                    <th class='tId'>User ID</th>
                                    <th class='tname'>Name</th>
                                    <th class='tdesig'>Designation</th>
                                    <th class='tstatus'>Status</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>--%>

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

