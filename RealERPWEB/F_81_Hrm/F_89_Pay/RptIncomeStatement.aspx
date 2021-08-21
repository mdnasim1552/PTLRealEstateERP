<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptIncomeStatement.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.RptIncomeStatement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



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
                    <asp:Panel ID="Panel1" runat="server">

                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                            <asp:TextBox ID="txtSrcCompanyAgg" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindCompanyAgg" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindCompanyAgg_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlCompanyAgg" OnSelectedIndexChanged="ddlCompanyAgg_SelectedIndexChanged" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblCompanyNameAgg" runat="server" Style="border: none; line-height: 1.5" CssClass="form-control dataLblview" Height="22" Visible="false"></asp:Label>
                                        </div>
                                        <div class="colMid">
                                            <asp:LinkButton ID="lnkbtnSerOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lbldeptnameagg" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                            <asp:TextBox ID="txtsrchdeptagg" runat="server" CssClass="inputTxt inputName inpPixedWidth" OnTextChanged="txtsrchdeptagg_TextChanged"></asp:TextBox>
                                            <asp:LinkButton ID="lbtndeptagg" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="lbtndeptagg_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddldepartmentagg" OnSelectedIndexChanged="ddldepartmentagg_SelectedIndexChanged" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblvaldeptagg" runat="server" CssClass="form-control dataLblview" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>
                                        </div>



                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblsection" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control dataLblview" Height="22" Style="border: none; line-height: 1.5" Visible="false"></asp:Label>
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3 pull-right">
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Employee List:</asp:Label>
                                            <asp:TextBox ID="txtEmpSrcInfo" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnEmpListAllinfo_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control inputTxt" TabIndex="2" Width="385px" OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>


                                    </div>


                           
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                            </fieldset>
                        </div>


                    </asp:Panel>
                    <div class="row table-responsive">

                        <asp:GridView ID="gvpayinfo" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Month of </br>Payment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salary and</br> Allowances">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvIncSalary" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFsalary" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Text="Grand Total :" Width="80px" ></asp:Label>
                                    </FooterTemplate>
                                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DBBL Payment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbank" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#, ##0;(#, ##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="gvFbank" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px" ></asp:Label>
                                    </FooterTemplate>
                                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="DBL Payment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbank2" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt2")).ToString("#, ##0;(#, ##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="gvFbank2" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px" ></asp:Label>
                                    </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                     <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Cash">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcash" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#, ##0;(#, ##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="gvFcashamt" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px" ></asp:Label>
                                    </FooterTemplate>
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Salary </br>and Allowances">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtoSal" runat="server" Font-Size="11PX"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalsal")).ToString("#, ##0;(#, ##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="gvFtosal" runat="server" Font-Size="11PX" Font-Bold="true" Style="text-align: right" Width="80px" ></asp:Label>
                                    </FooterTemplate>
                                     <HeaderStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>





