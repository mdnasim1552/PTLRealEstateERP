<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkActiComments.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.LinkActiComments" %>

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
                                    <div class="col-md-8 pading5px ">
                                        <asp:Label ID="lblprojectName" runat="server" CssClass="  lblName lblTxt">Project Name</asp:Label>
                                           <asp:Label ID="lblvalprojectName" runat="server" CssClass="inputlblVal"></asp:Label>
                                        <asp:Label ID="lblactivities" runat="server" CssClass="smLbl_to">Activities</asp:Label>
                                           <asp:Label ID="lblvalactivities" runat="server" CssClass="inputlblVal" Style="width:350px;"></asp:Label>
                                  </div>


                                    

                                    <div class="col-md-2 pading5px">

                                        <div class="msgHandSt">
                                            <asp:Label ID="lmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>

                                 <div class="form-group">
                                    <div class="col-md-5 pading5px ">
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblName lblTxt">Date</asp:Label>
                                           <asp:Label ID="lblvaldate" runat="server" CssClass=" inputlblVal" Style="width:90px;"></asp:Label>
                                  </div>
                                   
                                </div>
                            </div>
                        </fieldset>

                    </div>

                    <div class="row">
                       <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="True" Width="500px" 
                                        CssClass="table-striped table-hover table-bordered grvContentarea">
                                        
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
                                            <asp:TemplateField HeaderText="Date" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdate" runat="server" Height="16px" 
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>' 
                                                        Width="90px" ForeColor="Black"></asp:Label>
                                   
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           
                                           
                                            <asp:TemplateField HeaderText="Note">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnUpdate" runat="server"  CssClass="btn btn-danger primarygrdBtn"  onclick="lbtnUpdate_Click" >Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvcomments" runat="server" BackColor="Transparent"  BorderStyle="None" TextMode="MultiLine" Height="80px"
                                                       
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comments")) %>' 
                                                        Width="500px"></asp:TextBox>
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





