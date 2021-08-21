<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ImgUpload.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.ImgUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>




  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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
                                            <div class="col-md-1 pading5px">
                                                <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                              
                                            </div>
                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddlCompanyAgg" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="true" TabIndex="2">
                                                </asp:DropDownList>
                                             
                                            </div>
                                        
                                           
                                   

                                        </div>

                                         <div class="form-group">
                                            <div class="col-md-1 pading5px ">
                                                <asp:Label ID="lbldeptnameagg" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                               
                                            </div>
                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddldepartmentagg" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged">
                                                </asp:DropDownList>
                                               
                                            </div>
                                           
                                           

                                        </div>
                                          <div class="form-group">
                                            <div class="col-md-1 pading5px ">
                                                <asp:Label ID="lblsection" runat="server" CssClass="lblTxt lblName">Section</asp:Label>                                                
                                            </div>
                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                                </asp:DropDownList>
                                               
                                            </div>
                                           
                                           

                                        </div>
                                
                                
                                
                                  <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName">Employee List:</asp:Label>
                                       <%-- <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>--%>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>


                                    <div class="col-md-3 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-2 pull-right">
                                        
                                      <%--   <asp:LinkButton ID="btnCv" runat="server" CssClass="btn btn-primary okBtn" OnClick="btnCv_Click">Print CV</asp:LinkButton>
                                        <a class="btn btn-info primaryBtn margin5px" href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpAllInfo")%>">Print CV</a>--%>

                                        <a href="#" class="btn btn-info primaryBtn margin5px" onclick="history.go(-1)">Back</a>
                                        <a class="btn btn-info primaryBtn margin5px" href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HRDailyAttenManually.aspx")%>">Next</a>

                                    </div>

                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel2" runat="server">

                            <div class="col-md-3">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">


                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName text-warning">Employee Image</asp:Label>
                                            <asp:Image ID="EmpImg" runat="server" Height="100px" Width="100px" />
                                            <div>

                                                <asp:FileUpload ID="imgFileUpload" runat="server" Height="26px"
                                                    onchange="submitform();" ToolTip="Employee Image" Width="216px" />
                                            </div>

                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="col-md-3">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="msgHandSt">
                                                <asp:Label ID="Label2" CssClass=" smLbl_to" runat="server">Employee Signature</asp:Label>
                                            </div>
                                            <asp:Image ID="EmpSig" runat="server" Height="100px" Style="margin-left: 0px"
                                                Width="100px" />
                                            <div>

                                                <asp:FileUpload ID="imgSigFileUpload" runat="server" Height="26px"
                                                    onchange="submitform();" ToolTip="Employee Signature" Width="216px" />


                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                 

                </asp:Panel>
            </div>
            <div class="row">
                <div class="form-group">
                    <asp:Panel ID="Panel3" runat="server">




                        <div class="col-md-4 pading5px col-md-offset-2">
                            <asp:LinkButton ID="lbtnUpdateImg" runat="server" CssClass="btn btn-danger primaryBtn margin5px " OnClick="lbtnUpdateImg_Click">Update</asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-info primaryBtn" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>



                        </div>
                    </asp:Panel>
                </div>
            </div>
            </div>
            </div>



        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


