<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HREmpConfirmation.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.HREmpConfirmation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        div#ContentPlaceHolder1_ddlCompany_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlProjectName_chzn {
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
            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                         <button onclick="printPage()" style="display:none;"></button>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server">Department</asp:Label>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control  chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-1">
                            <asp:Label ID="lblfrmdate" runat="server">From</asp:Label>
                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>


                        <div class="col-lg-1">
                            <asp:Label ID="lbltodate" runat="server">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>


                        <div class="col-lg-2 mt20">
                            <asp:CheckBox ID="chkconfrmdt" runat="server" Text="Confirmed Employee" />

                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                 <asp:Label ID="Label2" runat="server">
                                      Emp. Code
                                    <asp:LinkButton ID="imgbtnEmpSeach" runat="server" OnClick="imgbtnEmpSeach_Click"><i class="fas fa-search "></i></asp:LinkButton>
                                 </asp:Label>
              
                                <asp:TextBox ID="txtEmpSearch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-1">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                        </div>


                    </div>
                </div>
                <div class="card-body">
                    <asp:GridView ID="dgvEmpCon" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Style="text-align: left" Width="635px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Card ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvIDCard" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ID Card")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Empoyee ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvEmID" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Empoyee Name & Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lgvEmpNam" runat="server" Font-Size="12px"
                                        Text='<%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+
                                                                        "<br>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Joining Date">

                                <ItemTemplate>
                                    <asp:Label ID="lgvJoindat" runat="server" Font-Size="12px" Style="text-align: left"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindat")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Comfirm Date">

                                <ItemTemplate>
                                    <asp:Label ID="lgvCondat" runat="server" Font-Size="12px" Style="text-align: left"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "condat")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                        

                            <asp:TemplateField HeaderText="Gross Salary">

                                <ItemTemplate>
                                    <asp:Label ID="lgvgsal" runat="server" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grssal")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRem" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="12px" Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                        Width="80px"></asp:TextBox>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkvmrno" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                        Height="16px" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbok" runat="server" Width="30px" CommandArgument="lbok"
                                        OnClick="lbok_Click">OK</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Effective Date">
                                <ItemTemplate>
                                    
                                    <asp:TextBox ID="txtEffecDate" runat="server" CssClass="form-control form-control-sm pd4"  
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "effectdate")).ToString("dd-MMM-yyyy") %>'></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtEffecDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEffecDate"></cc1:CalendarExtender>
                                     
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
            </div>

            <%--  <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">



                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCompanyName" runat="server" Width="233" CssClass="dataLblview" Visible="False"></asp:Label>
                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">ok</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnProSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>

                                </div>


                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkconfrmdt" runat="server" TabIndex="10" Text="Confirmed Employee" CssClass="btn btn-outline-light" BorderStyle="Solid" />
                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>
                        </fieldset>--%>
            <div class="row">
                <asp:Panel ID="PnlProRemarks" runat="server" Visible="False">
                </asp:Panel>
            </div>


            <%--<table style="width: 100%;">

                <tr>
                    <td colspan="12">
                        <%-- <asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px" Width="909px">
                            <table style="width: 95%;">
                                <tr>
                                    <td class="style16">&nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Text="Company:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="txtboxformat"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style63">
                                        <asp:ImageButton ID="imgbtnCompany" runat="server" Height="16px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnCompany_Click" Width="16px" />
                                    </td>
                                    <td colspan="7" align="left">
                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True"
                                            Font-Bold="True" Font-Size="12px"
                                            OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" Width="300px">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" BackColor="#003366"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="White" Height="16px" OnClick="lnkbtnShow_Click"
                                            Style="text-align: center;" Width="50px">Ok</asp:LinkButton>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style16">&nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Text="Department:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style63">
                                        <asp:ImageButton ID="imgbtnProSrch" runat="server" Height="16px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="imgbtnProSrch_Click" Width="16px" />
                                    </td>
                                    <td align="left" colspan="7">
                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            Font-Bold="True" Font-Size="12px"
                                            OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>

                                <tr>
                                    <td class="style16">&nbsp;</td>
                                    <td class="style15">
                                        <asp:Label ID="lblfrmdate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Text="From:" Width="80px"></asp:Label>
                                    </td>
                                    <td align="left" class="style44">
                                        <asp:TextBox ID="txtfrmdate" runat="server" Width="100px" AutoPostBack="True"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style63">
                                        <asp:Label ID="lbltodate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Text="To:"></asp:Label>
                                    </td>
                                    <td class="style43">
                                        <asp:TextBox ID="txttodate" runat="server" Width="100px" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style38" align="left">&nbsp;</td>
                                    <td class="style62">&nbsp;</td>
                                    <td class="style41">
                                        
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td class="style45">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12"></td>
                </tr>
                <tr>
                    <td class="style56" colspan="12">
                        
                    </td>
                </tr>
                <tr>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td class="style56">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

