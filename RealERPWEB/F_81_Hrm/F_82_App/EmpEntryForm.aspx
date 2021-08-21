
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EmpEntryForm.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EmpEntryForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewServices" runat="server">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">


                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                                <asp:TextBox ID="txtComSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="imgbtnComp" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnComp_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:DropDownList ID="ddlCompName" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlCompName_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                 <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-md-2 pull-right">
                                                <a href="#" class="btn btn-info primaryBtn margin5px"  onClick="history.go(-1)">Back</a>
                                            <asp:LinkButton ID="lnkNextbtn" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" OnClick="lnkNextbtn_Click"><span class="flaticon-add118"></span> Next</asp:LinkButton>
                                                
                                                <%--<a  class="btn btn-info primaryBtn margin5px" href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/EmpEntry01.aspx")%>">Next</a>--%>

                                            </div>


                                        </div>

                                        <div class="form-group" style="display:none;" >
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Location</asp:Label>
                                                <asp:TextBox ID="txtLocSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="ingbtnLoc" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ingbtnLoc_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>


                                        </div>
                                        <div class="form-group" style="display:none;" >
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblPreSLNo" runat="server" CssClass="lblTxt lblName">Branch</asp:Label>
                                                <asp:TextBox ID="txtBraSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="imgbtnBra" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnBra_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-3 pading5px  asitCol3">

                                               

                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none;" >
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                                <asp:TextBox ID="txtDptSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="imgbtnDept" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnDept_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>


                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Pre.Emp List:</asp:Label>
                                                <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="mgbtnPreEMP" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="mgbtnPreEMP_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>


                                        </div>


                                        <div class="form-group">
                                            <div class="col-md-2 pading5px ">
                                                <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Employee Name</asp:Label>
                                                <asp:CheckBox ID="chkNewEmp" runat="server" Text="New Emp." CssClass=" checkbox-inline" Style="margin-left: 10px;" OnCheckedChanged="chkNewEmp_CheckedChanged"
                                                    AutoPostBack="True" />

                                            </div>
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:TextBox ID="txtEmpName" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="margin-left: 6px;" Width="326"></asp:TextBox>
                                            </div>

                                            <div class="col-md-2 pading5px asitCol2">
                                                <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" CssClass="btn btn-danger primaryBtn">Save</asp:LinkButton>
                                                <asp:Label ID="lblEmplastId" runat="server" Visible="false"></asp:Label>
                                                
                                            </div>


                                        </div>
                                    </div>
                                </fieldset>


                            </asp:View>

                        </asp:MultiView>
                    </div>
                </div>
            </div>
        
</asp:Content>

