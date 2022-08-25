<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HRLeaveOpening.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.HRLeaveOpening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <style>
                div#ContentPlaceHolder1_ddlCompany_chzn{
                    width:100%!important;
                }
                div#ContentPlaceHolder1_ddlProjectName_chzn{
                         width:100%!important;
                }
                .chzn-drop{
                    width:100%!important;
                }
                .mt20{
                    margin-top:20px;
                }
                                .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
                .card-body{
                    min-height:400px!important;
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
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblSection" runat="server">Section</asp:Label>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control  chzn-select" TabIndex="7" AutoPostBack="True" >
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="lblfrmdate" runat="server">Yearly Leave</asp:Label>
                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-6">
                             <asp:Label ID="lblCode" runat="server">Emp. Code</asp:Label>
                     
                                         <div class="input-group input-group-sm mb-3">
                                          <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="imgbtnEmpSrch" runat="server" CssClass="btn btn-primary btn" OnClick="imgbtnEmpSrch_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                            
                        
                        </div>
       

                        <div class="col-lg-1 col-md-2 col-sm-6">
                                   <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
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
                                        </asp:DropDownList>
                        </div>
                                         <div class="col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table table-responsive">
                        <asp:GridView ID="gvLeaveRule" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnPageIndexChanging="gvLeaveRule_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section">
                                      <HeaderTemplate>
                                                    <asp:Label ID="lblexle2" runat="server" Font-Bold="True" Width="100px"
                                                        Text="Section">
                                                        <asp:HyperLink ID="hlbtntbCdataExelSP2" runat="server"
                                                            CssClass="btn btn-success ml-2 btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                    </asp:Label>
                                                </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSection" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secname")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp. ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempid" runat="server" Visible="false" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                           ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CARD #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvidcard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnFUpLeave" runat="server" CssClass="btn  btn-success btn-sm" OnClick="lnkbtnFUpLeave_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesig" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Opening">
                                    <ItemTemplate>
                                        &nbsp;<asp:TextBox ID="txtgvelOpen" runat="server" BackColor="Transparent" BorderStyle="None"
                                            ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opening")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Entitlement">
                                    <ItemTemplate>
                                        &nbsp;<asp:TextBox ID="txtgvel" runat="server" BackColor="Transparent" BorderStyle="None"
                                            ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ernleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
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

            <%--           <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Yearly Leave</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" Font-Size="12px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"  AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>

                                        <div class="pull-left">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>

                                </div>
                    
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblSection" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnProSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Width="233" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                   


                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblCode" runat="server" CssClass="lblTxt lblName">Emp. Code</asp:Label>
                                        <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnEmpSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnEmpSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White"></asp:Label>

                                        <asp:Label ID="lblgmtime" runat="server"></asp:Label>
                                    </div>
                                </div>



                            </div>
                        </fieldset>--%>
            

     


        

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
