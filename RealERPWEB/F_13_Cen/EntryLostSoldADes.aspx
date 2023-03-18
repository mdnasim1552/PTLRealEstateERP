<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntryLostSoldADes.aspx.cs" Inherits="RealERPWEB.F_13_Cen.EntryLostSoldADes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lblDate" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblCurNo" runat="server" CssClass="smLbl_to" Text="ID"></asp:Label>
                            <div class="d-flex">
                                <asp:TextBox ID="lblCurNo1" runat="server" CssClass="form-control form-control-sm" Text="SDL00-" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtCurNo2" runat="server" CssClass="form-control form-control-sm disabled" ReadOnly="True">00000</asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblSMCR" runat="server" CssClass="control-label" Text="Ref No"></asp:Label>
                            <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblProjectFromList" runat="server" CssClass="control-label" Text="Project"></asp:Label>
                                <asp:TextBox ID="txtsrchProject" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindProject" runat="server" OnClick="ImgbtnFindProject_Click"><i class="fa fa-search"> </i></asp:LinkButton>
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control form-control-sm" >
                                </asp:DropDownList>
                                <asp:Label ID="lblddlProject" runat="server" CssClass="form-control dataLblview" Height="30" Style="line-height: 1.5" Visible="false"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="ImgbtnFindPrevious" runat="server" CssClass="text-primary" OnClick="ImgbtnFindPrevious_Click">
                                    <i class="fa fa-search"></i> Previous List
                            </asp:LinkButton>
                            <asp:TextBox ID="txtSrchPrevious" runat="server" TabIndex="7" CssClass=" inputtextbox" Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlPreList" runat="server" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>
                        </div>

                        <div class="row" id="PanelSub" runat="server" visible="false">
                              <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblResList" runat="server" CssClass="control-label" Text="Materials List"></asp:Label>
                                <asp:TextBox ID="txtResSearch" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindRes" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="9"><i class="fa fa-search"> </i></asp:LinkButton>
                                <asp:DropDownList ID="ddlResList" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblSpecification" runat="server" CssClass="control-label" Text="Specification"></asp:Label>
                                <asp:TextBox ID="txtSrchSpecification" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnSpecification" runat="server" OnClick="ImgbtnSpecification_Click" TabIndex="9"><i class="fa fa-search"> </i></asp:LinkButton>
                                <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="chzn-select form-control form-control-sm" >
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelect_Click">Select</asp:LinkButton>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body">
                    <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="501px">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMatCode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Resource Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvspcfdesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit" runat="server"
                                        Style="font-size: 11px; text-align: center;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                               <%-- <FooterTemplate>
                                    <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" OnClick="lnktotal_Click">Total</asp:LinkButton>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />--%>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Stock Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvstkqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>


                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Stock  Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvstkrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>


                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Lost">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvlqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                             <%--   <FooterTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkupdate_Click">Update</asp:LinkButton>
                                </FooterTemplate>--%>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sold">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvsqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>


                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Destroyed">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvdqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>


                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                        Width="100px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                    HorizontalAlign="right" VerticalAlign="Middle" />

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

