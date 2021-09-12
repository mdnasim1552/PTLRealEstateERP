<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSubConBill.aspx.cs" Inherits="RealERPWEB.F_09_PImp.RptSubConBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
<script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName"
                                            Text="Date:"></asp:Label>
                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                        <asp:Label ID="lbldateTo" runat="server" Font-Bold="True"
                                            Text="Date:" CssClass="smLbl_to" Visible="true"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="Label4" runat="server"
                                            Text="Project Name:"
                                            CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server"
                                            Width="300px" AutoPostBack="True" CssClass="chzn-select ddlistPull"
                                            OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </div>
                                    <div class="col-md-1 asitCol1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server"
                                            OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn" Style="margin-left:-35px;">Ok</asp:LinkButton>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 asitCol75 pading5px">
                                        <asp:Label ID="Label16" runat="server" Font-Bold="True"
                                            Text="Contractor Name:"
                                            CssClass="lblTxt lblName"></asp:Label>

                                        <asp:TextBox ID="txtSrcSub" runat="server" CssClass="inputtextbox" Style="margin-left:2px;"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindSubConName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindSubConName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <div class="col-md-3 asitCol3 pading5px">
                                        <asp:DropDownList ID="ddlSubName" runat="server"
                                          Font-Bold="True" CssClass="ddlistPull" Width="300px">
                                        </asp:DropDownList>
                                    </div>

                                    <%--<div class="col-md-3 asitCol3 pading5px">--%>
                                       <%-- <asp:DropDownList ID="ddlSubName" runat="server"
                                            Font-Bold="True" CssClass="ddlistPull" Width="300px">
                                        </asp:DropDownList>--%>
                                        <cc1:ListSearchExtender ID="ddlSubName_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlSubName">
                                        </cc1:ListSearchExtender>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Font-Bold="True"
                                            Text="Size:" Visible="False"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            CssClass="ddlistPull"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                            Width="85px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblflrlist" runat="server" CssClass="smLbl_to"
                                            Text="Floor :"></asp:Label>

                                        <asp:DropDownList ID="ddlFloorListRpt" runat="server" Font-Bold="True" CssClass="ddlistPull"
                                            Style="text-transform: capitalize" Width="120px">
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="smLbl_to"
                                            Text="Group :"></asp:Label>

                                        <asp:DropDownList ID="ddlRptGroup" runat="server" Font-Bold="True"
                                            CssClass="ddlistPull" Width="69px">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                </div>
                            </div>
                        </fieldset>
                    </div>                  

                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="table table-responsive">
                                <asp:GridView ID="gvSubBill" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" AllowPaging="True"
                                    OnPageIndexChanging="gvSubBill_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Floor Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcFlrDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcRptDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                    Width="240px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="240px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBgdqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Budgeted.Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBgdrate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Budgeted Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBgdAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvBgdFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Sub-Cont.Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSubqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub-Cont.Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSubRate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "subconrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sub-Cont Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSubAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "subconamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvSubFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Diff.Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdiffqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "difqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Diff.Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdiffRate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "difrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Diff. Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdiffAmt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "difamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdiffAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>

                        <asp:View ID="View2" runat="server">
                            <div class="table table-responsive">
                                <asp:GridView ID="gvSubCon" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True"
                                    PageSize="20" OnPageIndexChanging="gvSubCon_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Floor Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvflrdesc" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcWDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitdesc")) %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Previous Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPreqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPreAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFPreAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCurqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCurAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCurAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqut")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





