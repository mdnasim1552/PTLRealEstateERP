
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TasProjectcost.aspx.cs" Inherits="RealERPWEB.F_07_Ten.TasProjectcost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectName" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"></asp:TextBox>



                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>



                                    </div>

                                    <div class="col-md-1">

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                            

                                        </div>

                                    </div>
                                    <div class="col-md-3 pading5px pull-right">

                                        <div class="msgHandSt">
                                            <asp:Label ID="lmsg" CssClass="btn  btn-success primarygrdBtn" runat="server" Visible="false"></asp:Label>



                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="PrjInfo" runat="server">
                                <asp:GridView ID="gvprjcost" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="360px"  CssClass="table table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle/>
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatInfo" runat="server" CssClass="btn  btn-danger primarygrdBtn" OnClick="lUpdatInfo_Click"
                                                   >Update Information</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcResunit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gunit")) %>'
                                                    Width="40px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="80px" Style="text-align: right; font-size: 11px;" BorderStyle="None"></asp:TextBox>
                                            </ItemTemplate>
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

                            </asp:View>
                        </asp:MultiView>
                    </div>



                </div>
                <!-- End of contentpart-->

            </div>
            <!-- End of Container-->







        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>


