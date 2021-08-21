<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPrjWiseMrfHistory.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPrjWiseMrfHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

            $('.chzn-select').chosen({ search_contains: true });

            $(function () {
                $('[id*=chkProjectName]').multiselect({
                    includeSelectAllOption: true,

                    enableCaseInsensitiveFiltering: true,
                    //enableFiltering: true,

                });

            });



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
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <%--   <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control  inputTxt" Style="width: 336px">
                                        </asp:DropDownList>--%>
                                        <asp:ListBox ID="chkProjectName" runat="server" CssClass="form-control" Style="min-width: 200px !important;" SelectionMode="Multiple"></asp:ListBox>


                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" Style="margin-left: -50px" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>

                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="lblTxt lblName">Date</asp:Label>


                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                        <asp:Label ID="lbldateto" runat="server" CssClass="lblTxt smLbl_to" Style="margin-right: 7px;">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" Text="Size:" CssClass="lblName lblTxt"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage"
                                            TabIndex="4">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                            <asp:ListItem Value="500">500</asp:ListItem>
                                            <asp:ListItem Value="700">700</asp:ListItem>
                                            <asp:ListItem Value="900">900</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>


                                </div>


                            </div>
                        </fieldset>
                    </div>



                    <asp:GridView ID="gvmrfstatus" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False"
                        Style="text-align: left" Width="889px" ShowFooter="true">
                        <PagerSettings Position="Top" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>


                            <%--   <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvProjDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Project Name">

                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name" Width="130px"></asp:Label>

                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>
                                    </asp:HyperLink>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblgvProjDesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lgvFApprQty11" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right"> Total:</asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>





                            <asp:TemplateField HeaderText="Total Mrf">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />

                                <FooterTemplate>
                                    <asp:Label ID="lgvFApprQty" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>

                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />

                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Total Order </br> Process">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvprocess" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "procesqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lgvFprocess" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>

                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Total Purchase</br> Order">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvporder" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFporder" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Purchase </br> Order Receive">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmrr" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFmrr" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>

                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Bill </br> Confirmation">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvbill" runat="server" Font-Size="11px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFbill" runat="server" Font-Size="11px" Height="16px"
                                        Style="text-align: right" Width="60px"></asp:Label>
                                </FooterTemplate>

                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
