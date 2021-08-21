<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjectMapView.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.ProjectMapView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
     <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <div class="col-md-9" id="MapView" runat="server">

                </div>
                 <div class="col-md-3">
                      <asp:Label ID="lblProjectName" runat="server" CssClass="lblTxt ">Select Project Lands:</asp:Label>
                      <asp:DropDownList ID="ddlLandinfo" AutoPostBack="true" OnSelectedIndexChanged="ddlLandinfo_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control inputTxt">
                                </asp:DropDownList>


                     <asp:GridView ID="gvLandINfo" runat="server" AutoGenerateColumns="False"  
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvLandINfo_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                            Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                            Width="2px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgval" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField>
                                   <%-- <FooterTemplate>

                                        <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatPerInfo_Click">Final Update</asp:LinkButton>

                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvVal" runat="server" BackColor="Transparent"
                                            BorderStyle="Solid" BorderWidth="1px" Height="20px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Images">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="hyprrr" Visible="false" runat="server" NavigateUrl='<%# (Eval("imageurl").ToString()=="")?"~/images/no_img_preview.png":Eval("imageurl") %>' Target="_blank">
                                                                        <asp:Image ID="lblimageurl" Width="60" Height="40" runat="server" imageurl='<%# (Eval("imageurl").ToString()=="")?"~/images/no_img_preview.png":Eval("imageurl") %>' class="img-responsive"></asp:Image>
                                                                    </asp:HyperLink>

                                      
                                    
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
               <%-- https://www.google.com/maps/d/u/0/embed?mid=1CIUvBHTzzsODBB86Dw4MXM0EIsn3UmN7&ll=23.83,90.46&z=21
    <iframe src="https://www.google.com/maps/d/u/0/embed?mid=1CIUvBHTzzsODBB86Dw4MXM0EIsn3UmN7&ll=23.834331392037353,90.46946762983457&z=21" width="940" height="580"></iframe>--%>
                </div>
            </div>
         </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>

