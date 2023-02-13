<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjectSummary.aspx.cs" Inherits="RealERPWEB.F_32_Mis.ProjectSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });
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
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label21" runat="server" CssClass="lblTxt lblName">Project Name:</asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="10"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="12">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>
                                </div>
                            </div>
                    </div>
                    <asp:Panel ID="pnlgrv" runat="server" Visible="false">

                          <fieldset >

                        <div class="form-horizontal">

                            <div class="form-group">
                                <asp:Label ID="lblgrp5" CssClass="btn btn-primary primaryBtn bg-success" runat="server"></asp:Label>
                            </div>
                        </div>
                    </fieldset>

                    <asp:GridView ID="gv05" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="440px">

                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvdesc09" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                        Width="150px"></asp:Label>
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
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />

                    </asp:GridView>

                   

                    <fieldset>

                        <div class="form-horizontal">

                            <div class="form-group">
                                <asp:Label ID="lblgrp1" CssClass="btn btn-primary primaryBtn bg-success" runat="server"></asp:Label>
                            </div>
                        </div>
                    </fieldset>

                    <asp:GridView ID="gv01" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowDataBound="gv01_RowDataBound">

                        <Columns>
                            <asp:TemplateField HeaderText="Sl.#">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgrpdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                           
                            <asp:TemplateField HeaderText="As Per Budget">
                                <ItemTemplate>
                                   
                                    <asp:HyperLink ID="hlnkgvBgdam" runat="server" Style="text-align: right" Target="_blank"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="As Per Projection">
                                <ItemTemplate>
                                    <asp:Label ID="lgvActAC" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "in_actamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>



                             <asp:TemplateField HeaderText="As Per WIP">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvacam" runat="server" Style="text-align: right" Target="_blank"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accramt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            

                        </Columns>

                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    <fieldset>

                        <div class="form-horizontal">

                            <div class="form-group">
                                <asp:Label ID="lblgrp2" CssClass="btn btn-primary primaryBtn bg-success" runat="server"></asp:Label>
                            </div>
                        </div>
                    </fieldset>


               

                  
                         </asp:Panel>
                </div>
            </div>




            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

