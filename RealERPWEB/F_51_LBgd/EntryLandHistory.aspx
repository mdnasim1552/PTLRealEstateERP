<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryLandHistory.aspx.cs" Inherits="RealERPWEB.F_51_LBgd.EntryLandHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrchProjectName" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblProjectdesc" runat="server" Visible="false" CssClass="form-control inputTxt"></asp:Label>

                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>



                                    </div>

                                </div>


                            </div>




                        </fieldset>

                    </div>
                    <asp:GridView ID="gvEnLandHist" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" Width="450px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                         OnRowDataBound="gvEnLandHist_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HLgvsircode" runat="server"  Font-Underline="false" Target="_blank" ForeColor="Black"
                                         Style="text-align: right;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                        Width="60px"></asp:HyperLink>

                                  
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Item">
                                <ItemTemplate>
                                    <asp:Label ID="lgcResDesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                        Width="250px"></asp:Label>
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

