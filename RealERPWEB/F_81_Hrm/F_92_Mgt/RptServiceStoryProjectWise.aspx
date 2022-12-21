<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptServiceStoryProjectWise.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RptServiceStoryProjectWise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

          <%--  <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">

                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label8" runat="server">Company</asp:Label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6" id="divBracnhLsit" runat="server">
                            <asp:Label ID="Label9" runat="server">Branch</asp:Label>
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label10" runat="server">Department</asp:Label>
                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="form-control chzn-select" TabIndex="6">
                            </asp:DropDownList>

                            <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlProjectName">
                            </cc1:ListSearchExtender>
                            <asp:Label ID="lblComBonLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                            <asp:Label ID="lblComSalLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:Label ID="Label11" runat="server">Section</asp:Label>
                            <asp:DropDownList ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control chzn-select" TabIndex="6" AutoPostBack="true">
                            </asp:DropDownList>

                            <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlSection">
                            </cc1:ListSearchExtender>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6" id="divEMplist" runat="server" visible="false">
                            <asp:Label ID="lblemp" runat="server">Employee List                                  
                            <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" OnClick="ibtnEmpListAllinfo_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                            </asp:Label>
                            <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged" CssClass="form-control chzn-select" TabIndex="2" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>





                    </div>
                    <div class="row mt-1">
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="lblfrmdate" runat="server">Date</asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" AutoComplete="off"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="lbltodate" runat="server">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control form-control-sm" AutoComplete="off" ></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="Label14" runat="server">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>1000</asp:ListItem>
                                <asp:ListItem>2000</asp:ListItem>
                                <asp:ListItem>3000</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6" id="SeachDivForGrid" runat="server" visible="false">
                            <asp:Label ID="Label2" runat="server">Search </asp:Label>
                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True"
                                OnTextChanged="txtSearch_TextChanged" CssClass="form-control form-control-sm" placeholder="Search here..."></asp:TextBox>
                        </div>


                        <div class="col-lg-3 col-md-3 col-sm-6" id="rbtnPayTypeDiv" runat="server" visible="false">
                            <asp:RadioButtonList ID="rbtnPayType" RepeatDirection="Horizontal" CssClass="rbtnList1" Visible="false" runat="server">
                                <asp:ListItem>Cash</asp:ListItem>
                                <asp:ListItem>Bank</asp:ListItem>
                                <asp:ListItem>Cheque</asp:ListItem>
                                <asp:ListItem Selected="True">All</asp:ListItem>

                            </asp:RadioButtonList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2" id="gndDiv" visible="false" runat="server">
                            <asp:Label ID="Label15" CssClass="d-block" runat="server">Print Grand Total</asp:Label>
                            <asp:CheckBox ID="chkgrndt" runat="server" CssClass="form-control form-control-sm" />
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-2" id="lblBanglaDiv" runat="server" visible="false">
                            <asp:Label ID="lblBangla" runat="server" Visible="false">Bangla Print</asp:Label>
                            <asp:CheckBox ID="chkBangla" runat="server" Visible="false" />
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left mt20 btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                        </div>

                       

                    </div>

                </div>

                <div class="card-body pt-0">

                    

                </div>
            </div>--%>


          
            


</script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
