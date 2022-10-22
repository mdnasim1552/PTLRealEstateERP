<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpPro.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EmpPro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>


        div#ContentPlaceHolder1_ddlCompany_chzn {
            width: 100%;
        }

        div#ContentPlaceHolder1_ddlDepartment_chzn {
            width: 100% !important;
        }
                .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }
        .chzn-container{
            width: 100% !important;
            height:28px!important;
        }

        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 35px !important;
        }

        .card-body {
            min-height: 400px !important;
        }
                  .pd4{
                    padding:4px!important;
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
              
                        <div class="col-lg-1 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server">Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group mt20">
                                <asp:Label ID="Label1" runat="server">Promotion No</asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" CssClass="form-control form-control-sm"
                                    Width="95px"></asp:Label>

                                <asp:Label ID="lblCurNo2" runat="server" CssClass="form-control form-control-sm"
                                    Width="95px" TabIndex="2"></asp:Label>
                            </div>




                        </div>

                        <div class="col-lg-1 col-md-2 col-sm-6">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click"
                                TabIndex="3">Ok</asp:LinkButton>
                        </div>
                        <div class="col-lg-5"></div>
                                  <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <asp:Label runat="server">
                                    <asp:LinkButton ID="lbtnPrevProList" runat="server" OnClick="lbtnPrevProList_Click" CssClass="lblTxt lblName">Prev. Pro List:</asp:LinkButton></asp:Label>
                                <asp:DropDownList ID="ddlPrevProList" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">

                    <asp:Panel ID="pnlprj" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server">Company </asp:Label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <div class="form-group">
                                    <asp:Label ID="lblDept" runat="server">Department </asp:Label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control  chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>



                            <div class="col-lg-3  col-sm-3 col-sm-6">
                                <div class="form-group">
                                    <asp:Label ID="lblResList" runat="server">Employee List     
                                        <asp:LinkButton ID="ibtnEmpList" runat="server" OnClick="ibtnEmpList_Click"><i class="fa fa-search"> </i></asp:LinkButton></asp:Label>
                                    <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                           


                        </div>

                        <div class="row">

                            <div class="col-lg-3  col-sm-3 col-sm-6">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server">Present Desig.</asp:Label>
                                     <asp:Label ID="lblDesig" runat="server" CssClass=" form-control"></asp:Label>
                                </div>
                                 
                            </div>
                            
                            <div class="col-lg-3  col-sm-3 col-sm-6">
                                <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" ForeColor="red">Revised Desig.                                
                                    <asp:LinkButton ID="ibtnDesg" runat="server" OnClick="ibtnDesg_Click"><i class="fa fa-search"></i></asp:LinkButton></asp:Label>
                                <asp:DropDownList ID="ddlDesig" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                             <div class="col-lg-1 col-md-1 col-sm-6">
                                <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkselect_Click">Select</asp:LinkButton>
                            </div>

                        </div>


                    </asp:Panel>


                    <div class="row mt-1">
                        <asp:GridView ID="gvremppro" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="margin-right: 0px" Width="818px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Secrion Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtprjdesc" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvidcardno" runat="server" Font-Size="11PX"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempname" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger btn-xs" OnClick="lnkupdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pdesig")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revised Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrevdesig" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rdesig")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>



                    <asp:Panel ID="PnlProRemarks" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server">Remarks  
                                        </asp:Label>
                                    <asp:TextBox ID="txtRemarks" runat="server"
                                    TextMode="MultiLine" CssClass="form-control" TabIndex="17"></asp:TextBox>
                                </div>                                
                            </div>
                            
                        </div>

                        <%--                  <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Remarks</asp:Label>

                                </div>
                                <div class="col-md-6 pading5px">
                                    <asp:TextBox ID="txtRemarks" runat="server"
                                        TextMode="MultiLine" CssClass="form-control" TabIndex="17"></asp:TextBox>
                                </div>
                            </div>
                        </div>--%>
                    </fieldset>




                    </asp:Panel>




                </div>
            </div>



            <%--<div class="row">
          <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
             
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:LinkButton ID="lbtnPrevProList" runat="server" OnClick="lbtnPrevProList_Click" CssClass="lblTxt lblName">Prev. Pro List:</asp:LinkButton>

                                        <asp:DropDownList ID="ddlPrevProList" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to">Promotion No</asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass=" inputName "
                                            Width="50px"></asp:Label>
                                        <asp:Label ID="lblCurNo2" runat="server" CssClass=" inputName "
                                            Width="50px" TabIndex="2"></asp:Label>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click"
                                            TabIndex="3">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
            <asp:Panel ID="pnlprj" runat="server" Visible="False">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-4 pading5px asitCol4">
                                <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                <asp:TextBox ID="txtsrchDeptName" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="ibtnDepartment" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnDepartment_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-3 pading5px asitCol4">
                                <asp:DropDownList ID="ddlDepartment" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="6">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName">Employee List</asp:Label>
                                <asp:TextBox ID="txtsrchEmp" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-3 pading5px asitCol4">
                                <asp:DropDownList ID="ddlEmpList" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged" runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="6" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkselect_Click">Select</asp:LinkButton>

                            </div>

                        </div>

                        <div class="form-group">
                            <div class="col-md-6 pading5px">
                                <asp:Label ID="Label3" runat="server" CssClass=" lblTxt lblName">Present Desig.</asp:Label>
                                <asp:Label ID="lblDesig" runat="server" CssClass=" inputlblVal" Style="width: 335px;"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Revised Desig.</asp:Label>
                                <asp:TextBox ID="txtsrchDesg" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="ibtnDesg" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnDesg_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-4 pading5px asitCol4">
                                <asp:DropDownList ID="ddlDesig" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-3 pull-right">
                                <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                            </div>
                        </div>

                    </div>


                </asp:Panel>
            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

