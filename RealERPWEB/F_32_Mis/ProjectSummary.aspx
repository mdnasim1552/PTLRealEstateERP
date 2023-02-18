<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ProjectSummary.aspx.cs" Inherits="RealERPWEB.F_32_Mis.ProjectSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            $('.chzn-select').chosen({ search_contains: true });
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
           
        });
        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
    </script>


    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

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
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">





                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label21" runat="server">Project Name:</asp:Label>
                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inpPixedWidth d-none" TabIndex="10"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindProject" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="9"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="12">
                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="col-md-1" style="margin-top: 22px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>
                        </div>
                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right d-none"></asp:Label>

                    </div>

                </div>
            </div>
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <asp:Panel ID="pnlgrv" runat="server" Visible="false">

                        <div class="row">
                           <div class="form-group">
                                <asp:Label ID="lblgrp5" CssClass="btn btn-primary primaryBtn bg-primary mb-2" runat="server"></asp:Label>

                            <asp:GridView ID="gv05" runat="server" AutoGenerateColumns="False" CssClass=" table-striped  table-bordered grvContentarea"
                                ShowFooter="True" Width="440px">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesc09" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gunit"))%>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvalue" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPaginationNew" />
                        <HeaderStyle CssClass="grvHeaderNew" />

                            </asp:GridView>
                           </div>
                           
                            <div class="form-group ml-5">
                                 <asp:Label ID="lblgrp1" CssClass="btn btn-primary primaryBtn bg-primary mb-2" runat="server"></asp:Label>

                            <asp:GridView ID="gv01" runat="server" CssClass=" table-striped  table-bordered grvContentarea" AutoGenerateColumns="False" OnRowDataBound="gv01_RowDataBound" ShowFooter="True">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvgrpdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="As Per Budget">
                                        <ItemTemplate>

                                            <asp:HyperLink ID="hlnkgvBgdam" runat="server" Style="text-align: right" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:HyperLink>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                       <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="As Per Projection">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvActAC" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "in_actamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="As Per WIP">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkgvacam" runat="server" Style="text-align: right" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accramt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:HyperLink>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                       <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                </Columns>

                         <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPaginationNew" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                            </asp:GridView>

                            
                                <asp:Label ID="lblgrp2" CssClass="btn btn- primaryBtn bg-success d-none" runat="server"></asp:Label>
                            </div>

                           
                           


                        </div>
                    </asp:Panel>
                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

