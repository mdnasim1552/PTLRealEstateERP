
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProductPricing.aspx.cs" Inherits="RealERPWEB.F_02_Fea.RptProductPricing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style50 {
            color: white;
        }

        .txtboxformat {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 12px;
            font-weight: normal;
            margin-right: 0px;
            text-align: left;
            margin-left: 0px;
        }

        .style57 {
            width: 12px;
        }

        .style58 {
            width: 89px;
        }

        .style18 {
            color: #FFFFFF;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
    $(document).ready(function () {
        //For navigating using left and right arrow of the keyboard
                                Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
                                });
                                function pageLoaded() {

                                    $("input, select").bind("keydown", function (event) {
                                        var k1 = new KeyPress();
                                        k1.textBoxHandler(event);
                                    });

                                };
    </script>





                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                     <div class="col-md-3 pading5px asitCol3">

                                        <asp:Label ID="prname" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                   
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt">
                                            
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn"
                                                    OnClick="lnkbtnSerOk_Click" TabIndex="3"
                                              >Ok</asp:LinkButton>
                                       
                                    </div>

                                </div>
                                <div class="form-group">
                                       <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage1" runat="server" CssClass="lblName lblTxt"
                                            Text="Size :"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                        <asp:Label ID="lblGroup" runat="server" CssClass=" smLbl_to"
                                            Text="Group :"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlRptGroup_SelectedIndexChanged">
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
                    <div class="row">
                        <asp:GridView ID="gvFeaProPricing" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="785px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvFeaProPricing_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgroupdesc" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "udesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")).Trim(): "")  %>'
                                            Width="150px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgUnitn" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvQty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lgSize" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate/SFT">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvRate" runat="server" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAmtrep" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parking">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvPamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Utility">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvUtility" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utility")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Co-Operative">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvCamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coopamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Others">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvOthers" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otham")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Value">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvTamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Facing">
                                    <ItemTemplate>
                                        <asp:Label ID="lgFacing" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "facing")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:Label ID="lgview" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uview")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                           
<FooterStyle CssClass="grvFooter"/>
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


