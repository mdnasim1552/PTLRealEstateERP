<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SuppLierAss.aspx.cs" Inherits="RealERPWEB.F_14_Pro.SuppLierAss" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
            <div class="container contentPart">
                <div class="row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblSupList" runat="server" CssClass="lblTxt lblName" Text="Supplier Name"></asp:Label>
                                    <asp:TextBox ID="txtSupPro" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindSupply" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2" OnClick="ibtnFindSupply_OnClick"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:DropDownList ID="ddlSuplist" runat="server" Width="320" CssClass="chzn-select form-control  inputTxt" TabIndex="3" OnSelectedIndexChanged="ddlSuplist_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    <asp:Label ID="lblddlSupplier" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblmatList" runat="server" CssClass="lblTxt lblName" Text="Material List"></asp:Label>
                                    <asp:TextBox ID="txtSrcMat" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindmatname" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-4 pading5px asitCol4">
                                    <asp:DropDownList ID="ddlmatlist" runat="server" Width="320" CssClass="chzn-select form-control  inputTxt" TabIndex="3"></asp:DropDownList>
                                    <asp:Label ID="lblmaterial" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>


                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 pading5px">
                                    <asp:Label ID="lblAssNo" runat="server" CssClass="lblTxt lblName">Assessment No :</asp:Label>
                                    <asp:Label ID="lblCurNo1" runat="server" CssClass="smLbl_to"></asp:Label>
                                    <asp:Label ID="lblCurNo2" runat="server" CssClass="smLbl_to"></asp:Label>
                                    <asp:Label ID="lblfrmdate" runat="server" CssClass="smLbl_to">Date</asp:Label>
                                    <asp:TextBox ID="txtCurDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                    <asp:Label ID="lblRef" runat="server" CssClass="smLbl_to">Ass. Ref:</asp:Label>
                                    <asp:TextBox ID="txtassRef" runat="server" CssClass=" inputDateBox" Width="200"></asp:TextBox>
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>

                                </div>
                                <div class="col-md-3 pull-right">
                                    <asp:LinkButton ID="lbtnPrevAssNo" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevAssNo_OnClick">Prev. Ass No:</asp:LinkButton>
                                    <asp:DropDownList ID="ddlPrevAssNo" runat="server" Width="160" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                    </asp:DropDownList>
                                </div>

                            </div>
                           
                        </div>
                    </fieldset>


                    <asp:GridView ID="gvAssessment" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="1101px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtasscod" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode")) %>'
                                        Width="40px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description ">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assdesc"))%>'
                                        Width="500px" />
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnUpPerAppraisal" runat="server" Font-Bold="True"
                                        CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpPerAppraisal_OnClick">Update</asp:LinkButton>
                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="05">
                                <ItemTemplate>

                                    <asp:CheckBox ID="lblexec" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "exc"))=="True" %>'
                                        Width="80px" />
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="04">
                                <ItemTemplate>
                                    <asp:CheckBox ID="lblgood" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "good"))=="True" %>'
                                        Width="80px" />
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="03">
                                <ItemTemplate>
                                    <asp:CheckBox ID="lblavrg" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "avrg"))=="True" %>'
                                        Width="80px" />
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="02">
                                <ItemTemplate>
                                    <asp:CheckBox ID="lblpoor" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "poor"))=="True" %>'
                                        Width="80px" />
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="01">
                                <ItemTemplate>
                                    <asp:CheckBox ID="lblnill" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nill"))=="True" %>'
                                        Width="80px" />
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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


