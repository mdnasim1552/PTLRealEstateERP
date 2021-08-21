<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ClientDetail.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.ClientDetail" %>

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
                                        <asp:Label ID="lblClientName" runat="server" CssClass="lblTxt lblName">Client Name</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>



                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false" ></asp:Label>



                                    </div>

                                    <div class="col-md-1">

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="col-md-3 pading5px pull-right">

                                        <div class="msgHandSt">
                                            <asp:Label ID="lmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </fieldset>

                    </div>

                    <div class="row">
                       <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="True" Width="500px" 
                                        onrowdatabound="gvPersonalInfo_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px" 
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                                        Width="90px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc1" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                                        Width="150px" ForeColor="Black"></asp:Label>
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
                                            <asp:TemplateField HeaderText="Item Value">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lUpdatPerInfo" runat="server"  CssClass="btn btn-danger primarygrdBtn"  onclick="lUpdatPerInfo_Click" >Update Personal Info</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"  BorderStyle="None"
                                                       
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>' 
                                                        Width="350px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                           </asp:GridView>

                    </div>
                </div>
                <!-- End of contentpart-->

            </div>
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





