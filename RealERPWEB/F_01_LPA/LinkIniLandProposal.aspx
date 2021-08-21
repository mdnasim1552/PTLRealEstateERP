<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkIniLandProposal.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LinkIniLandProposal" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
                                    <div class="col-md-12">

                                        <asp:Label ID="lblProject" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>
                                        <asp:Label ID="lblProjectName" runat="server" CssClass=" smLbl_to" ></asp:Label>

                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="col-md-12">

                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:Label ID="lblDuration" runat="server" CssClass="smLbl_to"></asp:Label>

                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
    </div>
             <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" Width="772px"   CssClass=" table-striped table-hover table-bordered grvContentarea"
                            onrowdatabound="gvProjectInfo_RowDataBound">
                                  
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate><asp:Label ID="lblgvSlNo0" 
                                                runat="server" Font-Bold="True" Height="16px" style="text-align: right" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                              </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate><asp:Label 
                                                ID="lblgvItmCode" runat="server" Height="16px" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod")) %>' 
                                                Width="49px"></asp:Label>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label 
                                                ID="lgcResDesc1" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>' 
                                                Width="200px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" 
                                                HorizontalAlign="Left" />
                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Type" >
                                            <ItemTemplate><asp:Label 
                                                ID="lgvgval" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField>
                                            <ItemTemplate><asp:Label ID="lgp" runat="server" 
                                                Font-Bold="True" Font-Size="12px" Height="16px" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>' 
                                                Width="4px"></asp:Label>
                                             </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate><asp:TextBox 
                                                ID="txtgvVal" runat="server" BackColor="Transparent" BorderColor="#660033" 
                                                BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc1")) %>' 
                                                Width="200px"></asp:TextBox>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                           </asp:TemplateField>
                                           <asp:TemplateField>
                                           
                                            <ItemTemplate>
                                               <%-- <asp:TextBox 
                                                ID="txtgvVal2" runat="server" BackColor="Transparent" BorderColor="#660033" 
                                                BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc2")) %>' 
                                                Width="200px"></asp:TextBox>--%>
                                             </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                           </asp:TemplateField>
                                           <asp:TemplateField>
                                            
                                            <ItemTemplate>
                                              <%--   <asp:TextBox 
                                               ID="txtgvVal3" runat="server" BackColor="Transparent" BorderColor="#660033" 
                                                BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc3")) %>' 
                                                Width="200px"></asp:TextBox>--%>
                                             </ItemTemplate>
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

