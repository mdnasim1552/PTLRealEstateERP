
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpProjectSummary.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.LinkGrpProjectSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <script type="text/javascript">
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
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>

                                </div>


                            </div>

                        </fieldset>
                    </div>

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">

                                        <asp:Label ID="lblgrp1" runat="server" AutoCompleteType="Disabled"  CssClass="btn btn-success primaryBtn"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:GridView ID="gv01" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgrpdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Time of Feasibility">
                                <ItemTemplate>
                                    <asp:Label ID="lgvFea" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "feaamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="As per Budget">
                                <ItemTemplate>
                                    <asp:Label ID="lgvBdg" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual as on">
                                <ItemTemplate>
                                    <asp:Label ID="lgvActAC" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "in_actamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual-Accrual Basis">
                                <ItemTemplate>
                                    <asp:Label ID="lgvActC" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accramt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
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

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">

                                        <asp:Label ID="lblgrp2" runat="server" AutoCompleteType="Disabled"  CssClass="btn btn-success primaryBtn"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                   
                    <asp:GridView ID="gv02" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="False" Width="440px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgpdesc02" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SFT">
                                <ItemTemplate>
                                    <asp:Label ID="lgvSft" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sft")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvsqty" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRate" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "re_actamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Percentage">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpercntge02" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percentage")).ToString("#,##0.00;(#,##0.00); ") +"%" %>'
                                        Width="80px"></asp:Label>
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


                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">

                                        <asp:Label ID="lblgrp3" runat="server" AutoCompleteType="Disabled"  CssClass="btn btn-success primaryBtn"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>


                    <asp:GridView ID="gv03" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="False" Width="440px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgpdesc33" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Master Plan">
                                <ItemTemplate>
                                    <asp:Label ID="lgvMPln" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mplan_amt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcBdg" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cost_bdgamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvremaining" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c_actamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Percentage">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpercntge03" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percentage")).ToString("#,##0.00;(#,##0.00); ")+"%"  %>'
                                        Width="80px"></asp:Label>
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
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">

                                        <asp:Label ID="lblgrp4" runat="server" AutoCompleteType="Disabled" CssClass="btn btn-success primaryBtn"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>




                    <asp:GridView ID="gv04" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="False" Width="440px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvgpdesc33" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Collection">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcollection06" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expenses">
                                <ItemTemplate>
                                    <asp:Label ID="lginvamt06" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invamt")).ToString("#,##0;(#,##0); ")  %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Position">
                                <ItemTemplate>
                                    <asp:Label ID="lgvnerinvestmant06" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "net_invamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Percentage">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpercntge06" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percentage")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
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

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">

                                        <asp:Label ID="lblgrp5" runat="server" AutoCompleteType="Disabled" CssClass="btn btn-success primaryBtn"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>



                    <asp:GridView ID="gv05" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="440px">
                        <RowStyle />
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

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

