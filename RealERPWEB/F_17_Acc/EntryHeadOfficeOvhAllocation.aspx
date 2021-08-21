
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryHeadOfficeOvhAllocation.aspx.cs" Inherits="RealERPWEB.F_17_Acc.EntryHeadOfficeOvhAllocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
     </script>

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
            <div class="container moduleItemWrpper">
                <div class="contentPartSmall row">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <asp:Panel ID="Panel3" runat="server">

                                <div class="form-group">
                                    <div class="col-md-8   pading5px  asitCol8">
                                        <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="From:"></asp:Label>
                                        <asp:TextBox ID="txtFromdate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFromdate"></cc1:CalendarExtender>

                                        <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="To:"></asp:Label>
                                        <asp:TextBox ID="txtTodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>

                                       
                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click">Show</asp:LinkButton>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" Text="Size:" CssClass="lblName lblTxt"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage"
                                            TabIndex="4">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                           <asp:ListItem Value="500">500</asp:ListItem>
                                            <asp:ListItem Value="700">700</asp:ListItem>
                                            <asp:ListItem Value="900">900</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>
                                    
                                    
                                </div>

                            </asp:Panel>
                        </div>
                    </fieldset>
                </div>
                <div class="table table-responsive">

                 
                        <asp:GridView ID="grDep" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                AutoGenerateColumns="False" OnPageIndexChanging="grDep_PageIndexChanging"
                ShowFooter="True" Style="text-align: left">

                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="serialnoid1" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Name">
                        <ItemTemplate>
                            <asp:Label ID="lgvAsset" runat="server" Style="text-align: left"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                Width="180px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFT" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: left" Text="Total :" Width="80px"></asp:Label>

                                 <asp:HyperLink ID="hlnkgvFdep" runat="server" Font-Bold="True"
                                Font-Size="12px"  CssClass="btn   btn-primary " Style="text-align: right" Target="_blank" Text="Voucher"
                                Width="70px"></asp:HyperLink>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                    </asp:TemplateField>
         <%--           <asp:TemplateField HeaderText="Balance as on ">
                        <ItemTemplate>
                            <asp:Label ID="lgvOpening" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFTOpening" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label ID="lgvcost" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFcost" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Percentage of</br> Allocation (%)">
                        <ItemTemplate>
                            <asp:Label ID="lgvPercent" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                          <asp:Label ID="lgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="right" Width="80px" />
                    </asp:TemplateField>
                           
                    
                </Columns>
                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />

            </asp:GridView>
                    

                    
                    
                </div>
                <asp:Panel ID="pnlovhead" runat="server" Visible="false">
                    <fieldset class="fieldset_Nar">
                        <div class="form-horizontal">


                            <div class="form-group">
                                <div class="col-md-3 pading5px ">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="lbltotal" runat="server" CssClass="lblTxt lblName" Text="Overhead Amount :" Font-Size="12px" ></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txttotalOv" runat="server" Style="margin-left: 15px; text-align: right;" Width="90px" class="ddlPage62 inputTxt"></asp:TextBox>
                                    </div>


                                </div>

                                
                            </div>
                            
                        </div>






                    </fieldset>
                </asp:Panel>
                
            </div>
        
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

