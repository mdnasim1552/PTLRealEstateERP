<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptProjectStock.aspx.cs" Inherits="RealERPWEB.F_12_Inv.RptProjectStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modalcss {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
            position: fixed;
            overflow: scroll;
        }

        .multiselect {
            width: 300px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 300px !important;
        }

        .multiselect-container {
            height: 350px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 300px !important;
        }

        .form-control {
            height: 34px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });


        function pageLoaded() {


            $(function () {
                $('[id*=chkResourcelist]').multiselect({
                    includeSelectAllOption: true,

                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,

                });

            });
            //$(function () {
            //    $('[id*=listGroup]').multiselect({
            //        includeSelectAllOption: true
            //    })

            //    $('[id*=lstProject]').multiselect({
            //        includeSelectAllOption: true
            //    })
            //});




            //var name = (1/2" PVC pipe);
            //name = name.replace("''", "").toLowerCase();
            //alert(name);
            $('.chzn-select').chosen({ search_contains: true });
            var gv = $('#<%=this.gvMatStock.ClientID %>');
            gv.Scrollable();

            var gv1 = $('#<%=this.gvMatStockSpec.ClientID%>');
            gv1.Scrollable();


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
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                    </div>
                                </div>




                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProName" runat="server" CssClass="chzn-select form-control  inputTxt" TabIndex="3" Style="width: 336px;">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4" Style="margin-left: -147px;">ok</asp:LinkButton>

                                    </div>


                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblresName" runat="server" CssClass="lblTxt lblName" Text="Material"></asp:Label>
                                        <asp:TextBox ID="txtsrchresource" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnresource" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="lbtnresource_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:ListBox ID="chkResourcelist" runat="server" CssClass="form-control" Style="min-width: 200px !important;" SelectionMode="Multiple"></asp:ListBox>


                                    </div>




                                </div>


                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>




                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page Size"></asp:Label>


                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                            <asp:ListItem>1200</asp:ListItem>
                                            <asp:ListItem>1500</asp:ListItem>
                                            <asp:ListItem>3000</asp:ListItem>
                                            <asp:ListItem>6000</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">

                                        <asp:CheckBox ID="chln" CssClass="checkBox" runat="server" Text="Chalan Qty" />
                                    </div>


                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="gvMatStock" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnPageIndexChanging="gvMatStock_PageIndexChanging" OnRowDataBound="gvMatStock_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Description" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgcPactdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Resource Description">
                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Resource Description" Width="100px"></asp:Label>

                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>
                                    </asp:HyperLink>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgcResDesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                        Width="150px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lgcUnit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgcUnitF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"> Total :</asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Budgeted Qty" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvbudgetqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFbudgetqty" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opening Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvOp" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvOpF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRecamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvRecamtF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transfer In">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTraninqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvTraninqtyF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Transfer Out">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTranoutqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lgvTranoutqtyF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />

                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Damage/Lost/Sold">
                                <ItemTemplate>
                                    <asp:Label ID="lgvDamage" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lgvDamageF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Received">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvTamtF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvIsuamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvIsuamtF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />

                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual Stock">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAcSamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acstock")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvAcSamtF" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Right" />

                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Diviation" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvdiviation" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diviation")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFdiviation" runat="server" Width="70px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                        </Columns>

                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    <asp:GridView ID="gvMatStockSpec" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnPageIndexChanging="gvMatStock_PageIndexChanging" OnRowDataBound="gvMatStock_RowDataBound">
                        <RowStyle />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Resource Description">
                                <ItemTemplate>


                                    <asp:HyperLink ID="hlnkgcResDesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false" Target="_blank"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                        Width="100px">
                                    </asp:HyperLink>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lgcspec" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lgcUnit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opening Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvOp" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRecamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transfer In">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTraninqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Transfer Out">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTranoutqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Damage/Lost">
                                <ItemTemplate>
                                    <asp:Label ID="lgvDamage" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Received">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvIsuamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual Stock">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAcSamt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acstock")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
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


            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




