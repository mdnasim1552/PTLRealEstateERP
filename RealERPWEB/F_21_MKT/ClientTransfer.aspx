<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ClientTransfer.aspx.cs" Inherits="RealERPWEB.F_21_MKT.ClientTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="uppnlclint" runat="server">
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
            <div class="card card-fluid container-data">
                <div class="card-body" style="min-height: 600px;">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:LinkButton ID="lbtnPrevTransList" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevTransList_Click">Prev. Trans List:</asp:LinkButton>


                            <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="form-control inputTxt" Width="233" TabIndex="2">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Transfer Date</asp:Label>
                            <asp:TextBox ID="txtCurTransDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>

                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="trnsferNo" runat="server" CssClass=" smLbl_to">Transfer No</asp:Label>
                            <asp:TextBox ID="lblCurTransNo1" runat="server" CssClass=" inputDateBox">Transfer No</asp:TextBox>


                        </div>
                        <span style="margin-top:20px; margin-left: -68px;">
                        <asp:TextBox ID="txtCurTransNo2" runat="server"  CssClass=" inputDateBox "></asp:TextBox>
                            </span>
                        <div class="col-sm-1 " style="margin-top:20px">
                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn"
                                TabIndex="3">Ok</asp:LinkButton>
                        </div>


                    </div>
                    <div class="row" id="dvclintpanel" runat="server" visible="false">
                        <div class="col-md-4">
                            <asp:Label ID="lblFrmTeam" CssClass="smLbl_to" runat="server" Text="From Team"></asp:Label>
                            <asp:DropDownList ID="ddlFrmTeam" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFrmTeam_SelectedIndexChanged" CssClass="form-control">
                            </asp:DropDownList>

                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="Label1" CssClass="smLbl_to" runat="server" Text="Client Name"></asp:Label>
                            <asp:DropDownList ID="ddlClientNam" runat="server" AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-4">
                            <asp:Label ID="Label2" CssClass="smLbl_to" runat="server" Text="To Team"></asp:Label>
                            <asp:DropDownList ID="ddlToTeam" runat="server" AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1" style="padding-top: 22px">
                            <asp:LinkButton ID="lnkAdd" runat="server" CssClass="btn btn-success btn-sm" OnClick="lnkAdd_Click" Text="Add Client"></asp:LinkButton>
                        </div>
                    </div>


                    <div class="row">
                        <asp:GridView ID="GvClientTrans" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="910px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Client Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClientName" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clientname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="From Team">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtfCompany" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fteamname")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True" CssClass="btn btn-success btn-sm okBtn"
                                            ForeColor="#000" OnClick="lnkupdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="To Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttemname")) %>'
                                            Width="300px"></asp:Label>
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
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

