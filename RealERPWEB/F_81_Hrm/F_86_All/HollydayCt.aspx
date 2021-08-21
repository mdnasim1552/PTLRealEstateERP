<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HollydayCt.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_86_All.HollydayCt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

          //  $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        <%--   var gridview = $('#<%=this.dgv1.ClientID %>');
           $.keynavigation(gridview);--%>
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Emp.  Name</asp:Label>
                                        <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass="inputTxt inputName inpPixedWidth" TabIndex="0"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="btn btn-primary srearchBtn" TabIndex="1" OnClick="imgbtnEmployee_Click"><span class="glyphicon glyphicon-search asitGlyp" > </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlEmpName" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="2">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-sm-3 pading5px">
                                        <asp:Label ID="lmsg11" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="lblcom" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:Label ID="lblCompany" runat="server" CssClass="inputTxt"></asp:Label>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:Label ID="lblSection" runat="server" CssClass="inputTxt"></asp:Label>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Designation</asp:Label>
                                        <asp:Label ID="lblDesignation" runat="server" CssClass="inputTxt"></asp:Label>

                                    </div>
                                    <div class="col-md-4 pull-right">
                                        
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Month</asp:Label>

                                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" CssClass=" ddlistPull"                                           
                                            OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <cc1:ListSearchExtender ID="ddlMonth_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlMonth">
                                        </cc1:ListSearchExtender>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                    <asp:CheckBoxList ID="chkDate" runat="server" CssClass="chkBoxControl"  ForeColor="#000" RepeatDirection="Horizontal" Width="900px"
                            RepeatColumns="7">
                                    </asp:CheckBoxList>
                                        </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>



            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

