<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptCalTotalAvgValue.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptCalTotalAvgValue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container moduleItemWrpper">
        <div class="contentPart">
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
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 asitCol3 pading5px">

                                <asp:Label ID="Label5" runat="server"
                                    CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"
                                    Font-Bold="True"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            <div class="col-md-4 asitCol4 pading5px">
                                <asp:DropDownList ID="ddlProjectName" CssClass=" ddlPage" runat="server" Font-Bold="True"
                                    Width="300px">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                    QueryPattern="Contains" TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>

                                
                            </div>
                            <div class="col-md-1 pading5px pull-left">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>



                        </div>

                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">


                                <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName"
                                    Text="Date:"></asp:Label>


                                <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox " Width="80px"
                                    BorderStyle="None"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>




                            </div>
                            <div class="col-md-5 pading5px ">
                                <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to"
                                    Text="To:"></asp:Label>

                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox inputTxt" AutoPostBack="True"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Font-Bold="True"
                                    Text="Size:" Visible="False"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="ddlPage" AutoPostBack="True"
                                    Font-Bold="True"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Width="85px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        <%--<div class="form-group">
                            <div class="col-md-4 asitCol4 pading5px">

                                <asp:Label ID="lblPage0" runat="server" CssClass="lblTxt lblName" Text="Size:" ></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                    CssClass="ddlPage"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="71px">
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
                        </div>--%>

                    </div>
                        <%--<div class="form-group">
                            <div class="col-md-4 asitCol4 pading5px">

                                <asp:Label ID="lblPage0" runat="server" CssClass="lblTxt lblName" Text="Size:" ></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                    CssClass="ddlPage"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="71px">
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
                        </div>--%>

                    </div>

                </fieldset>
                <div class="table table-responsive">
                    <asp:GridView ID="grvsoldinf" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" OnPageIndexChanging="grvsoldinf_PageIndexChanging"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowDataBound="grvsoldinf_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgactdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Desc.">
                                <ItemTemplate>
                                    <asp:Label ID="lgudesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cutomer Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgacuname" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sft Size">
                                <ItemTemplate>
                                    <asp:Label ID="lgvsftsize" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                        Width="35px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Settled Price (Per Sft)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvpersft" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0;(#,##0); ") %>'
                                        Width="35px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Apt Value">
                                <ItemTemplate>
                                    <asp:Label ID="lgvaptval" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parking">
                                <ItemTemplate>
                                    <asp:Label ID="lgvparkam" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cparkam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Utility">
                                <ItemTemplate>
                                    <asp:Label ID="lgvutility" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utlityam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Co-operative">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcooparative" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coprtvam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Other's">
                                <ItemTemplate>
                                    <asp:Label ID="lgvotham" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otham")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Value">
                                <ItemTemplate>
                                    <asp:Label ID="lgvTVal" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalval")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
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
    </div>

</asp:Content>




